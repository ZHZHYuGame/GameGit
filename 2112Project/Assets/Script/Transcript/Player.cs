using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * 150 * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * 150 * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            ani.SetBool("Move", true);
        }
        else
        {
            ani.SetBool("Move", false);
        }
    }
}
