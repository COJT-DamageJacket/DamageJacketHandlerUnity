using NUnit.Framework;

public class MotorHandlerTest
{
    public static byte[] MakeByteList(string[] strs)
    {
        byte[] res = new byte[strs.Length];
        for (int i = 0; i < strs.Length; i++)
        {
            res[i] = System.Convert.ToByte(strs[i], 2);
        }
        return res;
    }

    static MotorHandler motorHandler;

    [Description("モーターの数が4つ")]
    public class FourMotorsTest
    {

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

        [Test, Description("送るデータは1Byte分のリスト")]
        public void DataIsAByteTest()
        {
            Assert.AreEqual(1, motorHandler.Data.Length);
        }

        [Description("指定したモーターの駆動強度を得る")]
        public class ActivatePartTest
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
                Assert.AreEqual(new[] { (byte)0 }, motorHandler.Data);
                motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Weak);
                Assert.AreEqual(new[] { (byte)4 }, motorHandler.Data);
                motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Strong);
                Assert.AreEqual(new[] { (byte)8 }, motorHandler.Data);
            }

            [Test, Description("部位ごとの駆動強度を変更すると、組み合わせで表示される")]
            public void ActivePartsValueTest()
            {
                motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Strong);
                Assert.AreEqual(MakeByteList(new[] { "00100000" }), motorHandler.Data); //32

                motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Strong);
                motorHandler.Activate(Side.Left, BodyPart.Shoulder, Power.Weak);
                motorHandler.Activate(Side.Right, BodyPart.Chest, Power.Strong);
                motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Stop);
                Assert.AreEqual(MakeByteList(new[] { "10001001" }), motorHandler.Data); // 137
            }
        }
    }

    [Description("モーターの数が8つ")]
    public class EightMotorsTest
    {

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            motorHandler = new MotorHandler(8);
        }

        [SetUp]
        public static void SetUp()
        {
            motorHandler.Reset();
        }

        [Test, Description("送るデータは2Byte分のリスト")]
        public void DataIsFourBytesTest()
        {
            Assert.AreEqual(2, motorHandler.Data.Length);
        }

        [Test, Description("上位1byteが右半分、下位1byteが左半分を担当する")]
        public void ActivePartsValueTest()
        {
            motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Strong);
            Assert.AreEqual(MakeByteList(new[] { "00000010", "00000000" }), motorHandler.Data); // 2, 0

            motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Stop);
            motorHandler.Activate(Side.Right, BodyPart.Chest, Power.Strong);
            motorHandler.Activate(Side.Right, BodyPart.Stomach, Power.Weak);
            motorHandler.Activate(Side.Right, BodyPart.Back, Power.Strong);
            Assert.AreEqual(MakeByteList(new[] { "10011000", "00000000" }), motorHandler.Data); // 152, 0

            motorHandler.Activate(Side.Left, BodyPart.Shoulder, Power.Weak);
            motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Stop);
            motorHandler.Activate(Side.Left, BodyPart.Stomach, Power.Strong);
            motorHandler.Activate(Side.Left, BodyPart.Back, Power.Weak);
            Assert.AreEqual(MakeByteList(new[] { "10011000", "01100001" }), motorHandler.Data); // 152, 97
        }
    }

    [Description("モーターの数が16つ")]
    public class SixTeenMotorsTest
    {

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            motorHandler = new MotorHandler(16);
        }

        [SetUp]
        public static void SetUp()
        {
            motorHandler.Reset();
        }

        [Test, Description("送るデータは4Byte分のリスト")]
        public void DataIsFourBytesTest()
        {
            Assert.AreEqual(4, motorHandler.Data.Length);
        }

        [Test, Description("上位2byteが右半分、下位2byteが左半分を担当する")]
        public void ActivePartsValueTest()
        {
            motorHandler.Activate(Side.Right, BodyPart.Shoulder, Power.Strong);
            motorHandler.Activate(Side.Right, BodyPart.ChestOut, Power.Weak);
            motorHandler.Activate(Side.Right, BodyPart.StomachOut, Power.Strong);
            motorHandler.Activate(Side.Right, BodyPart.Back, Power.Weak);
            Assert.AreEqual(MakeByteList(new[] { "00010010", "00010010", "00000000", "00000000" }), motorHandler.Data);

            motorHandler.Activate(Side.Left, BodyPart.Chest, Power.Weak);
            motorHandler.Activate(Side.Left, BodyPart.Stomach, Power.Strong);
            motorHandler.Activate(Side.Left, BodyPart.Navel, Power.Weak);
            motorHandler.Activate(Side.Left, BodyPart.Hip, Power.Strong);
            Assert.AreEqual(MakeByteList(new[] { "00010010", "00010010", "10000100", "10000100" }), motorHandler.Data);
        }

    }
}