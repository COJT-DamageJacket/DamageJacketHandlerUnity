using System;
using UnityEngine;

public class MotorHandler : MonoBehaviour {

    readonly BodyPart[] BODYPARTS_2 = { BodyPart.Shoulder, BodyPart.Chest };
    readonly BodyPart[] BODYPARTS_4 = { BodyPart.Shoulder, BodyPart.Chest, BodyPart.Stomach, BodyPart.Back };
    readonly BodyPart[] BODYPARTS_8 = { BodyPart.Shoulder, BodyPart.Chest, BodyPart.ChestOut, BodyPart.Stomach, BodyPart.StomachOut, BodyPart.Navel, BodyPart.Back, BodyPart.Hip };
    const int INF = 1000000000;

    public class Motor
    {
        public Power power;

        public void Reset ()
        {
            power = Power.Stop;
        }

        public Motor ()
        {
            Reset();
        }
    }

    int N, D;
    public Motor[] motors;

    public byte[] data;
    public byte[] Data {
        set {
            data = value;
        }

        get {
            data = new byte[N / 4];
            byte value = 0;
            int count = 0;
            for (int i = N - 1; i >= 0; i--)
            {
                value <<= 2;
                if (motors[i].power == Power.Strong)
                    value += 2;
                else if (motors[i].power == Power.Weak)
                    value += 1;

                count++;
                if (count%4 == 0)
                {
                    data[(count >> 2)-1] = value;
                    value = 0;
                }
            }
            return data;
        }
    }

    private BodyPart[] bodyParts;

    public MotorHandler(int N)
    {
        this.N = N;
        D = (int)Math.Log(N, 2);

        motors = new Motor[N];
        for (int i = 0; i < N; i++)
            motors[i] = new Motor();

        if (N == 4) bodyParts = BODYPARTS_2;
        if (N == 8) bodyParts = BODYPARTS_4;
        if (N == 16) bodyParts = BODYPARTS_8;
    }

    private int GetSide(Side side)
    {
        if (side == Side.Right) return N/2;
        else return 0;
    }

    private int GetPart(BodyPart part)
    {
        for (int i = 0; i < N/2; i++)
        {
            if (bodyParts[i] == part) return i;
        }
        return -INF;
    }

    private int GetPosition(Side side, BodyPart part)
    {
        return GetSide(side) + GetPart(part);
    }

    public void Activate(Side side, BodyPart part, Power power)
    {
        int position = GetPosition(side, part);
        if (position >= 0)
            motors[position].power = power;
    }

    public Power PartPower(Side side, BodyPart part)
    {
        int position = GetPosition(side, part);
        if (position >= 0)
            return motors[position].power;
        return Power.Stop;
    }

    public void Reset()
    {
        for (int i = 0; i < N; i++)
        {
            motors[i].Reset();
        }
    }

    public byte GetValue()
    {
        byte value = 0;
        for (int i = N - 1; i >= 0; i--)
        {
            value <<= 2;
            if (motors[i].power == Power.Strong)
                value += 2;
            else if (motors[i].power == Power.Weak)
                value += 1;
        }
        return value;
    }
}
