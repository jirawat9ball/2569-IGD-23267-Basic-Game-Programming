using System.Reflection;
using System.Text;

using NUnit.Framework;
using UnityEngine;

using Week06;
using SimpleDebugConsole = Workspace.Core.SimpleDebugConsole;

namespace Week06_Class
{
    public class TestBase
    {
        // =========================================================================================
        // 🎯 สลับตรวจไฟล์ อ. หรือ นักเรียน: เปลี่ยนเป็น true เมื่อต้องการตรวจไฟล์เฉลยอาจารย์
        // =========================================================================================
        protected const bool isTeacherMode = false;

        protected IAssignment assignment;
        protected GameObject testGo;

        protected const BindingFlags AnyInstance =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        protected static System.Type CarType => isTeacherMode ? typeof(Week06.Teacher.Ex01.Car) : typeof(Week06.Ex01.Car);
        protected static System.Type Dog02Type => isTeacherMode ? typeof(Week06.Teacher.Ex02.Dog) : typeof(Week06.Ex02.Dog);
        protected static System.Type EnemyType => isTeacherMode ? typeof(Week06.Teacher.Ex03.Enemy) : typeof(Week06.Ex03.Enemy);
        protected static System.Type ExitType => isTeacherMode ? typeof(Week06.Teacher.Ex04.Exit) : typeof(Week06.Ex04.Exit);
        protected static System.Type PotionType => isTeacherMode ? typeof(Week06.Teacher.Ex05.ItemPotion) : typeof(Week06.Ex05.ItemPotion);

        [SetUp]
        public void Setup()
        {
            testGo = new GameObject("Week06_TestRunner");
            if (isTeacherMode)
                assignment = testGo.AddComponent<Week06.Assignment_Teacher_Week06>();
            else
                assignment = testGo.AddComponent<Assignment_Student_Week06>();
            SimpleDebugConsole.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            if (testGo != null)
                Object.DestroyImmediate(testGo);
        }
    }

    public class Lecture : TestBase
    {
        // =========================================================================================
        // ข้อ 1: การสร้างคลาสเบื้องต้น (Car)
        // =========================================================================================

