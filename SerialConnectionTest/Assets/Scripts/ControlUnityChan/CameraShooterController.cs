using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShooterController : MonoBehaviour {

    const float DELTA_THETA = 10f;
    const float RADIUS = 5f;
    const float HEIGHT = 3f;

    private Transform trans;
    private float theta;
    [SerializeField] private UnityChanShooter unityChanShooter;

    // Use this for initialization
    void Start () {
        trans = transform;
    }
	
	// Update is called once per frame
	void Update () {
        theta = unityChanShooter.theta;
        float r = (theta+90) / 180 * Mathf.PI;
        trans.rotation = Quaternion.AngleAxis(theta, new Vector3(0, 1, 0));
        trans.position = new Vector3(Mathf.Cos(r) * RADIUS, HEIGHT , -Mathf.Sin(r) * RADIUS);
    }
}
