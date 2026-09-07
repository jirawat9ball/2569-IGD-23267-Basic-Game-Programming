using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using NUnit.Framework;
using UnityEngine;

using Week03;
using SimpleDebugConsole = Workspace.Core.SimpleDebugConsole;

namespace Week03_Loop
{
    public class TestBase
    {
        protected const string StudentPath = "Assets/Scripts/Workspace/Week03/Assignment_Student_Week03.cs";

        protected IAssignment assignment;
        protected GameObject testGo;

        [SetUp]
        public void Setup()
        {
            testGo = new GameObject("Week03_TestRunner");
            assignment = testGo.AddComponent<Assignment_Student_Week03>();
            SimpleDebugConsole.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            if (testGo != null)
                Object.DestroyImmediate(testGo);

            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (go != null && go.name.Contains("(Clone)"))
                    Object.DestroyImmediate(go);
            }
        }

        // ---- anti hardcode: อ่าน source ของ student ว่าใช้ loop จริงไหม ----

        protected static string GetStudentMethodBody(string methodName)
        {
            Assert.IsTrue(File.Exists(StudentPath),
                $"หาไฟล์ student ไม่เจอที่ '{StudentPath}' (cwd={Directory.GetCurrentDirectory()})");

            string src = File.ReadAllText(StudentPath);
            src = Regex.Replace(src, @"//.*?$", "", RegexOptions.Multiline);
            src = Regex.Replace(src, @"/\*.*?\*/", "", RegexOptions.Singleline);
            src = Regex.Replace(src, "\"([^\"\\\\]|\\\\.)*\"", "\"\"");
            src = Regex.Replace(src, "'([^'\\\\]|\\\\.)*'", "' '");

            int sig = src.IndexOf("public void " + methodName, System.StringComparison.Ordinal);
            Assert.Greater(sig, -1, $"ไม่พบเมธอด public void {methodName} ในไฟล์ student");

            int open = src.IndexOf('{', sig);
            Assert.Greater(open, -1, $"เมธอด {methodName} ไม่มี body");

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
            Assert.Fail($"บอดี้เมธอด {methodName} ปีกกาไม่ครบ");
            return null;
        }

        protected static void AssertUsesRealLoop(string methodName, bool requireWhile = false, int minLoops = 1)
        {
            string body = GetStudentMethodBody(methodName);
            int forCount = Regex.Matches(body, @"\bfor\s*\(").Count;
            int foreachCount = Regex.Matches(body, @"\bforeach\s*\(").Count;
            int whileCount = Regex.Matches(body, @"\bwhile\s*\(").Count;

            if (requireWhile)
                Assert.GreaterOrEqual(whileCount, minLoops,
                    $"{methodName}: ต้องใช้ while loop จริงอย่างน้อย {minLoops} ลูป (ห้าม hardcode พิมพ์ทีละบรรทัด)");
            else
                Assert.GreaterOrEqual(forCount + foreachCount + whileCount, minLoops,
                    $"{methodName}: ต้องใช้ลูป (for/while) จริงอย่างน้อย {minLoops} ลูป (ห้าม hardcode พิมพ์ทีละบรรทัด)");
        }

