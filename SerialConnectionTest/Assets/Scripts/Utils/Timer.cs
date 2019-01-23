using UnityEngine;

public class Timer
{
    public delegate void TimeExpireEventHandler();
    public event TimeExpireEventHandler expire = null;

    private float LIMIT;
    private float time;
    private bool isRunning;
    private int state;

    public Timer()
    {
        isRunning = false;
        LIMIT = 0;
    }

    public void Start(float LIMIT)
    {
        if (isRunning)
        {
            Debug.LogWarning("This timer is already running");
        }
        else
        {
            this.LIMIT = LIMIT;
            time = 0;
            isRunning = true;
        }
    }

    public void UpdateTime(float deltaTime)
    {
        if (isRunning)
        {
            time += deltaTime;
            if (time >= LIMIT)
            {
                isRunning = false;
                expire();
            }
        }
    }

    public void Stop()
    {
        isRunning = false;
    }

    public void ExpiredReset() {
        expire = null;
    }
}
