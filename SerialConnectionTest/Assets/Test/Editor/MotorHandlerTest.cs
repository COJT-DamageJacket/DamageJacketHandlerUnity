using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NewTestScript
{
    static MotorHandler motorHandler;

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        motorHandler = new MotorHandler(4);
    }

    [SetUp]
    public static void SetUp()
    {
        motorHandler.Reset();
    }

    [Description("指定したモーターの駆動強度を得る")]
    public class ActivatePartTest
    {
        public void OneTimeSetUp_()
        {
            OneTimeSetUp();
        }

        [SetUp]
        public void SetUp_()
        {
            SetUp();
        }

        [Test, Description("駆動を変更していない箇所は停止している")]
        public void IsStopTest()
        {
            Assert.AreEqual(
                Power.Stop,
                motorHandler.PartPower(Side.Left, BodyPart.Chest)
            );
            Assert.AreEqual(
                Power.Stop,
                motorHandler.PartPower(Side.Right, BodyPart.Shoulder)
            );
        }

        [Test, Description("最後に指定された強度で駆動する")]
        public void StrongthTest()
        {
            motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Strong);
            Assert.AreEqual(Power.Strong, motorHandler.PartPower(Side.Right, BodyPart.Shoulder));

            motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Weak);
            Assert.AreEqual(Power.Weak, motorHandler.PartPower(Side.Left, BodyPart.Chest));

            motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Weak);
            Assert.AreEqual(Power.Weak, motorHandler.PartPower(Side.Right, BodyPart.Shoulder));
        }
    }

    [Description("送信するByteデータをテストする")]
    public class GetByteDataTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp_()
        {
            OneTimeSetUp();
        }

        [SetUp]
        public void SetUp_()
        {
            SetUp();
        }

        [Test, Description("停止->00、弱->01、強->10")]
        public void ActivaPartValueTest()
        {
            Assert.AreEqual(0, motorHandler.GetValue());
            motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Weak);
            Assert.AreEqual(1, motorHandler.GetValue());
            motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Strong);
            Assert.AreEqual(2, motorHandler.GetValue());
        }

        [Test, Description("部位ごとの駆動強度を変更すると、組み合わせで表示される")]
        public void ActivePartsValueTest()
        {
            motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Strong);
            Assert.AreEqual(System.Convert.ToByte("10000000", 2), motorHandler.GetValue());

            motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Strong);
            motorHandler.Activate(Side.Left, BodyPart.Shoulder, Power.Weak);
            motorHandler.Activate(Side.Right, BodyPart.Chest, Power.Strong);
            motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Stop);
            Assert.AreEqual(System.Convert.ToByte("00100110", 2), motorHandler.GetValue());
        }
    }
}
