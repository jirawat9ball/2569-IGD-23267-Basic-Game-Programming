using System.Text;
using NUnit.Framework;
using UnityEngine;
using Workspace.Core;

namespace Week07.Tests
{
    public abstract class TestBase
    {
        protected static readonly System.Reflection.BindingFlags AnyInstance =
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.NonPublic;

        protected static bool isTeacherMode =>
            System.Type.GetType("Week07.Assignment_Teacher_Week07, Workspace") != null
            && System.Environment.GetEnvironmentVariable("UNITY_TEACHER_TEST") == "1";

        protected GameObject testGo;
        protected IAssignment assignment;

        // ข้อ 1: Inheritance
        protected static System.Type Animal01Type => isTeacherMode ? typeof(Week07.Teacher.Ex01.Animal) : System.Type.GetType("Week07.Ex01.Animal, Workspace");
        protected static System.Type Dog01Type => isTeacherMode ? typeof(Week07.Teacher.Ex01.Dog) : System.Type.GetType("Week07.Ex01.Dog, Workspace");
        protected static System.Type Bird01Type => isTeacherMode ? typeof(Week07.Teacher.Ex01.Bird) : System.Type.GetType("Week07.Ex01.Bird, Workspace");

        // ข้อ 2: Access Modifier
        protected static System.Type Animal02Type => isTeacherMode ? typeof(Week07.Teacher.Ex02.Animal) : System.Type.GetType("Week07.Ex02.Animal, Workspace");
        protected static System.Type Dog02Type => isTeacherMode ? typeof(Week07.Teacher.Ex02.Dog) : System.Type.GetType("Week07.Ex02.Dog, Workspace");

        // ข้อ 3: Virtual and Override
        protected static System.Type Animal03Type => isTeacherMode ? typeof(Week07.Teacher.Ex03.Animal) : System.Type.GetType("Week07.Ex03.Animal, Workspace");
        protected static System.Type Dog03Type => isTeacherMode ? typeof(Week07.Teacher.Ex03.Dog) : System.Type.GetType("Week07.Ex03.Dog, Workspace");
        protected static System.Type Cat03Type => isTeacherMode ? typeof(Week07.Teacher.Ex03.Cat) : System.Type.GetType("Week07.Ex03.Cat, Workspace");

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

        [TestCase("01_DogAndBird_InheritFromAnimal")]
        [TestCase("02_Dog_CanUseInheritedNameAndMakeSound")]
        [TestCase("03_Bird_CanUseInheritedNameAndMakeSound")]
        [TestCase("04_Ex01_InheritanceDemo_Output")]
        public void As01_Inheritance(string subTask)
        {
            switch (subTask)
            {
                case "01_DogAndBird_InheritFromAnimal":
                    Assert.AreEqual(Animal01Type, Dog01Type.BaseType,
                        "คลาส Dog ต้องสืบทอดจาก Animal (เขียน : Animal ต่อท้ายชื่อคลาส)");
                    Assert.AreEqual(Animal01Type, Bird01Type.BaseType,
                        "คลาส Bird ต้องสืบทอดจาก Animal");
                    break;

                case "02_Dog_CanUseInheritedNameAndMakeSound":
                    var dog = System.Activator.CreateInstance(Dog01Type);
                    Dog01Type.GetField("name", AnyInstance)?.SetValue(dog, "Rex");

                    var makeMethod = Dog01Type.GetMethod("MakeSound", AnyInstance);
                    var walkMethod = Dog01Type.GetMethod("Walk", AnyInstance);

                    Assert.IsNotNull(makeMethod, "คลาส Dog ต้องเรียกเมธอด MakeSound() ได้");
                    Assert.IsNotNull(walkMethod, "คลาส Dog ต้องมีเมธอด Walk()");

                    makeMethod.Invoke(dog, null);
                    walkMethod.Invoke(dog, null);

                    var sbDog = new StringBuilder();
                    sbDog.AppendLine("Animal Rex is making sound");
                    sbDog.AppendLine("Dog Rex is walking");

                    TestUtils.AssertMultilineEqual(sbDog.ToString(), SimpleDebugConsole.GetOutput());
                    break;

                case "03_Bird_CanUseInheritedNameAndMakeSound":
                    var bird = System.Activator.CreateInstance(Bird01Type);
                    Bird01Type.GetField("name", AnyInstance)?.SetValue(bird, "Sky");

                    var makeBirdMethod = Bird01Type.GetMethod("MakeSound", AnyInstance);
                    var flyMethod = Bird01Type.GetMethod("Fly", AnyInstance);

                    Assert.IsNotNull(makeBirdMethod, "คลาส Bird ต้องเรียกเมธอด MakeSound() ได้");
                    Assert.IsNotNull(flyMethod, "คลาส Bird ต้องมีเมธอด Fly()");

                    makeBirdMethod.Invoke(bird, null);
                    flyMethod.Invoke(bird, null);

                    var sbBird = new StringBuilder();
                    sbBird.AppendLine("Animal Sky is making sound");
                    sbBird.AppendLine("Bird Sky is flying");

                    TestUtils.AssertMultilineEqual(sbBird.ToString(), SimpleDebugConsole.GetOutput());
                    break;

                case "04_Ex01_InheritanceDemo_Output":
                    assignment.Ex01_InheritanceDemo();

                    var sb = new StringBuilder();
                    sb.AppendLine("Animal Buddy is making sound");
                    sb.AppendLine("Dog Buddy is walking");
                    sb.AppendLine("Animal Twitty is making sound");
                    sb.AppendLine("Bird Twitty is flying");

                    TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
                    break;
            }
        }

