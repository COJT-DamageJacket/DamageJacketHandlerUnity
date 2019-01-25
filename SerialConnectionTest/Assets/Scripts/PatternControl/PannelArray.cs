using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PannelArray : MonoBehaviour {

    Image[] panels;
    int[] pattern;

	// Use this for initialization
	void Start () {
        panels = new Image[Pattern.RANGE];
        pattern = new int[Pattern.RANGE];
        for (int i = 0; i < Pattern.RANGE; i++) {
            panels[i] = GameObject.Find(" (" + (i + 1) + ")").GetComponent<Image>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPattern(int[] pattern) {
        this.pattern = pattern;
        for (int i = 0; i < Pattern.RANGE; i++) {
            Color c = (pattern[i] == 1) ? Color.cyan : Color.white;
            panels[i].color = c;
        }
    }

    public void SetPattern(int idx, int value) {
        pattern[idx] = value;
        Color c = (value == 1) ? Color.cyan : Color.white;
        panels[idx].color = c;
    }

    public void ResetPattern() {
        Color c = new Color(1,1,1,100f/255);
        for (int i = 0; i < Pattern.RANGE; i++)
        {
            panels[i].color = c;
        }
    }
}
