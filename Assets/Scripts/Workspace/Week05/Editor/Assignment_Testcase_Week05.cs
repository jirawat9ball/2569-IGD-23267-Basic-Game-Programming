using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using NUnit.Framework;
using UnityEngine;

using Week05;
using SimpleDebugConsole = Workspace.Core.SimpleDebugConsole;

namespace Week05_Method
{
    public class TestBase
    {
        // =========================================================================================
        // 🎯 สลับตรวจไฟล์ อ. หรือ นักเรียน: เปลี่ยนเป็น true เมื่อต้องการตรวจไฟล์เฉลยอาจารย์
        // =========================================================================================
        protected const bool isTeacherMode = false;

        protected const string StudentPath = "Assets/Scripts/Workspace/Week05/Assignment_Student_Week05.cs";
        protected const string TeacherPath = "Assets/Scripts/Workspace/Teacher/Assignment_Teacher_Week05.cs";

        protected static string CurrentTargetFilePath => isTeacherMode ? TeacherPath : StudentPath;

        protected IAssignment assignment;
        protected Assignment_Student_Week05 student;
        protected GameObject testGo;

        [SetUp]
        public void Setup()
        {
            testGo = new GameObject("Week05_TestRunner");
            student = testGo.AddComponent<Assignment_Student_Week05>();
            assignment = student;
            SimpleDebugConsole.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            if (testGo != null)
                Object.DestroyImmediate(testGo);

            DestroyAllClones();
        }

        protected static void DestroyAllClones()
        {
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (go != null && go.name.Contains("(Clone)")) Object.DestroyImmediate(go);
            }
        }