        // ===================== ข้อ 2: Access Modifiers =====================

        [TestCase("01_Animal_FieldsHaveCorrectAccessLevels")]
        [TestCase("02_Dog_InheritsAnimalAndHasNameConstructor")]
        [TestCase("03_Dog_ConstructorSetsNameAndSpecie")]
        [TestCase("04_Feed_ThenMakeSound_ChecksHealthThreshold")]
        [TestCase("05_Ex02_AccessModifierDemo_Output")]
        public void As02_AccessModifier(string subTask)
        {
            switch (subTask)
            {
                case "01_Animal_FieldsHaveCorrectAccessLevels":
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
                    break;

                case "02_Dog_InheritsAnimalAndHasNameConstructor":
                    Assert.AreEqual(Animal02Type, Dog02Type.BaseType, "คลาส Dog ต้องสืบทอดจาก Animal");

                    var ctor = Dog02Type.GetConstructor(new[] { typeof(string) });
                    Assert.IsNotNull(ctor, "คลาส Dog ต้องมี Constructor ที่รับ string name");
                    break;

                case "03_Dog_ConstructorSetsNameAndSpecie":
                    var ctor3 = Dog02Type.GetConstructor(new[] { typeof(string) });
                    var dog = ctor3.Invoke(new object[] { "Buddy" });

                    var nameVal = Dog02Type.GetField("name", AnyInstance)?.GetValue(dog) as string;
                    var specieVal = Dog02Type.GetField("specie", AnyInstance)?.GetValue(dog) as string;

                    Assert.AreEqual("Buddy", nameVal, "Constructor ต้องกำหนดค่า name");
                    Assert.AreEqual("Dog", specieVal, "Constructor ต้องกำหนดค่า specie = \"Dog\"");
                    break;

                case "04_Feed_ThenMakeSound_ChecksHealthThreshold":
                    TestFeedMood(50, "Buddy happy!");
                    TestFeedMood(40, "Buddy weak!");
                    TestFeedMood(0, "Buddy weak!");
                    break;

                case "05_Ex02_AccessModifierDemo_Output":
                    assignment.Ex02_AccessModifierDemo();

                    var sb = new StringBuilder();
                    sb.AppendLine("my name is Buddy");
                    sb.AppendLine("Buddy weak!");
                    sb.AppendLine("Buddy got 50 food");
                    sb.AppendLine("Buddy happy!");

                    TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
                    break;
            }
        }

