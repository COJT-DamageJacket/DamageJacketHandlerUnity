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

        Debug.Log("unity chan shooter");
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

    public void damage(int d)
    {
        hp = Mathf.Max(0, hp - d);
    }

    public int getHp()
    {
        return hp;
    }
}
