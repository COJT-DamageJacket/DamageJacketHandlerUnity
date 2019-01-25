using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionPanel : MonoBehaviour
{

    [SerializeField] Button front;
    [SerializeField] Button right;
    [SerializeField] Button back;
    [SerializeField] Button left;

    Button[] directions;
    public int direction;

    void Start () {
        directions = new Button[]{ front, right, back, left };
        SetDirection(0);

        directions[0].onClick.AddListener(() => SetDirection(0));
        directions[1].onClick.AddListener(() => SetDirection(1));
        directions[2].onClick.AddListener(() => SetDirection(2));
        directions[3].onClick.AddListener(() => SetDirection(3));
    }
	
	void Update () {
		
	}

    void SetDirection (int idx)
    {
        direction = idx;
        for (int i = 0; i < 4; i++)
        {
            if (i == idx)
            {
                directions[i].targetGraphic.color = Color.red;
            }
            else
                directions[i].targetGraphic.color = Color.white;
        }
    }

}