        [TestCase("01_PublicFields")]
        [TestCase("02_MethodsPrintCorrectMessages")]
        [TestCase("03_StartInitializesDefaultValues")]
        [TestCase("04_UpdateMethodAndDemo")]
        public void As01_Car(string subTask)
        {
            var t = CarType;
            switch (subTask)
            {
                case "01_PublicFields":
                    var nameField = t.GetField("name", AnyInstance);
                    var colorField = t.GetField("color", AnyInstance);
                    var speedField = t.GetField("speed", AnyInstance);

                    Assert.IsNotNull(nameField, "คลาส Car ต้องมีฟิลด์ชื่อ name");
                    Assert.IsNotNull(colorField, "คลาส Car ต้องมีฟิลด์ชื่อ color");
                    Assert.IsNotNull(speedField, "คลาส Car ต้องมีฟิลด์ชื่อ speed");

                    Assert.AreEqual(typeof(string), nameField.FieldType, "name ต้องเป็น string");
                    Assert.AreEqual(typeof(string), colorField.FieldType, "color ต้องเป็น string");
                    Assert.AreEqual(typeof(float), speedField.FieldType, "speed ต้องเป็น float");

                    Assert.IsTrue(nameField.IsPublic && colorField.IsPublic && speedField.IsPublic, "ฟิลด์ทั้งสามต้องเป็น public");
                    break;

                case "02_MethodsPrintCorrectMessages":
                    var go = new GameObject("TestCar");
                    var car = go.AddComponent(CarType) as MonoBehaviour;
                    Assert.IsNotNull(car, "ไม่สามารถสร้าง Component จากคลาส Car ได้");

                    t.GetField("name", AnyInstance)?.SetValue(car, "civic");
                    t.GetField("color", AnyInstance)?.SetValue(car, "black");
                    t.GetField("speed", AnyInstance)?.SetValue(car, 110f);

                    var moveMethod = t.GetMethod("Move", AnyInstance);
                    var turnMethod = t.GetMethod("Turn", AnyInstance);
                    var honkMethod = t.GetMethod("Honk", AnyInstance);

                    Assert.IsNotNull(moveMethod, "คลาส Car ต้องมีเมธอด Move()");
                    Assert.IsNotNull(turnMethod, "คลาส Car ต้องมีเมธอด Turn()");
                    Assert.IsNotNull(honkMethod, "คลาส Car ต้องมีเมธอด Honk()");

                    SimpleDebugConsole.Clear();
                    moveMethod.Invoke(car, null);
                    turnMethod.Invoke(car, null);
                    honkMethod.Invoke(car, null);

                    var sb = new StringBuilder();
                    sb.AppendLine("Car is moving");
                    sb.AppendLine("Car is turning");
                    sb.AppendLine("Car is honking");
                    TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());

                    Object.DestroyImmediate(go);
                    break;

                case "03_StartInitializesDefaultValues":
                    var goStart = new GameObject("TestCar");
                    var carStart = goStart.AddComponent(CarType) as MonoBehaviour;

                    var startMethod = t.GetMethod("Start", AnyInstance);
                    Assert.IsNotNull(startMethod, "คลาส Car ต้องมีเมธอด Start()");
                    startMethod.Invoke(carStart, null);

                    var nameVal = t.GetField("name", AnyInstance)?.GetValue(carStart) as string;
                    var colorVal = t.GetField("color", AnyInstance)?.GetValue(carStart) as string;
                    var speedVal = t.GetField("speed", AnyInstance)?.GetValue(carStart);

                    Assert.AreEqual("civic", nameVal, "Start() ต้องกำหนดค่า name เป็น civic");
                    Assert.AreEqual("black", colorVal, "Start() ต้องกำหนดค่า color เป็น black");
                    Assert.AreEqual(110f, speedVal != null ? System.Convert.ToSingle(speedVal) : 0f, "Start() ต้องกำหนดค่า speed เป็น 110");

                    Object.DestroyImmediate(goStart);
                    break;

                case "04_UpdateMethodAndDemo":
                    var updateMethod = t.GetMethod("Update", AnyInstance);
                    Assert.IsNotNull(updateMethod, "คลาส Car ต้องมีเมธอด Update()");
                    Assert.DoesNotThrow(() => assignment.Ex01_CarDemo(), "Ex01_CarDemo() ต้องรันได้โดยไม่เกิดข้อผิดพลาด");
                    break;
            }
        }

        // =========================================================================================
        // ข้อ 2: คอนสตรัคเตอร์ (Dog)
        // =========================================================================================

