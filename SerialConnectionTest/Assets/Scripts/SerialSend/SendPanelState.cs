using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendPanelState : MonoBehaviour {

    [SerializeField] GameObject panel7;
    [SerializeField] GameObject panel6;
    [SerializeField] GameObject panel5;
    [SerializeField] GameObject panel4;
    [SerializeField] GameObject panel3;
    [SerializeField] GameObject panel2;
    [SerializeField] GameObject panel1;
    [SerializeField] GameObject panel0;
    [SerializeField] GameObject panelSend;
    [SerializeField] Text stateText;
    [SerializeField] Text valueText;
    [SerializeField] Text receiveText;
    [SerializeField] SerialHandler serialHandler;
    [SerializeField] Toggle withUnityChan;

    const int N = 8;
    private GameObject[] panels;
    private int[] masks;
    private KeyCode[] keys;

    private int state;
    private Timer timerSend;
    private Timer timerReceive;

	// Use this for initialization
	void Start () {
        state = 0;
        InitTimer();

        GameObject[] p = { panel0, panel1, panel2, panel3, panel4, panel5, panel6, panel7 };
        panels = p;
        KeyCode[] k = { KeyCode.K, KeyCode.J, KeyCode.H, KeyCode.G, KeyCode.F, KeyCode.D, KeyCode.S, KeyCode.A };
        keys = k;

        masks = new int[N];
        for (int i = 0; i < N; i++)
        {
            panels[i].GetComponent<Image>().color = Color.white;
            masks[i] = (int)Mathf.Pow(2, i);
        }

        // データの受け取り
        if (serialHandler != null)
            serialHandler.OnDataReceived += ReadMessage;
	}

    // Update is called once per frame
    void Update()
    {
        UpdateTimer(Time.deltaTime);

        Color color;
        for (int i = 0; i < N; i++) {
            if (Input.GetKeyDown(keys[i]))
            {
                if ((state & masks[i]) == 0) color = Color.red;
                else color = Color.white;
                state ^= masks[i];
                panels[i].GetComponent<Image>().color = color;
                Debug.Log(state);
            }
        }
        valueText.text = state.ToString();

        if (!withUnityChan.isOn && Input.GetKeyDown(KeyCode.X))
        {
            panelSend.GetComponent<Image>().color = Color.blue;
            serialHandler.WriteByte(state);
            stateText.text = "Sending";
            timerSend.Start(1.2f);
        }
    }

    // -------- serialHandler Read event --------
    private void ReadMessage (string message)
    {
        receiveText.text = message;
        timerReceive.Start(1.0f);
    }

    // -------- handling Timer --------
    private void ResetSending()
    {
        stateText.text = "-";
        panelSend.GetComponent<Image>().color = Color.white;
    }

    private void ResetReceiving()
    {
        receiveText.text = "-";
    }

    private void InitTimer()
    {
        timerSend = new Timer();
        timerSend.expire += ResetSending;

        timerReceive = new Timer();
        timerReceive.expire += ResetReceiving;
    }

    private void UpdateTimer (float deltaTime)
    {
        timerSend.UpdateTime(deltaTime);
        timerReceive.UpdateTime(deltaTime);
    }

}
