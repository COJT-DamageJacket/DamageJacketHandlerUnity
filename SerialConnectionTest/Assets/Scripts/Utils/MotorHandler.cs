using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorHandler : MonoBehaviour {

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

    private int N, D;
    public Motor[] motors;

    public MotorHandler(int N)
    {
        this.N = N;
        D = (int)Math.Log(N, 2);

        motors = new Motor[N];
        for (int i = 0; i < N; i++)
            motors[i] = new Motor();
    }

    private int GetSide(Side side)
    {
        if (side == Side.Right) return 1 << (D-1);
        else return 0;
    }

    private int GetPart(BodyPart part)
    {
        if (part == BodyPart.Shoulder) return 1 << 0;
        else return 0;
    }

    private int GetPosition(Side side, BodyPart part)
    {
        return GetSide(side) + GetPart(part);
    }

    public void Activate(Side side, BodyPart part, Power power)
    {
        int position = GetPosition(side, part);
        motors[position].power = power;
    }

    public Power PartPower(Side side, BodyPart part)
    {
        int position = GetPosition(side, part);
        return motors[position].power;
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
