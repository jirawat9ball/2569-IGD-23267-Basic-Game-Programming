using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using NUnit.Framework;
using UnityEngine;

using Week04;
using SimpleDebugConsole = Workspace.Core.SimpleDebugConsole;

namespace Week04_Array2D
{
    public class TestBase
    {
        // =========================================================================================
        // 🎯 สลับตรวจไฟล์ อ. หรือ นักเรียน: เปลี่ยนเป็น true เมื่อต้องการตรวจไฟล์เฉลยอาจารย์
        // =========================================================================================
        protected const bool isTeacherMode = false;

        protected const string StudentPath = "Assets/Scripts/Workspace/Week04/Assignment_Student_Week04.cs";
        protected const string TeacherPath = "Assets/Scripts/Workspace/Teacher/Assignment_Teacher_Week04.cs";

        protected static string CurrentTargetFilePath => isTeacherMode ? TeacherPath : StudentPath;

        protected IAssignment assignment;
        protected GameObject testGo;

        [SetUp]
        public void Setup()
        {
            testGo = new GameObject("Week04_TestRunner");
            if (isTeacherMode)
                assignment = testGo.AddComponent<Assignment_Teacher_Week04>();
            else
                assignment = testGo.AddComponent<Assignment_Student_Week04>();
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

        protected static int CountClones()
        {
            int n = 0;
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (go != null && go.name.Contains("(Clone)")) n++;
            }
            return n;
        }

        protected static void DestroyAllClones()
        {
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (go != null && go.name.Contains("(Clone)")) Object.DestroyImmediate(go);
            }
        }

        protected static GameObject[] MakeItems(params string[] names)
        {
            var items = new GameObject[names.Length];
            for (int i = 0; i < names.Length; i++) items[i] = new GameObject(names[i]);
            return items;
        }

        protected static void DestroyItems(GameObject[] items)
        {
            foreach (var go in items)
            {
                if (go != null) Object.DestroyImmediate(go);
            }
        }

        protected static string GetStudentMethodBody(string methodName)
        {
            string path = CurrentTargetFilePath;
            Assert.IsTrue(File.Exists(path),
                $"หาไฟล์เป้าหมายไม่เจอที่ '{path}' (cwd={Directory.GetCurrentDirectory()})");

            string src = File.ReadAllText(path);
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

        protected static void AssertUsesRealLoop(string methodName, int minLoops = 1)
        {
            string body = GetStudentMethodBody(methodName);
            int loops = Regex.Matches(body, @"\bfor\s*\(").Count
                      + Regex.Matches(body, @"\bforeach\s*\(").Count
                      + Regex.Matches(body, @"\bwhile\s*\(").Count;

            Assert.GreaterOrEqual(loops, minLoops,
                $"{methodName}: ต้องใช้ลูปจริงอย่างน้อย {minLoops} ลูป (ห้าม hardcode พิมพ์ทีละบรรทัด)");
        }

        protected static void AssertBodyContains(string methodName, string needle, string reason)
        {
            StringAssert.Contains(needle, GetStudentMethodBody(methodName), $"{methodName}: {reason}");
        }
    }

    public class Lecture : TestBase
    {
        // ============ Lecture (As01 - As09) ============

