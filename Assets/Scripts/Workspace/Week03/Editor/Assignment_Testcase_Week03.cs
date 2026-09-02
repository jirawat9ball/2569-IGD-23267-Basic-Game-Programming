using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using AssignmentSystem.Services;
using NUnit.Framework;
using UnityEngine;

namespace Week03
{
    /// <summary>
    /// ตัวตรวจ Week 03 — ตรวจ 2 ชั้น:
    /// (1) output ต้องตรงเป๊ะ  (2) กัน hardcode: ยิง input หลากหลาย + ตรวจ source ว่าใช้ for/while จริง
    /// </summary>
    public class Assignment_Testcase_Week03
    {
        private const string StudentPath = "Assets/Scripts/Workspace/Week03/Assignment_Student_Week03.cs";

        private IAssignment assignment;
        private GameObject testGo;

        [SetUp]
        public void Setup()
        {
            testGo = new GameObject("Week03_TestRunner");
            assignment = testGo.AddComponent<Assignment_Student_Week03>();
            AssignmentDebugConsole.Clear();
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

        // ---------- helpers ----------

        private static void AssertOutput(string expected, string actual)
        {
            string normExpected = expected.Replace("\r\n", "\n").Replace("\r", "\n").Trim();
            string normActual = actual.Replace("\r\n", "\n").Replace("\r", "\n").Trim();
            Assert.AreEqual(normExpected, normActual,
                $"Expected output:\n{normExpected}\n----\nActual output:\n{normActual}");
        }

        /// <summary>ดึงบอดี้ของเมธอดจาก source ของ student (ตัด comment + string literal ออก)</summary>
        private static string GetStudentMethodBody(string methodName)
        {
            Assert.IsTrue(File.Exists(StudentPath),
                $"หาไฟล์ student ไม่เจอที่ '{StudentPath}' (cwd={Directory.GetCurrentDirectory()})");

            string src = File.ReadAllText(StudentPath);

            // ตัด comment + string literal ทิ้งก่อน เพื่อไม่ให้ { } ในนั้นทำ brace matching เพี้ยน
            src = Regex.Replace(src, @"//.*?$", "", RegexOptions.Multiline);
            src = Regex.Replace(src, @"/\*.*?\*/", "", RegexOptions.Singleline);
            src = Regex.Replace(src, "\"([^\"\\\\]|\\\\.)*\"", "\"\"");
            src = Regex.Replace(src, "'([^'\\\\]|\\\\.)*'", "' '");

            int sig = src.IndexOf("public void " + methodName, StringComparison.Ordinal);
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

        private static void AssertUsesRealLoop(string methodName, bool requireWhile = false, int minLoops = 1)
        {
            string body = GetStudentMethodBody(methodName);

            int forCount = Regex.Matches(body, @"\bfor\s*\(").Count;
            int foreachCount = Regex.Matches(body, @"\bforeach\s*\(").Count;
            int whileCount = Regex.Matches(body, @"\bwhile\s*\(").Count;

            if (requireWhile)
            {
                Assert.GreaterOrEqual(whileCount, minLoops,
                    $"{methodName}: ต้องใช้ while loop จริงอย่างน้อย {minLoops} ลูป (ห้าม hardcode พิมพ์ทีละบรรทัด)");
            }
            else
            {
                Assert.GreaterOrEqual(forCount + foreachCount + whileCount, minLoops,
                    $"{methodName}: ต้องใช้ลูป (for/while) จริงอย่างน้อย {minLoops} ลูป (ห้าม hardcode พิมพ์ทีละบรรทัด)");
            }
        }

        private static void AssertBodyContains(string methodName, string needle, string reason)
        {
            string body = GetStudentMethodBody(methodName);
            StringAssert.Contains(needle, body, $"{methodName}: {reason}");
        }

        // ================= Array (ข้อ 1-6) =================

        [Test]
        public void Ex01_IronManSuit()
        {
            assignment.Ex01_IronManSuit();

            var sb = new StringBuilder();
            sb.AppendLine("TonyStark Wear : Mark I");
            sb.AppendLine("Room size IronManSuit : 7");
            sb.AppendLine("===All suit in collection===");
            foreach (var s in new[] { "Mark I", "Mark II", "Mark III", "Mark IV", "Mark V", "Mark VI", "Mark VII" })
                sb.AppendLine(s);

            AssertOutput(sb.ToString(), AssignmentDebugConsole.GetOutput());
            AssertUsesRealLoop("Ex01_IronManSuit");
        }

        [Test]
        public void Ex02_SpiderManAndBatMan()
        {
            assignment.Ex02_SpiderManAndBatMan();

            var sb = new StringBuilder();
            sb.AppendLine("Room size spiderMan : 5");
            sb.AppendLine("===All spiderMan in collection===");
            foreach (var s in new[] { "Classic SpiderMan", "Symbiote SpiderMan", "Iron Spider", "Miles Morales", "Spider-Man 2099" })
                sb.AppendLine(s);
            sb.AppendLine("Room size BatMan : 4");
            sb.AppendLine("===All BatMan in collection===");
            foreach (var s in new[] { "Classic BatMan", "Dark Knight", "Batman Beyond", "The Batman" })
                sb.AppendLine(s);

            AssertOutput(sb.ToString(), AssignmentDebugConsole.GetOutput());
            AssertUsesRealLoop("Ex02_SpiderManAndBatMan", minLoops: 2);
        }

        static readonly object[] AttackCases =
        {
            new object[] { new[] { 100, 80, 60, 40 }, 10, 2 },
            new object[] { new[] { 50, 50, 50 }, 5, 1 },
            new object[] { new[] { 200, 150, 120, 90, 30 }, 25, 3 },
            new object[] { new[] { 10, 10 }, 3, 0 },
            new object[] { new[] { 1000 }, 100, 0 },
            new object[] { new[] { 7, 8, 9, 10, 11, 12, 13 }, 4, 5 },
        };

        [TestCaseSource(nameof(AttackCases))]
        public void Ex03_AttackTarget(int[] hp, int damage, int target)
        {
            assignment.Ex03_AttackTarget((int[])hp.Clone(), damage, target);

            int last = hp.Length - 1;
            int[] sim = (int[])hp.Clone();
            var exp = new StringBuilder();
            sim[0] -= damage; exp.AppendLine($"FirstEnemy hp :{sim[0]}");
            sim[last] -= damage; exp.AppendLine($"LastEnemy hp :{sim[last]}");
            sim[target] -= damage; exp.AppendLine($"TargetEnemy {target} hp :{sim[target]}");

            AssertOutput(exp.ToString(), AssignmentDebugConsole.GetOutput());
        }

        [Test]
        public void Ex04_RandomItemDrop_UsesRandomAndInstantiate()
        {
            string[] names = { "Potion", "Sword", "Shield", "Bow", "Ring" };
            var picks = new System.Collections.Generic.HashSet<string>();

            for (int seed = 1; seed <= 25; seed++)
            {
                AssignmentDebugConsole.Clear();
                Random.InitState(seed);
                var items = new GameObject[names.Length];
                for (int i = 0; i < names.Length; i++) items[i] = new GameObject(names[i]);

                assignment.Ex04_RandomItemDrop(items);

                string output = AssignmentDebugConsole.GetOutput().Trim();
                Assert.IsTrue(output.StartsWith("Got item: "), $"seed {seed}: ต้องขึ้นต้นด้วย 'Got item: ' แต่ได้ '{output}'");
                string picked = output.Substring("Got item: ".Length);
                CollectionAssert.Contains(names, picked, $"seed {seed}: ชื่อไอเทมไม่อยู่ใน array");
                picks.Add(picked);

                foreach (var go in items) Object.DestroyImmediate(go);
                foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
                    if (go.name.Contains("(Clone)")) Object.DestroyImmediate(go);
            }

            Assert.Greater(picks.Count, 1, "สุ่ม 25 รอบได้ผลเดิมทุกครั้ง — น่าจะ hardcode index ไม่ได้สุ่มจริง");
            AssertBodyContains("Ex04_RandomItemDrop", "Random.Range", "ต้องใช้ Random.Range ในการสุ่ม");
            AssertBodyContains("Ex04_RandomItemDrop", "Instantiate", "ต้อง Instantiate ไอเทมที่สุ่มได้");
        }

        static readonly object[] HealCases =
        {
            new object[] { new[] { 100, 80, 60, 40 }, 10, 1 },
            new object[] { new[] { 50, 50, 50 }, 5, 2 },
            new object[] { new[] { 1, 1, 1, 1, 1 }, 99, 3 },
            new object[] { new[] { 500 }, 250, 0 },
            new object[] { new[] { 20, 30, 40, 50, 60, 70 }, 15, 4 },
        };

        [TestCaseSource(nameof(HealCases))]
        public void Ex05_HealTarget(int[] hp, int heal, int target)
        {
            assignment.Ex05_HealTarget((int[])hp.Clone(), heal, target);

            int last = hp.Length - 1;
            int[] sim = (int[])hp.Clone();
            var exp = new StringBuilder();
            sim[0] += heal; exp.AppendLine($"FirstEnemy hp :{sim[0]}");
            sim[last] += heal; exp.AppendLine($"LastEnemy hp :{sim[last]}");
            sim[target] += heal; exp.AppendLine($"TargetEnemy {target} hp :{sim[target]}");

            AssertOutput(exp.ToString(), AssignmentDebugConsole.GetOutput());
        }

        [Test]
        public void Ex06_RandomDialogue_ActuallyRandom()
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
                AssignmentDebugConsole.Clear();
                Random.InitState(seed);
                assignment.Ex06_RandomDialogue(dialogues);
                string output = AssignmentDebugConsole.GetOutput().Trim();
                CollectionAssert.Contains(dialogues, output, $"seed {seed}: บทสนทนาไม่อยู่ใน array");
                seen.Add(output);
            }

            Assert.Greater(seen.Count, 1, "สุ่ม 25 รอบได้บทสนทนาเดิมทุกครั้ง — น่าจะไม่ได้สุ่มจริง");
            AssertBodyContains("Ex06_RandomDialogue", "Random.Range", "ต้องใช้ Random.Range");
        }

