using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UnityChan")
        {
            Destroy(this.gameObject);
            other.GetComponent<UnityChanShooter>().hp -= 8; // TODO : 定数
        }
    }
}