        private void TestFeedMood(int food, string expectedMood)
        {
            SimpleDebugConsole.Clear();
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

        // ===================== ข้อ 3: Virtual and Override =====================

        [TestCase("01_Animal_MakeSound_IsVirtual")]
        [TestCase("02_Dog_MakeSound_IsOverrideNotNew")]
        [TestCase("03_MakeSound_WorksPolymorphically")]
        [TestCase("04_Cat_MakeSound_IsOverride")]
        [TestCase("05_Ex03_VirtualOverrideDemo_Output")]
        public void As03_VirtualOverride(string subTask)
        {
            switch (subTask)
            {
                case "01_Animal_MakeSound_IsVirtual":
                    var method = Animal03Type.GetMethod("MakeSound", AnyInstance);
                    Assert.IsNotNull(method, "Animal ต้องมีเมธอด MakeSound()");
                    Assert.IsTrue(method.IsVirtual && !method.IsFinal, "Animal.MakeSound() ต้องเป็น virtual");
                    break;

                case "02_Dog_MakeSound_IsOverrideNotNew":
                    var dogMethod = Dog03Type.GetMethod("MakeSound", AnyInstance);
                    Assert.IsNotNull(dogMethod, "Dog ต้องมีเมธอด MakeSound()");
                    Assert.AreEqual(Dog03Type, dogMethod.DeclaringType, "Dog ต้อง override MakeSound() ของตัวเอง");
                    Assert.AreEqual(Animal03Type, dogMethod.GetBaseDefinition().DeclaringType,
                        "MakeSound() ของ Dog ต้องสืบมาจาก Animal (ต้องใช้คีย์เวิร์ด override)");
                    break;

                case "03_MakeSound_WorksPolymorphically":
                    object dog = System.Activator.CreateInstance(Dog03Type);
                    Dog03Type.GetMethod("MakeSound", AnyInstance)?.Invoke(dog, null);
                    TestUtils.AssertMultilineEqual("Woof!", SimpleDebugConsole.GetOutput());
                    break;

                case "04_Cat_MakeSound_IsOverride":
                    var catMethod = Cat03Type.GetMethod("MakeSound", AnyInstance);
                    Assert.IsNotNull(catMethod, "Cat ต้องมีเมธอด MakeSound()");
                    Assert.AreEqual(Cat03Type, catMethod.DeclaringType, "Cat ต้อง override MakeSound() ของตัวเอง");
                    Assert.AreEqual(Animal03Type, catMethod.GetBaseDefinition().DeclaringType,
                        "MakeSound() ของ Cat ต้องสืบมาจาก Animal (ต้องใช้คีย์เวิร์ด override)");

                    SimpleDebugConsole.Clear();
                    object cat = System.Activator.CreateInstance(Cat03Type);
                    catMethod.Invoke(cat, null);
                    TestUtils.AssertMultilineEqual("Meow!", SimpleDebugConsole.GetOutput());
                    break;

                case "05_Ex03_VirtualOverrideDemo_Output":
                    assignment.Ex03_VirtualOverrideDemo();

                    var sb = new StringBuilder();
                    sb.AppendLine("Woof!");
                    sb.AppendLine("Meow!");
                    sb.AppendLine("Generic animal sound");

                    TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
                    break;
            }
        }
    }

    // ===================== ข้อ 4-9: การบ้านระบบเกมเชิงวัตถุ =====================

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
        // ===================== ข้อ 4: ผู้เล่น (Player) =====================

