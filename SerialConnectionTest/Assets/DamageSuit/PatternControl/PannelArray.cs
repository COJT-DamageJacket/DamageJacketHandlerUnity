using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PannelArray : MonoBehaviour {

    const float width = 512;
    [SerializeField] GameObject buttonPrefab;

    Button[] panels;
    public int[] pattern;
    public bool editable;

	// Use this for initialization
	void Start () {
        editable = false;
        panels = new Button[Pattern.RANGE];
        pattern = new int[Pattern.RANGE];

        for (int j = 0; j < Pattern.RANGE; j++) {
            int i = j + 0;
            panels[i] = Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform).GetComponent<Button>();
            panels[i].transform.localPosition = new Vector3(width/Pattern.RANGE * (i - Pattern.RANGE/2), 0, 0);
            panels[i].name = "panel" + i;
            panels[i].GetComponent<RectTransform>().sizeDelta = new Vector2(width/Pattern.RANGE, 100);
            panels[i].onClick.AddListener(() =>
            {
                if (editable)
                    SetPattern(i, 1 - pattern[i]);
            });
        }
        Destroy(buttonPrefab);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPattern(int[] pattern) {
        this.pattern = pattern;
        for (int i = 0; i < Pattern.RANGE; i++) {
            Color c = (pattern[i] == 1) ? Color.cyan : Color.white;
            panels[i].image.color = c;
        }
    }

    public void SetPattern(int idx, int value) {
        pattern[idx] = value;
        Color c = (value == 1) ? Color.cyan : Color.white;
        panels[idx].image.color = c;
    }

    public void ResetPattern() {
        Color c = new Color(1,1,1,100f/255);
        for (int i = 0; i < Pattern.RANGE; i++)
        {
            panels[i].image.color = c;
        }
    }
}
