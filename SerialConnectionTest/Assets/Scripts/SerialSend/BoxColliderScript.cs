using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxColliderScript : MonoBehaviour {

    private bool state;
    Color color;
    [SerializeField] bool send;
    [SerializeField] BoxColliderScript box;
    [SerializeField] Toggle withUnityChan;

    public SerialHandler serialHandler;
    Timer timerColor;

    // Use this for initialization
    void Start () {
        timerColor = new Timer();
        if (send)
            timerColor.expire += ResetSend;

        state = false;
        if (send) color = Color.blue;
        else color = Color.red;

        if (serialHandler != null)
            serialHandler.OnDataReceived += OnDataReceived;
    }
	
	// Update is called once per frame
	void Update () {
        timerColor.UpdateTime(Time.deltaTime);

	}

    void OnTriggerEnter (Collider other) {
        state = !state;
        if (state)
        {
            GetComponent<Renderer>().material.color = color;
            if (send)
            {
                if (withUnityChan.isOn)
                {
                    int value = 0;
                    if (box.state) value = 1;
                    SerialSend(value);
                }
                timerColor.Start(1.0f);
            }
        }
        else
            GetComponent<Renderer>().material.color = Color.white;
    }

    private void SerialSend(int value) {
        Debug.Log("send : " + value.ToString());
        serialHandler.WriteByte((byte)value);
        state = false;
    }

    void OnDataReceived(string message)
    {
        string[] data = message.Split(new string[] { "\t" }, System.StringSplitOptions.None);
        if (data.Length < 2) return;

        try
        {
            for (int i = 0; i < data.Length; ++i)
            {
                Debug.Log("Received Data " + i + " : " + data[i]);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    void ResetSend()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
