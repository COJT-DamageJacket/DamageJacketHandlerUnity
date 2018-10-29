using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevateStage : MonoBehaviour {

    const float DELTA_HEIGHT = 0.1f;
    const float MAX_HEIGHT = 10f;
    const float MIN_HEIGHT = 4f;

    public float height;
    private Transform trans;

	// Use this for initialization
	void Start () {
        trans = transform;
        height = (MAX_HEIGHT + MIN_HEIGHT) / 2;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (height < MAX_HEIGHT)
                height += DELTA_HEIGHT;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (height > MIN_HEIGHT)
                height -= DELTA_HEIGHT;
        }

        Vector3 pos = trans.position;
        pos.y = height;
        trans.position = pos;
    }
}