        [Test]
        public void As01_Create2DArray()
        {
            assignment.As01_Create2DArray();

            var sb = new StringBuilder();
            sb.AppendLine("1 2 3");
            sb.AppendLine("4 5 6");
            sb.AppendLine("7 8 9");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [TestCase(3, 5)]
        [TestCase(4, 8)]
        [TestCase(1, 1)]
        [TestCase(2, 7)]
        [TestCase(10, 3)]
        public void As02_ArraySize(int rows, int cols)
        {
            assignment.As02_ArraySize(rows, cols);
            TestUtils.AssertMultilineEqual($"rows = {rows}\ncols = {cols}", SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As03_GetSet2DArray()
        {
            assignment.As03_GetSet2DArray();

            const string sep = "============================";
            var sb = new StringBuilder();
            sb.AppendLine("get : 6");
            sb.AppendLine("set : 70");
            sb.AppendLine(sep);
            sb.AppendLine("1 2 3");
            sb.AppendLine("4 5 70");
            sb.AppendLine("7 8 9");
            sb.AppendLine(sep);
            sb.AppendLine("get : C");
            sb.AppendLine("set : Cat");
            sb.AppendLine(sep);
            sb.AppendLine("A B Cat");
            sb.AppendLine("D E F");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("As03_GetSet2DArray", minLoops: 2);
        }

        [TestCase(5)]
        [TestCase(3)]
        [TestCase(1)]
        [TestCase(8)]
        public void As04_CreateWallRow(int columns)
        {
            var wall = new GameObject("Wall");

            assignment.As04_CreateWallRow(columns, wall);

            TestUtils.AssertMultilineEqual(new string('*', columns), SimpleDebugConsole.GetOutput());
            Assert.AreEqual(columns, CountClones(), $"ต้อง Instantiate กำแพง {columns} ชิ้น");

            Object.DestroyImmediate(wall);
            AssertUsesRealLoop("As04_CreateWallRow", minLoops: 1);
            AssertBodyContains("As04_CreateWallRow", "Instantiate", "ต้อง Instantiate กำแพงจริง");
        }

        [Test]
        public void As05_CreateFloor()
        {
            string[] names = { "0", "1", "2" };
            var maps = new System.Collections.Generic.HashSet<string>();
            const int columns = 4, rows = 3;

            for (int seed = 1; seed <= 12; seed++)
            {
                SimpleDebugConsole.Clear();
                DestroyAllClones();
                Random.InitState(seed);
                var tiles = MakeItems(names);

                assignment.As05_CreateFloor(columns, rows, tiles);

                string output = SimpleDebugConsole.GetOutput().Trim();
                string[] lines = output.Replace("\r\n", "\n").Split('\n');

                Assert.AreEqual(rows, lines.Length, $"seed {seed}: ต้องพิมพ์ {rows} บรรทัด");
                foreach (var line in lines)
                {
                    Assert.AreEqual(columns, line.Length, $"seed {seed}: แต่ละบรรทัดต้องยาว {columns} ตัว — ได้ '{line}'");
                    foreach (char ch in line)
                        CollectionAssert.Contains(names, ch.ToString(), $"seed {seed}: เจอตัวอักษรแปลก '{ch}'");
                }
                Assert.AreEqual(columns * rows, CountClones(), $"seed {seed}: ต้อง Instantiate ครบทุกช่อง");

                maps.Add(output);
                DestroyItems(tiles);
                DestroyAllClones();
            }

            Assert.Greater(maps.Count, 1, "สุ่ม 12 รอบได้แผนที่เดิมทุกครั้ง — น่าจะไม่ได้สุ่มจริง");
            AssertUsesRealLoop("As05_CreateFloor", minLoops: 2);
            AssertBodyContains("As05_CreateFloor", "Random.Range", "ต้องสุ่มพื้นด้วย Random.Range");
            AssertBodyContains("As05_CreateFloor", "Instantiate", "ต้อง Instantiate พื้นทุกช่อง");
        }

        [TestCase(5, 3)]
        [TestCase(3, 3)]
        [TestCase(1, 1)]
        [TestCase(4, 2)]
        public void As06_CreateWall(int columns, int rows)
        {
            var wall = new GameObject("Wall");

            assignment.As06_CreateWall(columns, rows, wall);

            var sb = new StringBuilder();
            for (int y = -1; y <= rows; y++)
            {
                var line = new StringBuilder();
                for (int x = -1; x <= columns; x++)
                {
                    line.Append(x == -1 || x == columns || y == -1 || y == rows ? '*' : ' ');
                }
                sb.AppendLine(line.ToString());
            }

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());

            int expectedWalls = (columns + 2) * (rows + 2) - columns * rows;
            Assert.AreEqual(expectedWalls, CountClones(), "จำนวนกำแพงที่สร้างไม่ตรงกับขอบนอก");

            Object.DestroyImmediate(wall);
            AssertUsesRealLoop("As06_CreateWall", minLoops: 2);
            AssertBodyContains("As06_CreateWall", "Instantiate", "ต้อง Instantiate กำแพงจริง");
        }

        [TestCase(1, 2)]
        [TestCase(11, 31)]
        [TestCase(0, 0)]
        [TestCase(-3, 5)]
        public void As07_SetItemPosition(int x, int y)
        {
            var item = new GameObject("Item");

            assignment.As07_SetItemPosition(item.transform, x, y);

            TestUtils.AssertMultilineEqual(new Vector3(x, y, 0f).ToString(), SimpleDebugConsole.GetOutput());
            Assert.AreEqual(x, item.transform.position.x, 0.0001f);
            Assert.AreEqual(y, item.transform.position.y, 0.0001f);

            Object.DestroyImmediate(item);
        }

        [Test]
        public void As08_RandomFoodItem()
        {
            string[] names = { "Soda", "Hamburger" };
            const int columns = 5, rows = 5;
            var results = new System.Collections.Generic.HashSet<string>();

            for (int seed = 1; seed <= 15; seed++)
            {
                SimpleDebugConsole.Clear();
                DestroyAllClones();
                Random.InitState(seed);
                var tiles = MakeItems(names);

                assignment.As08_RandomFoodItem(columns, rows, tiles);

                string output = SimpleDebugConsole.GetOutput().Trim();
                var match = Regex.Match(output, @"^(\S+) at x: (\d+) y: (\d+)$");
                Assert.IsTrue(match.Success, $"seed {seed}: รูปแบบต้องเป็น '<ชื่อ> at x: <x> y: <y>' แต่ได้ '{output}'");

                CollectionAssert.Contains(names, match.Groups[1].Value, $"seed {seed}: ชื่อของไม่อยู่ใน foodTiles");
                int x = int.Parse(match.Groups[2].Value);
                int y = int.Parse(match.Groups[3].Value);
                Assert.IsTrue(x >= 0 && x < columns, $"seed {seed}: x ต้องอยู่ในช่วง 0-{columns - 1} แต่ได้ {x}");
                Assert.IsTrue(y >= 0 && y < rows, $"seed {seed}: y ต้องอยู่ในช่วง 0-{rows - 1} แต่ได้ {y}");
                Assert.AreEqual(1, CountClones(), $"seed {seed}: ต้อง Instantiate 1 ชิ้น");

                results.Add(output);
                DestroyItems(tiles);
                DestroyAllClones();
            }

            Assert.Greater(results.Count, 1, "สุ่ม 15 รอบได้ผลเดิมทุกครั้ง — น่าจะไม่ได้สุ่มจริง");
            AssertBodyContains("As08_RandomFoodItem", "Random.Range", "ต้องสุ่มด้วย Random.Range");
            AssertBodyContains("As08_RandomFoodItem", "Instantiate", "ต้อง Instantiate ของที่สุ่มได้");
        }

        static readonly object[] As09Cases =
        {
            new object[] { new string[] { "Soda", "Food", "Water" }, 1, 0, "Create Item Soda at x: 1 y: 0", 1 },
            new object[] { new string[] { "Soda", "Food", "Water" }, 2, 2, "Create Item Food at x: 2 y: 2", 1 },
            new object[] { new string[] { "Soda", "Food", "Water" }, 0, 0, "No items at x: 0 y: 0", 0 },
            new object[] { new string[] { "Water" }, 1, 0, "No items at x: 1 y: 0", 0 },
        };

        [TestCaseSource(nameof(As09Cases))]
        public void As09_CreateItemFromArray(string[] itemNames, int x, int y, string expectedOutput, int expectedClones)
        {
            var items = MakeItems(itemNames);

            assignment.As09_CreateItemFromArray(items, x, y);

            TestUtils.AssertMultilineEqual(expectedOutput, SimpleDebugConsole.GetOutput());
            Assert.AreEqual(expectedClones, CountClones(), expectedClones > 0 ? "ต้อง Instantiate ไอเทม 1 ชิ้น" : "ช่องว่างต้องไม่ Instantiate อะไร");

            DestroyItems(items);
            DestroyAllClones();
            if (expectedClones > 0)
            {
                AssertBodyContains("As09_CreateItemFromArray", "Instantiate", "ต้อง Instantiate ไอเทมที่เจอ");
            }
        }
    }

    public class Homework : TestBase
    {
        // ================= Level 1: Simple (Lv01 - Lv10) =================

        static readonly int[,] Matrix3x3 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        static readonly int[,] Matrix2x4 = { { 10, 20, 30, 40 }, { 1, 2, 3, 4 } };
        static readonly int[,] Matrix1x1 = { { 99 } };

        static readonly object[] RowCases =
        {
            new object[] { Matrix3x3, 0 },
            new object[] { Matrix3x3, 1 },
            new object[] { Matrix3x3, 2 },
            new object[] { Matrix2x4, 0 },
            new object[] { Matrix2x4, 1 },
            new object[] { Matrix1x1, 0 },
        };

        [TestCaseSource(nameof(RowCases))]
        public void Lv01_SumRow(int[,] matrix, int row)
        {
            assignment.Lv01_SumRow(matrix, row);

            int expected = 0;
            for (int c = 0; c < matrix.GetLength(1); c++) expected += matrix[row, c];

            TestUtils.AssertMultilineEqual(expected.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv01_SumRow");
        }

        static readonly object[] ColCases =
        {
            new object[] { Matrix3x3, 0 },
            new object[] { Matrix3x3, 1 },
            new object[] { Matrix3x3, 2 },
            new object[] { Matrix2x4, 3 },
            new object[] { Matrix1x1, 0 },
        };

        [TestCaseSource(nameof(ColCases))]
        public void Lv02_SumColumn(int[,] matrix, int col)
        {
            assignment.Lv02_SumColumn(matrix, col);

            int expected = 0;
            for (int r = 0; r < matrix.GetLength(0); r++) expected += matrix[r, col];

            TestUtils.AssertMultilineEqual(expected.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv02_SumColumn");
        }

        [TestCase(3, 4)]
        [TestCase(1, 1)]
        [TestCase(5, 2)]
        [TestCase(7, 7)]
        public void Lv03_StarPattern(int columns, int rows)
        {
            assignment.Lv03_StarPattern(columns, rows);

            var sb = new StringBuilder();
            for (int y = 0; y < rows; y++)
            {
                sb.AppendLine(new string('*', columns));
            }

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv03_StarPattern", minLoops: 2);
        }

        [TestCase(5)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(8)]
        public void Lv04_TrianglePattern(int size)
        {
            assignment.Lv04_TrianglePattern(size);

            var sb = new StringBuilder();
            for (int r = 1; r <= size; r++)
            {
                sb.AppendLine(new string('*', r));
            }

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv04_TrianglePattern", minLoops: 2);
        }

        [TestCase(2, 4)]
        [TestCase(2, 2)]
        [TestCase(5, 9)]
        [TestCase(1, 3)]
        public void Lv05_MultiplicationTableNested(int fromTable, int toTable)
        {
            assignment.Lv05_MultiplicationTableNested(fromTable, toTable);

            var sb = new StringBuilder();
            for (int i = 1; i <= 12; i++)
            {
                var line = new StringBuilder();
                for (int table = fromTable; table <= toTable; table++)
                {
                    line.Append($"{table} x {i} = {table * i}");
                    if (table < toTable) line.Append('\t');
                }
                sb.AppendLine(line.ToString());
            }

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv05_MultiplicationTableNested", minLoops: 2);
        }

        static readonly object[] MaxCases =
        {
            new object[] { Matrix3x3, 9, 2, 2 },
            new object[] { new int[,] { { 50, 20 }, { 10, 30 } }, 50, 0, 0 },
            new object[] { new int[,] { { 10, 80, 20 }, { 30, 40, 50 } }, 80, 0, 1 },
            new object[] { Matrix1x1, 99, 0, 0 },
            new object[] { new int[,] { { -10, -5 }, { -20, -1 } }, -1, 1, 1 },
        };

        [TestCaseSource(nameof(MaxCases))]
        public void Lv06_FindMaxInMatrix(int[,] matrix, int expectedMax, int expectedR, int expectedC)
        {
            assignment.Lv06_FindMaxInMatrix(matrix);

            TestUtils.AssertMultilineEqual($"Max value {expectedMax} at [{expectedR}, {expectedC}]", SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv06_FindMaxInMatrix");
        }

        static readonly object[] CountCases =
        {
            new object[] { new int[,] { { 1, 2, 1 }, { 3, 1, 4 }, { 1, 5, 1 } }, 1, 5 },
            new object[] { Matrix3x3, 5, 1 },
            new object[] { Matrix3x3, 99, 0 },
            new object[] { Matrix1x1, 99, 1 },
            new object[] { Matrix1x1, 0, 0 },
            new object[] { new int[,] { { 0, 0 }, { 0, 0 } }, 0, 4 },
        };

        [TestCaseSource(nameof(CountCases))]
        public void Lv07_CountTargetValue(int[,] matrix, int target, int expectedCount)
        {
            assignment.Lv07_CountTargetValue(matrix, target);

            TestUtils.AssertMultilineEqual($"Found target {target}: {expectedCount} cells", SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv07_CountTargetValue");
        }

        static readonly object[] SumAllCases =
        {
            new object[] { Matrix3x3, 45 },
            new object[] { Matrix2x4, 110 },
            new object[] { Matrix1x1, 99 },
            new object[] { new int[,] { { -5, 5 }, { -10, 10 } }, 0 },
        };

        [TestCaseSource(nameof(SumAllCases))]
        public void Lv08_SumAllElements(int[,] matrix, int expectedSum)
        {
            assignment.Lv08_SumAllElements(matrix);

            TestUtils.AssertMultilineEqual(expectedSum.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv08_SumAllElements");
        }

        [TestCase(4)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void Lv09_InvertedTrianglePattern(int size)
        {
            assignment.Lv09_InvertedTrianglePattern(size);

            var sb = new StringBuilder();
            for (int r = size; r >= 1; r--)
            {
                sb.AppendLine(new string('*', r));
            }

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv09_InvertedTrianglePattern", minLoops: 2);
        }

        static readonly object[] DiagonalCases =
        {
            new object[] { Matrix3x3, "1 5 9" },
            new object[] { Matrix2x4, "10 2" },
            new object[] { Matrix1x1, "99" },
            new object[] { new int[,] { { 7, 0, 0 }, { 0, 8, 0 }, { 0, 0, 9 } }, "7 8 9" },
        };

        [TestCaseSource(nameof(DiagonalCases))]
        public void Lv10_PrintMainDiagonal(int[,] matrix, string expectedOutput)
        {
            assignment.Lv10_PrintMainDiagonal(matrix);

            TestUtils.AssertMultilineEqual(expectedOutput, SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv10_PrintMainDiagonal");
        }

        // ================= Level 2: Moderate (Ex01 - Ex03) =================

        static readonly object[] TicTacToeCases =
        {
            // X ชนะแนวนอนแถวบน
            new object[] { new int[,] { { 0, 0 }, { 1, 0 }, { 0, 1 }, { 1, 1 }, { 0, 2 } } },
            // O ชนะแนวตั้งคอลัมน์กลาง
            new object[] { new int[,] { { 0, 0 }, { 0, 1 }, { 2, 2 }, { 1, 1 }, { 2, 0 }, { 2, 1 } } },
            // X ชนะแนวทแยง
            new object[] { new int[,] { { 0, 0 }, { 0, 1 }, { 1, 1 }, { 0, 2 }, { 2, 2 } } },
            // เสมอ (ลงครบ 9 ช่อง)
            new object[] { new int[,] { { 0, 0 }, { 0, 1 }, { 0, 2 }, { 1, 2 }, { 1, 0 }, { 2, 0 }, { 1, 1 }, { 2, 2 }, { 2, 1 } } },
            // ลงทับช่องเดิม
            new object[] { new int[,] { { 0, 0 }, { 0, 0 }, { 1, 1 }, { 0, 0 }, { 2, 2 } } },
            // เดินไม่จบเกม
            new object[] { new int[,] { { 1, 1 }, { 0, 0 } } },
        };

        [TestCaseSource(nameof(TicTacToeCases))]
        public void Ex01_TicTacToe(int[,] moves)
        {
            assignment.Ex01_TicTacToe(moves);
            TestUtils.AssertMultilineEqual(SimulateTicTacToe(moves), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Ex01_TicTacToe");
        }

        private static string SimulateTicTacToe(int[,] moves)
        {
            char[,] board = new char[3, 3];
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    board[r, c] = ' ';

            char current = 'X';
            int placed = 0;
            var sb = new StringBuilder();

            for (int m = 0; m < moves.GetLength(0); m++)
            {
                int row = moves[m, 0];
                int col = moves[m, 1];

                sb.AppendLine("Player " + current + ":");
                sb.AppendLine(row + " " + col);

                if (board[row, col] != ' ')
                {
                    sb.AppendLine("cannot set " + row + " " + col);
                    continue;
                }

                board[row, col] = current;
                placed++;

                for (int r = 0; r < 3; r++)
                {
                    sb.AppendLine("-------------");
                    sb.AppendLine("| " + board[r, 0] + " | " + board[r, 1] + " | " + board[r, 2] + " |");
                }

                if (HasWinner(board, current))
                {
                    sb.AppendLine(current + " wins!");
                    return sb.ToString();
                }

                if (placed == 9)
                {
                    sb.AppendLine("Draw!");
                    return sb.ToString();
                }

                current = (current == 'X') ? 'O' : 'X';
            }

            return sb.ToString();
        }

        private static bool HasWinner(char[,] board, char p)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == p && board[i, 1] == p && board[i, 2] == p) return true;
                if (board[0, i] == p && board[1, i] == p && board[2, i] == p) return true;
            }
            if (board[0, 0] == p && board[1, 1] == p && board[2, 2] == p) return true;
            if (board[0, 2] == p && board[1, 1] == p && board[2, 0] == p) return true;
            return false;
        }

        static readonly int[,] SampleMap =
        {
            { 1, 1, 1, 1 },
            { 1, 0, 0, 1 },
            { 1, 2, 0, 1 },
            { 1, 1, 1, 1 }
        };

        [TestCase(1, 1, "Position (1, 1) is Walkable")]
        [TestCase(2, 1, "Position (2, 1) is Walkable")]
        [TestCase(0, 0, "Position (0, 0) is Blocked by Wall")]
        [TestCase(3, 2, "Position (3, 2) is Blocked by Wall")]
        [TestCase(1, 2, "Position (1, 2) is Blocked")]
        [TestCase(-1, 0, "Position (-1, 0) is Out of Bounds")]
        [TestCase(4, 1, "Position (4, 1) is Out of Bounds")]
        [TestCase(1, -1, "Position (1, -1) is Out of Bounds")]
        [TestCase(1, 4, "Position (1, 4) is Out of Bounds")]
        public void Ex02_CheckWalkableTile(int targetX, int targetY, string expectedOutput)
        {
            assignment.Ex02_CheckWalkableTile(SampleMap, targetX, targetY);

            TestUtils.AssertMultilineEqual(expectedOutput, SimpleDebugConsole.GetOutput());
        }

        [TestCase(5, 5)]
        [TestCase(8, 6)]
        [TestCase(2, 2)]
        public void Ex03_SpawnChestsInCorners(int columns, int rows)
        {
            var chest = new GameObject("Chest");

            assignment.Ex03_SpawnChestsInCorners(columns, rows, chest);

            TestUtils.AssertMultilineEqual("Spawned 4 chests at corners", SimpleDebugConsole.GetOutput());
            Assert.AreEqual(4, CountClones(), "ต้อง Instantiate หีบสมบัติทั้ง 4 มุม");

            var clones = new System.Collections.Generic.List<Vector2>();
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (go != null && go.name.Contains("(Clone)"))
                {
                    clones.Add(new Vector2(go.transform.position.x, go.transform.position.y));
                }
            }

            Vector2[] expectedCorners = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(columns - 1, 0),
                new Vector2(0, rows - 1),
                new Vector2(columns - 1, rows - 1)
            };

            foreach (var corner in expectedCorners)
            {
                bool found = clones.Exists(c => Mathf.Approximately(c.x, corner.x) && Mathf.Approximately(c.y, corner.y));
                Assert.IsTrue(found, $"ไม่พบหีบที่มุม ({corner.x}, {corner.y})");
            }

            Object.DestroyImmediate(chest);
            DestroyAllClones();
            AssertBodyContains("Ex03_SpawnChestsInCorners", "Instantiate", "ต้อง Instantiate หีบสมบัติทั้ง 4 มุม");
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
