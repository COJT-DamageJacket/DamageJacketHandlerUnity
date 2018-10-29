using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCircleRun : MonoBehaviour {

    const float RADIUS = 12.0f;
    const float DELTA_THETA = 0.7f;

    private Animator animator;
    private Transform trans;
    public float theta;
    private Timer launchTimer;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        theta = 90.0f;
        trans = transform;

        launchTimer = null;
        Launch();
    }
	
	void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isRunning", true);
            theta += DELTA_THETA;
            trans.rotation = Quaternion.AngleAxis(theta+180, new Vector3(0, 1, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isRunning", true);
            theta -= DELTA_THETA;
            trans.rotation = Quaternion.AngleAxis(theta, new Vector3(0, 1, 0));
        }
        else
        {
            animator.SetBool("isRunning", false);
            trans.rotation = Quaternion.AngleAxis(theta-90, new Vector3(0, 1, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        float r = theta / 180 * Mathf.PI;
        trans.localPosition = new Vector3(Mathf.Cos(r) * RADIUS, 1.5f, -Mathf.Sin(r) * RADIUS);

        if (launchTimer != null)
            launchTimer.UpdateTime(Time.deltaTime);
    }

    void Launch()
    {
        if (launchTimer == null)
        {
            Vector3 pos = trans.position;
            pos /= 1.06f;
            pos.y = pos.y*1.06f + 0.5f;
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Bullet");
            GameObject bullet = Instantiate(prefab, pos, Quaternion.identity);
            launchTimer = new Timer();
            launchTimer.expire += () => { bullet.GetComponent<Bullet>().SetVelocity(theta); launchTimer = null; };
            launchTimer.Start(0.1f);
        }
    }

    void OnCallChangeFace()
    {

    }
}
