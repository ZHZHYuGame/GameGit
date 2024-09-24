using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWeapon : WeaponBase
{
    public bool isAtk;
    protected override void Start()
    {
        base.Start();
        atk = 10;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy"&&isAtk)
        {
            collision.collider.GetComponent<EnemyControl>().EnemyHurt(atk);
        }
    }
}
