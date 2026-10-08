using System.Reflection;
using System.Text;

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
        protected static System.Type GetGameType(string typeName)
        {
            return System.Type.GetType($"Week06.Game.{typeName}, Workspace")
                ?? System.Type.GetType($"Week06.Game.{typeName}, Assembly-CSharp")
                ?? System.Type.GetType($"Week06.Game.{typeName}");
        }

        protected static System.Type GetTypeWithFallback(string typeName, string altNamespace)
        {
            return GetGameType(typeName)
                ?? System.Type.GetType($"{altNamespace}.{typeName}, Workspace")
                ?? System.Type.GetType($"{altNamespace}.{typeName}, Assembly-CSharp")
                ?? System.Type.GetType($"{altNamespace}.{typeName}");
        }

        protected static System.Type EnemyType => isTeacherMode ? typeof(Week06.Teacher.Ex03.Enemy) : GetTypeWithFallback("Enemy", "Week06.Ex03");
        protected static System.Type ExitType => isTeacherMode ? typeof(Week06.Teacher.Ex04.Exit) : GetTypeWithFallback("Exit", "Week06.Ex04");
        protected static System.Type PotionType => isTeacherMode ? typeof(Week06.Teacher.Ex05.ItemPotion) : GetTypeWithFallback("ItemPotion", "Week06.Ex05");

        protected static System.Type SwordType => isTeacherMode ? typeof(Week06.Teacher.HW01.ItemSword) : GetGameType("ItemSword");
        protected static System.Type TrapType => isTeacherMode ? typeof(Week06.Teacher.HW02.Trap) : GetGameType("Trap");
        protected static System.Type WallType => isTeacherMode ? typeof(Week06.Teacher.HW03.Wall) : GetGameType("Wall");
        protected static System.Type ChestType => isTeacherMode ? typeof(Week06.Teacher.HW04.Chest) : GetGameType("Chest");
        protected static System.Type PlayerType => typeof(Week06.Game.Player);
        protected static System.Type MapGeneratorType => typeof(Week06.Game.MapGenerator);

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
            Assert.IsNotNull(t, "ยังไม่พบสคริปต์ Enemy ในโปรเจกต์ (ให้นักเรียนสร้างคลาส Week06.Game.Enemy หรือ Week06.Ex03.Enemy)");
            switch (subTask)
            {
                case "01_RequiredFields":
                    var nameField = t.GetField("Name", AnyInstance) ?? t.GetField("name", AnyInstance);
                    var energyField = t.GetField("energy", AnyInstance);
                    var atkField = t.GetField("attackPoint", AnyInstance);

                    Assert.IsNotNull(nameField, "Enemy ต้องมีฟิลด์ Name หรือ name");
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
            Assert.IsNotNull(t, "ยังไม่พบสคริปต์ Exit ในโปรเจกต์ (ให้นักเรียนสร้างคลาส Week06.Game.Exit หรือ Week06.Ex04.Exit)");
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
            Assert.IsNotNull(t, "ยังไม่พบสคริปต์ ItemPotion ในโปรเจกต์ (ให้นักเรียนสร้างคลาส Week06.Game.ItemPotion หรือ Week06.Ex05.ItemPotion)");
            switch (subTask)
            {
                case "01_RequiredFields":
                    var nameField = t.GetField("Name", AnyInstance) ?? t.GetField("name", AnyInstance);
                    var healField = t.GetField("healPoint", AnyInstance);

                    Assert.IsNotNull(nameField, "ItemPotion ต้องมีฟิลด์ Name หรือ name");
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

        // =========================================================================================
        // ข้อ 6: ผู้เล่น (Player)
        // =========================================================================================

        [TestCase("01_RequiredFields")]
        [TestCase("02_MoveAndTakeDamage")]
        [TestCase("03_HealAndIncreaseAttack")]
        [TestCase("04_RevertPosition")]
        public void As06_Player(string subTask)
        {
            var t = PlayerType;
            switch (subTask)
            {
                case "01_RequiredFields":
                    var nameField = t.GetField("Name", AnyInstance) ?? t.GetField("name", AnyInstance);
                    var energyField = t.GetField("energy", AnyInstance);
                    var atkField = t.GetField("attackPoint", AnyInstance);
                    var posX = t.GetField("positionX", AnyInstance);
                    var posY = t.GetField("positionY", AnyInstance);

                    Assert.IsNotNull(nameField, "Player ต้องมีฟิลด์ Name");
                    Assert.IsNotNull(energyField, "Player ต้องมีฟิลด์ energy");
                    Assert.IsNotNull(atkField, "Player ต้องมีฟิลด์ attackPoint");
                    Assert.IsNotNull(posX, "Player ต้องมีฟิลด์ positionX");
                    Assert.IsNotNull(posY, "Player ต้องมีฟิลด์ positionY");
                    break;

                case "02_MoveAndTakeDamage":
                    var pGo = new GameObject("TestPlayer");
                    var player = pGo.AddComponent(t) as MonoBehaviour;
                    var energyF = t.GetField("energy", AnyInstance);
                    var posXF = t.GetField("positionX", AnyInstance);
                    energyF?.SetValue(player, 20);
                    posXF?.SetValue(player, 0);

                    var moveMethod = t.GetMethod("Move", new System.Type[] { typeof(Vector2) });
                    Assert.IsNotNull(moveMethod, "Player ต้องมีเมธอด Move(Vector2)");

                    moveMethod.Invoke(player, new object[] { Vector2.right });
                    int currentPosX = (int)posXF.GetValue(player);
                    int currentEnergy = (int)energyF.GetValue(player);

                    Assert.AreEqual(1, currentPosX, "เมื่อ Move ไปทางขวา positionX ต้องเป็น 1");
                    Assert.AreEqual(19, currentEnergy, "เมื่อ Move 1 ก้าว ต้องเสีย 1 energy");

                    if (pGo != null) Object.DestroyImmediate(pGo);
                    break;

                case "03_HealAndIncreaseAttack":
                    var pGo2 = new GameObject("TestPlayer2");
                    var player2 = pGo2.AddComponent(t) as MonoBehaviour;
                    var energyF2 = t.GetField("energy", AnyInstance);
                    var atkF2 = t.GetField("attackPoint", AnyInstance);
                    energyF2?.SetValue(player2, 10);
                    atkF2?.SetValue(player2, 10);

                    var healMethod = t.GetMethod("Heal", new System.Type[] { typeof(int) });
                    var incAtkMethod = t.GetMethod("IncreaseAttack", new System.Type[] { typeof(int) });

                    Assert.IsNotNull(healMethod, "Player ต้องมีเมธอด Heal(int)");
                    Assert.IsNotNull(incAtkMethod, "Player ต้องมีเมธอด IncreaseAttack(int)");

                    healMethod.Invoke(player2, new object[] { 5 });
                    incAtkMethod.Invoke(player2, new object[] { 10 });

                    Assert.AreEqual(15, (int)energyF2.GetValue(player2), "เมื่อ Heal 5 ค่า energy ต้องเพิ่มเป็น 15");
                    Assert.AreEqual(20, (int)atkF2.GetValue(player2), "เมื่อ IncreaseAttack 10 ค่า attackPoint ต้องเพิ่มเป็น 20");

                    if (pGo2 != null) Object.DestroyImmediate(pGo2);
                    break;

                case "04_RevertPosition":
                    var pGo3 = new GameObject("TestPlayer3");
                    var player3 = pGo3.AddComponent(t) as MonoBehaviour;
                    var posXF3 = t.GetField("positionX", AnyInstance);
                    var prevPosXF3 = t.GetField("previousPositionX", AnyInstance);
                    posXF3?.SetValue(player3, 5);
                    prevPosXF3?.SetValue(player3, 4);

                    var revertMethod = t.GetMethod("RevertPosition", AnyInstance);
                    Assert.IsNotNull(revertMethod, "Player ต้องมีเมธอด RevertPosition()");

                    revertMethod.Invoke(player3, null);
                    Assert.AreEqual(4, (int)posXF3.GetValue(player3), "เมื่อ RevertPosition ค่า positionX ต้องกลับไปเป็น previousPositionX");

                    if (pGo3 != null) Object.DestroyImmediate(pGo3);
                    break;
            }
        }

        // =========================================================================================
        // ข้อ 7: ตัวสร้างแผนที่ (MapGenerator)
        // =========================================================================================

        [TestCase("01_RequiredFields")]
        public void As07_MapGenerator(string subTask)
        {
            var t = MapGeneratorType;
            switch (subTask)
            {
                case "01_RequiredFields":
                    var rowField = t.GetField("Row", AnyInstance);
                    var colField = t.GetField("Col", AnyInstance);
                    var playerField = t.GetField("player", AnyInstance);
                    var exitField = t.GetField("Exit", AnyInstance);

                    Assert.IsNotNull(rowField, "MapGenerator ต้องมีฟิลด์ Row");
                    Assert.IsNotNull(colField, "MapGenerator ต้องมีฟิลด์ Col");
                    Assert.IsNotNull(playerField, "MapGenerator ต้องมีฟิลด์ player");
                    Assert.IsNotNull(exitField, "MapGenerator ต้องมีฟิลด์ Exit");
                    break;
            }
        }
    }

    public class Homework : TestBase
    {
        // =========================================================================================
        // ข้อ 1: ไอเทมดาบเพิ่มพลังโจมตี (ItemSword)
        // =========================================================================================

        [TestCase("01_RequiredFields")]
        [TestCase("02_TriggerExists")]
        [TestCase("03_PickUpIncreasesAttack")]
        public void Hw01_ItemSword(string subTask)
        {
            var t = SwordType;
            Assert.IsNotNull(t, "ยังไม่พบสคริปต์ ItemSword ในโปรเจกต์ (ให้นักเรียนสร้างคลาส Week06.Game.ItemSword)");
            switch (subTask)
            {
                case "01_RequiredFields":
                    var nameField = t.GetField("Name", AnyInstance) ?? t.GetField("name", AnyInstance);
                    var bonusField = t.GetField("attackBonus", AnyInstance);

                    Assert.IsNotNull(nameField, "ItemSword ต้องมีฟิลด์ Name");
                    Assert.IsNotNull(bonusField, "ItemSword ต้องมีฟิลด์ attackBonus");
                    Assert.AreEqual(typeof(string), nameField.FieldType, "Name ต้องเป็น string");
                    Assert.AreEqual(typeof(int), bonusField.FieldType, "attackBonus ต้องเป็น int");
                    break;

                case "02_TriggerExists":
                    var triggerMethod = t.GetMethod("OnTriggerEnter2D", AnyInstance);
                    Assert.IsNotNull(triggerMethod, "ItemSword ต้องมีเมธอด OnTriggerEnter2D()");
                    break;

                case "03_PickUpIncreasesAttack":
                    var swordGo = new GameObject("TestSword");
                    var sword = swordGo.AddComponent(t);

                    var playerGo = new GameObject("TestPlayer");
                    var col = playerGo.AddComponent<BoxCollider2D>();
                    var player = playerGo.AddComponent<Week06.Game.Player>();
                    player.attackPoint = 10;

                    LogAssert.Expect(LogType.Error, "Destroy may not be called from edit mode! Use DestroyImmediate instead.");

                    var trig = t.GetMethod("OnTriggerEnter2D", AnyInstance);
                    trig?.Invoke(sword, new object[] { col });

                    Assert.AreEqual(20, player.attackPoint, "เมื่อเก็บดาบ (bonus=10) ค่า attackPoint ของ Player ต้องเพิ่มขึ้น");

                    if (playerGo != null) Object.DestroyImmediate(playerGo);
                    if (swordGo != null) Object.DestroyImmediate(swordGo);
                    break;
            }
        }

        // =========================================================================================
        // ข้อ 2: กับดักหนาม (Trap)
        // =========================================================================================

        [TestCase("01_RequiredFields")]
        [TestCase("02_TriggerExists")]
        [TestCase("03_SteppingTrapsPlayer")]
        public void Hw02_Trap(string subTask)
        {
            var t = TrapType;
            Assert.IsNotNull(t, "ยังไม่พบสคริปต์ Trap ในโปรเจกต์ (ให้นักเรียนสร้างคลาส Week06.Game.Trap)");
            switch (subTask)
            {
                case "01_RequiredFields":
                    var nameField = t.GetField("Name", AnyInstance) ?? t.GetField("name", AnyInstance);
                    var damageField = t.GetField("damage", AnyInstance);

                    Assert.IsNotNull(nameField, "Trap ต้องมีฟิลด์ Name");
                    Assert.IsNotNull(damageField, "Trap ต้องมีฟิลด์ damage");
                    Assert.AreEqual(typeof(string), nameField.FieldType, "Name ต้องเป็น string");
                    Assert.AreEqual(typeof(int), damageField.FieldType, "damage ต้องเป็น int");
                    break;

                case "02_TriggerExists":
                    var triggerMethod = t.GetMethod("OnTriggerEnter2D", AnyInstance);
                    Assert.IsNotNull(triggerMethod, "Trap ต้องมีเมธอด OnTriggerEnter2D()");
                    break;

                case "03_SteppingTrapsPlayer":
                    var trapHolder = new GameObject("TestTrap");
                    var trap = trapHolder.AddComponent(t);

                    var playerGo = new GameObject("TestPlayer");
                    var col = playerGo.AddComponent<BoxCollider2D>();
                    var player = playerGo.AddComponent<Week06.Game.Player>();
                    player.energy = 20;
                    player.positionX = 0;
                    player.positionY = 0;

                    var trig = t.GetMethod("OnTriggerEnter2D", AnyInstance);
                    trig?.Invoke(trap, new object[] { col });

                    Assert.IsTrue(player.isTrapped, "เมื่อเหยียบกับดัก ค่า isTrapped ของ Player ต้องเป็น true");

                    // ทดลองเดิน 1 ครั้งขณะติดกับดัก
                    player.Move(Vector2.right);
                    Assert.AreEqual(0, player.positionX, "เมื่อติดกับดัก จะเดินไม่ได้ในครั้งนั้น (พิกัด positionX ต้องยังอยู่ที่เดิม)");
                    Assert.IsFalse(player.isTrapped, "หลังพยายามเดินแล้ว ค่า isTrapped ต้องถูกปลดเป็น false เพื่อให้เดินได้ในครั้งถัดไป");

                    if (playerGo != null) Object.DestroyImmediate(playerGo);
                    if (trapHolder != null) Object.DestroyImmediate(trapHolder);
                    break;
            }
        }

        // =========================================================================================
        // ข้อ 3: กำแพงพังได้ (Wall)
        // =========================================================================================

        [TestCase("01_RequiredFields")]
        [TestCase("02_HitReducesDurability")]
        [TestCase("03_TriggerCallsHitAndRevertsPlayer")]
        public void Hw03_Wall(string subTask)
        {
            var t = WallType;
            Assert.IsNotNull(t, "ยังไม่พบสคริปต์ Wall ในโปรเจกต์ (ให้นักเรียนสร้างคลาส Week06.Game.Wall)");
            switch (subTask)
            {
                case "01_RequiredFields":
                    var nameField = t.GetField("Name", AnyInstance) ?? t.GetField("name", AnyInstance);
                    var durField = t.GetField("durability", AnyInstance);

                    Assert.IsNotNull(nameField, "Wall ต้องมีฟิลด์ Name");
                    Assert.IsNotNull(durField, "Wall ต้องมีฟิลด์ durability");
                    Assert.AreEqual(typeof(string), nameField.FieldType, "Name ต้องเป็น string");
                    Assert.AreEqual(typeof(int), durField.FieldType, "durability ต้องเป็น int");
                    break;

                case "02_HitReducesDurability":
                    var wallGo = new GameObject("TestWall");
                    var wall = wallGo.AddComponent(t);

                    var hitMethod = t.GetMethod("Hit", AnyInstance);
                    Assert.IsNotNull(hitMethod, "Wall ต้องมีเมธอด Hit()");

                    var dur = t.GetField("durability", AnyInstance);
                    dur?.SetValue(wall, 2);

                    hitMethod.Invoke(wall, null);
                    int remaining = (int)dur.GetValue(wall);
                    Assert.AreEqual(1, remaining, "เมื่อเรียก Hit() ค่า durability ต้องลดลง 1 หน่วย");

                    if (wallGo != null) Object.DestroyImmediate(wallGo);
                    break;

                case "03_TriggerCallsHitAndRevertsPlayer":
                    var wallGo2 = new GameObject("TestWall");
                    var wall2 = wallGo2.AddComponent(t);

                    var playerGo2 = new GameObject("TestPlayer");
                    var col2 = playerGo2.AddComponent<BoxCollider2D>();
                    var player2 = playerGo2.AddComponent<Week06.Game.Player>();
                    player2.positionX = 0;
                    player2.positionY = 0;

                    // ผู้เล่นก้าวเดินไปยังตำแหน่งกำแพง
                    player2.Move(Vector2.right);

                    var trig2 = t.GetMethod("OnTriggerEnter2D", AnyInstance);
                    trig2?.Invoke(wall2, new object[] { col2 });

                    Assert.AreEqual(0, player2.positionX, "เมื่อชนกำแพง ผู้เล่นต้องกลับไปอยู่ที่เดิม (RevertPosition) ทำให้เดินผ่านไม่ได้");

                    if (playerGo2 != null) Object.DestroyImmediate(playerGo2);
                    if (wallGo2 != null) Object.DestroyImmediate(wallGo2);
                    break;
            }
        }

        // =========================================================================================
        // ข้อ 4: กล่องสมบัติสร้างวัตถุใหม่ (Chest)
        // =========================================================================================

        [TestCase("01_RequiredFields")]
        [TestCase("02_OpenChestInstantiatesPrefabAbove")]
        [TestCase("03_TriggerExists")]
        public void Hw04_Chest(string subTask)
        {
            var t = ChestType;
            Assert.IsNotNull(t, "ยังไม่พบสคริปต์ Chest ในโปรเจกต์ (ให้นักเรียนสร้างคลาส Week06.Game.Chest)");
            switch (subTask)
            {
                case "01_RequiredFields":
                    var nameField = t.GetField("Name", AnyInstance) ?? t.GetField("name", AnyInstance);
                    var prefabField = t.GetField("spawnPrefab", AnyInstance);

                    Assert.IsNotNull(nameField, "Chest ต้องมีฟิลด์ Name");
                    Assert.IsNotNull(prefabField, "Chest ต้องมีฟิลด์ spawnPrefab");
                    Assert.AreEqual(typeof(string), nameField.FieldType, "Name ต้องเป็น string");
                    Assert.AreEqual(typeof(GameObject), prefabField.FieldType, "spawnPrefab ต้องเป็น GameObject");
                    break;

                case "02_OpenChestInstantiatesPrefabAbove":
                    var chestGo = new GameObject("TestChest");
                    chestGo.transform.position = new Vector3(2, 3, 0);
                    var chest = chestGo.AddComponent(t);

                    var dummyPrefab = new GameObject("DummySpawnItem");
                    var prefabF = t.GetField("spawnPrefab", AnyInstance);
                    prefabF?.SetValue(chest, dummyPrefab);

                    var openMethod = t.GetMethod("OpenChest", AnyInstance);
                    Assert.IsNotNull(openMethod, "Chest ต้องมีเมธอด OpenChest()");

                    LogAssert.Expect(LogType.Error, "Destroy may not be called from edit mode! Use DestroyImmediate instead.");

                    openMethod.Invoke(chest, null);

                    var spawned = GameObject.Find("DummySpawnItem(Clone)");
                    Assert.IsNotNull(spawned, "เมื่อเปิดกล่อง ต้องทำการ Instantiate spawnPrefab ออกมาในฉาก");
                    Assert.AreEqual(4f, spawned.transform.position.y, 0.01f, "วัตถุที่เสกออกมาต้องอยู่ 'ด้านบน 1 ช่อง' (y + 1) จากตำแหน่งของกล่อง");

                    if (dummyPrefab != null) Object.DestroyImmediate(dummyPrefab);
                    if (spawned != null) Object.DestroyImmediate(spawned);
                    if (chestGo != null) Object.DestroyImmediate(chestGo);
                    break;

                case "03_TriggerExists":
                    var triggerMethod = t.GetMethod("OnTriggerEnter2D", AnyInstance);
                    Assert.IsNotNull(triggerMethod, "Chest ต้องมีเมธอด OnTriggerEnter2D()");
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