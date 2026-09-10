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
        protected IAssignment assignment;
        protected GameObject testGo;

        protected const BindingFlags AnyInstance =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        [SetUp]
        public void Setup()
        {
            testGo = new GameObject("Week06_TestRunner");
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

    // ===================== ข้อ 1: สร้างคลาส Car =====================

    public class Ex01_Class : TestBase
    {
        [Test]
        public void Car_HasRequiredFields()
        {
            var t = typeof(Week06.Ex01.Car);

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
        public void Car_MethodsPrintCorrectMessages()
        {
            var car = new Week06.Ex01.Car();
            car.name = "civic";
            car.color = "black";
            car.speed = 110f;

            car.Move();
            car.Turn();
            car.Honk();

            var sb = new StringBuilder();
            sb.AppendLine("Car is moving");
            sb.AppendLine("Car is turning");
            sb.AppendLine("Car is honking");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void Ex01_CarDemo_Output()
        {
            assignment.Ex01_CarDemo();

            var sb = new StringBuilder();
            sb.AppendLine("Car is moving");
            sb.AppendLine("Car is turning");
            sb.AppendLine("Car is honking");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }
    }

    // ===================== ข้อ 2: Constructor =====================

    public class Ex02_Constructor : TestBase
    {
        [Test]
        public void Dog_HasConstructorWithThreeParameters()
        {
            var ctor = typeof(Week06.Ex02.Dog).GetConstructor(
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
        public void Dog_ConstructorAssignsAllFields(string name, string breed, int age)
        {
            var dog = new Week06.Ex02.Dog(name, breed, age);

            Assert.AreEqual(name, dog.name, "Constructor ต้องกำหนดค่า name");
            Assert.AreEqual(breed, dog.breed, "Constructor ต้องกำหนดค่า breed");
            Assert.AreEqual(age, dog.age, "Constructor ต้องกำหนดค่า age");
        }

        [TestCase("Buddy")]
        [TestCase("Max")]
        public void Dog_MethodsUseName(string name)
        {
            var dog = new Week06.Ex02.Dog(name, "Mixed", 2);

            dog.Bark();
            dog.WagTail();
            dog.StopBarking();

            var sb = new StringBuilder();
            sb.AppendLine($"{name} is barking");
            sb.AppendLine($"{name} is wagging tail");
            sb.AppendLine($"{name} stopped barking");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void Ex02_DogDemo_UsesBuddy()
        {
            assignment.Ex02_DogDemo();

            var sb = new StringBuilder();
            sb.AppendLine("Buddy is barking");
            sb.AppendLine("Buddy is wagging tail");
            sb.AppendLine("Buddy stopped barking");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }
    }

    // ===================== ข้อ 3: Inheritance =====================

    public class Ex03_Inheritance : TestBase
    {
        [Test]
        public void DogAndBird_InheritFromAnimal()
        {
            Assert.AreEqual(typeof(Week06.Ex03.Animal), typeof(Week06.Ex03.Dog).BaseType,
                "คลาส Dog ต้องสืบทอดจาก Animal (เขียน : Animal ต่อท้ายชื่อคลาส)");
            Assert.AreEqual(typeof(Week06.Ex03.Animal), typeof(Week06.Ex03.Bird).BaseType,
                "คลาส Bird ต้องสืบทอดจาก Animal");
        }

        [Test]
        public void Dog_CanUseInheritedNameAndMakeSound()
        {
            var dog = new Week06.Ex03.Dog();
            dog.name = "Rex";

            dog.MakeSound();
            dog.Walk();

            var sb = new StringBuilder();
            sb.AppendLine("Animal Rex is making sound");
            sb.AppendLine("Dog Rex is walking");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void Bird_CanUseInheritedNameAndMakeSound()
        {
            var bird = new Week06.Ex03.Bird();
            bird.name = "Sky";

            bird.MakeSound();
            bird.Fly();

            var sb = new StringBuilder();
            sb.AppendLine("Animal Sky is making sound");
            sb.AppendLine("Bird Sky is flying");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void Ex03_InheritanceDemo_Output()
        {
            assignment.Ex03_InheritanceDemo();

            var sb = new StringBuilder();
            sb.AppendLine("Animal Buddy is making sound");
            sb.AppendLine("Dog Buddy is walking");
            sb.AppendLine("Animal Twitty is making sound");
            sb.AppendLine("Bird Twitty is flying");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }
    }

    // ===================== ข้อ 4: Access Modifiers =====================

    public class Ex04_AccessModifier : TestBase
    {
        [Test]
        public void Animal_FieldsHaveCorrectAccessLevels()
        {
            var t = typeof(Week06.Ex04.Animal);

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
        public void Dog_InheritsAnimalAndHasNameConstructor()
        {
            Assert.AreEqual(typeof(Week06.Ex04.Animal), typeof(Week06.Ex04.Dog).BaseType,
                "คลาส Dog ต้องสืบทอดจาก Animal");

            var ctor = typeof(Week06.Ex04.Dog).GetConstructor(new[] { typeof(string) });
            Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ string name");
        }

        [Test]
        public void Dog_ConstructorSetsNameAndSpecie()
        {
            var dog = new Week06.Ex04.Dog("Buddy");

            Assert.AreEqual("Buddy", dog.name, "Constructor ต้องกำหนดค่า name");

            var specie = typeof(Week06.Ex04.Animal).GetField("specie", AnyInstance);
            Assert.AreEqual("Dog", specie.GetValue(dog), "Constructor ต้องกำหนด specie = \"Dog\"");
        }

        [TestCase(0, "weak!")]
        [TestCase(40, "weak!")]
        [TestCase(41, "happy!")]
        [TestCase(90, "happy!")]
        public void Feed_ThenMakeSound_ChecksHealthThreshold(int food, string expectedMood)
        {
            var dog = new Week06.Ex04.Dog("Buddy");

            dog.Feed(food);
            dog.MakeSound();

            var sb = new StringBuilder();
            sb.AppendLine($"Buddy got {food} food");
            sb.AppendLine($"Buddy {expectedMood}");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void Ex04_AccessModifierDemo_Output()
        {
            assignment.Ex04_AccessModifierDemo();

            var sb = new StringBuilder();
            sb.AppendLine("my name is Buddy");
            sb.AppendLine("Buddy weak!");
            sb.AppendLine("Buddy got 50 food");
            sb.AppendLine("Buddy happy!");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }
    }

    // ===================== ข้อ 5: Virtual / Override =====================

    public class Ex05_VirtualOverride : TestBase
    {
        [Test]
        public void Animal_MakeSound_IsVirtual()
        {
            var method = typeof(Week06.Ex05.Animal).GetMethod("MakeSound");

            Assert.IsNotNull(method, "Animal ต้องมีเมธอด MakeSound");
            Assert.IsTrue(method.IsVirtual && !method.IsFinal,
                "MakeSound ของ Animal ต้องเป็น virtual เพื่อให้คลาสลูก override ได้");
        }

        [Test]
        public void Dog_MakeSound_IsOverrideNotNew()
        {
            var method = typeof(Week06.Ex05.Dog).GetMethod("MakeSound");

            Assert.IsNotNull(method, "Dog ต้องมีเมธอด MakeSound");
            Assert.AreEqual(typeof(Week06.Ex05.Dog), method.DeclaringType,
                "Dog ต้องประกาศเมธอด MakeSound ของตัวเอง");
            Assert.AreEqual(typeof(Week06.Ex05.Animal), method.GetBaseDefinition().DeclaringType,
                "MakeSound ของ Dog ต้องใช้คำว่า override (ไม่ใช่ new) เพื่อเขียนทับของ Animal");
        }

        [Test]
        public void MakeSound_WorksPolymorphically()
        {
            Week06.Ex05.Animal asAnimal = new Week06.Ex05.Dog();
            asAnimal.MakeSound();

            TestUtils.AssertMultilineEqual("Woof!", SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void Ex05_VirtualOverrideDemo_Output()
        {
            assignment.Ex05_VirtualOverrideDemo();

            var sb = new StringBuilder();
            sb.AppendLine("Woof!");
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

    public class Ex06_Enemy : GameTestBase
    {
        [Test]
        public void Enemy_InheritsCharacter()
        {
            Assert.AreEqual(typeof(Week06.Game.Character), typeof(Week06.Game.Enemy).BaseType,
                "class Enemy ต้องสืบทอดจาก class Character");
        }

        [Test]
        public void Enemy_Hit_IsOverride()
        {
            var method = typeof(Week06.Game.Enemy).GetMethod("Hit");
            Assert.IsNotNull(method, "Enemy ต้องมีเมธอด Hit()");
            Assert.AreEqual(typeof(Week06.Game.Enemy), method.DeclaringType, "Enemy ต้อง override Hit() ของตัวเอง");
            Assert.AreEqual(typeof(Week06.Game.Identity), method.GetBaseDefinition().DeclaringType,
                "Hit() ของ Enemy ต้องใช้ override (สืบมาจาก Identity)");
        }

        [Test]
        public void Enemy_Hit_AttacksPlayer()
        {
            BuildMap();
            var enemy = map.enemies[3, 3];
            int before = map.player.energy;

            enemy.Hit();

            Assert.AreEqual(before - enemy.attackPoint, map.player.energy,
                "Enemy.Hit() ต้องตีผู้เล่นด้วย attackPoint ของตัวเอง");
        }

        [Test]
        public void Enemy_Hit_DoesNothingWhenDead()
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
        public void Move_IntoEnemy_PlayerAttacksFirstThenEnemyStrikesBack()
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
        public void Move_IntoDeadEnemy_PlayerMovesIn()
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
        public void Ex06_BattleDemo_FullScenario()
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
    }

    public class Ex07_Potion : GameTestBase
    {
        [Test]
        public void ItemPotion_InheritsIdentity()
        {
            Assert.AreEqual(typeof(Week06.Game.Identity), typeof(Week06.Game.ItemPotion).BaseType,
                "class ItemPotion ต้องสืบทอดจาก class Identity");
        }

        [Test]
        public void ItemPotion_HasHealPointField()
        {
            var field = typeof(Week06.Game.ItemPotion).GetField("healPoint", AnyInstance);
            Assert.IsNotNull(field, "ItemPotion ต้องมีตัวแปร healPoint");
            Assert.AreEqual(typeof(int), field.FieldType, "healPoint ต้องเป็น int");
            Assert.IsTrue(field.IsPublic, "healPoint ต้องเป็น public");
        }

        [Test]
        public void Potion_Hit_HealsPlayerAndLeavesMap()
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
        public void Move_IntoPotion_NoEnergyLossAndPlayerMovesIn()
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
        public void Ex07_PotionDemo_Output()
        {
            assignment.Ex07_PotionDemo();

            var sb = new StringBuilder();
            sb.AppendLine("You got Potion1 : 20");
            sb.AppendLine("Player energy after picking up potion: 117");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }
    }

    public class Ex08_Sword : GameTestBase
    {
        [Test]
        public void ItemSword_InheritsIdentity()
        {
            Assert.AreEqual(typeof(Week06.Game.Identity), typeof(Week06.Game.ItemSword).BaseType,
                "class ItemSword ต้องสืบทอดจาก class Identity");
        }

        [Test]
        public void ItemSword_HasAttackBonusField()
        {
            var field = typeof(Week06.Game.ItemSword).GetField("attackBonus", AnyInstance);
            Assert.IsNotNull(field, "ItemSword ต้องมีตัวแปร attackBonus");
            Assert.AreEqual(typeof(int), field.FieldType, "attackBonus ต้องเป็น int");
            Assert.IsTrue(field.IsPublic, "attackBonus ต้องเป็น public");
        }

        [Test]
        public void Sword_Hit_IncreasesAttackAndLeavesMap()
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
        public void Move_IntoSword_NoEnergyLossAndPlayerMovesIn()
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
        public void Ex08_SwordDemo_Output()
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