        // ================= For Loop (ข้อ 7-10) =================

        [Test]
        public void Ex07_ForLoopBasic()
        {
            assignment.Ex07_ForLoopBasic();

            var sb = new StringBuilder();
            for (int i = 0; i < 10; i++) sb.AppendLine($"<10 : {i}");
            sb.AppendLine("======================");
            for (int i = 1; i <= 10; i++) sb.AppendLine($"<=10 : {i}");

            AssertOutput(sb.ToString(), AssignmentDebugConsole.GetOutput());
            AssertUsesRealLoop("Ex07_ForLoopBasic", minLoops: 2);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(50)]
        [TestCase(137)]
        public void Ex08_ForLoopN(int n)
        {
            assignment.Ex08_ForLoopN(n);

            var sb = new StringBuilder();
            for (int i = 0; i < n; i++) sb.AppendLine(i.ToString());

            AssertOutput(sb.ToString(), AssignmentDebugConsole.GetOutput());
            if (n > 0) AssertUsesRealLoop("Ex08_ForLoopN");
        }

        static readonly object[] StepCases =
        {
            new object[] { new[] { "A", "B", "C", "D" } },
            new object[] { new[] { "A", "B", "C", "D", "E" } },
            new object[] { new[] { "Mark I", "Mark II", "Mark III", "Mark IV", "Mark V", "Mark VI" } },
            new object[] { new[] { "s0", "s1", "s2", "s3", "s4", "s5", "s6" } },
        };

