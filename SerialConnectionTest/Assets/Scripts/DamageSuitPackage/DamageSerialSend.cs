using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSerialSend : SerialHandler {

    const byte FRONT = 0b00000101; // 0, 1
    const byte RIGHT = 0b00001100; // 1, 2
    const byte BACK  = 0b00001010; // 2, 3
    const byte LEFT  = 0b00000011; // 3, 0
    const byte ALL = FRONT | RIGHT | BACK | LEFT;
    const byte DEAD  = 0b00010000; // 4

    Pattern pattern;

    readonly byte[] DIRECTION = { FRONT, RIGHT, BACK, LEFT, ALL };
    private int position = 0;
    private int[] patternArray;

    Timer sendTimer;

    private int sendIndex;
    private bool isSending;

    // Use this for initialization
    void Start () {
        pattern = new Pattern();

        OnDataReceived += ReadMessage;

        sendTimer = new Timer();
        sendIndex = -1;

        isSending = false;
    }

	// Update is called once per frame
	void Update () {
        if (sendTimer != null) {
            sendTimer.UpdateTime(Time.deltaTime);
        }
	}

    private void ReadMessage(string message)
    {
        Debug.Log("Received message : " + message);
    }

    public void DeadDamage() {
        WriteByte(DEAD);
    }

    public void SendDamage(int position, int[] pattern)
    {
        if (!isSending)
        {
            this.patternArray = pattern;
            this.position = position;

            sendIndex = 0;
            isSending = true;
            _send();
        }
    }

    public void SendDamage(int position, string key)
    {
        if (!isSending)
        {
            Debug.Log(key);
            this.position = position;
            this.patternArray = pattern.Get(key);
            sendIndex = 0;
            isSending = true;
            _send();
        }
    }

    public void SendDamage(int position, string key, int count)
    {
        if (!isSending)
        {
            Debug.Log(key);
            this.position = position;
            this.patternArray = pattern.Get(key);
            sendIndex = 0;
            isSending = true;
            _send(count);
        }
    }

    private void _send() {
        _send(1);
    }

    private void _send(int count) {
        Debug.Log(patternArray[sendIndex] * DIRECTION[position]);
        WriteByte(patternArray[sendIndex] * DIRECTION[position]);
        sendIndex++;

        sendTimer.ExpiredReset();
        if (sendIndex == Pattern.RANGE)
        {
            // hasSend = false;
            sendTimer.expire += () =>
            {
                Debug.Log("count : " + count);
                if (count == 1)
                {
                    WriteByte(0);
                    isSending = false;
                }
                else
                {
                    sendIndex = 0;
                    _send(count - 1);
                }
            };
        }
        else
        {
            sendTimer.expire += () =>
            {
                _send(count);
            };
        }
        sendTimer.Start(Pattern.INTERVAL);
    }
}
