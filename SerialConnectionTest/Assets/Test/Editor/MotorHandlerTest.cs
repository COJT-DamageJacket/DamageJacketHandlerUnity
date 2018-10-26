using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NewTestScript
{
    private MotorHandler motorHandler;
    // 8bitデータで駆動するので、動作させるモータは4つ
    const int MOTOR_NUM = 4;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        motorHandler = new MotorHandler(MOTOR_NUM);
    }

    [SetUp]
    public void SetUp()
    {
        motorHandler.Reset();
    }

    [Test]
    public void SetUpTest ()
    {
        Assert.AreEqual(MOTOR_NUM, motorHandler.motors.Length);
    }

    [Test]
    public void ActivatePartTest ()
    {
        motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Strong);
        Assert.AreEqual(Power.Strong, motorHandler.PartPower(Side.Right, BodyPart.Shoulder));
        Assert.AreEqual(Power.Stop, motorHandler.PartPower(Side.Left, BodyPart.Chest));
        motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Weak);
        Assert.AreEqual(Power.Weak, motorHandler.PartPower(Side.Left, BodyPart.Chest));
    }

    [Test]
    public void ActivaPartValueTest()
    {
        // 位置と強さを指定してできる値の正当性を検証
        // [右肩、右胴、左肩、左胴]の順に2bitずつ割り当てている
        // 2bitのうち左のほうが強、右が弱
        Assert.AreEqual(System.Convert.ToByte("00000000", 2), motorHandler.GetValue());

        motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Strong);
        Assert.AreEqual(System.Convert.ToByte("10000000", 2), motorHandler.GetValue());

        motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Weak);
        Assert.AreEqual(System.Convert.ToByte("01000000", 2), motorHandler.GetValue());

        motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Strong);
        motorHandler.Activate(Side.Left, BodyPart.Shoulder, Power.Weak);
        motorHandler.Activate(Side.Right, BodyPart.Chest, Power.Strong);
        motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Stop);
        Assert.AreEqual(System.Convert.ToByte("00100110", 2), motorHandler.GetValue());
    }
}
