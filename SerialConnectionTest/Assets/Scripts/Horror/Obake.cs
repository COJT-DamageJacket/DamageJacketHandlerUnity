using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obake : MonoBehaviour {

    RectTransform rect;
    Rigidbody rigidbody;
    const float HEIGHT = 527;
    const float WIDTH = 957;
	// Use this for initialization
	void Start ()
    {
        rect = GetComponent<RectTransform>();
        rect.position = new Vector3(Random.Range(0, WIDTH), Random.Range(0, HEIGHT), 0);
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0) * 80;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0) * 80, ForceMode.Acceleration);
        Vector3 scale = transform.localScale;
        if (rigidbody.velocity.x < -10)
        {
            scale.x = 1;
        }
        else if (rigidbody.velocity.x > 10)
        {
            scale.x = -1;
        }
        transform.localScale = scale;

        if (rect.position.x < 0 || rect.position.x > WIDTH)
        {
            Vector3 pos = rect.position;
            pos.x = Random.Range(0, WIDTH);
            rect.position = pos;
            rigidbody.velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0) * 80;
        }
        if (rect.position.y < 0 || rect.position.y >= HEIGHT)
        {
            Vector3 pos = rect.position;
            pos.y = Random.Range(0, HEIGHT);
            rect.position = pos;
            rigidbody.velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0) * 80;
        }

    }
}
