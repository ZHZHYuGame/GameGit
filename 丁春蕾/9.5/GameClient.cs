using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

namespace SignalRChatClient
{
    public class PlayerInfo
    {
        public string id;
        public Transform transform;
    }

    public class GameClient : MonoBehaviour
    {
        HubConnection _connection;
        [SerializeField]
        Transform _player;
        [SerializeField]
        GameObject EnemyPrefab;

        Dictionary<string, Transform> enemies;

        readonly ConcurrentQueue<string> idQueue = new();
        readonly ConcurrentQueue<Vector3> posQueue = new();

        string _connectInfo;

        private void Update()
        {
            if (idQueue.TryDequeue(out string id))
            {
                if (id != _connectInfo)
                {
                    var trans = CreatingPlayer();
                    trans.position = Vector3.zero;
                    enemies.Add(id, trans);
                }
            }

            if (posQueue.TryDequeue(out Vector3 pos))
            {
                enemies.Values.First().position = pos;
            }

        }

        async Task ConnectionHandler()
        {


            _connection.Closed += async (error) =>
            {
                await Task.Delay(100);

                Debug.Log("���Ӷ���");

                await _connection.StartAsync();
                Debug.Log("������������ʧ��");
                await _connection.SendAsync("SendDisconnectionInfo", _connectInfo);
            };
            //���÷�������������ߵķ���;
            _connection.On<string>("ReceiveConnectionInfo", (id) =>
            {
                print($"����id{id}");
                idQueue.Enqueue(id);
            });


            //���÷�������������ߵķ���;
            _connection.On<string>("ReceiveDisconnectionInfo", (id) =>
            {
                if (enemies.ContainsKey(id))
                {
                    Destroy(enemies[id].gameObject);
                    enemies.Remove(id);
                }
            });

            //���÷�������SetPosition����;
            _connection.On<float, float, float, string>("ReceivePosition", (x, y, z, id) =>
            {
                var pos = new Vector3(x, y, z);
                posQueue.Enqueue(pos);
            });

            await _connection.StartAsync();

            _connectInfo = _connection.ConnectionId;
            Debug.Log($"����Success; id==>{_connectInfo}");
            await _connection.SendAsync("SendConnectionInfo", _connectInfo);

            await Task.Run(UpdateQueue);
        }

        void Awake()
        {
            _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5136/mainhub")
            .Build();


            //���˵ļ��ϵĳ�ʼ��;
            enemies = new Dictionary<string, Transform>();
            _ = Task.Run(ConnectionHandler);



            StartCoroutine(PlayerMovingUpdate());
        }

        float _moveSpeed = 5f; // �ƶ��ٶ�  


        //��Ϣ���д����첽�߼�
        readonly ConcurrentQueue<(string, Vector3)> _infos = new();

        async Task UpdateQueue()
        {
            while (_connection.State == HubConnectionState.Connected)
            {
                while (_infos.Count > 0)
                {
                    _infos.TryDequeue(out var position);
                    await _connection.SendAsync("SendPosition", position.Item2.x, position.Item2.y, position.Item2.z, _connectInfo);
                }

                await Task.Delay(100);
            }
        }
        IEnumerator PlayerMovingUpdate()
        {
            while (true)
            {
                // Update is called once per frame  

                // ��ȡ��ҵ�ǰ��ˮƽ���루A��D����  
                float moveHorizontal = Input.GetAxis("Horizontal");
                // ��ȡ��ҵ�ǰ�Ĵ�ֱ���루W��S����  
                float moveVertical = Input.GetAxis("Vertical");
                // ʹ��Vector3.right��Vector3.forward���������뷽�򴴽��ƶ�����  
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                if (movement == Vector3.zero)
                {
                    yield return null;
                    continue;
                }
                // ���ƶ����������ƶ��ٶ�  
                movement *= _moveSpeed;
                // Ӧ���ƶ���������Ҷ���  
                _player.Translate(movement * Time.deltaTime);
                var position = _player.position;
                _infos.Enqueue((_connectInfo, position));
                yield return null;
            }
        }

        GameObject enemy;
        Transform CreatingPlayer()
        {
            enemy = Instantiate(EnemyPrefab);
            return enemy.transform;
        }


        private void OnApplicationQuit()
        {
            _connection.SendAsync("SendDisconnectionInfo", _connectInfo);
        }
    }
}