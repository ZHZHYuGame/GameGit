using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 1 * Time.deltaTime);
        
        player = GameObject.Find("1(Clone)");
        if (player != null)
        {
            if (Vector3.Distance(transform.position,player.transform.position)<=5)
            {
                transform.LookAt(player.transform.position);
                ani.SetBool("Move", false);
            }
            else
            {
                ani.SetBool("Move", true);
                //transform.LookAt(transform.forward);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        TerrainMap.enemynum += 1;
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
