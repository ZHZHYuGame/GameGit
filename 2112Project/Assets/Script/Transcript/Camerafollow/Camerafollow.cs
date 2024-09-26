using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    GameObject player;
    Vector3 off;
    // Start is called before the first frame update
    void Start()
    {
        off = new Vector3(0, 3, -6);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Jammo_Player");
        if(player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position +
                             player.transform.TransformDirection(off), 10 * Time.deltaTime);
            transform.LookAt(player.transform);
        }
        
    }
}
