using UnityEngine;

public class SendMessageToArduino : MonoBehaviour {

    [SerializeField]public SerialHandler serialHandler;

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void Update ()
    {
        // serialHandler.Write("Hello, Arduino! I'm Unity.");
    }


    void OnDataReceived(string message)
    {
        string[] data = message.Split(new string[] { "\t" }, System.StringSplitOptions.None);
        if (data.Length < 2) return;

        try
        {
            for (int i = 0; i < data.Length; ++i) {
                Debug.Log("Received Data " + i + " : " + data[i]);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
