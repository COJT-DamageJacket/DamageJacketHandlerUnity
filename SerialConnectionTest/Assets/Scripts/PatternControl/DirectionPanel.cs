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
    [SerializeField] Button all;

    Button[] directions;
    public int direction;

    void Start () {
        directions = new Button[]{ front, right, back, left, all };
        SetDirection(0);

        for (int j = 0; j < 5; j++)
        {
            int i = j + 0;
            directions[i].onClick.AddListener(() => SetDirection(i));
        }
    }
	
	void Update () {
		
	}

    void SetDirection (int idx)
    {
        direction = idx;
        for (int i = 0; i < 5; i++)
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
