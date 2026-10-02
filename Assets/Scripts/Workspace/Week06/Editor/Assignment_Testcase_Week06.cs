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

        [Test]
        public void As01_01_Car_InheritsMonoBehaviour()
        {
            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(CarType), "คลาส Car ต้องสืบทอดจาก MonoBehaviour");
        }

        [TestCase("name", typeof(string))]
        [TestCase("color", typeof(string))]
        [TestCase("speed", typeof(float))]
        public void As01_02_Car_PublicFields(string fieldName, System.Type expectedType)
        {
            var field = CarType.GetField(fieldName, AnyInstance);
            Assert.IsNotNull(field, $"คลาส Car ต้องมีฟิลด์ชื่อ {fieldName}");
            Assert.AreEqual(expectedType, field.FieldType, $"ฟิลด์ {fieldName} ต้องเป็นชนิด {expectedType.Name}");
            Assert.IsTrue(field.IsPublic, $"ฟิลด์ {fieldName} ต้องเป็น public");
        }

        [TestCase("Move", "Car is moving")]
        [TestCase("Turn", "Car is turning")]
        [TestCase("Honk", "Car is honking")]
        public void As01_03_Car_MethodsPrintCorrectMessages(string methodName, string expectedMessage)
        {
            var go = new GameObject("TestCar");
            var car = go.AddComponent(CarType) as MonoBehaviour;

            CarType.GetField("name", AnyInstance)?.SetValue(car, "civic");
            CarType.GetField("color", AnyInstance)?.SetValue(car, "black");
            CarType.GetField("speed", AnyInstance)?.SetValue(car, 110f);

            var method = CarType.GetMethod(methodName, AnyInstance);
            Assert.IsNotNull(method, $"คลาส Car ต้องมีเมธอด {methodName}()");

            SimpleDebugConsole.Clear();
            method.Invoke(car, null);
            TestUtils.AssertMultilineEqual(expectedMessage, SimpleDebugConsole.GetOutput());

            Object.DestroyImmediate(go);
        }

        [Test]
        public void As01_04_Car_StartInitializesDefaultValues()
        {
            var go = new GameObject("TestCar");
            var car = go.AddComponent(CarType) as MonoBehaviour;

            var startMethod = CarType.GetMethod("Start", AnyInstance);
            Assert.IsNotNull(startMethod, "คลาส Car ต้องมีเมธอด Start()");
            startMethod.Invoke(car, null);

            var nameVal = CarType.GetField("name", AnyInstance)?.GetValue(car) as string;
            var colorVal = CarType.GetField("color", AnyInstance)?.GetValue(car) as string;
            var speedVal = CarType.GetField("speed", AnyInstance)?.GetValue(car);

            Assert.AreEqual("civic", nameVal, "Start() ต้องกำหนดค่า name เป็น civic");
            Assert.AreEqual("black", colorVal, "Start() ต้องกำหนดค่า color เป็น black");
            Assert.AreEqual(110f, speedVal != null ? System.Convert.ToSingle(speedVal) : 0f, "Start() ต้องกำหนดค่า speed เป็น 110");

            Object.DestroyImmediate(go);
        }

        [Test]
        public void As01_05_Car_UpdateMethodAndDemo()
        {
            var updateMethod = CarType.GetMethod("Update", AnyInstance);
            Assert.IsNotNull(updateMethod, "คลาส Car ต้องมีเมธอด Update()");
            Assert.DoesNotThrow(() => assignment.Ex01_CarDemo(), "Ex01_CarDemo() ต้องรันได้โดยไม่เกิดข้อผิดพลาด");
        }

        // =========================================================================================
        // ข้อ 2: คอนสตรัคเตอร์ (Dog)
        // =========================================================================================

        [Test]
        public void As02_01_Dog_ConstructorParameters()
        {
            var ctor = Dog02Type.GetConstructor(new[] { typeof(string), typeof(string), typeof(int) });
            Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ (string name, string breed, int age)");

            var ps = ctor.GetParameters();
            Assert.AreEqual("name", ps[0].Name, "พารามิเตอร์ตัวที่ 1 ต้องชื่อ name");
            Assert.AreEqual("breed", ps[1].Name, "พารามิเตอร์ตัวที่ 2 ต้องชื่อ breed");
            Assert.AreEqual("age", ps[2].Name, "พารามิเตอร์ตัวที่ 3 ต้องชื่อ age");
        }

        [TestCase("Buddy", "Golden Retriever", 3)]
        [TestCase("Max", "Beagle", 5)]
        [TestCase("Coco", "Poodle", 1)]
        public void As02_02_Dog_ConstructorAssignsFields(string name, string breed, int age)
        {
            var ctor = Dog02Type.GetConstructor(new[] { typeof(string), typeof(string), typeof(int) });
            var dog = ctor.Invoke(new object[] { name, breed, age });

            var nameField = Dog02Type.GetField("name", AnyInstance);
            var breedField = Dog02Type.GetField("breed", AnyInstance);
            var ageField = Dog02Type.GetField("age", AnyInstance);

            Assert.AreEqual(name, nameField?.GetValue(dog), "Constructor ต้องกำหนดค่า name");
            Assert.AreEqual(breed, breedField?.GetValue(dog), "Constructor ต้องกำหนดค่า breed");
            Assert.AreEqual(age, ageField?.GetValue(dog), "Constructor ต้องกำหนดค่า age");
        }

        [TestCase("Buddy", "Bark", "Buddy is barking")]
        [TestCase("Buddy", "WagTail", "Buddy is wagging tail")]
        [TestCase("Buddy", "StopBarking", "Buddy stopped barking")]
        public void As02_03_Dog_MethodsOutput(string dogName, string methodName, string expectedMessage)
        {
            var ctor = Dog02Type.GetConstructor(new[] { typeof(string), typeof(string), typeof(int) });
            var dog = ctor.Invoke(new object[] { dogName, "Golden Retriever", 3 });

            var method = Dog02Type.GetMethod(methodName, AnyInstance);
            Assert.IsNotNull(method, $"คลาส Dog ต้องมีเมธอด {methodName}()");

            SimpleDebugConsole.Clear();
            method.Invoke(dog, null);
            TestUtils.AssertMultilineEqual(expectedMessage, SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As02_04_DogDemo_UsesBuddy()
        {
            SimpleDebugConsole.Clear();
            assignment.Ex02_DogDemo();

            var sb = new StringBuilder();
            sb.AppendLine("Buddy is barking");
            sb.AppendLine("Buddy is wagging tail");
            sb.AppendLine("Buddy stopped barking");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // =========================================================================================
        // ข้อ 3: ศัตรูและการต่อสู้ (Enemy)
        // =========================================================================================

        [Test]
        public void As03_01_Enemy_InheritsMonoBehaviour()
        {
            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(EnemyType), "คลาส Enemy ต้องสืบทอดจาก MonoBehaviour");
        }

        [TestCase("name", typeof(string))]
        [TestCase("energy", typeof(int))]
        [TestCase("attackPoint", typeof(int))]
        public void As03_02_Enemy_RequiredFields(string fieldName, System.Type expectedType)
        {
            var field = EnemyType.GetField(fieldName, AnyInstance);
            Assert.IsNotNull(field, $"Enemy ต้องมีฟิลด์ {fieldName}");
            Assert.AreEqual(expectedType, field.FieldType, $"ฟิลด์ {fieldName} ต้องเป็นชนิด {expectedType.Name}");
        }

        [TestCase(2, 8)]
        [TestCase(5, 5)]
        [TestCase(10, 0)]
        public void As03_03_Enemy_TakeDamage(int damage, int expectedHp)
        {
            var go = new GameObject("TestEnemy");
            var enemy = go.AddComponent(EnemyType) as MonoBehaviour;

            var energyField = EnemyType.GetField("energy", AnyInstance);
            energyField?.SetValue(enemy, 10);

            var takeDamageMethod = EnemyType.GetMethod("TakeDamage", AnyInstance);
            Assert.IsNotNull(takeDamageMethod, "Enemy ต้องมีเมธอด TakeDamage(int)");

            takeDamageMethod.Invoke(enemy, new object[] { damage });
            int remaining = (int)energyField.GetValue(enemy);
            Assert.AreEqual(expectedHp, remaining, $"รับดาเมจ {damage} จาก 10 ต้องเหลือ {expectedHp}");

            if (go != null) Object.DestroyImmediate(go);
        }

        [Test]
        public void As03_04_Enemy_AttackAndTriggerMethodsExist()
        {
            var attackMethod = EnemyType.GetMethod("Attack", AnyInstance);
            var triggerMethod = EnemyType.GetMethod("OnTriggerEnter2D", AnyInstance);

            Assert.IsNotNull(attackMethod, "Enemy ต้องมีเมธอด Attack()");
            Assert.IsNotNull(triggerMethod, "Enemy ต้องมีเมธอด OnTriggerEnter2D()");
        }

        // =========================================================================================
        // ข้อ 4: ทางออกของเกม (Exit)
        // =========================================================================================

        [Test]
        public void As04_01_Exit_InheritsMonoBehaviour()
        {
            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(ExitType), "คลาส Exit ต้องสืบทอดจาก MonoBehaviour");
        }

        [TestCase("positionX", typeof(int))]
        [TestCase("positionY", typeof(int))]
        public void As04_02_Exit_RequiredFields(string fieldName, System.Type expectedType)
        {
            var field = ExitType.GetField(fieldName, AnyInstance);
            Assert.IsNotNull(field, $"Exit ต้องมีฟิลด์ {fieldName}");
            Assert.AreEqual(expectedType, field.FieldType, $"ฟิลด์ {fieldName} ต้องเป็นชนิด {expectedType.Name}");
        }

        [Test]
        public void As04_03_Exit_OnTriggerEnter2DExists()
        {
            var triggerMethod = ExitType.GetMethod("OnTriggerEnter2D", AnyInstance);
            Assert.IsNotNull(triggerMethod, "Exit ต้องมีเมธอด OnTriggerEnter2D()");
        }

        // =========================================================================================
        // ข้อ 5: ไอเทมยาฟื้นพลัง (ItemPotion)
        // =========================================================================================

        [Test]
        public void As05_01_ItemPotion_InheritsMonoBehaviour()
        {
            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(PotionType), "คลาส ItemPotion ต้องสืบทอดจาก MonoBehaviour");
        }

        [TestCase("name", typeof(string))]
        [TestCase("healPoint", typeof(int))]
        public void As05_02_ItemPotion_RequiredFields(string fieldName, System.Type expectedType)
        {
            var field = PotionType.GetField(fieldName, AnyInstance);
            Assert.IsNotNull(field, $"ItemPotion ต้องมีฟิลด์ {fieldName}");
            Assert.AreEqual(expectedType, field.FieldType, $"ฟิลด์ {fieldName} ต้องเป็นชนิด {expectedType.Name}");
        }

        [Test]
        public void As05_03_ItemPotion_OnTriggerEnter2DExists()
        {
            var triggerMethod = PotionType.GetMethod("OnTriggerEnter2D", AnyInstance);
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