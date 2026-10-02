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
        // ===================== ข้อ 1: สร้างคลาส Car =====================

        [Test]
        public void As01_Car()
        {
            var t = CarType;

            // 1. ตรวจสอบการสืบทอดและฟิลด์
            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(t), "คลาส Car ต้องสืบทอดจาก MonoBehaviour");

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

            // 2. ตรวจสอบเมธอด Move, Turn, Honk และข้อความที่พิมพ์
            var go = new GameObject("TestCar");
            var car = go.AddComponent(CarType) as MonoBehaviour;
            Assert.IsNotNull(car, "ไม่สามารถสร้าง Component จากคลาส Car ได้");

            nameField.SetValue(car, "civic");
            colorField.SetValue(car, "black");
            speedField.SetValue(car, 110f);

            var moveMethod = t.GetMethod("Move", AnyInstance);
            var turnMethod = t.GetMethod("Turn", AnyInstance);
            var honkMethod = t.GetMethod("Honk", AnyInstance);

            Assert.IsNotNull(moveMethod, "คลาส Car ต้องมีเมธอด Move()");
            Assert.IsNotNull(turnMethod, "คลาส Car ต้องมีเมธอด Turn()");
            Assert.IsNotNull(honkMethod, "คลาส Car ต้องมีเมธอด Honk()");

            moveMethod.Invoke(car, null);
            turnMethod.Invoke(car, null);
            honkMethod.Invoke(car, null);

            var sb = new StringBuilder();
            sb.AppendLine("Car is moving");
            sb.AppendLine("Car is turning");
            sb.AppendLine("Car is honking");
            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());

            // 3. ตรวจสอบ Start() ค่าเริ่มต้น
            SimpleDebugConsole.Clear();
            var startMethod = t.GetMethod("Start", AnyInstance);
            Assert.IsNotNull(startMethod, "คลาส Car ต้องมีเมธอด Start()");
            startMethod.Invoke(car, null);

            var nameVal = nameField.GetValue(car) as string;
            var colorVal = colorField.GetValue(car) as string;
            var speedVal = speedField.GetValue(car);

            Assert.AreEqual("civic", nameVal, "Start() ต้องกำหนดค่า name เป็น civic");
            Assert.AreEqual("black", colorVal, "Start() ต้องกำหนดค่า color เป็น black");
            Assert.AreEqual(110f, speedVal != null ? System.Convert.ToSingle(speedVal) : 0f, "Start() ต้องกำหนดค่า speed เป็น 110");

            // 4. ตรวจสอบ Update() และ Ex01_CarDemo
            var updateMethod = t.GetMethod("Update", AnyInstance);
            Assert.IsNotNull(updateMethod, "คลาส Car ต้องมีเมธอด Update()");
            Assert.DoesNotThrow(() => assignment.Ex01_CarDemo(), "Ex01_CarDemo() ต้องรันได้โดยไม่เกิดข้อผิดพลาด");

            Object.DestroyImmediate(go);
        }

        // ===================== ข้อ 2: คอนสตรัคเตอร์ Dog =====================

        [Test]
        public void As02_ClassConstructor()
        {
            // 1. ตรวจสอบ Constructor 3 พารามิเตอร์ (name, breed, age)
            var ctor = Dog02Type.GetConstructor(new[] { typeof(string), typeof(string), typeof(int) });
            Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ (string name, string breed, int age)");

            var ps = ctor.GetParameters();
            Assert.AreEqual("name", ps[0].Name, "พารามิเตอร์ตัวที่ 1 ต้องชื่อ name");
            Assert.AreEqual("breed", ps[1].Name, "พารามิเตอร์ตัวที่ 2 ต้องชื่อ breed");
            Assert.AreEqual("age", ps[2].Name, "พารามิเตอร์ตัวที่ 3 ต้องชื่อ age");

            // 2. ตรวจสอบการกำหนดค่าลงฟิลด์
            var nameField = Dog02Type.GetField("name", AnyInstance);
            var breedField = Dog02Type.GetField("breed", AnyInstance);
            var ageField = Dog02Type.GetField("age", AnyInstance);

            var dog = ctor.Invoke(new object[] { "Buddy", "Golden Retriever", 3 });
            Assert.AreEqual("Buddy", nameField?.GetValue(dog), "Constructor ต้องกำหนดค่า name");
            Assert.AreEqual("Golden Retriever", breedField?.GetValue(dog), "Constructor ต้องกำหนดค่า breed");
            Assert.AreEqual(3, ageField?.GetValue(dog), "Constructor ต้องกำหนดค่า age");

            // 3. ตรวจสอบพฤติกรรม Bark(), WagTail(), StopBarking()
            var barkMethod = Dog02Type.GetMethod("Bark", AnyInstance);
            var wagMethod = Dog02Type.GetMethod("WagTail", AnyInstance);
            var stopMethod = Dog02Type.GetMethod("StopBarking", AnyInstance);

            Assert.IsNotNull(barkMethod, "คลาส Dog ต้องมีเมธอด Bark()");
            Assert.IsNotNull(wagMethod, "คลาส Dog ต้องมีเมธอด WagTail()");
            Assert.IsNotNull(stopMethod, "คลาส Dog ต้องมีเมธอด StopBarking()");

            SimpleDebugConsole.Clear();
            barkMethod.Invoke(dog, null);
            wagMethod.Invoke(dog, null);
            stopMethod.Invoke(dog, null);

            var sb = new StringBuilder();
            sb.AppendLine("Buddy is barking");
            sb.AppendLine("Buddy is wagging tail");
            sb.AppendLine("Buddy stopped barking");
            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());

            // 4. ตรวจสอบ Ex02_DogDemo()
            SimpleDebugConsole.Clear();
            assignment.Ex02_DogDemo();
            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // ===================== ข้อ 3: ศัตรู (Enemy) =====================

        [Test]
        public void As03_Enemy()
        {
            var t = EnemyType;
            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(t), "คลาส Enemy ต้องสืบทอดจาก MonoBehaviour");

            // 1. ตรวจสอบฟิลด์ name, energy, attackPoint
            var nameField = t.GetField("name", AnyInstance);
            var energyField = t.GetField("energy", AnyInstance);
            var atkField = t.GetField("attackPoint", AnyInstance);

            Assert.IsNotNull(nameField, "Enemy ต้องมีฟิลด์ name");
            Assert.IsNotNull(energyField, "Enemy ต้องมีฟิลด์ energy");
            Assert.IsNotNull(atkField, "Enemy ต้องมีฟิลด์ attackPoint");

            Assert.AreEqual(typeof(string), nameField.FieldType, "name ต้องเป็น string");
            Assert.AreEqual(typeof(int), energyField.FieldType, "energy ต้องเป็น int");
            Assert.AreEqual(typeof(int), atkField.FieldType, "attackPoint ต้องเป็น int");

            // 2. ตรวจสอบเมธอด Attack, TakeDamage, OnTriggerEnter2D
            var attackMethod = t.GetMethod("Attack", AnyInstance);
            var takeDamageMethod = t.GetMethod("TakeDamage", AnyInstance);
            var triggerMethod = t.GetMethod("OnTriggerEnter2D", AnyInstance);

            Assert.IsNotNull(attackMethod, "Enemy ต้องมีเมธอด Attack()");
            Assert.IsNotNull(takeDamageMethod, "Enemy ต้องมีเมธอด TakeDamage(int)");
            Assert.IsNotNull(triggerMethod, "Enemy ต้องมีเมธอด OnTriggerEnter2D()");

            // 3. ทดสอบการรับดาเมจ TakeDamage
            var go = new GameObject("TestEnemy");
            var enemy = go.AddComponent(EnemyType) as MonoBehaviour;
            energyField.SetValue(enemy, 10);

            takeDamageMethod.Invoke(enemy, new object[] { 4 });
            int remaining = (int)energyField.GetValue(enemy);
            Assert.AreEqual(6, remaining, "โดนดาเมจ 4 จาก 10 ต้องเหลือ 6");

            if (go != null) Object.DestroyImmediate(go);
        }

        // ===================== ข้อ 4: ทางออก (Exit) =====================

        [Test]
        public void As04_Exit()
        {
            var t = ExitType;
            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(t), "คลาส Exit ต้องสืบทอดจาก MonoBehaviour");

            // 1. ตรวจสอบฟิลด์ positionX, positionY
            var posX = t.GetField("positionX", AnyInstance);
            var posY = t.GetField("positionY", AnyInstance);

            Assert.IsNotNull(posX, "Exit ต้องมีฟิลด์ positionX");
            Assert.IsNotNull(posY, "Exit ต้องมีฟิลด์ positionY");
            Assert.AreEqual(typeof(int), posX.FieldType, "positionX ต้องเป็น int");
            Assert.AreEqual(typeof(int), posY.FieldType, "positionY ต้องเป็น int");

            // 2. ตรวจสอบเมธอด OnTriggerEnter2D
            var triggerMethod = t.GetMethod("OnTriggerEnter2D", AnyInstance);
            Assert.IsNotNull(triggerMethod, "Exit ต้องมีเมธอด OnTriggerEnter2D()");
        }

        // ===================== ข้อ 5: ไอเทมยาฟื้นพลัง (ItemPotion) =====================

        [Test]
        public void As05_ItemPotion()
        {
            var t = PotionType;
            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(t), "คลาส ItemPotion ต้องสืบทอดจาก MonoBehaviour");

            // 1. ตรวจสอบฟิลด์ name, healPoint
            var nameField = t.GetField("name", AnyInstance);
            var healField = t.GetField("healPoint", AnyInstance);

            Assert.IsNotNull(nameField, "ItemPotion ต้องมีฟิลด์ name");
            Assert.IsNotNull(healField, "ItemPotion ต้องมีฟิลด์ healPoint");
            Assert.AreEqual(typeof(string), nameField.FieldType, "name ต้องเป็น string");
            Assert.AreEqual(typeof(int), healField.FieldType, "healPoint ต้องเป็น int");

            // 2. ตรวจสอบเมธอด OnTriggerEnter2D
            var triggerMethod = t.GetMethod("OnTriggerEnter2D", AnyInstance);
            Assert.IsNotNull(triggerMethod, "ItemPotion ต้องมีเมธอด OnTriggerEnter2D()");
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