        [TestCase("01_ConstructorParameters")]
        [TestCase("02_ConstructorAssignsFields")]
        [TestCase("03_MethodsOutput")]
        [TestCase("04_DogDemo_UsesBuddy")]
        public void As02_ClassConstructor(string subTask)
        {
            var t = Dog02Type;
            switch (subTask)
            {
                case "01_ConstructorParameters":
                    var ctor = t.GetConstructor(new[] { typeof(string), typeof(string), typeof(int) });
                    Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ (string name, string breed, int age)");

                    var ps = ctor.GetParameters();
                    Assert.AreEqual("name", ps[0].Name, "พารามิเตอร์ตัวที่ 1 ต้องชื่อ name");
                    Assert.AreEqual("breed", ps[1].Name, "พารามิเตอร์ตัวที่ 2 ต้องชื่อ breed");
                    Assert.AreEqual("age", ps[2].Name, "พารามิเตอร์ตัวที่ 3 ต้องชื่อ age");
                    break;

                case "02_ConstructorAssignsFields":
                    var ctorAssign = t.GetConstructor(new[] { typeof(string), typeof(string), typeof(int) });
                    var dog = ctorAssign.Invoke(new object[] { "Buddy", "Golden Retriever", 3 });

                    var nameField = t.GetField("name", AnyInstance);
                    var breedField = t.GetField("breed", AnyInstance);
                    var ageField = t.GetField("age", AnyInstance);

                    Assert.AreEqual("Buddy", nameField?.GetValue(dog), "Constructor ต้องกำหนดค่า name");
                    Assert.AreEqual("Golden Retriever", breedField?.GetValue(dog), "Constructor ต้องกำหนดค่า breed");
                    Assert.AreEqual(3, ageField?.GetValue(dog), "Constructor ต้องกำหนดค่า age");
                    break;

                case "03_MethodsOutput":
                    var ctorMethod = t.GetConstructor(new[] { typeof(string), typeof(string), typeof(int) });
                    var dogTest = ctorMethod.Invoke(new object[] { "Buddy", "Golden Retriever", 3 });

                    var barkMethod = t.GetMethod("Bark", AnyInstance);
                    var wagMethod = t.GetMethod("WagTail", AnyInstance);
                    var stopMethod = t.GetMethod("StopBarking", AnyInstance);

                    Assert.IsNotNull(barkMethod, "คลาส Dog ต้องมีเมธอด Bark()");
                    Assert.IsNotNull(wagMethod, "คลาส Dog ต้องมีเมธอด WagTail()");
                    Assert.IsNotNull(stopMethod, "คลาส Dog ต้องมีเมธอด StopBarking()");

                    SimpleDebugConsole.Clear();
                    barkMethod.Invoke(dogTest, null);
                    wagMethod.Invoke(dogTest, null);
                    stopMethod.Invoke(dogTest, null);

                    var sb = new StringBuilder();
                    sb.AppendLine("Buddy is barking");
                    sb.AppendLine("Buddy is wagging tail");
                    sb.AppendLine("Buddy stopped barking");
                    TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
                    break;

                case "04_DogDemo_UsesBuddy":
                    SimpleDebugConsole.Clear();
                    assignment.Ex02_DogDemo();

                    var sbDemo = new StringBuilder();
                    sbDemo.AppendLine("Buddy is barking");
                    sbDemo.AppendLine("Buddy is wagging tail");
                    sbDemo.AppendLine("Buddy stopped barking");
                    TestUtils.AssertMultilineEqual(sbDemo.ToString(), SimpleDebugConsole.GetOutput());
                    break;
            }
        }

        // =========================================================================================
        // ข้อ 3: ศัตรูและการต่อสู้ (Enemy)
        // =========================================================================================

        [TestCase("01_RequiredFields")]
        [TestCase("02_TakeDamage")]
        [TestCase("03_AttackAndTrigger")]
        public void As03_Enemy(string subTask)
        {
            var t = EnemyType;
            switch (subTask)
            {
                case "01_RequiredFields":
                    var nameField = t.GetField("name", AnyInstance);
                    var energyField = t.GetField("energy", AnyInstance);
                    var atkField = t.GetField("attackPoint", AnyInstance);

                    Assert.IsNotNull(nameField, "Enemy ต้องมีฟิลด์ name");
                    Assert.IsNotNull(energyField, "Enemy ต้องมีฟิลด์ energy");
                    Assert.IsNotNull(atkField, "Enemy ต้องมีฟิลด์ attackPoint");

                    Assert.AreEqual(typeof(string), nameField.FieldType, "name ต้องเป็น string");
                    Assert.AreEqual(typeof(int), energyField.FieldType, "energy ต้องเป็น int");
                    Assert.AreEqual(typeof(int), atkField.FieldType, "attackPoint ต้องเป็น int");
                    break;

                case "02_TakeDamage":
                    var go = new GameObject("TestEnemy");
                    var enemy = go.AddComponent(EnemyType) as MonoBehaviour;

                    var eField = t.GetField("energy", AnyInstance);
                    eField?.SetValue(enemy, 10);

                    var takeDamageMethod = t.GetMethod("TakeDamage", AnyInstance);
                    Assert.IsNotNull(takeDamageMethod, "Enemy ต้องมีเมธอด TakeDamage(int)");

                    takeDamageMethod.Invoke(enemy, new object[] { 4 });
                    int remaining = (int)eField.GetValue(enemy);
                    Assert.AreEqual(6, remaining, "โดนดาเมจ 4 จาก 10 ต้องเหลือ 6");

                    if (go != null) Object.DestroyImmediate(go);
                    break;

                case "03_AttackAndTrigger":
                    var attackMethod = t.GetMethod("Attack", AnyInstance);
                    var triggerMethod = t.GetMethod("OnTriggerEnter2D", AnyInstance);

                    Assert.IsNotNull(attackMethod, "Enemy ต้องมีเมธอด Attack()");
                    Assert.IsNotNull(triggerMethod, "Enemy ต้องมีเมธอด OnTriggerEnter2D()");
                    break;
            }
        }

