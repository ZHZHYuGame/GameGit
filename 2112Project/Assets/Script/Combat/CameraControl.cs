using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        // 使用平滑插值移动相机
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10 * Time.deltaTime);
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