        [TestCase("01_Player_InheritsCharacter")]
        [TestCase("02_Player_HasRequiredFields")]
        [TestCase("03_Player_CanMove_BoundsCheck")]
        [TestCase("04_Player_RevertPosition_RestoresState")]
        [TestCase("05_Player_Trapped_CannotMoveForOneTurn")]
        [TestCase("06_Ex04_PlayerDemo_Output")]
        public void As04_Player(string subTask)
        {
            switch (subTask)
            {
                case "01_Player_InheritsCharacter":
                    Assert.AreEqual(typeof(Week07.Game.Character), typeof(Week07.Game.Player).BaseType,
                        "class Player ต้องสืบทอดจาก class Character");
                    break;

                case "02_Player_HasRequiredFields":
                    var t = typeof(Week07.Game.Player);
                    var prevX = t.GetField("previousPositionX", AnyInstance);
                    var prevY = t.GetField("previousPositionY", AnyInstance);
                    var trapped = t.GetField("isTrapped", AnyInstance);

                    Assert.IsNotNull(prevX, "Player ต้องมีฟิลด์ previousPositionX");
                    Assert.IsNotNull(prevY, "Player ต้องมีฟิลด์ previousPositionY");
                    Assert.IsNotNull(trapped, "Player ต้องมีฟิลด์ isTrapped");
                    break;

                case "03_Player_CanMove_BoundsCheck":
                    BuildMap();
                    var p = map.player as Week07.Game.Player;
                    p.positionX = 0;
                    p.positionY = 0;

                    Assert.IsFalse(p.CanMove(Vector2.left), "เดินออกซ้ายขอบแผนที่ต้อง CanMove เป็น false");
                    Assert.IsFalse(p.CanMove(Vector2.down), "เดินออกล่างขอบแผนที่ต้อง CanMove เป็น false");
                    Assert.IsTrue(p.CanMove(Vector2.right), "เดินไปขวาในขอบเขตต้อง CanMove เป็น true");
                    Assert.IsTrue(p.CanMove(Vector2.up), "เดินขึ้นในขอบเขตต้อง CanMove เป็น true");
                    break;

                case "04_Player_RevertPosition_RestoresState":
                    BuildMap();
                    var player = map.player as Week07.Game.Player;
                    player.positionX = 2;
                    player.positionY = 2;
                    player.previousPositionX = 1;
                    player.previousPositionY = 2;
                    player.energy = 50;

                    player.RevertPosition();

                    Assert.AreEqual(1, player.positionX, "RevertPosition ต้องย้อนกลับไป previousPositionX");
                    Assert.AreEqual(2, player.positionY, "RevertPosition ต้องย้อนกลับไป previousPositionY");
                    Assert.AreEqual(51, player.energy, "RevertPosition ต้องคืนค่า energy + 1");
                    break;

                case "05_Player_Trapped_CannotMoveForOneTurn":
                    BuildMap();
                    var trappedPlayer = map.player as Week07.Game.Player;
                    trappedPlayer.positionX = 0;
                    trappedPlayer.positionY = 0;
                    trappedPlayer.isTrapped = true;

                    trappedPlayer.Move(Vector2.right);

                    Assert.AreEqual(0, trappedPlayer.positionX, "เมื่อติดกับดัก ต้องขยับไม่ได้");
                    Assert.AreEqual(0, trappedPlayer.positionY, "เมื่อติดกับดัก ต้องขยับไม่ได้");
                    Assert.IsFalse(trappedPlayer.isTrapped, "เมื่อพยายามเดินตอนติดกับดัก สถานะติดกับดักต้องถูกปลดออก");
                    break;

                case "06_Ex04_PlayerDemo_Output":
                    assignment.Ex04_PlayerDemo();

                    var sb = new StringBuilder();
                    sb.AppendLine("Player position: (1, 0)");
                    sb.AppendLine("Player energy: 99");

                    TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
                    break;
            }
        }

        // ===================== ข้อ 5: ศัตรู (Enemy) =====================

