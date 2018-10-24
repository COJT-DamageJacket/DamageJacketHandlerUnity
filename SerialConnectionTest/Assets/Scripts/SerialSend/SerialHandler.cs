using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived = null;

    public string portName = "";
    public int baudRate = 9600;

    private SerialPort serialPort;
    private Thread thread;
    private bool isPortOpen = false;

    private string newMessage;
    private bool isNewMessageReceived = false;

    void Awake()
    {
        // FIXME 環境変数に保存しておいて読み込むようにする（？）
#if UNITY_STANDALONE_OSX
        portName = "/dev/tty.usb-hogehoge";
#elif UNITY_STANDALONE_LINUX
        portName = "/dev/ttyUSB0"
#elif UNITY_STANDALONE_WIN
        portName = "COM1";
#endif
        Debug.Log("SerialPort : " + portName);
        Open();
    }

    void Update()
    {
        if (isNewMessageReceived)
        {
            if (OnDataReceived == null) Debug.LogWarning("SerialHandler.OnDataReceived is null");
            else OnDataReceived(newMessage);
        }
        isNewMessageReceived = false;
    }

    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        /*
        serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        serialPort.Open();

        isPortOpen = true;

        thread = new Thread(Read);
        thread.Start();
        */
    }

    private void Close()
    {
        isNewMessageReceived = false;
        isPortOpen = false;

        if (thread != null && thread.IsAlive)
        {
            thread.Join();
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            serialPort.Dispose();
        }
    }

    private void Read()
    {
        while (isPortOpen && serialPort != null && serialPort.IsOpen)
        {
            try
            {
                newMessage = serialPort.ReadLine();
                isNewMessageReceived = true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public void Write(string message)
    {
        try
        {
            serialPort.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