        protected static void AssertBodyContains(string methodName, string needle, string reason)
        {
            StringAssert.Contains(needle, GetStudentMethodBody(methodName), $"{methodName}: {reason}");
        }
    }

    public class Lecture : TestBase
    {
        // ================= Array (ข้อ 1-6) =================

        [Test]
        public void As01_IronManSuit()
        {
            assignment.As01_IronManSuit();

            var sb = new StringBuilder();
            sb.AppendLine("TonyStark Wear : Mark I");
            sb.AppendLine("Room size IronManSuit : 4");
            sb.AppendLine("===All suit in collection===");
            foreach (var s in new[] { "Mark I", "Mark II", "Mark III", "Mark IV" })
                sb.AppendLine(s);

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("As01_IronManSuit");
        }

        [Test]
        public void As02_SpiderManAndBatMan()
        {
            assignment.As02_SpiderManAndBatMan();

            var sb = new StringBuilder();
            sb.AppendLine("Room size spiderMan : 3");
            sb.AppendLine("===All spiderMan in collection===");
            foreach (var s in new[] { "Classic SpiderMan", "Symbiote SpiderMan", "Iron Spider" })
                sb.AppendLine(s);
            sb.AppendLine("Room size BatMan : 4");
            sb.AppendLine("===All BatMan in collection===");
            foreach (var s in new[] { "Classic BatMan", "Dark Knight", "Batman Beyond", "The Batman" })
                sb.AppendLine(s);

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("As02_SpiderManAndBatMan", minLoops: 2);
        }

        [Test]
        public void As03_RandomItemDrop_UsesRandomAndInstantiate()
        {
            string[] names = { "Potion", "Sword", "Shield", "Bow", "Ring" };
            var picks = new System.Collections.Generic.HashSet<string>();

            for (int seed = 1; seed <= 25; seed++)
            {
                SimpleDebugConsole.Clear();
                Random.InitState(seed);
                var items = new GameObject[names.Length];
                for (int i = 0; i < names.Length; i++) items[i] = new GameObject(names[i]);

                assignment.As03_RandomItemDrop(items);

                string output = SimpleDebugConsole.GetOutput().Trim();
                Assert.IsTrue(output.StartsWith("Got item: "), $"seed {seed}: ต้องขึ้นต้นด้วย 'Got item: ' แต่ได้ '{output}'");
                CollectionAssert.Contains(names, output.Substring("Got item: ".Length), $"seed {seed}: ชื่อไอเทมไม่อยู่ใน array");
                picks.Add(output.Substring("Got item: ".Length));

                foreach (var go in items) Object.DestroyImmediate(go);
                foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
                    if (go.name.Contains("(Clone)")) Object.DestroyImmediate(go);
            }

            Assert.Greater(picks.Count, 1, "สุ่ม 25 รอบได้ผลเดิมทุกครั้ง — น่าจะ hardcode index");
            AssertBodyContains("As03_RandomItemDrop", "Random.Range", "ต้องใช้ Random.Range");
            AssertBodyContains("As03_RandomItemDrop", "Instantiate", "ต้อง Instantiate ไอเทมที่สุ่มได้");
        }

        // ================= For Loop (ข้อ 7-10) =================

        [Test]
        public void As04_ForLoopBasic()
        {
            assignment.As04_ForLoopBasic();

            var sb = new StringBuilder();
            for (int i = 0; i < 10; i++) sb.AppendLine($"<10 : {i}");
            sb.AppendLine("======================");
            for (int i = 1; i <= 10; i++) sb.AppendLine($"<=10 : {i}");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("As04_ForLoopBasic", minLoops: 2);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(50)]
        [TestCase(137)]
        public void As05_ForLoopN(int n)
        {
            assignment.As05_ForLoopN(n);

            var sb = new StringBuilder();
            for (int i = 0; i < n; i++) sb.AppendLine(i.ToString());

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            if (n > 0) AssertUsesRealLoop("As05_ForLoopN");
        }

        static readonly TestCaseData[] StepCases =
        {
            new TestCaseData((object)new[] { "A", "B", "C", "D" }).SetName("As06_ForLoopWithArray(\"A\", \"B\", \"C\", \"D\")"),
            new TestCaseData((object)new[] { "A", "B", "C", "D", "E" }).SetName("As06_ForLoopWithArray(\"A\", \"B\", \"C\", \"D\", \"E\")"),
            new TestCaseData((object)new[] { "Mark I", "Mark II", "Mark III", "Mark IV", "Mark V", "Mark VI" }).SetName("As06_ForLoopWithArray(\"Mark I\", \"Mark II\", \"Mark III\", \"Mark IV\", \"Mark V\", \"Mark VI\")"),
            new TestCaseData((object)new[] { "s0", "s1", "s2", "s3", "s4", "s5", "s6" }).SetName("As06_ForLoopWithArray(\"s0\", \"s1\", \"s2\", \"s3\", \"s4\", \"s5\", \"s6\")"),
        };

        [TestCaseSource(nameof(StepCases))]
        public void As06_ForLoopWithArray(string[] suites)
        {
            assignment.As06_ForLoopWithArray(suites);
            TestUtils.AssertMultilineEqual(ExpectedStepOutput(suites), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("As06_ForLoopWithArray", minLoops: 2);
        }

        // ================= While Loop (ข้อ 11-14) =================

        [Test]
        public void As07_WhileLoopBasic()
        {
            assignment.As07_WhileLoopBasic();

            var sb = new StringBuilder();
            for (int i = 0; i < 10; i++) sb.AppendLine($"while loop : {i}");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("As07_WhileLoopBasic", requireWhile: true);
        }

        private static string ExpectedStepOutput(string[] suites)
        {
            var sb = new StringBuilder();
            sb.AppendLine("======Log by One======");
            for (int i = 0; i < suites.Length; i++) sb.AppendLine(suites[i]);
            sb.AppendLine("======Log by Two======");
            for (int i = 0; i < suites.Length; i += 2) sb.AppendLine(suites[i]);
            return sb.ToString();
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

    public class Homework : TestBase
    {
        // ================= Level 1: Simple (Lv01 - Lv05) =================

        static readonly TestCaseData[] AttackCases =
        {
            new TestCaseData(new[] { 100, 80, 60, 40 }, 10, 2).SetName("Lv01_AttackTarget([100, 80, 60, 40], damage: 10, target: 2)"),
            new TestCaseData(new[] { 50, 50, 50 }, 5, 1).SetName("Lv01_AttackTarget([50, 50, 50], damage: 5, target: 1)"),
            new TestCaseData(new[] { 200, 150, 120, 90, 30 }, 25, 3).SetName("Lv01_AttackTarget([200, 150, 120, 90, 30], damage: 25, target: 3)"),
            new TestCaseData(new[] { 10, 10 }, 3, 0).SetName("Lv01_AttackTarget([10, 10], damage: 3, target: 0)"),
            new TestCaseData(new[] { 1000 }, 100, 0).SetName("Lv01_AttackTarget([1000], damage: 100, target: 0)"),
            new TestCaseData(new[] { 7, 8, 9, 10, 11, 12, 13 }, 4, 5).SetName("Lv01_AttackTarget([7, 8, 9, 10, 11, 12, 13], damage: 4, target: 5)"),
        };

        [TestCaseSource(nameof(AttackCases))]
        public void Lv01_AttackTarget(int[] hp, int damage, int target)
        {
            assignment.Lv01_AttackTarget((int[])hp.Clone(), damage, target);

            int last = hp.Length - 1;
            int[] sim = (int[])hp.Clone();
            var exp = new StringBuilder();
            sim[0] -= damage; exp.AppendLine($"FirstEnemy hp :{sim[0]}");
            sim[last] -= damage; exp.AppendLine($"LastEnemy hp :{sim[last]}");
            sim[target] -= damage; exp.AppendLine($"TargetEnemy {target} hp :{sim[target]}");

            TestUtils.AssertMultilineEqual(exp.ToString(), SimpleDebugConsole.GetOutput());
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(9)]
        [TestCase(12)]
        [TestCase(0)]
        [TestCase(-3)]
        public void Lv02_MultiplicationTable(int n)
        {
            assignment.Lv02_MultiplicationTable(n);

            var sb = new StringBuilder();
            for (int i = 1; i <= 12; i++) sb.AppendLine($"{n} x {i} = {n * i}");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv02_MultiplicationTable");
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(50)]
        [TestCase(137)]
        public void Lv03_WhileLoopN(int n)
        {
            assignment.Lv03_WhileLoopN(n);

            var sb = new StringBuilder();
            for (int i = 0; i < n; i++) sb.AppendLine(i.ToString());

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            if (n > 0) AssertUsesRealLoop("Lv03_WhileLoopN", requireWhile: true);
        }

        static readonly TestCaseData[] StepCases =
        {
            new TestCaseData((object)new[] { "A", "B", "C", "D" }).SetName("Lv04_WhileLoopStep(\"A\", \"B\", \"C\", \"D\")"),
            new TestCaseData((object)new[] { "A", "B", "C", "D", "E" }).SetName("Lv04_WhileLoopStep(\"A\", \"B\", \"C\", \"D\", \"E\")"),
            new TestCaseData((object)new[] { "Mark I", "Mark II", "Mark III", "Mark IV", "Mark V", "Mark VI" }).SetName("Lv04_WhileLoopStep(\"Mark I\", \"Mark II\", \"Mark III\", \"Mark IV\", \"Mark V\", \"Mark VI\")"),
            new TestCaseData((object)new[] { "s0", "s1", "s2", "s3", "s4", "s5", "s6" }).SetName("Lv04_WhileLoopStep(\"s0\", \"s1\", \"s2\", \"s3\", \"s4\", \"s5\", \"s6\")"),
        };

        [TestCaseSource(nameof(StepCases))]
        public void Lv04_WhileLoopStep(string[] suites)
        {
            assignment.Lv04_WhileLoopStep(suites);
            TestUtils.AssertMultilineEqual(ExpectedStepOutput(suites), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv04_WhileLoopStep", requireWhile: true, minLoops: 2);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 15)]
        [TestCase(10, 55)]
        [TestCase(100, 5050)]
        public void Lv05_WhileLoopSum(int n, int expectedSum)
        {
            assignment.Lv05_WhileLoopSum(n);
            TestUtils.AssertMultilineEqual($"Sum of n from 0 to {n} is {expectedSum}", SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv05_WhileLoopSum", requireWhile: true);
        }

        // ================= Level 2: Moderate (Ex01 - Ex04) =================

        static readonly TestCaseData[] HealCases =
        {
            new TestCaseData(new[] { 100, 80, 60, 40 }, 10, 1).SetName("Ex01_HealTarget([100, 80, 60, 40], heal: 10, target: 1)"),
            new TestCaseData(new[] { 50, 50, 50 }, 5, 2).SetName("Ex01_HealTarget([50, 50, 50], heal: 5, target: 2)"),
            new TestCaseData(new[] { 1, 1, 1, 1, 1 }, 99, 3).SetName("Ex01_HealTarget([1, 1, 1, 1, 1], heal: 99, target: 3)"),
            new TestCaseData(new[] { 500 }, 250, 0).SetName("Ex01_HealTarget([500], heal: 250, target: 0)"),
            new TestCaseData(new[] { 20, 30, 40, 50, 60, 70 }, 15, 4).SetName("Ex01_HealTarget([20, 30, 40, 50, 60, 70], heal: 15, target: 4)"),
        };

        [TestCaseSource(nameof(HealCases))]
        public void Ex01_HealTarget(int[] hp, int heal, int target)
        {
            assignment.Ex01_HealTarget((int[])hp.Clone(), heal, target);

            int last = hp.Length - 1;
            int[] sim = (int[])hp.Clone();
            var exp = new StringBuilder();
            sim[0] += heal; exp.AppendLine($"FirstEnemy hp :{sim[0]}");
            sim[last] += heal; exp.AppendLine($"LastEnemy hp :{sim[last]}");
            sim[target] += heal; exp.AppendLine($"TargetEnemy {target} hp :{sim[target]}");

            TestUtils.AssertMultilineEqual(exp.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void Ex02_RandomDialogue_ActuallyRandom()
        {
            string[] dialogues =
            {
                "Nice weather today, isn't it?",
                "I heard there are monsters in the cave.",
                "Welcome, traveler!",
                "Have you seen my cat?",
                "The blacksmith needs more coal."
            };
            var seen = new System.Collections.Generic.HashSet<string>();

            for (int seed = 1; seed <= 25; seed++)
            {
                SimpleDebugConsole.Clear();
                Random.InitState(seed);
                assignment.Ex02_RandomDialogue(dialogues);
                string output = SimpleDebugConsole.GetOutput().Trim();
                CollectionAssert.Contains(dialogues, output, $"seed {seed}: บทสนทนาไม่อยู่ใน array");
                seen.Add(output);
            }

            Assert.Greater(seen.Count, 1, "สุ่ม 25 รอบได้บทสนทนาเดิมทุกครั้ง");
            AssertBodyContains("Ex02_RandomDialogue", "Random.Range", "ต้องใช้ Random.Range");
        }

        static readonly TestCaseData[] SpawnCases =
        {
            new TestCaseData((object)new[] { 10, 20, 30 }).SetName("Ex03_InstantiateEnemies([10, 20, 30])"),
            new TestCaseData((object)new[] { 5 }).SetName("Ex03_InstantiateEnemies([5])"),
            new TestCaseData((object)new[] { 1, 2, 3, 4, 5 }).SetName("Ex03_InstantiateEnemies([1, 2, 3, 4, 5])"),
            new TestCaseData((object)new[] { 100, 90, 80, 70, 60, 50 }).SetName("Ex03_InstantiateEnemies([100, 90, 80, 70, 60, 50])"),
        };

        [TestCaseSource(nameof(SpawnCases))]
        public void Ex03_InstantiateEnemies_PositionsIncrementByOne(int[] hpEnemy)
        {
            var enemy = new GameObject("Goblin");

            assignment.Ex03_InstantiateEnemies(enemy, hpEnemy);

            var sb = new StringBuilder();
            for (int i = 0; i < hpEnemy.Length; i++)
                sb.AppendLine($"new enemy at position x = {i + 1}");
            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());

            var xs = new System.Collections.Generic.List<float>();
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
                if (go.name == "Goblin(Clone)") xs.Add(go.transform.position.x);

            Assert.AreEqual(hpEnemy.Length, xs.Count, "จำนวน enemy ที่ Instantiate ไม่ตรงกับขนาด HpEnemy");
            xs.Sort();
            for (int i = 0; i < xs.Count; i++)
                Assert.AreEqual(i + 1, xs[i], 0.0001f, $"enemy ตัวที่ {i} ควรอยู่ที่ x={i + 1} แต่อยู่ที่ {xs[i]}");

            Object.DestroyImmediate(enemy);
            AssertUsesRealLoop("Ex03_InstantiateEnemies");
            AssertBodyContains("Ex03_InstantiateEnemies", "Instantiate", "ต้อง Instantiate ศัตรูจริง");
        }

        [TestCase(10f, 3f, 3)]
        [TestCase(5f, 2f, 4)]
        [TestCase(20f, 5f, 3)]
        [TestCase(10f, 10f, 10)]
        public void Ex04_MoveToTarget(float speed, float targetX, int expectedSteps)
        {
            var target = new GameObject("Target");
            target.transform.position = new Vector3(targetX, 0f, 0f);

            assignment.Ex04_MoveToTarget(target.transform, speed);

            var sb = new StringBuilder();
            float x = 0f;
            int steps = 0;
            while (x < targetX && steps < 100000)
            {
                x += speed * 0.1f;
                sb.AppendLine(x.ToString("F2"));
                steps++;
            }

            Assert.AreEqual(expectedSteps, steps, "จำนวนรอบที่จำลองไม่ตรงกับที่คาด (เช็คค่า test case)");
            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            Assert.GreaterOrEqual(testGo.transform.position.x, targetX);

            Object.DestroyImmediate(target);
            AssertUsesRealLoop("Ex04_MoveToTarget");
            AssertBodyContains("Ex04_MoveToTarget", "Translate", "ต้องเคลื่อนที่ด้วย transform.Translate");
        }

        private static string ExpectedStepOutput(string[] suites)
        {
            var sb = new StringBuilder();
            sb.AppendLine("======Log by One======");
            for (int i = 0; i < suites.Length; i++) sb.AppendLine(suites[i]);
            sb.AppendLine("======Log by Two======");
            for (int i = 0; i < suites.Length; i += 2) sb.AppendLine(suites[i]);
            return sb.ToString();
        }
    }
}
