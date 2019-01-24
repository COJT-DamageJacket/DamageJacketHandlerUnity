using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UnityChan")
        {
            Destroy(this.gameObject);
            other.GetComponent<UnityChanShooter>().damage(8); // TODO :
        }
        else if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