        [TestCase("01_Enemy_InheritsCharacter")]
        [TestCase("02_Enemy_Hit_IsOverride")]
        [TestCase("03_Enemy_Hit_AttacksPlayer")]
        [TestCase("04_Enemy_Hit_DoesNothingWhenDead")]
        [TestCase("05_Move_IntoEnemy_PlayerAttacksFirstThenEnemyStrikesBack")]
        [TestCase("06_Move_IntoDeadEnemy_PlayerMovesIn")]
        [TestCase("07_Ex05_BattleDemo_FullScenario")]
        public void As05_Enemy(string subTask)
        {
            switch (subTask)
            {
                case "01_Enemy_InheritsCharacter":
                    Assert.AreEqual(typeof(Week07.Game.Character), typeof(Week07.Game.Enemy).BaseType,
                        "class Enemy ต้องสืบทอดจาก class Character");
                    break;

                case "02_Enemy_Hit_IsOverride":
                    var method = typeof(Week07.Game.Enemy).GetMethod("Hit");
                    Assert.IsNotNull(method, "Enemy ต้องมีเมธอด Hit()");
                    Assert.AreEqual(typeof(Week07.Game.Enemy), method.DeclaringType, "Enemy ต้อง override Hit() ของตัวเอง");
                    Assert.AreEqual(typeof(Week07.Game.Identity), method.GetBaseDefinition().DeclaringType,
                        "Hit() ของ Enemy ต้องใช้ override (สืบมาจาก Identity)");
                    break;

                case "03_Enemy_Hit_AttacksPlayer":
                    BuildMap();
                    var enemy = map.enemies[3, 3];
                    int before = map.player.energy;

                    enemy.Hit();

                    Assert.AreEqual(before - enemy.attackPoint, map.player.energy,
                        "Enemy.Hit() ต้องตีผู้เล่นด้วย attackPoint ของตัวเอง");
                    break;

                case "04_Enemy_Hit_DoesNothingWhenDead":
                    BuildMap();
                    var deadEnemy = map.enemies[3, 3];
                    deadEnemy.energy = 0;
                    int beforeDead = map.player.energy;

                    deadEnemy.Hit();

                    Assert.AreEqual(beforeDead, map.player.energy,
                        "ถ้า energy <= 0 แล้ว Enemy.Hit() ต้อง return ทันที ไม่ตีผู้เล่น");
                    break;

                case "05_Move_IntoEnemy_PlayerAttacksFirstThenEnemyStrikesBack":
                    BuildMap();
                    var player = map.player;
                    var enemyTarget = map.enemies[3, 3];

                    player.positionX = 3;
                    player.positionY = 2;
                    player.energy = 100;
                    player.attackPoint = 20;

                    player.Move(Vector2.up);

                    Assert.AreEqual(40, enemyTarget.energy, "ผู้เล่นต้องตีศัตรูก่อน 60 - 20 = 40");
                    Assert.AreEqual(95, player.energy, "ศัตรูยังไม่ตายต้องตีสวนกลับ 100 - 5 = 95");
                    Assert.AreEqual(3, player.positionX, "ศัตรูยังไม่ตาย ผู้เล่นต้องยังไม่ขยับ");
                    Assert.AreEqual(2, player.positionY, "ศัตรูยังไม่ตาย ผู้เล่นต้องยังไม่ขยับ");
                    break;

                case "06_Move_IntoDeadEnemy_PlayerMovesIn":
                    BuildMap();
                    var p = map.player;
                    var e = map.enemies[3, 3];

                    p.positionX = 3;
                    p.positionY = 2;
                    p.energy = 100;
                    p.attackPoint = 20;
                    e.energy = 20;

                    p.Move(Vector2.up);

                    Assert.AreEqual(0, e.energy, "ศัตรูต้องเหลือ 0");
                    Assert.AreEqual(100, p.energy, "ศัตรูตายแล้วต้องตีสวนไม่ได้");
                    Assert.AreEqual(3, p.positionX, "ศัตรูตายแล้ว ผู้เล่นต้องเดินเข้าไปที่ช่องนั้น");
                    Assert.AreEqual(3, p.positionY, "ศัตรูตายแล้ว ผู้เล่นต้องเดินเข้าไปที่ช่องนั้น");
                    break;

                case "07_Ex05_BattleDemo_FullScenario":
                    assignment.Ex05_BattleDemo();

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
                    break;
            }
        }

        // ===================== ข้อ 6: ยาฟื้นพลัง (Potion) =====================

