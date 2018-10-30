using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("angle : " + other.gameObject.GetComponent<Bullet>().theta);

        Vector3 hitPos = new Vector3();
        foreach (ContactPoint point in other.contacts)
        {
            hitPos = point.point;
            Debug.Log("height : " + hitPos.y);
        }

        Side side;
        BodyPart part;
        (side, part) = CollisionAt(other.gameObject.GetComponent<Bullet>().theta, hitPos.y);
        Debug.Log(side);
        Debug.Log(part);
    }

    (Side, BodyPart) CollisionAt(float theta, float height)
    {
        Side side = (theta >= 90 && theta < 270) ? Side.Right : Side.Left;
        BodyPart part = BodyPart.Back;
        if (height >= 13.2) part = BodyPart.Shoulder;
        else if (theta >= 0 && theta <= 180)
        {
            if (height >= 11.45)
            {
                if (theta >= 50 && theta <= 130) part = BodyPart.Chest;
                else part = BodyPart.ChestOut;
            }
            else if (height >= 9.5)
            {
                if (theta >= 50 && theta <= 130) part = BodyPart.Stomach;
                else part = BodyPart.StomachOut;
            }
            else part = BodyPart.Navel;
        }
        else
        {
            if (height <= 9) part = BodyPart.Hip;
            else part = BodyPart.Back;
        }
        return (side, part);
    }
}
