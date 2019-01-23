using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanShooter : MonoBehaviour {

    const float DELTA_THETA = 3f;
    const float BULLET_HEIGHT = 1f;

    private Transform trans;
    public float theta;

    private Timer launchTimer;

    // Use this for initialization
    void Start () {
        trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            theta -= DELTA_THETA;
            trans.rotation = Quaternion.AngleAxis(theta, new Vector3(0, 1, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            theta += DELTA_THETA;
            trans.rotation = Quaternion.AngleAxis(theta, new Vector3(0, 1, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        if (launchTimer != null)
            launchTimer.UpdateTime(Time.deltaTime);
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
}
