using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniUniZombi : MonoBehaviour {

    const int RUNNING = 1;
    const int STOP = 2;

    [SerializeField] private float VELOCITY = 1f;
    private float theta;
    private Timer timer;
    private Timer launchTimer;
    private bool destroyFlag;

    // Use this for initialization
    void Start ()
    {
        timer = new Timer();
        MoveForSeconds(1.0f);
        destroyFlag = true;
    }

    public void SetAngle(float theta) {
        this.theta = theta;
    }

    public void Move() {
        GetComponent<Rigidbody>().velocity = new Vector3(-Mathf.Sin(theta), 0, -Mathf.Cos(theta)) * VELOCITY;
    }

    public void MoveForSeconds(float limit) {
        timer.ExpiredReset();
        timer.expire += () =>
        {
            Stop();
            StartAttack(limit);
        };
        timer.Start(limit);
        Move();
    }

    private void StartAttack(float limit) {
        // 少し待って攻撃をする
        timer.ExpiredReset();
        timer.expire += () =>
        {
            Launch();
            timer.ExpiredReset();
            timer.expire += () =>
            {
                MoveForSeconds(limit);
            };
            timer.Start(Mathf.Min(limit - 0.5f, 0.5f));
        };
        timer.Start(0.5f);
    }

    void Launch()
    {
        if (launchTimer == null)
        {
            Vector3 pos = transform.position;
            pos.y += 1;
            GameObject prefab = (GameObject)Resources.Load("Prefabs/EnemyBullet");
            GameObject bullet = Instantiate(prefab, pos, Quaternion.identity);

            launchTimer = new Timer();
            launchTimer.expire += () => { bullet.GetComponent<Bullet>().SetVelocity(theta*180/Mathf.PI-90); launchTimer = null; };
            launchTimer.Start(0.1f);
        }
    }

    public void Stop() {
        GetComponent<Rigidbody>().velocity = new Vector3();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.magnitude < 2 && destroyFlag)
        {
            destroyFlag = false;
            Stop();
            timer.Stop();
            timer.ExpiredReset();
            timer.expire += () => Destroy(gameObject);
            timer.Start(2.0f);
            launchTimer = null;
        }

        timer.UpdateTime(Time.deltaTime);
        if (launchTimer != null)
            launchTimer.UpdateTime(Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
