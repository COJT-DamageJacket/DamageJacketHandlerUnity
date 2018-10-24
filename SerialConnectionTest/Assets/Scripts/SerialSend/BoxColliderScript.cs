using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderScript : MonoBehaviour {

    private bool state;
    Color color;
    [SerializeField] bool send;
    [SerializeField]BoxColliderScript box;
    public SerialHandler serialHandler;


    // Use this for initialization
    void Start () {
        state = false;
        if (send) color = Color.blue;
        else color = Color.red;

        if (serialHandler != null)
            serialHandler.OnDataReceived += OnDataReceived;
    }
	
	// Update is called once per frame
	void Update () {
        if (!state) GetComponent<Renderer>().material.color = Color.white;
	}

    void OnTriggerEnter (Collider other) {
        state = !state;
        if (state)
        {
            GetComponent<Renderer>().material.color = color;
            if (box != null) {
                int value = 0;
                if (box.state) value = 1;
                SerialSend(value);
            }
        }
        else
            GetComponent<Renderer>().material.color = Color.white;
    }

    void SerialSend(int value) {
        Debug.Log("send" + value.ToString());
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
}