        protected static System.Collections.Generic.List<GameObject> ClonesNamed(string prefabName)
        {
            var list = new System.Collections.Generic.List<GameObject>();
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (go != null && go.name == prefabName + "(Clone)") list.Add(go);
            }
            return list;
        }

        protected static GameObject[] MakePrefabs(params string[] names)
        {
            var arr = new GameObject[names.Length];
            for (int i = 0; i < names.Length; i++) arr[i] = new GameObject(names[i]);
            return arr;
        }

        protected static void DestroyAll(params GameObject[] objects)
        {
            foreach (var go in objects)
            {
                if (go != null) Object.DestroyImmediate(go);
            }
        }

        // ---- อ่าน source ของ student เพื่อกัน hardcode ----

        private static string ReadStudentSourceStripped()
        {
            string path = CurrentTargetFilePath;
            Assert.IsTrue(File.Exists(path),
                $"หาไฟล์เป้าหมายไม่เจอที่ '{path}' (cwd={Directory.GetCurrentDirectory()})");

            string src = File.ReadAllText(path);
            src = Regex.Replace(src, @"//.*?$", "", RegexOptions.Multiline);
            src = Regex.Replace(src, @"/\*.*?\*/", "", RegexOptions.Singleline);
            src = Regex.Replace(src, "\"([^\"\\\\]|\\\\.)*\"", "\"\"");
            src = Regex.Replace(src, "'([^'\\\\]|\\\\.)*'", "' '");
            return src;
        }

        /// <summary>ดึงบอดี้ของเมธอด โดยระบุ signature เต็ม เช่น "public int Add(int a, int b)"</summary>
        protected static string GetMethodBody(string signature)
        {
            string src = ReadStudentSourceStripped();

            int sig = src.IndexOf(signature, System.StringComparison.Ordinal);
            Assert.Greater(sig, -1, $"ไม่พบเมธอด '{signature}' ในไฟล์เป้าหมาย");

            int open = src.IndexOf('{', sig);
            Assert.Greater(open, -1, $"เมธอด '{signature}' ไม่มี body");

            int depth = 0;
            for (int i = open; i < src.Length; i++)
            {
                if (src[i] == '{') depth++;
                else if (src[i] == '}')
                {
                    depth--;
                    if (depth == 0)
                        return src.Substring(open + 1, i - open - 1);
                }
            }
            Assert.Fail($"บอดี้เมธอด '{signature}' ปีกกาไม่ครบ");
            return null;
        }

        protected static void AssertSignatureExists(string signature)
        {
            StringAssert.Contains(signature, ReadStudentSourceStripped(),
                $"ต้องประกาศเมธอดตามรูปแบบ '{signature}'");
        }

        protected static void AssertRawSourceContains(string needle, string reason)
        {
            string path = CurrentTargetFilePath;
            Assert.IsTrue(File.Exists(path), $"หาไฟล์เป้าหมายไม่เจอที่ '{path}'");
            StringAssert.Contains(needle, File.ReadAllText(path), reason);
        }

        protected static void AssertBodyContains(string signature, string needle, string reason)
        {
            StringAssert.Contains(needle, GetMethodBody(signature), $"{signature}: {reason}");
        }

        protected static void AssertUsesRealLoop(string signature, int minLoops = 1)
        {
            string body = GetMethodBody(signature);
            int loops = Regex.Matches(body, @"\bfor\s*\(").Count
                      + Regex.Matches(body, @"\bforeach\s*\(").Count
                      + Regex.Matches(body, @"\bwhile\s*\(").Count;

            Assert.GreaterOrEqual(loops, minLoops,
                $"{signature}: ต้องใช้ลูปจริงอย่างน้อย {minLoops} ลูป (ห้าม hardcode พิมพ์ทีละบรรทัด)");
        }
    }

    public class Exercises : TestBase
    {
        // ============ ข้อ 1: Method แบบ void และ Parameter (Overloading) ============

        [Test]
        public void Ex01_UserNameIdentification_NoParameter()
        {
            assignment.UserNameIdentification();
            TestUtils.AssertMultilineEqual("user name is UntitleUser", SimpleDebugConsole.GetOutput());
            AssertSignatureExists("public void UserNameIdentification()");
        }

        [TestCase("boy")]
        [TestCase("Anna")]
        [TestCase("นักศึกษา")]
        public void Ex01_UserNameIdentification_WithName(string name)
        {
            assignment.UserNameIdentification(name);
            TestUtils.AssertMultilineEqual("user name is " + name, SimpleDebugConsole.GetOutput());
            AssertSignatureExists("public void UserNameIdentification(string name)");
        }

        [TestCase("big", 18)]
        [TestCase("Tom", 7)]
        [TestCase("Ann", 0)]
        public void Ex01_UserNameIdentification_WithNameAndAge(string name, int age)
        {
            assignment.UserNameIdentification(name, age);
            TestUtils.AssertMultilineEqual($"user name is {name} age is {age}", SimpleDebugConsole.GetOutput());
            AssertSignatureExists("public void UserNameIdentification(string name, int age)");
        }

        [Test]
        public void Ex01_UserCountry_UsesDefaultValue()
        {
            assignment.UserCountry();
            TestUtils.AssertMultilineEqual("Thailand", SimpleDebugConsole.GetOutput());
            AssertRawSourceContains("string country = \"Thailand\"",
                "ต้องกำหนดค่าเริ่มต้นของพารามิเตอร์เป็น \"Thailand\"");
        }

        [TestCase("Japan")]
        [TestCase("Laos")]
        public void Ex01_UserCountry_WithValue(string country)
        {
            assignment.UserCountry(country);
            TestUtils.AssertMultilineEqual(country, SimpleDebugConsole.GetOutput());
        }

        // ============ ข้อ 2: Method แบบมีค่าส่งกลับ (Return Type) ============

        [TestCase(1, 9, 10)]
        [TestCase(0, 0, 0)]
        [TestCase(-5, 5, 0)]
        [TestCase(100, 250, 350)]
        [TestCase(-7, -3, -10)]
        public void Ex02_Add(int a, int b, int expected)
        {
            Assert.AreEqual(expected, assignment.Add(a, b), $"Add({a}, {b}) ต้อง return {expected}");
            AssertSignatureExists("public int Add(int a, int b)");
        }

        [TestCase("hello", 5)]
        [TestCase("", 0)]
        [TestCase("Unity Engine", 12)]
        [TestCase("a", 1)]
        public void Ex02_GetStringLength(string text, int expected)
        {
            Assert.AreEqual(expected, assignment.GetStringLength(text), $"GetStringLength(\"{text}\") ต้อง return {expected}");
            AssertSignatureExists("public int GetStringLength(string text)");
        }

        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(2, false)]
        [TestCase(-1, false)]
        public void Ex02_ConvertInttoBool(int sex, bool expected)
        {
            Assert.AreEqual(expected, assignment.ConvertInttoBool(sex), $"ConvertInttoBool({sex}) ต้อง return {expected}");
            AssertSignatureExists("public bool ConvertInttoBool(int sex)");
        }

        // ============ ข้อ 3: แยกโค้ดสร้างแผนที่ออกเป็น Method ============

        [TestCase(3, 4)]
        [TestCase(1, 1)]
        [TestCase(5, 2)]
        public void Ex03_GenerateFloor(int columns, int rows)
        {
            var tiles = MakePrefabs("Floor");
            student.columns = columns;
            student.rows = rows;
            student.floorTiles = tiles;

            assignment.GenerateFloor();

            var clones = ClonesNamed("Floor");
            Assert.AreEqual(columns * rows, clones.Count, "ต้องสร้างพื้นให้ครบทุกช่องของแผนที่");
            foreach (var go in clones)
            {
                float x = go.transform.position.x;
                float y = go.transform.position.y;
                Assert.IsTrue(x >= 0 && x <= columns - 1, $"พื้นอยู่นอกแผนที่ (x = {x})");
                Assert.IsTrue(y >= 0 && y <= rows - 1, $"พื้นอยู่นอกแผนที่ (y = {y})");
            }

            DestroyAll(tiles);
            AssertUsesRealLoop("public void GenerateFloor()", minLoops: 2);
            AssertBodyContains("public void GenerateFloor()", "Instantiate", "ต้อง Instantiate พื้นจริง");
        }

        [TestCase(3, 4)]
        [TestCase(1, 1)]
        [TestCase(5, 2)]
        public void Ex03_GenerateWalls(int columns, int rows)
        {
            var tiles = MakePrefabs("Wall");
            student.columns = columns;
            student.rows = rows;
            student.wallTiles = tiles;

            assignment.GenerateWalls();

            var clones = ClonesNamed("Wall");
            int expected = (columns + 2) * (rows + 2) - columns * rows;
            Assert.AreEqual(expected, clones.Count, "จำนวนกำแพงต้องเท่ากับขอบนอกของแผนที่");
            foreach (var go in clones)
            {
                float x = go.transform.position.x;
                float y = go.transform.position.y;
                bool onBorder = Mathf.Approximately(x, -1) || Mathf.Approximately(x, columns)
                             || Mathf.Approximately(y, -1) || Mathf.Approximately(y, rows);
                Assert.IsTrue(onBorder, $"กำแพงต้องอยู่ที่ขอบนอกเท่านั้น แต่เจอที่ ({x}, {y})");
            }

            DestroyAll(tiles);
            AssertUsesRealLoop("public void GenerateWalls()", minLoops: 2);
            AssertBodyContains("public void GenerateWalls()", "Instantiate", "ต้อง Instantiate กำแพงจริง");
        }

        [TestCase(3, 4, 3)]
        [TestCase(5, 5, 1)]
        [TestCase(2, 2, 6)]
        public void Ex03_GenerateFoods(int columns, int rows, int foodCount)
        {
            var tiles = MakePrefabs("Food");
            student.columns = columns;
            student.rows = rows;
            student.foodCount = foodCount;
            student.foodTiles = tiles;

            assignment.GenerateFoods();

            var clones = ClonesNamed("Food");
            Assert.AreEqual(foodCount, clones.Count, "จำนวนอาหารต้องเท่ากับ foodCount");
            foreach (var go in clones)
            {
                float x = go.transform.position.x;
                float y = go.transform.position.y;
                Assert.IsTrue(x >= 0 && x <= columns - 1, $"อาหารอยู่นอกแผนที่ (x = {x})");
                Assert.IsTrue(y >= 0 && y <= rows - 1, $"อาหารอยู่นอกแผนที่ (y = {y})");
            }

            DestroyAll(tiles);
            AssertUsesRealLoop("public void GenerateFoods()");
            AssertBodyContains("public void GenerateFoods()", "Instantiate", "ต้อง Instantiate อาหารจริง");
        }

        [Test]
        public void Ex03_PlacePlayer()
        {
            var prefab = new GameObject("Player");
            student.player = prefab;

            assignment.PlacePlayer();

            var clones = ClonesNamed("Player");
            Assert.AreEqual(1, clones.Count, "ต้องสร้างตัวละคร 1 ตัว");
            Assert.AreEqual(0f, clones[0].transform.position.x, 0.0001f, "ตัวละครต้องอยู่ที่ x = 0");
            Assert.AreEqual(0f, clones[0].transform.position.y, 0.0001f, "ตัวละครต้องอยู่ที่ y = 0");

            DestroyAll(prefab);
            AssertBodyContains("public void PlacePlayer()", "Instantiate", "ต้อง Instantiate ตัวละครจริง");
        }

        [TestCase(3, 4)]
        [TestCase(1, 1)]
        [TestCase(6, 2)]
        public void Ex03_PlaceExit(int columns, int rows)
        {
            var prefab = new GameObject("Exit");
            student.columns = columns;
            student.rows = rows;
            student.exitTile = prefab;

            assignment.PlaceExit();

            var clones = ClonesNamed("Exit");
            Assert.AreEqual(1, clones.Count, "ต้องสร้างทางออก 1 อัน");
            Assert.AreEqual(columns - 1, clones[0].transform.position.x, 0.0001f, $"ทางออกต้องอยู่ที่ x = {columns - 1}");
            Assert.AreEqual(rows - 1, clones[0].transform.position.y, 0.0001f, $"ทางออกต้องอยู่ที่ y = {rows - 1}");

            DestroyAll(prefab);
            AssertBodyContains("public void PlaceExit()", "Instantiate", "ต้อง Instantiate ทางออกจริง");
        }

        // ============ ข้อ 4: Move ============

        [Test]
        public void Ex04_Move_RightThreeThenUpThree()
        {
            student.energy = 20;
            student.transform.position = Vector3.zero;

            for (int i = 0; i < 3; i++) assignment.Move(Vector2.right);
            for (int i = 0; i < 3; i++) assignment.Move(Vector2.up);

            Assert.AreEqual(3f, student.transform.position.x, 0.0001f, "เดินขวา 3 ครั้ง x ต้องเป็น 3");
            Assert.AreEqual(3f, student.transform.position.y, 0.0001f, "เดินขึ้น 3 ครั้ง y ต้องเป็น 3");
            Assert.AreEqual(14, student.energy, "เดิน 6 ครั้ง energy ต้องลดจาก 20 เหลือ 14");

            AssertSignatureExists("public void Move(Vector2 direction)");
        }

        [TestCase(1, 0, 5)]
        [TestCase(0, -1, 1)]
        [TestCase(2, 3, 4)]
        public void Ex04_Move_SingleDirection(int dirX, int dirY, int times)
        {
            student.energy = 20;
            student.transform.position = Vector3.zero;

            for (int i = 0; i < times; i++) assignment.Move(new Vector2(dirX, dirY));

            Assert.AreEqual(dirX * times, student.transform.position.x, 0.0001f);
            Assert.AreEqual(dirY * times, student.transform.position.y, 0.0001f);
            Assert.AreEqual(20 - times, student.energy, "energy ต้องลดลง 1 ต่อการเดิน 1 ครั้ง");
        }

        // ============ ข้อ 5: TakeDamage ============

        [Test]
        public void Ex05_TakeDamage_ReducesEnergy()
        {
            student.energy = 20;

            assignment.TakeDamage(4);
            assignment.TakeDamage(5);
            assignment.TakeDamage(6);

            Assert.AreEqual(5, student.energy, "โดน 4 + 5 + 6 จาก 20 ต้องเหลือ 5");
            AssertSignatureExists("public void TakeDamage(int Damage)");
        }

        [TestCase(10, 50)]
        [TestCase(20, 20)]
        [TestCase(1, 999)]
        public void Ex05_TakeDamage_NeverBelowZero(int startEnergy, int damage)
        {
            student.energy = startEnergy;

            assignment.TakeDamage(damage);

            Assert.AreEqual(0, student.energy, "energy ต้องไม่ต่ำกว่า 0");
        }

        // ============ ข้อ 6: CheckDead ============

        [Test]
        public void Ex06_CheckDead_PrintsYouLoseWhenEnergyRunsOut()
        {
            student.energy = 40;
            SimpleDebugConsole.Clear();

            assignment.TakeDamage(10);
            assignment.TakeDamage(10);
            assignment.TakeDamage(10);
            assignment.TakeDamage(10);

            var sb = new StringBuilder();
            sb.AppendLine("Current Energy : 30");
            sb.AppendLine("Current Energy : 20");
            sb.AppendLine("Current Energy : 10");
            sb.AppendLine("Current Energy : 0");
            sb.AppendLine("You Lose");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void Ex06_CheckDead_NotDeadYet()
        {
            student.energy = 30;
            SimpleDebugConsole.Clear();

            assignment.TakeDamage(10);

            TestUtils.AssertMultilineEqual("Current Energy : 20", SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void Ex06_CheckDead_IsPrivateAndCalledFromTakeDamage()
        {
            AssertSignatureExists("private void CheckDead()");
            AssertBodyContains("public void TakeDamage(int Damage)", "CheckDead",
                "TakeDamage ต้องเรียก CheckDead() หลังลด energy");
        }

        // ============ ข้อ 7: Heal ============

        [TestCase(20, 4, 24)]
        [TestCase(0, 10, 10)]
        [TestCase(5, 0, 5)]
        [TestCase(100, 250, 350)]
        public void Ex07_Heal(int startEnergy, int healPoint, int expected)
        {
            student.energy = startEnergy;

            assignment.Heal(healPoint);

            Assert.AreEqual(expected, student.energy, $"Heal({healPoint}) จาก {startEnergy} ต้องได้ {expected}");
            AssertSignatureExists("public void Heal(int healPoint)");
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