        // =========================================================================================
        // ข้อ 4: ทางออกของเกม (Exit)
        // =========================================================================================

        [TestCase("01_RequiredFields")]
        [TestCase("02_TriggerExists")]
        public void As04_Exit(string subTask)
        {
            var t = ExitType;
            switch (subTask)
            {
                case "01_RequiredFields":
                    var posX = t.GetField("positionX", AnyInstance);
                    var posY = t.GetField("positionY", AnyInstance);

                    Assert.IsNotNull(posX, "Exit ต้องมีฟิลด์ positionX");
                    Assert.IsNotNull(posY, "Exit ต้องมีฟิลด์ positionY");
                    Assert.AreEqual(typeof(int), posX.FieldType, "positionX ต้องเป็น int");
                    Assert.AreEqual(typeof(int), posY.FieldType, "positionY ต้องเป็น int");
                    break;

                case "02_TriggerExists":
                    var triggerMethod = t.GetMethod("OnTriggerEnter2D", AnyInstance);
                    Assert.IsNotNull(triggerMethod, "Exit ต้องมีเมธอด OnTriggerEnter2D()");
                    break;
            }
        }

        // =========================================================================================
        // ข้อ 5: ไอเทมยาฟื้นพลัง (ItemPotion)
        // =========================================================================================

        [TestCase("01_RequiredFields")]
        [TestCase("02_TriggerExists")]
        public void As05_ItemPotion(string subTask)
        {
            var t = PotionType;
            switch (subTask)
            {
                case "01_RequiredFields":
                    var nameField = t.GetField("name", AnyInstance);
                    var healField = t.GetField("healPoint", AnyInstance);

                    Assert.IsNotNull(nameField, "ItemPotion ต้องมีฟิลด์ name");
                    Assert.IsNotNull(healField, "ItemPotion ต้องมีฟิลด์ healPoint");
                    Assert.AreEqual(typeof(string), nameField.FieldType, "name ต้องเป็น string");
                    Assert.AreEqual(typeof(int), healField.FieldType, "healPoint ต้องเป็น int");
                    break;

                case "02_TriggerExists":
                    var triggerMethod = t.GetMethod("OnTriggerEnter2D", AnyInstance);
                    Assert.IsNotNull(triggerMethod, "ItemPotion ต้องมีเมธอด OnTriggerEnter2D()");
                    break;
            }
        }
    }

    public class TestUtils
    {
        internal static void AssertMultilineEqual(string expected, string actual, string message = null)
        {
            string normExpected = expected.Replace("\r\n", "\n").Replace("\r", "\n").Trim();
            string normActual = actual.Replace("\r\n", "\n").Replace("\r", "\n").Trim();
            if (message == null)
                message = $"Expected output:\n{normExpected}\n----\nActual output:\n{normActual}";
            Assert.AreEqual(normExpected, normActual, message);
        }
    }
}