using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    const float TIMELIMIT = 1.2f;
    const float VELOCITY = 10;

    private Timer timer;
    public float theta;

	// Use this for initialization
	void Start () {
        timer = new Timer();
        timer.expire += () =>{ Destroy(gameObject); };
        theta = 0f;
	}

    void Destroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer.UpdateTime(Time.deltaTime);
    }

    public void SetVelocity(float theta)
    {
        timer.Start(TIMELIMIT);
        this.theta = theta;

        float r = theta / 180 * Mathf.PI;
        GetComponent<Rigidbody>().velocity = new Vector3(-Mathf.Cos(r), 0, Mathf.Sin(r)) * VELOCITY;
    }
}
