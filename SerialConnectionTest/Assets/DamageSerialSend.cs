using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSerialSend : MonoBehaviour {

    [SerializeField] SerialHandler serialHandler;

    const byte FRONT = 0b00000011; // 0, 1
    const byte RIGHT = 0b00000110; // 1, 2
    const byte BACK  = 0b00001100; // 2, 3
    const byte LEFT  = 0b00001001; // 3, 0
    const byte DEAD  = 0b00010000; // 4
    readonly byte[] DIRECTION = { FRONT, RIGHT, BACK, LEFT };
    private int position = 0;
    private int[] pattern;

    private int[][] patterns;
    const float INTERVAL = 0.1f;

    Timer sendTimer;

    private int sendIndex;
    const int SEND_RANGE = 16;

    private bool hasSend; // テスト用に一回しか送れない

    // Use this for initialization
    void Start () {
        if (serialHandler != null)
            serialHandler.OnDataReceived += ReadMessage;

        sendTimer = new Timer();
        sendIndex = -1;
        readPattern();

        hasSend = false;
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

    private void readPattern() {
        // TODO : csvかなんかで形式を決めてパターンを作る
        patterns = new int[1][];
        patterns[0] = new int[SEND_RANGE];
        for (int i = 0; i < SEND_RANGE; i++) {
            if ((i/4&1) == 0) patterns[0][i] = 1;
        }
    }

    public void deadDamage() {
        serialHandler.WriteByte(DEAD);
    }

    public void sendDamage(int position, int patternIndex)
    {
        if (!hasSend)
        {
            position = 0;
            patternIndex = 0;
            pattern = patterns[patternIndex];
            sendIndex = 0;
            this.position = position;

            hasSend = true;
            _send();
        }
    }

    private void _send() {
        serialHandler.WriteByte(pattern[sendIndex] * DIRECTION[position]);
        sendIndex++;

        sendTimer.ExpiredReset();
        if (sendIndex == SEND_RANGE)
        {
            // hasSend = false;
            sendTimer.expire += () =>
            {
                serialHandler.WriteByte(0);
            };
        }
        else
        {
            sendTimer.expire += () =>
            {
                _send();
            };
        }
        sendTimer.Start(INTERVAL);
    }
}
