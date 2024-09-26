using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            TerrainMap.enemynum += 5;
            TerrainMap.timenum += 5;
        }
    }
}