        [TestCaseSource(nameof(StepCases))]
        public void Ex09_ForLoopStep(string[] suites)
        {
            assignment.Ex09_ForLoopStep(suites);
            AssertOutput(ExpectedStepOutput(suites), AssignmentDebugConsole.GetOutput());
            AssertUsesRealLoop("Ex09_ForLoopStep", minLoops: 2);
        }

        [TestCase(2)]
        [TestCase(7)]
        [TestCase(9)]
        [TestCase(12)]
        [TestCase(0)]
        [TestCase(-3)]
        public void Ex10_MultiplicationTable(int n)
        {
            assignment.Ex10_MultiplicationTable(n);

            var sb = new StringBuilder();
            for (int i = 1; i <= 12; i++) sb.AppendLine($"{n} x {i} = {n * i}");

            AssertOutput(sb.ToString(), AssignmentDebugConsole.GetOutput());
            AssertUsesRealLoop("Ex10_MultiplicationTable");
        }

        // ================= While Loop (ข้อ 11-14) =================

        [Test]
        public void Ex11_WhileLoopBasic()
        {
            assignment.Ex11_WhileLoopBasic();

            var sb = new StringBuilder();
            for (int i = 0; i < 10; i++) sb.AppendLine($"while loop : {i}");

            AssertOutput(sb.ToString(), AssignmentDebugConsole.GetOutput());
            AssertUsesRealLoop("Ex11_WhileLoopBasic", requireWhile: true);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(50)]
        [TestCase(137)]
        public void Ex12_WhileLoopN(int n)
        {
            assignment.Ex12_WhileLoopN(n);

            var sb = new StringBuilder();
            for (int i = 0; i < n; i++) sb.AppendLine(i.ToString());

            AssertOutput(sb.ToString(), AssignmentDebugConsole.GetOutput());
            if (n > 0) AssertUsesRealLoop("Ex12_WhileLoopN", requireWhile: true);
        }

