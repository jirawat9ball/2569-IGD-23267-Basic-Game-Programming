using System.Reflection;
using System.Text;

using NUnit.Framework;
using UnityEngine;

using Week06;
using SimpleDebugConsole = Workspace.Core.SimpleDebugConsole;

namespace Week06_OOP
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
        protected static System.Type Animal03Type => isTeacherMode ? typeof(Week06.Teacher.Ex03.Animal) : typeof(Week06.Ex03.Animal);
        protected static System.Type Dog03Type => isTeacherMode ? typeof(Week06.Teacher.Ex03.Dog) : typeof(Week06.Ex03.Dog);
        protected static System.Type Bird03Type => isTeacherMode ? typeof(Week06.Teacher.Ex03.Bird) : typeof(Week06.Ex03.Bird);
        protected static System.Type Animal04Type => isTeacherMode ? typeof(Week06.Teacher.Ex04.Animal) : typeof(Week06.Ex04.Animal);
        protected static System.Type Dog04Type => isTeacherMode ? typeof(Week06.Teacher.Ex04.Dog) : typeof(Week06.Ex04.Dog);
        protected static System.Type Animal05Type => isTeacherMode ? typeof(Week06.Teacher.Ex05.Animal) : typeof(Week06.Ex05.Animal);
        protected static System.Type Dog05Type => isTeacherMode ? typeof(Week06.Teacher.Ex05.Dog) : typeof(Week06.Ex05.Dog);
        protected static System.Type Cat05Type => isTeacherMode ? typeof(Week06.Teacher.Ex05.Cat) : typeof(Week06.Ex05.Cat);

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
        public void As01_01_Car_InheritsMonoBehaviourAndHasRequiredFields()
        {
            var t = CarType;

            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(t), "คลาส Car ต้องสืบทอดจาก MonoBehaviour");

            var name = t.GetField("name", AnyInstance);
            var color = t.GetField("color", AnyInstance);
            var speed = t.GetField("speed", AnyInstance);

            Assert.IsNotNull(name, "คลาส Car ต้องมีฟิลด์ชื่อ name");
            Assert.IsNotNull(color, "คลาส Car ต้องมีฟิลด์ชื่อ color");
            Assert.IsNotNull(speed, "คลาส Car ต้องมีฟิลด์ชื่อ speed");

            Assert.AreEqual(typeof(string), name.FieldType, "name ต้องเป็น string");
            Assert.AreEqual(typeof(string), color.FieldType, "color ต้องเป็น string");
            Assert.AreEqual(typeof(float), speed.FieldType, "speed ต้องเป็น float");

            Assert.IsTrue(name.IsPublic && color.IsPublic && speed.IsPublic, "ฟิลด์ทั้งสามต้องเป็น public");
        }

        [Test]
        public void As01_02_Car_MethodsPrintCorrectMessages()
        {
            var go = new GameObject("TestCar");
            var car = go.AddComponent(CarType) as MonoBehaviour;
            Assert.IsNotNull(car, "ไม่สามารถสร้าง Component จากคลาส Car ได้");

            CarType.GetField("name", AnyInstance)?.SetValue(car, "civic");
            CarType.GetField("color", AnyInstance)?.SetValue(car, "black");
            CarType.GetField("speed", AnyInstance)?.SetValue(car, 110f);

            var moveMethod = CarType.GetMethod("Move", AnyInstance);
            var turnMethod = CarType.GetMethod("Turn", AnyInstance);
            var honkMethod = CarType.GetMethod("Honk", AnyInstance);

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
            Object.DestroyImmediate(go);
        }

        [Test]
        public void As01_03_Car_HasStartAndInitializesDefaultValues()
        {
            var t = CarType;
            var startMethod = t.GetMethod("Start", AnyInstance);
            Assert.IsNotNull(startMethod, "คลาส Car ต้องมีเมธอด Start()");

            var go = new GameObject("TestCar");
            var car = go.AddComponent(t) as MonoBehaviour;
            startMethod.Invoke(car, null);

            var nameVal = t.GetField("name", AnyInstance)?.GetValue(car) as string;
            var colorVal = t.GetField("color", AnyInstance)?.GetValue(car) as string;
            var speedVal = t.GetField("speed", AnyInstance)?.GetValue(car);

            Assert.AreEqual("civic", nameVal, "Start() ต้องกำหนดค่า name เป็น civic");
            Assert.AreEqual("black", colorVal, "Start() ต้องกำหนดค่า color เป็น black");
            Assert.AreEqual(110f, speedVal != null ? System.Convert.ToSingle(speedVal) : 0f, "Start() ต้องกำหนดค่า speed เป็น 110");

            Object.DestroyImmediate(go);
        }

        [Test]
        public void As01_04_Car_HasUpdateMethod()
        {
            var t = CarType;
            var updateMethod = t.GetMethod("Update", AnyInstance);
            Assert.IsNotNull(updateMethod, "คลาส Car ต้องมีเมธอด Update()");
        }

        [Test]
        public void As01_05_Ex01_CarDemo_RunsWithoutError()
        {
            Assert.DoesNotThrow(() => assignment.Ex01_CarDemo());
        }

        // ===================== ข้อ 2: Constructor =====================

        [Test]
        public void As02_01_Dog_HasConstructorWithThreeParameters()
        {
            var ctor = Dog02Type.GetConstructor(
                new[] { typeof(string), typeof(string), typeof(int) });

            Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ (string name, string breed, int age)");

            var ps = ctor.GetParameters();
            Assert.AreEqual("name", ps[0].Name, "พารามิเตอร์ตัวที่ 1 ต้องชื่อ name");
            Assert.AreEqual("breed", ps[1].Name, "พารามิเตอร์ตัวที่ 2 ต้องชื่อ breed");
            Assert.AreEqual("age", ps[2].Name, "พารามิเตอร์ตัวที่ 3 ต้องชื่อ age");
        }

        [TestCase("Buddy", "Golden Retriever", 3)]
        [TestCase("Max", "Beagle", 5)]
        [TestCase("Coco", "Poodle", 1)]
        public void As02_02_Dog_ConstructorAssignsAllFields(string name, string breed, int age)
        {
            var ctor = Dog02Type.GetConstructor(new[] { typeof(string), typeof(string), typeof(int) });
            Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ (string name, string breed, int age)");
            var dog = ctor.Invoke(new object[] { name, breed, age });

            var nameField = Dog02Type.GetField("name", AnyInstance);
            var breedField = Dog02Type.GetField("breed", AnyInstance);
            var ageField = Dog02Type.GetField("age", AnyInstance);

            Assert.AreEqual(name, nameField?.GetValue(dog), "Constructor ต้องกำหนดค่า name");
            Assert.AreEqual(breed, breedField?.GetValue(dog), "Constructor ต้องกำหนดค่า breed");
            Assert.AreEqual(age, ageField?.GetValue(dog), "Constructor ต้องกำหนดค่า age");
        }

        [TestCase("Buddy")]
        [TestCase("Max")]
        public void As02_03_Dog_MethodsUseName(string name)
        {
            var ctor = Dog02Type.GetConstructor(new[] { typeof(string), typeof(string), typeof(int) });
            Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ (string name, string breed, int age)");
            var dog = ctor.Invoke(new object[] { name, "Mixed", 2 });

            var barkMethod = Dog02Type.GetMethod("Bark", AnyInstance);
            var wagMethod = Dog02Type.GetMethod("WagTail", AnyInstance);
            var stopMethod = Dog02Type.GetMethod("StopBarking", AnyInstance);

            Assert.IsNotNull(barkMethod, "คลาส Dog ต้องมีเมธอด Bark()");
            Assert.IsNotNull(wagMethod, "คลาส Dog ต้องมีเมธอด WagTail()");
            Assert.IsNotNull(stopMethod, "คลาส Dog ต้องมีเมธอด StopBarking()");

            barkMethod.Invoke(dog, null);
            wagMethod.Invoke(dog, null);
            stopMethod.Invoke(dog, null);

            var sb = new StringBuilder();
            sb.AppendLine($"{name} is barking");
            sb.AppendLine($"{name} is wagging tail");
            sb.AppendLine($"{name} stopped barking");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As02_04_Ex02_DogDemo_UsesBuddy()
        {
            assignment.Ex02_DogDemo();

            var sb = new StringBuilder();
            sb.AppendLine("Buddy is barking");
            sb.AppendLine("Buddy is wagging tail");
            sb.AppendLine("Buddy stopped barking");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // ===================== ข้อ 3: Inheritance =====================

        [Test]
        public void As03_01_DogAndBird_InheritFromAnimal()
        {
            Assert.AreEqual(Animal03Type, Dog03Type.BaseType,
                "คลาส Dog ต้องสืบทอดจาก Animal (เขียน : Animal ต่อท้ายชื่อคลาส)");
            Assert.AreEqual(Animal03Type, Bird03Type.BaseType,
                "คลาส Bird ต้องสืบทอดจาก Animal");
        }

        [Test]
        public void As03_02_Dog_CanUseInheritedNameAndMakeSound()
        {
            var dog = System.Activator.CreateInstance(Dog03Type);
            Dog03Type.GetField("name", AnyInstance)?.SetValue(dog, "Rex");

            var makeMethod = Dog03Type.GetMethod("MakeSound", AnyInstance);
            var walkMethod = Dog03Type.GetMethod("Walk", AnyInstance);

            Assert.IsNotNull(makeMethod, "คลาส Dog ต้องเรียกเมธอด MakeSound() ได้");
            Assert.IsNotNull(walkMethod, "คลาส Dog ต้องมีเมธอด Walk()");

            makeMethod.Invoke(dog, null);
            walkMethod.Invoke(dog, null);

            var sb = new StringBuilder();
            sb.AppendLine("Animal Rex is making sound");
            sb.AppendLine("Dog Rex is walking");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As03_03_Bird_CanUseInheritedNameAndMakeSound()
        {
            var bird = System.Activator.CreateInstance(Bird03Type);
            Bird03Type.GetField("name", AnyInstance)?.SetValue(bird, "Sky");

            var makeMethod = Bird03Type.GetMethod("MakeSound", AnyInstance);
            var flyMethod = Bird03Type.GetMethod("Fly", AnyInstance);

            Assert.IsNotNull(makeMethod, "คลาส Bird ต้องเรียกเมธอด MakeSound() ได้");
            Assert.IsNotNull(flyMethod, "คลาส Bird ต้องมีเมธอด Fly()");

            makeMethod.Invoke(bird, null);
            flyMethod.Invoke(bird, null);

            var sb = new StringBuilder();
            sb.AppendLine("Animal Sky is making sound");
            sb.AppendLine("Bird Sky is flying");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As03_04_Ex03_InheritanceDemo_Output()
        {
            assignment.Ex03_InheritanceDemo();

            var sb = new StringBuilder();
            sb.AppendLine("Animal Buddy is making sound");
            sb.AppendLine("Dog Buddy is walking");
            sb.AppendLine("Animal Twitty is making sound");
            sb.AppendLine("Bird Twitty is flying");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // ===================== ข้อ 4: Access Modifiers =====================

        [Test]
        public void As04_01_Animal_FieldsHaveCorrectAccessLevels()
        {
            var t = Animal04Type;

            var name = t.GetField("name", AnyInstance);
            var specie = t.GetField("specie", AnyInstance);
            var health = t.GetField("health", AnyInstance);

            Assert.IsNotNull(name, "Animal ต้องมีฟิลด์ name");
            Assert.IsNotNull(specie, "Animal ต้องมีฟิลด์ specie");
            Assert.IsNotNull(health, "Animal ต้องมีฟิลด์ health");

            Assert.IsTrue(name.IsPublic, "name ต้องเป็น public");
            Assert.IsTrue(specie.IsFamily, "specie ต้องเป็น protected");
            Assert.IsTrue(health.IsPrivate, "health ต้องเป็น private");
        }

        [Test]
        public void As04_02_Dog_InheritsAnimalAndHasNameConstructor()
        {
            Assert.AreEqual(Animal04Type, Dog04Type.BaseType,
                "คลาส Dog ต้องสืบทอดจาก Animal");

            var ctor = Dog04Type.GetConstructor(new[] { typeof(string) });
            Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ string name");
        }

        [Test]
        public void As04_03_Dog_ConstructorSetsNameAndSpecie()
        {
            var ctor = Dog04Type.GetConstructor(new[] { typeof(string) });
            Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ string name");
            var dog = ctor.Invoke(new object[] { "Buddy" });

            var name = Dog04Type.GetField("name", AnyInstance)?.GetValue(dog) as string;
            Assert.AreEqual("Buddy", name, "Constructor ต้องกำหนดค่า name");

            var specie = Animal04Type.GetField("specie", AnyInstance)?.GetValue(dog) as string;
            Assert.AreEqual("Dog", specie, "Constructor ต้องกำหนด specie = \"Dog\"");
        }

        [TestCase(0, "weak!")]
        [TestCase(40, "weak!")]
        [TestCase(41, "happy!")]
        [TestCase(90, "happy!")]
        public void As04_04_Feed_ThenMakeSound_ChecksHealthThreshold(int food, string expectedMood)
        {
            var ctor = Dog04Type.GetConstructor(new[] { typeof(string) });
            Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ string name");
            var dog = ctor.Invoke(new object[] { "Buddy" });

            var feedMethod = Dog04Type.GetMethod("Feed", AnyInstance);
            var makeMethod = Dog04Type.GetMethod("MakeSound", AnyInstance);

            Assert.IsNotNull(feedMethod, "คลาส Dog หรือ Animal ต้องมีเมธอด Feed(int)");
            Assert.IsNotNull(makeMethod, "คลาส Dog หรือ Animal ต้องมีเมธอด MakeSound()");

            feedMethod.Invoke(dog, new object[] { food });
            makeMethod.Invoke(dog, null);

            var sb = new StringBuilder();
            sb.AppendLine($"Buddy got {food} food");
            sb.AppendLine($"Buddy {expectedMood}");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As04_05_Ex04_AccessModifierDemo_Output()
        {
            assignment.Ex04_AccessModifierDemo();

            var sb = new StringBuilder();
            sb.AppendLine("my name is Buddy");
            sb.AppendLine("Buddy weak!");
            sb.AppendLine("Buddy got 50 food");
            sb.AppendLine("Buddy happy!");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // ===================== ข้อ 5: Virtual / Override =====================

        [Test]
        public void As05_01_Animal_MakeSound_IsVirtual()
        {
            var method = Animal05Type.GetMethod("MakeSound");

            Assert.IsNotNull(method, "Animal ต้องมีเมธอด MakeSound");
            Assert.IsTrue(method.IsVirtual && !method.IsFinal,
                "MakeSound ของ Animal ต้องเป็น virtual เพื่อให้คลาสลูก override ได้");
        }

        [Test]
        public void As05_02_Dog_MakeSound_IsOverrideNotNew()
        {
            var method = Dog05Type.GetMethod("MakeSound");

            Assert.IsNotNull(method, "Dog ต้องมีเมธอด MakeSound");
            Assert.AreEqual(Dog05Type, method.DeclaringType,
                "Dog ต้องประกาศเมธอด MakeSound ของตัวเอง");
            Assert.AreEqual(Animal05Type, method.GetBaseDefinition().DeclaringType,
                "MakeSound ของ Dog ต้องใช้คำว่า override (ไม่ใช่ new) เพื่อเขียนทับของ Animal");
        }

        [Test]
        public void As05_03_MakeSound_WorksPolymorphically()
        {
            var dog = System.Activator.CreateInstance(Dog05Type);
            Dog05Type.GetMethod("MakeSound")?.Invoke(dog, null);

            TestUtils.AssertMultilineEqual("Woof!", SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As05_04_Cat_MakeSound_IsOverride()
        {
            Assert.IsNotNull(Cat05Type, "ต้องมีคลาส Cat ใน Ex05");
            Assert.AreEqual(Animal05Type, Cat05Type.BaseType, "Cat ต้องสืบทอดจาก Animal");

            var method = Cat05Type.GetMethod("MakeSound");
            Assert.IsNotNull(method, "Cat ต้องมีเมธอด MakeSound");
            Assert.AreEqual(Cat05Type, method.DeclaringType, "Cat ต้อง override MakeSound ของตัวเอง");

            var cat = System.Activator.CreateInstance(Cat05Type);
            Cat05Type.GetMethod("MakeSound")?.Invoke(cat, null);
            TestUtils.AssertMultilineEqual("Meow!", SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As05_05_Ex05_VirtualOverrideDemo_Output()
        {
            assignment.Ex05_VirtualOverrideDemo();

            var sb = new StringBuilder();
            sb.AppendLine("Woof!");
            sb.AppendLine("Meow!");
            sb.AppendLine("Generic animal sound");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }
    }

    // ===================== ข้อ 6-8: ระบบเกม (Enemy / Potion / Sword) =====================

    public class GameTestBase : TestBase
    {
        protected Week06.Game.MapGenerator map;

        protected void BuildMap()
        {
            map = Week06.Game.MapGenerator.CreateDemoMap();
            SimpleDebugConsole.Clear();
        }

        [TearDown]
        public void ClearMapAfterTest()
        {
            if (map != null) map.ClearMap();
            map = null;
        }
    }

    public class Homework : GameTestBase
    {
        // ===================== ข้อ 6: ศัตรู (Enemy) =====================

        [Test]
        public void As06_01_Enemy_InheritsCharacter()
        {
            Assert.AreEqual(typeof(Week06.Game.Character), typeof(Week06.Game.Enemy).BaseType,
                "class Enemy ต้องสืบทอดจาก class Character");
        }

        [Test]
        public void As06_02_Enemy_Hit_IsOverride()
        {
            var method = typeof(Week06.Game.Enemy).GetMethod("Hit");
            Assert.IsNotNull(method, "Enemy ต้องมีเมธอด Hit()");
            Assert.AreEqual(typeof(Week06.Game.Enemy), method.DeclaringType, "Enemy ต้อง override Hit() ของตัวเอง");
            Assert.AreEqual(typeof(Week06.Game.Identity), method.GetBaseDefinition().DeclaringType,
                "Hit() ของ Enemy ต้องใช้ override (สืบมาจาก Identity)");
        }

        [Test]
        public void As06_03_Enemy_Hit_AttacksPlayer()
        {
            BuildMap();
            var enemy = map.enemies[3, 3];
            int before = map.player.energy;

            enemy.Hit();

            Assert.AreEqual(before - enemy.attackPoint, map.player.energy,
                "Enemy.Hit() ต้องตีผู้เล่นด้วย attackPoint ของตัวเอง");
        }

        [Test]
        public void As06_04_Enemy_Hit_DoesNothingWhenDead()
        {
            BuildMap();
            var enemy = map.enemies[3, 3];
            enemy.energy = 0;
            int before = map.player.energy;

            enemy.Hit();

            Assert.AreEqual(before, map.player.energy,
                "ถ้า energy <= 0 แล้ว Enemy.Hit() ต้อง return ทันที ไม่ตีผู้เล่น");
        }

        [Test]
        public void As06_05_Move_IntoEnemy_PlayerAttacksFirstThenEnemyStrikesBack()
        {
            BuildMap();
            var player = map.player;
            var enemy = map.enemies[3, 3];

            player.positionX = 3;
            player.positionY = 2;
            player.energy = 100;
            player.attackPoint = 20;

            player.Move(Vector2.up);

            Assert.AreEqual(40, enemy.energy, "ผู้เล่นต้องตีศัตรูก่อน 60 - 20 = 40");
            Assert.AreEqual(95, player.energy, "ศัตรูยังไม่ตายต้องตีสวนกลับ 100 - 5 = 95");
            Assert.AreEqual(3, player.positionX, "ศัตรูยังไม่ตาย ผู้เล่นต้องยังไม่ขยับ");
            Assert.AreEqual(2, player.positionY, "ศัตรูยังไม่ตาย ผู้เล่นต้องยังไม่ขยับ");
        }

        [Test]
        public void As06_06_Move_IntoDeadEnemy_PlayerMovesIn()
        {
            BuildMap();
            var player = map.player;
            var enemy = map.enemies[3, 3];

            player.positionX = 3;
            player.positionY = 2;
            player.energy = 100;
            player.attackPoint = 20;
            enemy.energy = 20;

            player.Move(Vector2.up);

            Assert.AreEqual(0, enemy.energy, "ศัตรูต้องเหลือ 0");
            Assert.AreEqual(100, player.energy, "ศัตรูตายแล้วต้องตีสวนไม่ได้");
            Assert.AreEqual(3, player.positionX, "ศัตรูตายแล้ว ผู้เล่นต้องเดินเข้าไปที่ช่องนั้น");
            Assert.AreEqual(3, player.positionY, "ศัตรูตายแล้ว ผู้เล่นต้องเดินเข้าไปที่ช่องนั้น");
        }

        [Test]
        public void As06_07_Ex06_BattleDemo_FullScenario()
        {
            assignment.Ex06_BattleDemo();

            var sb = new StringBuilder();
            sb.AppendLine("You got Potion1 : 20");
            sb.AppendLine("You got Sword1 : 10");
            sb.AppendLine("Player energy after picking up potion: 117");
            sb.AppendLine("Player attack point after picking up sword: 20");
            sb.AppendLine("first attack ...");
            sb.AppendLine("Player energy after attack: 112");
            sb.AppendLine("Enemy energy after attack: 40");
            sb.AppendLine("second attack ...");
            sb.AppendLine("Player energy after attack: 107");
            sb.AppendLine("Enemy energy after attack: 20");
            sb.AppendLine("thrid attack ...");
            sb.AppendLine("Player energy after attack: 107");
            sb.AppendLine("Enemy energy after attack: 0");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // ===================== ข้อ 7: ยาฟื้นพลัง (Potion) =====================

        [Test]
        public void As07_01_ItemPotion_InheritsIdentity()
        {
            Assert.AreEqual(typeof(Week06.Game.Identity), typeof(Week06.Game.ItemPotion).BaseType,
                "class ItemPotion ต้องสืบทอดจาก class Identity");
        }

        [Test]
        public void As07_02_ItemPotion_HasHealPointField()
        {
            var field = typeof(Week06.Game.ItemPotion).GetField("healPoint", AnyInstance);
            Assert.IsNotNull(field, "ItemPotion ต้องมีตัวแปร healPoint");
            Assert.AreEqual(typeof(int), field.FieldType, "healPoint ต้องเป็น int");
            Assert.IsTrue(field.IsPublic, "healPoint ต้องเป็น public");
        }

        [Test]
        public void As07_03_Potion_Hit_HealsPlayerAndLeavesMap()
        {
            BuildMap();
            var potion = map.potions[2, 2];
            int before = map.player.energy;

            potion.Hit();

            TestUtils.AssertMultilineEqual("You got Potion1 : 20", SimpleDebugConsole.GetOutput());
            Assert.AreEqual(before + 20, map.player.energy, "ต้องเพิ่ม energy ให้ผู้เล่นเท่ากับ healPoint");
            Assert.AreEqual(map.empty, map.mapData[2, 2], "ต้องเอาไอเทมออกจากแผนที่ (ตั้งช่องนั้นเป็น 0)");
        }

        [Test]
        public void As07_04_Move_IntoPotion_NoEnergyLossAndPlayerMovesIn()
        {
            BuildMap();
            var player = map.player;
            player.positionX = 1;
            player.positionY = 2;
            player.energy = 100;

            player.Move(Vector2.right);

            Assert.AreEqual(120, player.energy, "ช่องที่มีไอเทมไม่หัก energy และต้องได้เลือดจากยา");
            Assert.AreEqual(2, player.positionX, "ผู้เล่นต้องเดินเข้าไปที่ช่องยา");
            Assert.AreEqual(2, player.positionY, "ผู้เล่นต้องเดินเข้าไปที่ช่องยา");
        }

        [Test]
        public void As07_05_Ex07_PotionDemo_Output()
        {
            assignment.Ex07_PotionDemo();

            var sb = new StringBuilder();
            sb.AppendLine("You got Potion1 : 20");
            sb.AppendLine("Player energy after picking up potion: 117");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // ===================== ข้อ 8: ดาบ (Sword) =====================

        [Test]
        public void As08_01_ItemSword_InheritsIdentity()
        {
            Assert.AreEqual(typeof(Week06.Game.Identity), typeof(Week06.Game.ItemSword).BaseType,
                "class ItemSword ต้องสืบทอดจาก class Identity");
        }

        [Test]
        public void As08_02_ItemSword_HasAttackBonusField()
        {
            var field = typeof(Week06.Game.ItemSword).GetField("attackBonus", AnyInstance);
            Assert.IsNotNull(field, "ItemSword ต้องมีตัวแปร attackBonus");
            Assert.AreEqual(typeof(int), field.FieldType, "attackBonus ต้องเป็น int");
            Assert.IsTrue(field.IsPublic, "attackBonus ต้องเป็น public");
        }

        [Test]
        public void As08_03_Sword_Hit_IncreasesAttackAndLeavesMap()
        {
            BuildMap();
            var theSword = map.swords[3, 2];
            int before = map.player.attackPoint;

            theSword.Hit();

            TestUtils.AssertMultilineEqual("You got Sword1 : 10", SimpleDebugConsole.GetOutput());
            Assert.AreEqual(before + 10, map.player.attackPoint, "ต้องเพิ่ม attackPoint ให้ผู้เล่นเท่ากับ attackBonus");
            Assert.AreEqual(map.empty, map.mapData[3, 2], "ต้องเอาไอเทมออกจากแผนที่ (ตั้งช่องนั้นเป็น 0)");
        }

        [Test]
        public void As08_04_Move_IntoSword_NoEnergyLossAndPlayerMovesIn()
        {
            BuildMap();
            var player = map.player;
            player.positionX = 2;
            player.positionY = 2;
            player.energy = 100;
            player.attackPoint = 10;
            map.mapData[2, 2] = map.empty;

            player.Move(Vector2.right);

            Assert.AreEqual(100, player.energy, "ช่องที่มีไอเทมไม่หัก energy");
            Assert.AreEqual(20, player.attackPoint, "ต้องได้พลังโจมตีเพิ่มจากดาบ");
            Assert.AreEqual(3, player.positionX, "ผู้เล่นต้องเดินเข้าไปที่ช่องดาบ");
        }

        [Test]
        public void As08_05_Ex08_SwordDemo_Output()
        {
            assignment.Ex08_SwordDemo();

            var sb = new StringBuilder();
            sb.AppendLine("You got Potion1 : 20");
            sb.AppendLine("You got Sword1 : 10");
            sb.AppendLine("Player attack point after picking up sword: 20");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
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
