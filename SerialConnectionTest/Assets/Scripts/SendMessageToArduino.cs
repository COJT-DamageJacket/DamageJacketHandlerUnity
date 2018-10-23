using UnityEngine;

public class SendMessageToArduino : MonoBehaviour {

    public SerialHandler serialHandler;

    void Start()
    {
        serialHandler = new SerialHandler();
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void Update ()
    {
        serialHandler.Write("Hello, Arduino! I'm Unity.");
    }

    void OnDataReceived(string message)
    {
        string[] data = message.Split(new string[] { "\t" }, System.StringSplitOptions.None);
        if (data.Length < 2) return;

        try
        {

        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
