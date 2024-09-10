using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : WeaponBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.collider.GetComponent<EnemyControl>().EnemyHurt(atk);
        }
    }
}
