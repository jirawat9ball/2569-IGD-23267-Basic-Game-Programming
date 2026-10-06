using System.Reflection;
using System.Text;

using NUnit.Framework;
using UnityEngine;

using Week07;
using SimpleDebugConsole = Workspace.Core.SimpleDebugConsole;

namespace Week07_OOP
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

        protected static System.Type Animal01Type => isTeacherMode ? typeof(Week07.Teacher.Ex01.Animal) : typeof(Week07.Ex01.Animal);
        protected static System.Type Dog01Type => isTeacherMode ? typeof(Week07.Teacher.Ex01.Dog) : typeof(Week07.Ex01.Dog);
        protected static System.Type Bird01Type => isTeacherMode ? typeof(Week07.Teacher.Ex01.Bird) : typeof(Week07.Ex01.Bird);

        protected static System.Type Animal02Type => isTeacherMode ? typeof(Week07.Teacher.Ex02.Animal) : typeof(Week07.Ex02.Animal);
        protected static System.Type Dog02Type => isTeacherMode ? typeof(Week07.Teacher.Ex02.Dog) : typeof(Week07.Ex02.Dog);

        protected static System.Type Animal03Type => isTeacherMode ? typeof(Week07.Teacher.Ex03.Animal) : typeof(Week07.Ex03.Animal);
        protected static System.Type Dog03Type => isTeacherMode ? typeof(Week07.Teacher.Ex03.Dog) : typeof(Week07.Ex03.Dog);
        protected static System.Type Cat03Type => isTeacherMode ? typeof(Week07.Teacher.Ex03.Cat) : typeof(Week07.Ex03.Cat);

        [SetUp]
        public void Setup()
        {
            testGo = new GameObject("Week07_TestRunner");
            if (isTeacherMode)
                assignment = testGo.AddComponent<Week07.Assignment_Teacher_Week07>();
            else
                assignment = testGo.AddComponent<Assignment_Student_Week07>();
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
        // ===================== ข้อ 1: Inheritance =====================

        [Test]
        public void As01_01_DogAndBird_InheritFromAnimal()
        {
            Assert.AreEqual(Animal01Type, Dog01Type.BaseType,
                "คลาส Dog ต้องสืบทอดจาก Animal (เขียน : Animal ต่อท้ายชื่อคลาส)");
            Assert.AreEqual(Animal01Type, Bird01Type.BaseType,
                "คลาส Bird ต้องสืบทอดจาก Animal");
        }

        [Test]
        public void As01_02_Dog_CanUseInheritedNameAndMakeSound()
        {
            var dog = System.Activator.CreateInstance(Dog01Type);
            Dog01Type.GetField("name", AnyInstance)?.SetValue(dog, "Rex");

            var makeMethod = Dog01Type.GetMethod("MakeSound", AnyInstance);
            var walkMethod = Dog01Type.GetMethod("Walk", AnyInstance);

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
        public void As01_03_Bird_CanUseInheritedNameAndMakeSound()
        {
            var bird = System.Activator.CreateInstance(Bird01Type);
            Bird01Type.GetField("name", AnyInstance)?.SetValue(bird, "Sky");

            var makeMethod = Bird01Type.GetMethod("MakeSound", AnyInstance);
            var flyMethod = Bird01Type.GetMethod("Fly", AnyInstance);

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
        public void As01_04_Ex01_InheritanceDemo_Output()
        {
            assignment.Ex01_InheritanceDemo();

            var sb = new StringBuilder();
            sb.AppendLine("Animal Buddy is making sound");
            sb.AppendLine("Dog Buddy is walking");
            sb.AppendLine("Animal Twitty is making sound");
            sb.AppendLine("Bird Twitty is flying");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // ===================== ข้อ 2: Access Modifiers =====================

        [Test]
        public void As02_01_Animal_FieldsHaveCorrectAccessLevels()
        {
            var t = Animal02Type;

            var name = t.GetField("name", AnyInstance);
            var specie = t.GetField("specie", AnyInstance);
            var health = t.GetField("health", AnyInstance);

            Assert.IsNotNull(name, "Animal ต้องมีฟิลด์ name");
            Assert.IsNotNull(specie, "Animal ต้องมีฟิลด์ specie");
            Assert.IsNotNull(health, "Animal ต้องมีฟิลด์ health");

            Assert.IsTrue(name.IsPublic, "ฟิลด์ name ต้องเป็น public");
            Assert.IsTrue(specie.IsFamily, "ฟิลด์ specie ต้องเป็น protected");
            Assert.IsTrue(health.IsPrivate, "ฟิลด์ health ต้องเป็น private");
        }

        [Test]
        public void As02_02_Dog_InheritsAnimalAndHasNameConstructor()
        {
            Assert.AreEqual(Animal02Type, Dog02Type.BaseType, "คลาส Dog ต้องสืบทอดจาก Animal");

            var ctor = Dog02Type.GetConstructor(new[] { typeof(string) });
            Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ string name");
        }

        [Test]
        public void As02_03_Dog_ConstructorSetsNameAndSpecie()
        {
            var ctor = Dog02Type.GetConstructor(new[] { typeof(string) });
            var dog = ctor.Invoke(new object[] { "Buddy" });

            var nameVal = Dog02Type.GetField("name", AnyInstance)?.GetValue(dog) as string;
            var specieVal = Dog02Type.GetField("specie", AnyInstance)?.GetValue(dog) as string;

            Assert.AreEqual("Buddy", nameVal, "Constructor ต้องกำหนดค่า name");
            Assert.AreEqual("Dog", specieVal, "Constructor ต้องกำหนดค่า specie = \"Dog\"");
        }

        [TestCase(50, "Buddy happy!")]
        [TestCase(40, "Buddy weak!")]
        [TestCase(0, "Buddy weak!")]
        public void As02_04_Feed_ThenMakeSound_ChecksHealthThreshold(int food, string expectedMood)
        {
            var dog = System.Activator.CreateInstance(Dog02Type, new object[] { "Buddy" });

            var feedMethod = Dog02Type.GetMethod("Feed", AnyInstance);
            var soundMethod = Dog02Type.GetMethod("MakeSound", AnyInstance);

            Assert.IsNotNull(feedMethod, "คลาส Dog ต้องมีเมธอด Feed(int)");
            Assert.IsNotNull(soundMethod, "คลาส Dog ต้องมีเมธอด MakeSound()");

            feedMethod.Invoke(dog, new object[] { food });
            soundMethod.Invoke(dog, null);

            var sb = new StringBuilder();
            sb.AppendLine($"Buddy got {food} food");
            sb.AppendLine(expectedMood);

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As02_05_Ex02_AccessModifierDemo_Output()
        {
            assignment.Ex02_AccessModifierDemo();

            var sb = new StringBuilder();
            sb.AppendLine("my name is Buddy");
            sb.AppendLine("Buddy weak!");
            sb.AppendLine("Buddy got 50 food");
            sb.AppendLine("Buddy happy!");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // ===================== ข้อ 3: Virtual and Override =====================

        [Test]
        public void As03_01_Animal_MakeSound_IsVirtual()
        {
            var method = Animal03Type.GetMethod("MakeSound", AnyInstance);
            Assert.IsNotNull(method, "Animal ต้องมีเมธอด MakeSound()");
            Assert.IsTrue(method.IsVirtual && !method.IsFinal, "Animal.MakeSound() ต้องเป็น virtual");
        }

        [Test]
        public void As03_02_Dog_MakeSound_IsOverrideNotNew()
        {
            var method = Dog03Type.GetMethod("MakeSound", AnyInstance);
            Assert.IsNotNull(method, "Dog ต้องมีเมธอด MakeSound()");
            Assert.AreEqual(Dog03Type, method.DeclaringType, "Dog ต้อง override MakeSound() ของตัวเอง");
            Assert.AreEqual(Animal03Type, method.GetBaseDefinition().DeclaringType,
                "MakeSound() ของ Dog ต้องสืบมาจาก Animal (ต้องใช้คีย์เวิร์ด override)");
        }

        [Test]
        public void As03_03_MakeSound_WorksPolymorphically()
        {
            object dog = System.Activator.CreateInstance(Dog03Type);
            Dog03Type.GetMethod("MakeSound", AnyInstance)?.Invoke(dog, null);
            TestUtils.AssertMultilineEqual("Woof!", SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As03_04_Cat_MakeSound_IsOverride()
        {
            var method = Cat03Type.GetMethod("MakeSound", AnyInstance);
            Assert.IsNotNull(method, "Cat ต้องมีเมธอด MakeSound()");
            Assert.AreEqual(Cat03Type, method.DeclaringType, "Cat ต้อง override MakeSound() ของตัวเอง");
            Assert.AreEqual(Animal03Type, method.GetBaseDefinition().DeclaringType,
                "MakeSound() ของ Cat ต้องสืบมาจาก Animal (ต้องใช้คีย์เวิร์ด override)");

            SimpleDebugConsole.Clear();
            object cat = System.Activator.CreateInstance(Cat03Type);
            method.Invoke(cat, null);
            TestUtils.AssertMultilineEqual("Meow!", SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As03_05_Ex03_VirtualOverrideDemo_Output()
        {
            assignment.Ex03_VirtualOverrideDemo();

            var sb = new StringBuilder();
            sb.AppendLine("Woof!");
            sb.AppendLine("Meow!");
            sb.AppendLine("Generic animal sound");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }
    }

    // ===================== ข้อ 4-6: ระบบเกม (Enemy / Potion / Sword) =====================

    public class GameTestBase : TestBase
    {
        protected Week07.Game.MapGenerator map;

        protected void BuildMap()
        {
            map = Week07.Game.MapGenerator.CreateDemoMap();
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
        // ===================== ข้อ 4: ศัตรู (Enemy) =====================

        [Test]
        public void As04_01_Enemy_InheritsCharacter()
        {
            Assert.AreEqual(typeof(Week07.Game.Character), typeof(Week07.Game.Enemy).BaseType,
                "class Enemy ต้องสืบทอดจาก class Character");
        }

        [Test]
        public void As04_02_Enemy_Hit_IsOverride()
        {
            var method = typeof(Week07.Game.Enemy).GetMethod("Hit");
            Assert.IsNotNull(method, "Enemy ต้องมีเมธอด Hit()");
            Assert.AreEqual(typeof(Week07.Game.Enemy), method.DeclaringType, "Enemy ต้อง override Hit() ของตัวเอง");
            Assert.AreEqual(typeof(Week07.Game.Identity), method.GetBaseDefinition().DeclaringType,
                "Hit() ของ Enemy ต้องใช้ override (สืบมาจาก Identity)");
        }

        [Test]
        public void As04_03_Enemy_Hit_AttacksPlayer()
        {
            BuildMap();
            var enemy = map.enemies[3, 3];
            int before = map.player.energy;

            enemy.Hit();

            Assert.AreEqual(before - enemy.attackPoint, map.player.energy,
                "Enemy.Hit() ต้องตีผู้เล่นด้วย attackPoint ของตัวเอง");
        }

        [Test]
        public void As04_04_Enemy_Hit_DoesNothingWhenDead()
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
        public void As04_05_Move_IntoEnemy_PlayerAttacksFirstThenEnemyStrikesBack()
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
        public void As04_06_Move_IntoDeadEnemy_PlayerMovesIn()
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
        public void As04_07_Ex04_BattleDemo_FullScenario()
        {
            assignment.Ex04_BattleDemo();

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

        // ===================== ข้อ 5: ยาฟื้นพลัง (Potion) =====================

        [Test]
        public void As05_01_ItemPotion_InheritsIdentity()
        {
            Assert.AreEqual(typeof(Week07.Game.Identity), typeof(Week07.Game.ItemPotion).BaseType,
                "class ItemPotion ต้องสืบทอดจาก class Identity");
        }

        [Test]
        public void As05_02_ItemPotion_HasHealPointField()
        {
            var field = typeof(Week07.Game.ItemPotion).GetField("healPoint", AnyInstance);
            Assert.IsNotNull(field, "ItemPotion ต้องมีตัวแปร healPoint");
            Assert.AreEqual(typeof(int), field.FieldType, "healPoint ต้องเป็น int");
            Assert.IsTrue(field.IsPublic, "healPoint ต้องเป็น public");
        }

        [Test]
        public void As05_03_Potion_Hit_HealsPlayerAndLeavesMap()
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
        public void As05_04_Move_IntoPotion_NoEnergyLossAndPlayerMovesIn()
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
        public void As05_05_Ex05_PotionDemo_Output()
        {
            assignment.Ex05_PotionDemo();

            var sb = new StringBuilder();
            sb.AppendLine("You got Potion1 : 20");
            sb.AppendLine("Player energy after picking up potion: 117");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // ===================== ข้อ 6: ดาบ (Sword) =====================

        [Test]
        public void As06_01_ItemSword_InheritsIdentity()
        {
            Assert.AreEqual(typeof(Week07.Game.Identity), typeof(Week07.Game.ItemSword).BaseType,
                "class ItemSword ต้องสืบทอดจาก class Identity");
        }

        [Test]
        public void As06_02_ItemSword_HasAttackBonusField()
        {
            var field = typeof(Week07.Game.ItemSword).GetField("attackBonus", AnyInstance);
            Assert.IsNotNull(field, "ItemSword ต้องมีตัวแปร attackBonus");
            Assert.AreEqual(typeof(int), field.FieldType, "attackBonus ต้องเป็น int");
            Assert.IsTrue(field.IsPublic, "attackBonus ต้องเป็น public");
        }

        [Test]
        public void As06_03_Sword_Hit_IncreasesAttackAndLeavesMap()
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
        public void As06_04_Move_IntoSword_NoEnergyLossAndPlayerMovesIn()
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
        public void As06_05_Ex06_SwordDemo_Output()
        {
            assignment.Ex06_SwordDemo();

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
