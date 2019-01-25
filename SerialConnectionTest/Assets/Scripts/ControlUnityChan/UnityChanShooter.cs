using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanShooter : MonoBehaviour {

    const float DELTA_THETA = 3f;
    const float BULLET_HEIGHT = 1f;

    private Transform trans;
    public float theta;
    private int hp;

    private Timer launchTimer;
    [SerializeField] private DamageSerialSend damageSerialSend;

    // Use this for initialization
    void Start () {
        trans = transform;
        hp = 100; // TODO : 定数を扱うクラスをつくる
    }

    // Update is called once per frame
    void Update()
    {
        int y = (int)Input.GetAxis("Vertical 1");
        int x = (int)Input.GetAxis("Horizontal 1");
        if (Input.GetKey(KeyCode.LeftArrow) || x == -1)
        {
            theta -= DELTA_THETA;
            trans.rotation = Quaternion.AngleAxis(theta, new Vector3(0, 1, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow) || x == 1)
        {
            theta += DELTA_THETA;
            trans.rotation = Quaternion.AngleAxis(theta, new Vector3(0, 1, 0));
        }

        if (hp > 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick2Button0)))
        {
            Launch();
        }

        if (launchTimer != null)
        {
            try
            {
                launchTimer.UpdateTime(Time.deltaTime);
            }
            catch
            {
                Debug.Log("unity chan shooter launch timer was null");
                launchTimer = null;
            }
        }
    }

    void Launch()
    {
        if (launchTimer == null)
        {
            Vector3 pos = new Vector3(0, BULLET_HEIGHT, 0);
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Bullet");
            GameObject bullet = Instantiate(prefab, pos, Quaternion.identity);

            launchTimer = new Timer();
            launchTimer.expire += () => { bullet.GetComponent<Bullet>().SetVelocity(theta+90); launchTimer = null; };
            launchTimer.Start(0.1f);
        }
    }

    private void damage(int d, float angle)
    {
        int pos = 0;
        if (angle >= 90 && angle < 180) pos = 1;
        else if (angle >= 180 && angle < 270) pos = 2;
        else if (angle >= 270) pos = 3;
        if (hp > 0 && Mathf.Max(0, hp - d) == 0)
        {
            hp = 0;
            damageSerialSend.deadDamage();
        }
        else if (hp > d)
        {
            hp -= d;
            damageSerialSend.sendDamage(pos, 0);
        }
    }

    public int getHp()
    {
        return hp;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            Vector3 pos = other.gameObject.GetComponent<Rigidbody>().position;
            Destroy(other.gameObject);
            float angle = (((Mathf.Atan2(pos.z, pos.x) * 180 / Mathf.PI - theta - 45) % 360 + 360) %360);
            damage(8, angle); // TODO :
        }
    }
}