        [TestCase("01_ItemPotion_InheritsIdentity")]
        [TestCase("02_ItemPotion_HasHealPointField")]
        [TestCase("03_Potion_Hit_HealsPlayerAndLeavesMap")]
        [TestCase("04_Move_IntoPotion_NoEnergyLossAndPlayerMovesIn")]
        [TestCase("05_Ex06_PotionDemo_Output")]
        public void As06_ItemPotion(string subTask)
        {
            switch (subTask)
            {
                case "01_ItemPotion_InheritsIdentity":
                    Assert.AreEqual(typeof(Week07.Game.Identity), typeof(Week07.Game.ItemPotion).BaseType,
                        "class ItemPotion ต้องสืบทอดจาก class Identity");
                    break;

                case "02_ItemPotion_HasHealPointField":
                    var field = typeof(Week07.Game.ItemPotion).GetField("healPoint", AnyInstance);
                    Assert.IsNotNull(field, "ItemPotion ต้องมีตัวแปร healPoint");
                    Assert.AreEqual(typeof(int), field.FieldType, "healPoint ต้องเป็น int");
                    Assert.IsTrue(field.IsPublic, "healPoint ต้องเป็น public");
                    break;

                case "03_Potion_Hit_HealsPlayerAndLeavesMap":
                    BuildMap();
                    var potion = map.potions[2, 2];
                    int before = map.player.energy;

                    potion.Hit();

                    TestUtils.AssertMultilineEqual("You got Potion1 : 20", SimpleDebugConsole.GetOutput());
                    Assert.AreEqual(before + 20, map.player.energy, "ต้องเพิ่ม energy ให้ผู้เล่นเท่ากับ healPoint");
                    Assert.AreEqual(map.empty, map.mapData[2, 2], "ต้องเอาไอเทมออกจากแผนที่ (ตั้งช่องนั้นเป็น 0)");
                    break;

                case "04_Move_IntoPotion_NoEnergyLossAndPlayerMovesIn":
                    BuildMap();
                    var player = map.player;
                    player.positionX = 1;
                    player.positionY = 2;
                    player.energy = 100;

                    player.Move(Vector2.right);

                    Assert.AreEqual(120, player.energy, "ช่องที่มีไอเทมไม่หัก energy และต้องได้เลือดจากยา");
                    Assert.AreEqual(2, player.positionX, "ผู้เล่นต้องเดินเข้าไปที่ช่องยา");
                    Assert.AreEqual(2, player.positionY, "ผู้เล่นต้องเดินเข้าไปที่ช่องยา");
                    break;

                case "05_Ex06_PotionDemo_Output":
                    assignment.Ex06_PotionDemo();

                    var sb = new StringBuilder();
                    sb.AppendLine("You got Potion1 : 20");
                    sb.AppendLine("Player energy after picking up potion: 117");

                    TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
                    break;
            }
        }

        // ===================== ข้อ 7: ดาบ (Sword) =====================

        [TestCase("01_ItemSword_InheritsIdentity")]
        [TestCase("02_ItemSword_HasAttackBonusField")]
        [TestCase("03_Sword_Hit_IncreasesAttackAndLeavesMap")]
        [TestCase("04_Move_IntoSword_NoEnergyLossAndPlayerMovesIn")]
        [TestCase("05_Ex07_SwordDemo_Output")]
        public void As07_ItemSword(string subTask)
        {
            switch (subTask)
            {
                case "01_ItemSword_InheritsIdentity":
                    Assert.AreEqual(typeof(Week07.Game.Identity), typeof(Week07.Game.ItemSword).BaseType,
                        "class ItemSword ต้องสืบทอดจาก class Identity");
                    break;

                case "02_ItemSword_HasAttackBonusField":
                    var field = typeof(Week07.Game.ItemSword).GetField("attackBonus", AnyInstance);
                    Assert.IsNotNull(field, "ItemSword ต้องมีตัวแปร attackBonus");
                    Assert.AreEqual(typeof(int), field.FieldType, "attackBonus ต้องเป็น int");
                    Assert.IsTrue(field.IsPublic, "attackBonus ต้องเป็น public");
                    break;

                case "03_Sword_Hit_IncreasesAttackAndLeavesMap":
                    BuildMap();
                    var theSword = map.swords[3, 2];
                    int before = map.player.attackPoint;

                    theSword.Hit();

                    TestUtils.AssertMultilineEqual("You got Sword1 : 10", SimpleDebugConsole.GetOutput());
                    Assert.AreEqual(before + 10, map.player.attackPoint, "ต้องเพิ่ม attackPoint ให้ผู้เล่นเท่ากับ attackBonus");
                    Assert.AreEqual(map.empty, map.mapData[3, 2], "ต้องเอาไอเทมออกจากแผนที่ (ตั้งช่องนั้นเป็น 0)");
                    break;

                case "04_Move_IntoSword_NoEnergyLossAndPlayerMovesIn":
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
                    break;

                case "05_Ex07_SwordDemo_Output":
                    assignment.Ex07_SwordDemo();

                    var sb = new StringBuilder();
                    sb.AppendLine("You got Potion1 : 20");
                    sb.AppendLine("You got Sword1 : 10");
                    sb.AppendLine("Player attack point after picking up sword: 20");

                    TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
                    break;
            }
        }