        [TestCaseSource(nameof(StepCases))]
        public void Ex13_WhileLoopStep(string[] suites)
        {
            assignment.Ex13_WhileLoopStep(suites);
            AssertOutput(ExpectedStepOutput(suites), AssignmentDebugConsole.GetOutput());
            AssertUsesRealLoop("Ex13_WhileLoopStep", requireWhile: true, minLoops: 2);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 15)]
        [TestCase(10, 55)]
        [TestCase(100, 5050)]
        public void Ex14_WhileLoopSum(int n, int expectedSum)
        {
            assignment.Ex14_WhileLoopSum(n);
            AssertOutput($"ผลรวมของ n จาก 0 ถึง {n} คือ {expectedSum}", AssignmentDebugConsole.GetOutput());
            AssertUsesRealLoop("Ex14_WhileLoopSum", requireWhile: true);
        }

        // ================= Instantiate & Translate (ข้อ 15-16) =================

        static readonly object[] SpawnCases =
        {
            new object[] { new[] { 10, 20, 30 } },
            new object[] { new[] { 5 } },
            new object[] { new[] { 1, 2, 3, 4, 5 } },
            new object[] { new[] { 100, 90, 80, 70, 60, 50 } },
        };

        [TestCaseSource(nameof(SpawnCases))]
        public void Ex15_InstantiateEnemies_PositionsIncrementByOne(int[] hpEnemy)
        {
            var enemy = new GameObject("Goblin");

            assignment.Ex15_InstantiateEnemies(enemy, hpEnemy);

            var sb = new StringBuilder();
            for (int i = 0; i < hpEnemy.Length; i++)
                sb.AppendLine($"new enemy at position x = {i + 1}");
            AssertOutput(sb.ToString(), AssignmentDebugConsole.GetOutput());

            var xs = new System.Collections.Generic.List<float>();
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
                if (go.name == "Goblin(Clone)") xs.Add(go.transform.position.x);

            Assert.AreEqual(hpEnemy.Length, xs.Count, "จำนวน enemy ที่ Instantiate ไม่ตรงกับขนาด HpEnemy");
            xs.Sort();
            for (int i = 0; i < xs.Count; i++)
                Assert.AreEqual(i + 1, xs[i], 0.0001f, $"enemy ตัวที่ {i} ควรอยู่ที่ x={i + 1} แต่อยู่ที่ {xs[i]}");

            Object.DestroyImmediate(enemy);
            AssertUsesRealLoop("Ex15_InstantiateEnemies");
            AssertBodyContains("Ex15_InstantiateEnemies", "Instantiate", "ต้อง Instantiate ศัตรูจริง");
        }

        [TestCase(10f, 3f, 3)]
        [TestCase(5f, 2f, 4)]
        [TestCase(20f, 5f, 3)]
        [TestCase(10f, 10f, 10)]
        public void Ex16_MoveToTarget(float speed, float targetX, int expectedSteps)
        {
            var target = new GameObject("Target");
            target.transform.position = new Vector3(targetX, 0f, 0f);

            assignment.Ex16_MoveToTarget(target.transform, speed);

            // จำลอง float accumulation แบบเดียวกับที่ student ทำ
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
            AssertOutput(sb.ToString(), AssignmentDebugConsole.GetOutput());
            Assert.GreaterOrEqual(testGo.transform.position.x, targetX);

            Object.DestroyImmediate(target);
            AssertUsesRealLoop("Ex16_MoveToTarget");
            AssertBodyContains("Ex16_MoveToTarget", "Translate", "ต้องเคลื่อนที่ด้วย transform.Translate");
        }

        // ---------- shared ----------

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
