using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendPanelState : MonoBehaviour {

    [SerializeField] GameObject panel3;
    [SerializeField] GameObject panel2;
    [SerializeField] GameObject panel1;
    [SerializeField] GameObject panel0;
    [SerializeField] SerialHandler serialHandler;

    private GameObject[] panels;
    private int[] masks;
    private KeyCode[] keys;

    int state;

	// Use this for initialization
	void Start () {
        state = 0;
        GameObject[] p = {panel0, panel1, panel2, panel3};
        panels = p;
        KeyCode[] k = { KeyCode.F, KeyCode.D, KeyCode.S, KeyCode.A };
        keys = k;

        masks = new int[4];
        for (int i = 0; i < 4; i++)
        {
            panels[i].GetComponent<Image>().color = Color.white;
            masks[i] = (int)Mathf.Pow(2, i);
        }
	}

    // Update is called once per frame
    void Update()
    {
        Color color;
        for (int i = 0; i < 4; i++) {
            if (Input.GetKeyDown(keys[i]))
            {
                if ((state & masks[i]) == 0) color = Color.red;
                else color = Color.white;
                state ^= masks[i];
                panels[i].GetComponent<Image>().color = color;
                Debug.Log(state);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            serialHandler.WriteBytes(state);
        }
    }

}