        // ===================== ข้อ 8: กำแพง (Wall) =====================

        [TestCase("01_Wall_InheritsIdentity")]
        [TestCase("02_Wall_HasDurabilityField")]
        [TestCase("03_Wall_Hit_DecreasesDurability")]
        [TestCase("04_Wall_Hit_DestroysWhenDurabilityZero")]
        [TestCase("05_Move_IntoWall_BlocksPlayer")]
        [TestCase("06_Ex08_WallDemo_Output")]
        public void As08_Wall(string subTask)
        {
            switch (subTask)
            {
                case "01_Wall_InheritsIdentity":
                    Assert.AreEqual(typeof(Week07.Game.Identity), typeof(Week07.Game.Wall).BaseType,
                        "class Wall ต้องสืบทอดจาก class Identity");
                    break;

                case "02_Wall_HasDurabilityField":
                    var field = typeof(Week07.Game.Wall).GetField("durability", AnyInstance);
                    Assert.IsNotNull(field, "Wall ต้องมีตัวแปร durability");
                    Assert.AreEqual(typeof(int), field.FieldType, "durability ต้องเป็น int");
                    Assert.IsTrue(field.IsPublic, "durability ต้องเป็น public");
                    break;

                case "03_Wall_Hit_DecreasesDurability":
                    BuildMap();
                    var wall = map.walls[1, 3];
                    int durBefore = wall.durability;

                    wall.Hit();

                    Assert.AreEqual(durBefore - 1, wall.durability, "Wall.Hit() ต้องลด durability ลง 1");
                    break;

                case "04_Wall_Hit_DestroysWhenDurabilityZero":
                    BuildMap();
                    var targetWall = map.walls[1, 3];

                    targetWall.Hit();
                    targetWall.Hit();
                    targetWall.Hit();

                    Assert.AreEqual(0, targetWall.durability, "ตี 3 ครั้ง durability ต้องเหลือ 0");
                    Assert.AreEqual(map.empty, map.mapData[1, 3], "กำแพงพังแล้ว ช่องแผนที่ต้องกลายเป็นช่องว่าง (0)");
                    break;

                case "05_Move_IntoWall_BlocksPlayer":
                    BuildMap();
                    var player = map.player;
                    player.positionX = 0;
                    player.positionY = 3;

                    player.Move(Vector2.right); // พยายามเดินเข้าหากำแพงที่ (1, 3)

                    Assert.AreEqual(0, player.positionX, "ชนกำแพงแล้วผู้เล่นต้องเดินผ่านไม่ได้ (positionX อยู่ที่เดิม)");
                    Assert.AreEqual(3, player.positionY, "ชนกำแพงแล้วผู้เล่นต้องเดินผ่านไม่ได้ (positionY อยู่ที่เดิม)");
                    Assert.AreEqual(2, map.walls[1, 3].durability, "การเดินชนกำแพงต้องเรียก Hit() ทำให้ durability ลดลง");
                    break;

                case "06_Ex08_WallDemo_Output":
                    assignment.Ex08_WallDemo();

                    var sb = new StringBuilder();
                    sb.AppendLine("Hit Wall Wall1! Remaining durability: 2");
                    sb.AppendLine("Hit Wall Wall1! Remaining durability: 1");
                    sb.AppendLine("Hit Wall Wall1! Remaining durability: 0");
                    sb.AppendLine("💥 Wall Wall1 destroyed!");

                    TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
                    break;
            }
        }

        // ===================== ข้อ 9: กล่องสมบัติ (Chest) =====================

        [TestCase("01_Chest_InheritsIdentity")]
        [TestCase("02_Chest_HasRequiredFields")]
        [TestCase("03_Chest_OpenChest_SpawnsItemAndClearsMap")]
        [TestCase("04_Chest_Hit_CallsOpenChest")]
        [TestCase("05_Move_IntoChest_OpensChestAndMovesIn")]
        [TestCase("06_Ex09_ChestDemo_Output")]
        public void As09_Chest(string subTask)
        {
            switch (subTask)
            {
                case "01_Chest_InheritsIdentity":
                    Assert.AreEqual(typeof(Week07.Game.Identity), typeof(Week07.Game.Chest).BaseType,
                        "class Chest ต้องสืบทอดจาก class Identity");
                    break;

                case "02_Chest_HasRequiredFields":
                    var t = typeof(Week07.Game.Chest);
                    var prefabField = t.GetField("spawnPrefab", AnyInstance);
                    var isOpenField = t.GetField("isOpen", AnyInstance);

                    Assert.IsNotNull(prefabField, "Chest ต้องมีฟิลด์ spawnPrefab");
                    Assert.AreEqual(typeof(GameObject), prefabField.FieldType, "spawnPrefab ต้องเป็น GameObject");
                    Assert.IsNotNull(isOpenField, "Chest ต้องมีฟิลด์ isOpen");
                    break;

                case "03_Chest_OpenChest_SpawnsItemAndClearsMap":
                    BuildMap();
                    var chest = map.chests[1, 1];
                    var dummyItem = new GameObject("DummyChestItem");
                    chest.spawnPrefab = dummyItem;

                    chest.OpenChest();

                    Assert.IsTrue(chest.isOpen, "OpenChest ต้องเปลี่ยนสถานะ isOpen เป็น true");
                    Assert.AreEqual(map.empty, map.mapData[1, 1], "เปิดกล่องแล้ว ช่องแผนที่ต้องกลายเป็นช่องว่าง (0)");

                    Object.DestroyImmediate(dummyItem);
                    break;

                case "04_Chest_Hit_CallsOpenChest":
                    BuildMap();
                    var hitChest = map.chests[1, 1];

                    hitChest.Hit();

                    Assert.IsTrue(hitChest.isOpen, "Chest.Hit() ต้องเรียก OpenChest()");
                    TestUtils.AssertMultilineEqual("📦 Opened Chest1!", SimpleDebugConsole.GetOutput());
                    break;

                case "05_Move_IntoChest_OpensChestAndMovesIn":
                    BuildMap();
                    var player = map.player;
                    player.positionX = 0;
                    player.positionY = 1;

                    player.Move(Vector2.right); // เดินเข้าไปที่กล่องสมบัติ (1, 1)

                    Assert.IsTrue(map.chests[1, 1].isOpen, "เมื่อเดินเข้าหากล่อง กล่องต้องถูกเปิด");
                    Assert.AreEqual(1, player.positionX, "เมื่อเปิดกล่องแล้ว ผู้เล่นต้องเดินเข้าไปที่ช่องกล่อง");
                    Assert.AreEqual(1, player.positionY, "เมื่อเปิดกล่องแล้ว ผู้เล่นต้องเดินเข้าไปที่ช่องกล่อง");
                    break;

                case "06_Ex09_ChestDemo_Output":
                    assignment.Ex09_ChestDemo();

                    var sb = new StringBuilder();
                    sb.AppendLine("📦 Opened Chest1!");

                    TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
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
