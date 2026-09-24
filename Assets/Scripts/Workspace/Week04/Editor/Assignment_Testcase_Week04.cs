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
            {
                assignment = testGo.AddComponent<Assignment_Teacher_Week04>();
            }
            else
            {
                assignment = testGo.AddComponent<Assignment_Student_Week04>();
            }
            SimpleDebugConsole.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            if (testGo != null)
                Object.DestroyImmediate(testGo);

            DestroyAllClones();
        }

        protected static int CountClones()
        {
            int n = 0;
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (go != null && (go.name.Contains("(Clone)") || go.name.StartsWith("Floor_"))) n++;
            }
            return n;
        }

        protected static void DestroyAllClones()
        {
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (go != null && (go.name.Contains("(Clone)") || go.name.StartsWith("Floor_"))) Object.DestroyImmediate(go);
            }
        }

        protected static System.Collections.Generic.List<GameObject> ClonesNamed(string prefabName)
        {
            var list = new System.Collections.Generic.List<GameObject>();
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (go != null && go.name.Contains("(Clone)") && (go.name == prefabName + "(Clone)" || go.name.StartsWith(prefabName))) list.Add(go);
            }
            return list;
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
            if (sig == -1) sig = src.IndexOf("void " + methodName, System.StringComparison.Ordinal);
            Assert.Greater(sig, -1, $"ไม่พบเมธอด {methodName} ในไฟล์ student");

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

        protected void InvokeStart()
        {
            var method = assignment.GetType().GetMethod("Start",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (method != null)
            {
                method.Invoke(assignment, null);
            }
        }

        protected static void SetFieldIfExists(object target, string fieldName, object value)
        {
            if (target == null) return;
            var field = target.GetType().GetField(fieldName,
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                if (field.FieldType == typeof(GameObject[]) && value is GameObject singleGo)
                {
                    field.SetValue(target, new GameObject[] { singleGo });
                }
                else if (field.FieldType == typeof(GameObject) && value is GameObject[] arr && arr.Length > 0)
                {
                    field.SetValue(target, arr[0]);
                }
                else
                {
                    field.SetValue(target, value);
                }
            }
        }

        protected void SetupMapParameters(int cols, int rows, GameObject[] walls, GameObject[] floors, GameObject item, int itemX, int itemY, GameObject[] food, GameObject player, GameObject exit)
        {
            var student = testGo.GetComponent<Assignment_Student_Week04>();
            if (student != null)
            {
                student.cols = cols;
                student.rows = rows;
                student.wall = walls;
                student.floorTiles = floors;
                student.Item = item;
                student.ItemPosX = itemX;
                student.ItemPosY = itemY;
                student.foodTiles = food;
                SetFieldIfExists(student, "player", player);
                SetFieldIfExists(student, "exitTile", exit);
            }
            var teacher = testGo.GetComponent<Assignment_Teacher_Week04>();
            if (teacher != null)
            {
                teacher.cols = cols;
                teacher.rows = rows;
                teacher.wall = walls;
                teacher.floorTiles = floors;
                teacher.Item = item;
                teacher.ItemPosX = itemX;
                teacher.ItemPosY = itemY;
                teacher.foodTiles = food;
                teacher.player = player != null ? new GameObject[] { player } : null;
                teacher.exitTile = exit;
            }
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
        // ============ Lecture (As01 - As12) ============

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
            TestUtils.AssertMultilineEqual($"rows = {rows}\ncols = {cols}\nlength = {rows * cols}", SimpleDebugConsole.GetOutput());
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

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertBodyContains("As03_GetSet2DArray", "As04_Print2DArray", "ต้องเรียกใช้ As04_Print2DArray ในการแสดงผล 2D Array");
        }

        // ============ As04: แสดงผล 2D Array ============
        [Test]
        public void As04_Print2DArray_Int()
        {
            int[,] array = { { 1, 2, 3 }, { 4, 5, 6 } };
            assignment.As04_Print2DArray(array);

            var sb = new StringBuilder();
            sb.AppendLine("1 2 3");
            sb.AppendLine("4 5 6");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("As04_Print2DArray");
        }

        [Test]
        public void As04_Print2DArray_String()
        {
            string[,] array = { { "A", "B" }, { "C", "D" } };
            assignment.As04_Print2DArray(array);

            var sb = new StringBuilder();
            sb.AppendLine("A B");
            sb.AppendLine("C D");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        // ============ As05: สร้างแถวกำแพง ============
        [TestCase(4, 3)]
        [TestCase(5, 4)]
        public void As05_Start_CreateWallRow(int cols, int rows)
        {
            var wallPrefabs = MakeItems("Wall");
            var floorPrefabs = MakeItems("Floor");
            var itemPrefab = new GameObject("KeyItem");
            var foodPrefabs = MakeItems("Soda", "Food");
            var playerPrefab = new GameObject("Player");
            var exitPrefab = new GameObject("Exit");

            SetupMapParameters(cols, rows, wallPrefabs, floorPrefabs, itemPrefab, 1, 1, foodPrefabs, playerPrefab, exitPrefab);
            InvokeStart();

            var wallClones = ClonesNamed("Wall");
            var rowZeroWalls = wallClones.FindAll(go => Mathf.Approximately(go.transform.position.y, 0f));
            Assert.GreaterOrEqual(rowZeroWalls.Count, cols,
                $"As05: Start() ต้องสร้างกำแพง 1 แถวที่ y = 0 อย่างน้อย {cols} ช่อง (x = 0 ถึง {cols - 1})");

            for (int x = 0; x < cols; x++)
            {
                bool found = rowZeroWalls.Exists(go => Mathf.Approximately(go.transform.position.x, (float)x));
                Assert.IsTrue(found, $"As05: ไม่พบกำแพงที่พิกัด ({x}, 0)");
            }

            DestroyItems(wallPrefabs);
            DestroyItems(floorPrefabs);
            DestroyItems(foodPrefabs);
            Object.DestroyImmediate(itemPrefab);
            Object.DestroyImmediate(playerPrefab);
            Object.DestroyImmediate(exitPrefab);
            DestroyAllClones();
        }

        // ============ As06: สร้างพื้นแผนที่ ============
        [TestCase(4, 3)]
        [TestCase(3, 4)]
        public void As06_Start_CreateFloor(int cols, int rows)
        {
            var wallPrefabs = MakeItems("Wall");
            var floorPrefabs = MakeItems("Floor");
            var itemPrefab = new GameObject("KeyItem");
            var foodPrefabs = MakeItems("Soda", "Food");
            var playerPrefab = new GameObject("Player");
            var exitPrefab = new GameObject("Exit");

            SetupMapParameters(cols, rows, wallPrefabs, floorPrefabs, itemPrefab, 1, 1, foodPrefabs, playerPrefab, exitPrefab);
            InvokeStart();

            int floorCount = 0;
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    var floorGo = GameObject.Find($"Floor_{x}_{y}");
                    if (floorGo != null)
                    {
                        Assert.AreEqual((float)x, floorGo.transform.position.x, 0.001f, $"As06: Floor_{x}_{y} พิกัด x ต้องอยู่ที่ {x}");
                        Assert.AreEqual((float)y, floorGo.transform.position.y, 0.001f, $"As06: Floor_{x}_{y} พิกัด y ต้องอยู่ที่ {y}");
                        floorCount++;
                    }
                }
            }
            Assert.AreEqual(cols * rows, floorCount,
                $"As06: Start() ต้องสร้างแผ่นพื้น Floor_{{x}}_{{y}} ครบทุกช่อง ({cols * rows} ช่อง)");

            string output = SimpleDebugConsole.GetOutput();
            Assert.IsTrue(output.Contains("Floor"), "As06: ต้องพิมพ์ชื่อแผ่นพื้นของแต่ละแถวออกทาง Debug.Log");

            DestroyItems(wallPrefabs);
            DestroyItems(floorPrefabs);
            DestroyItems(foodPrefabs);
            Object.DestroyImmediate(itemPrefab);
            Object.DestroyImmediate(playerPrefab);
            Object.DestroyImmediate(exitPrefab);
            DestroyAllClones();
        }

        // ============ As07: สร้างกำแพงล้อมรอบแผนที่ ============
        [TestCase(4, 3)]
        [TestCase(5, 5)]
        public void As07_Start_CreateWall(int cols, int rows)
        {
            var wallPrefabs = MakeItems("Wall");
            var floorPrefabs = MakeItems("Floor");
            var itemPrefab = new GameObject("KeyItem");
            var foodPrefabs = MakeItems("Soda", "Food");
            var playerPrefab = new GameObject("Player");
            var exitPrefab = new GameObject("Exit");

            SetupMapParameters(cols, rows, wallPrefabs, floorPrefabs, itemPrefab, 1, 1, foodPrefabs, playerPrefab, exitPrefab);
            InvokeStart();

            var wallClones = ClonesNamed("Wall");
            int expectedBorderWallCount = 2 * (cols + rows) + 4;
            int borderWallCount = wallClones.FindAll(go =>
                Mathf.Approximately(go.transform.position.x, -1f) ||
                Mathf.Approximately(go.transform.position.x, (float)cols) ||
                Mathf.Approximately(go.transform.position.y, -1f) ||
                Mathf.Approximately(go.transform.position.y, (float)rows)).Count;

            Assert.GreaterOrEqual(borderWallCount, expectedBorderWallCount,
                $"As07: Start() ต้องสร้างกำแพงล้อมรอบแผนที่ครบทุกช่อง ({expectedBorderWallCount} ช่อง) แต่พบ {borderWallCount} ช่อง");

            for (int y = -1; y <= rows; y++)
            {
                for (int x = -1; x <= cols; x++)
                {
                    if (x == -1 || x == cols || y == -1 || y == rows)
                    {
                        bool found = wallClones.Exists(go =>
                            Mathf.Approximately(go.transform.position.x, (float)x) &&
                            Mathf.Approximately(go.transform.position.y, (float)y));
                        Assert.IsTrue(found, $"As07: ไม่พบกำแพงที่ขอบพิกัด ({x}, {y})");
                    }
                }
            }

            string output = SimpleDebugConsole.GetOutput();
            Assert.IsTrue(output.Contains("*"), "As07: ต้องพิมพ์ '*' ออกทาง Debug.Log สำหรับกำแพงขอบนอก");

            DestroyItems(wallPrefabs);
            DestroyItems(floorPrefabs);
            DestroyItems(foodPrefabs);
            Object.DestroyImmediate(itemPrefab);
            Object.DestroyImmediate(playerPrefab);
            Object.DestroyImmediate(exitPrefab);
            DestroyAllClones();
        }

        // ============ As08: วางไอเทมเดี่ยวตามพิกัด ============
        [TestCase(1, 1)]
        [TestCase(2, 0)]
        public void As08_Start_CreateItem(int itemX, int itemY)
        {
            var wallPrefabs = MakeItems("Wall");
            var floorPrefabs = MakeItems("Floor");
            var itemPrefab = new GameObject("KeyItem");
            var foodPrefabs = MakeItems("Soda", "Food");
            var playerPrefab = new GameObject("Player");
            var exitPrefab = new GameObject("Exit");

            SetupMapParameters(4, 3, wallPrefabs, floorPrefabs, itemPrefab, itemX, itemY, foodPrefabs, playerPrefab, exitPrefab);
            InvokeStart();

            var itemClones = ClonesNamed("KeyItem");
            Assert.AreEqual(1, itemClones.Count, "As08: Start() ต้อง Instantiate Item 1 ชิ้น");
            Assert.AreEqual((float)itemX, itemClones[0].transform.position.x, 0.0001f, $"As08: Item x ต้องอยู่ที่ {itemX}");
            Assert.AreEqual((float)itemY, itemClones[0].transform.position.y, 0.0001f, $"As08: Item y ต้องอยู่ที่ {itemY}");

            string output = SimpleDebugConsole.GetOutput();
            Assert.IsTrue(output.Contains($"{itemX}") && output.Contains($"{itemY}"),
                $"As08: ต้องพิมพ์ตำแหน่ง ({itemX}, {itemY}) ออกทาง Debug.Log");

            DestroyItems(wallPrefabs);
            DestroyItems(floorPrefabs);
            DestroyItems(foodPrefabs);
            Object.DestroyImmediate(itemPrefab);
            Object.DestroyImmediate(playerPrefab);
            Object.DestroyImmediate(exitPrefab);
            DestroyAllClones();
        }

        // ============ As09: สุ่มวางอาหาร ============
        [Test]
        public void As09_Start_RandomFoodItem()
        {
            var wallPrefabs = MakeItems("Wall");
            var floorPrefabs = MakeItems("Floor");
            var itemPrefab = new GameObject("KeyItem");
            var foodPrefabs = MakeItems("Soda", "Food");
            var playerPrefab = new GameObject("Player");
            var exitPrefab = new GameObject("Exit");

            SetupMapParameters(4, 3, wallPrefabs, floorPrefabs, itemPrefab, 1, 1, foodPrefabs, playerPrefab, exitPrefab);
            InvokeStart();

            string output = SimpleDebugConsole.GetOutput();
            var match = Regex.Match(output, @"(\S+)\s+at x:\s*(\d+)\s+y:\s*(\d+)");
            Assert.IsTrue(match.Success,
                $"As09: รูปแบบข้อความต้องเป็น '<ชื่ออาหาร> at x: <x> y: <y>' แต่ได้:\n{output}");

            string foodName = match.Groups[1].Value;
            int foodX = int.Parse(match.Groups[2].Value);
            int foodY = int.Parse(match.Groups[3].Value);

            Assert.IsTrue(foodName == "Soda" || foodName == "Food", $"As09: ชื่ออาหารต้องอยู่ใน foodTiles แต่ได้ '{foodName}'");
            Assert.IsTrue(foodX >= 0 && foodX < 4, $"As09: foodX ต้องอยู่ในช่วง 0 ถึง 3 แต่ได้ {foodX}");
            Assert.IsTrue(foodY >= 0 && foodY < 3, $"As09: foodY ต้องอยู่ในช่วง 0 ถึง 2 แต่ได้ {foodY}");

            var foodClones = ClonesNamed(foodName);
            bool hasFoodAtPos = foodClones.Exists(go =>
                Mathf.Approximately(go.transform.position.x, (float)foodX) &&
                Mathf.Approximately(go.transform.position.y, (float)foodY));
            Assert.IsTrue(hasFoodAtPos, $"As09: ต้อง Instantiate {foodName} ที่ตำแหน่งพิกัด x: {foodX} y: {foodY}");

            DestroyItems(wallPrefabs);
            DestroyItems(floorPrefabs);
            DestroyItems(foodPrefabs);
            Object.DestroyImmediate(itemPrefab);
            Object.DestroyImmediate(playerPrefab);
            Object.DestroyImmediate(exitPrefab);
            DestroyAllClones();
        }

        // ============ As10: สร้างไอเทมจาก 2D Array ============
        [Test]
        public void As10_Start_CreateItemFromArray()
        {
            var wallPrefabs = MakeItems("Wall");
            var floorPrefabs = MakeItems("Floor");
            var itemPrefab = new GameObject("KeyItem");
            var foodPrefabs = MakeItems("Soda", "Food");
            var playerPrefab = new GameObject("Player");
            var exitPrefab = new GameObject("Exit");

            SetupMapParameters(4, 3, wallPrefabs, floorPrefabs, itemPrefab, 1, 1, foodPrefabs, playerPrefab, exitPrefab);
            InvokeStart();

            var sodaClones = ClonesNamed("Soda");
            var foodClones = ClonesNamed("Food");
            Assert.GreaterOrEqual(sodaClones.Count, 1, "As10: Start() ต้องสร้างไอเทม Soda จาก 2D Array");
            Assert.GreaterOrEqual(foodClones.Count, 1, "As10: Start() ต้องสร้างไอเทม Food จาก 2D Array");

            bool hasSodaAtPos = sodaClones.Exists(go => Mathf.Approximately(go.transform.position.x, 1f) && Mathf.Approximately(go.transform.position.y, 0f));
            bool hasFoodAtPos = foodClones.Exists(go => Mathf.Approximately(go.transform.position.x, 2f) && Mathf.Approximately(go.transform.position.y, 2f));
            Assert.IsTrue(hasSodaAtPos, "As10: ต้องสร้าง Soda ที่ตำแหน่ง x: 1 y: 0");
            Assert.IsTrue(hasFoodAtPos, "As10: ต้องสร้าง Food ที่ตำแหน่ง x: 2 y: 2");

            string output = SimpleDebugConsole.GetOutput();
            StringAssert.Contains("Create Item Soda at x: 1 y: 0", output, "As10: ต้องพิมพ์ 'Create Item Soda at x: 1 y: 0'");
            StringAssert.Contains("Create Item Food at x: 2 y: 2", output, "As10: ต้องพิมพ์ 'Create Item Food at x: 2 y: 2'");

            DestroyItems(wallPrefabs);
            DestroyItems(floorPrefabs);
            DestroyItems(foodPrefabs);
            Object.DestroyImmediate(itemPrefab);
            Object.DestroyImmediate(playerPrefab);
            Object.DestroyImmediate(exitPrefab);
            DestroyAllClones();
        }

        // ============ As11 PlacePlayer: วางผู้เล่นที่มุมซ้ายล่าง (0, 0) ============
        [Test]
        public void As11_Start_PlacePlayer()
        {
            var playerField = assignment.GetType().GetField("player",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.IsNotNull(playerField, "As11: ต้องประกาศตัวแปร player (เช่น public GameObject[] player;) ในคลาส");

            var wallPrefabs = MakeItems("Wall");
            var floorPrefabs = MakeItems("Floor");
            var itemPrefab = new GameObject("KeyItem");
            var foodPrefabs = MakeItems("Soda", "Food");
            var playerPrefab = new GameObject("Player");
            var exitPrefab = new GameObject("Exit");

            SetupMapParameters(4, 3, wallPrefabs, floorPrefabs, itemPrefab, 1, 1, foodPrefabs, playerPrefab, exitPrefab);
            InvokeStart();

            var playerClones = ClonesNamed("Player");
            Assert.AreEqual(1, playerClones.Count, "As11: Start() ต้องสร้าง Player 1 ตัว");
            Assert.AreEqual(0f, playerClones[0].transform.position.x, 0.0001f, "As11: Player x ต้องอยู่ที่ 0");
            Assert.AreEqual(0f, playerClones[0].transform.position.y, 0.0001f, "As11: Player y ต้องอยู่ที่ 0");

            DestroyItems(wallPrefabs);
            DestroyItems(floorPrefabs);
            DestroyItems(foodPrefabs);
            Object.DestroyImmediate(itemPrefab);
            Object.DestroyImmediate(playerPrefab);
            Object.DestroyImmediate(exitPrefab);
            DestroyAllClones();
        }

        // ============ As12 PlaceExit: วางทางออกที่มุมขวาบน (cols - 1, rows - 1) ============
        [TestCase(4, 3)]
        [TestCase(6, 4)]
        public void As12_Start_PlaceExit(int cols, int rows)
        {
            var exitField = assignment.GetType().GetField("exitTile",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.IsNotNull(exitField, "As12: ต้องประกาศตัวแปร exitTile (เช่น public GameObject exitTile;) ในคลาส");

            var wallPrefabs = MakeItems("Wall");
            var floorPrefabs = MakeItems("Floor");
            var itemPrefab = new GameObject("KeyItem");
            var foodPrefabs = MakeItems("Soda", "Food");
            var playerPrefab = new GameObject("Player");
            var exitPrefab = new GameObject("Exit");

            SetupMapParameters(cols, rows, wallPrefabs, floorPrefabs, itemPrefab, 1, 1, foodPrefabs, playerPrefab, exitPrefab);
            InvokeStart();

            var exitClones = ClonesNamed("Exit");
            Assert.AreEqual(1, exitClones.Count, "As12: Start() ต้องสร้าง Exit 1 อัน");
            Assert.AreEqual(cols - 1f, exitClones[0].transform.position.x, 0.0001f, $"As12: Exit x ต้องอยู่ที่ {cols - 1}");
            Assert.AreEqual(rows - 1f, exitClones[0].transform.position.y, 0.0001f, $"As12: Exit y ต้องอยู่ที่ {rows - 1}");

            DestroyItems(wallPrefabs);
            DestroyItems(floorPrefabs);
            DestroyItems(foodPrefabs);
            Object.DestroyImmediate(itemPrefab);
            Object.DestroyImmediate(playerPrefab);
            Object.DestroyImmediate(exitPrefab);
            DestroyAllClones();
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

        [Test]
        public void Lv01_GetSet2DStringArray()
        {
            assignment.Lv01_GetSet2DStringArray();

            const string sep = "============================";
            var sb = new StringBuilder();
            sb.AppendLine("get : C");
            sb.AppendLine("set : Cat");
            sb.AppendLine(sep);
            sb.AppendLine("A B Cat");
            sb.AppendLine("D E F");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertBodyContains("Lv01_GetSet2DStringArray", "As04_Print2DArray", "ต้องเรียกใช้ As04_Print2DArray ในการแสดงผล 2D Array");
        }

        [TestCaseSource(nameof(RowCases))]
        public void Lv02_SumRow(int[,] matrix, int row)
        {
            assignment.Lv02_SumRow(matrix, row);

            int expected = 0;
            for (int c = 0; c < matrix.GetLength(1); c++) expected += matrix[row, c];

            TestUtils.AssertMultilineEqual(expected.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv02_SumRow");
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
        public void Lv03_SumColumn(int[,] matrix, int col)
        {
            assignment.Lv03_SumColumn(matrix, col);

            int expected = 0;
            for (int r = 0; r < matrix.GetLength(0); r++) expected += matrix[r, col];

            TestUtils.AssertMultilineEqual(expected.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv03_SumColumn");
        }

        [TestCase(3, 4)]
        [TestCase(1, 1)]
        [TestCase(5, 2)]
        [TestCase(7, 7)]
        public void Lv04_BuildVillage(int columns, int rows)
        {
            var tile = new GameObject("VillageTile");

            assignment.Lv04_BuildVillage(columns, rows, tile);

            var sb = new StringBuilder();
            for (int y = 0; y < rows; y++)
            {
                sb.AppendLine(new string('*', columns));
            }
            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());

            var villageClones = ClonesNamed("VillageTile");
            Assert.AreEqual(columns * rows, villageClones.Count, "ต้องสร้างบ้านให้ครบทุกช่องของหมู่บ้าน");
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    bool found = villageClones.Exists(go =>
                        Mathf.Approximately(go.transform.position.x, (float)x) &&
                        Mathf.Approximately(go.transform.position.y, (float)y));
                    Assert.IsTrue(found, $"ไม่พบบ้านที่พิกัด ({x}, {y})");
                }
            }

            Object.DestroyImmediate(tile);
            DestroyAllClones();
            AssertUsesRealLoop("Lv04_BuildVillage", minLoops: 2);
            AssertBodyContains("Lv04_BuildVillage", "Instantiate", "ต้อง Instantiate บ้านลงในฉากจริง");
        }

        [TestCase(5)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(8)]
        public void Lv05_BuildRiver(int size)
        {
            var tile = new GameObject("RiverTile");

            assignment.Lv05_BuildRiver(size, tile);

            var sb = new StringBuilder();
            for (int r = 1; r <= size; r++)
            {
                sb.AppendLine(new string('*', r));
            }
            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());

            var riverClones = ClonesNamed("RiverTile");
            Assert.AreEqual(size * (size + 1) / 2, riverClones.Count, "จำนวนช่องแม่น้ำต้องเท่ากับพื้นที่สามเหลี่ยม");
            for (int r = 1; r <= size; r++)
            {
                for (int i = 0; i < r; i++)
                {
                    bool found = riverClones.Exists(go =>
                        Mathf.Approximately(go.transform.position.x, (float)i) &&
                        Mathf.Approximately(go.transform.position.y, (float)(r - 1)));
                    Assert.IsTrue(found, $"ไม่พบแม่น้ำที่พิกัด ({i}, {r - 1})");
                }
            }

            Object.DestroyImmediate(tile);
            DestroyAllClones();
            AssertUsesRealLoop("Lv05_BuildRiver", minLoops: 2);
            AssertBodyContains("Lv05_BuildRiver", "Instantiate", "ต้อง Instantiate แม่น้ำลงในฉากจริง");
        }

        [TestCase(2, 4)]
        [TestCase(2, 2)]
        [TestCase(5, 9)]
        [TestCase(1, 3)]
        public void Lv06_MultiplicationTableNested(int fromTable, int toTable)
        {
            assignment.Lv06_MultiplicationTableNested(fromTable, toTable);

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
            AssertUsesRealLoop("Lv06_MultiplicationTableNested", minLoops: 2);
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
        public void Lv07_FindMaxInMatrix(int[,] matrix, int expectedMax, int expectedR, int expectedC)
        {
            assignment.Lv07_FindMaxInMatrix(matrix);

            TestUtils.AssertMultilineEqual($"Max value {expectedMax} at [{expectedR}, {expectedC}]", SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv07_FindMaxInMatrix");
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
        public void Lv08_CountTargetValue(int[,] matrix, int target, int expectedCount)
        {
            assignment.Lv08_CountTargetValue(matrix, target);

            TestUtils.AssertMultilineEqual($"Found target {target}: {expectedCount} cells", SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv08_CountTargetValue");
        }

        static readonly object[] SumAllCases =
        {
            new object[] { Matrix3x3, 45 },
            new object[] { Matrix2x4, 110 },
            new object[] { Matrix1x1, 99 },
            new object[] { new int[,] { { -5, 5 }, { -10, 10 } }, 0 },
        };

        [TestCaseSource(nameof(SumAllCases))]
        public void Lv09_SumAllElements(int[,] matrix, int expectedSum)
        {
            assignment.Lv09_SumAllElements(matrix);

            TestUtils.AssertMultilineEqual(expectedSum.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv09_SumAllElements");
        }

        [TestCase(4)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void Lv10_BuildInvertedRiver(int size)
        {
            var tile = new GameObject("RiverTile");

            assignment.Lv10_BuildInvertedRiver(size, tile);

            var sb = new StringBuilder();
            for (int r = size; r >= 1; r--)
            {
                sb.AppendLine(new string('*', r));
            }
            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());

            var riverClones = ClonesNamed("RiverTile");
            Assert.AreEqual(size * (size + 1) / 2, riverClones.Count, "จำนวนช่องแม่น้ำต้องเท่ากับพื้นที่สามเหลี่ยม");
            int currY = 0;
            for (int r = size; r >= 1; r--)
            {
                for (int i = 0; i < r; i++)
                {
                    bool found = riverClones.Exists(go =>
                        Mathf.Approximately(go.transform.position.x, (float)i) &&
                        Mathf.Approximately(go.transform.position.y, (float)currY));
                    Assert.IsTrue(found, $"ไม่พบแม่น้ำกลับด้านที่พิกัด ({i}, {currY})");
                }
                currY++;
            }

            Object.DestroyImmediate(tile);
            DestroyAllClones();
            AssertUsesRealLoop("Lv10_BuildInvertedRiver", minLoops: 2);
            AssertBodyContains("Lv10_BuildInvertedRiver", "Instantiate", "ต้อง Instantiate แม่น้ำลงในฉากจริง");
        }

        static readonly object[] DiagonalCases =
        {
            new object[] { Matrix3x3, "1 5 9" },
            new object[] { Matrix2x4, "10 2" },
            new object[] { Matrix1x1, "99" },
            new object[] { new int[,] { { 7, 0, 0 }, { 0, 8, 0 }, { 0, 0, 9 } }, "7 8 9" },
        };

        [TestCaseSource(nameof(DiagonalCases))]
        public void Lv11_PrintMainDiagonal(int[,] matrix, string expectedOutput)
        {
            assignment.Lv11_PrintMainDiagonal(matrix);

            TestUtils.AssertMultilineEqual(expectedOutput, SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Lv11_PrintMainDiagonal");
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


        static char[,] MakeBoard(string r0, string r1, string r2)
        {
            string[] rowsText = { r0, r1, r2 };
            var b = new char[3, 3];
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    b[r, c] = rowsText[r][c];
            return b;
        }

        static readonly object[] WinnerCases =
        {
            // แนวนอน
            new object[] { "XXX", "OO ", "   ", 'X' },
            new object[] { "OO ", "XXX", "   ", 'X' },
            new object[] { "XX ", "   ", "OOO", 'O' },
            // แนวตั้ง
            new object[] { "X O", "X O", "X  ", 'X' },
            new object[] { "OX ", "OX ", " X ", 'X' },
            new object[] { "XXO", "  O", " XO", 'O' },
            // แนวทแยง
            new object[] { "X O", "OX ", "  X", 'X' },
            new object[] { "XXO", " O ", "O  ", 'O' },
            // ยังเล่นต่อได้
            new object[] { "X  ", " O ", "   ", ' ' },
            new object[] { "XOX", "XO ", "O X", ' ' },
            new object[] { "   ", "   ", "   ", ' ' },
            // เสมอ (เต็มกระดาน ไม่มีใครชนะ)
            new object[] { "XOX", "XXO", "OXO", 'D' },
            new object[] { "OXO", "XXO", "XOX", 'D' },
        };

        [TestCaseSource(nameof(WinnerCases))]
        public void Ex01_CheckWinner(string r0, string r1, string r2, char expected)
        {
            var board = MakeBoard(r0, r1, r2);

            char actual = assignment.Ex01_CheckWinner(board);

            Assert.AreEqual(expected, actual,
                "board [" + r0 + "][" + r1 + "][" + r2 + "] expected '" + expected + "' but got '" + actual + "'");
        }

        [Test]
        public void Ex01_CheckWinner_DoesNotTreatEmptyLineAsWin()
        {
            var board = MakeBoard("   ", "   ", "   ");
            Assert.AreEqual(' ', assignment.Ex01_CheckWinner(board),
                "กระดานว่างทั้งหมดต้องไม่นับว่ามีผู้ชนะ (ช่องว่าง 3 ช่องเรียงกันไม่ใช่การชนะ)");
        }

        [Test]
        public void Ex01_CheckWinner_DoesNotModifyBoard()
        {
            var board = MakeBoard("XOX", "XXO", "OXO");
            var copy = MakeBoard("XOX", "XXO", "OXO");

            assignment.Ex01_CheckWinner(board);

            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    Assert.AreEqual(copy[r, c], board[r, c], "Ex01_CheckWinner ต้องไม่แก้ค่าในกระดาน");
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

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(1, 2)]
        [TestCase(0, 0)]
        [TestCase(3, 3)]
        [TestCase(2, 2)]
        public void Ex02_CheckWalkableTile(int targetX, int targetY)
        {
            assignment.Ex02_CheckWalkableTile(SampleMap, targetX, targetY);

            int rows = SampleMap.GetLength(0);
            int cols = SampleMap.GetLength(1);
            string[] names = { "Up", "Down", "Left", "Right" };
            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { 1, -1, 0, 0 };

            var sb = new StringBuilder();
            sb.AppendLine($"Check around ({targetX}, {targetY})");
            for (int i = 0; i < names.Length; i++)
            {
                int nx = targetX + dx[i];
                int ny = targetY + dy[i];
                string result;
                if (nx < 0 || nx >= cols || ny < 0 || ny >= rows) result = "Out of Bounds";
                else if (SampleMap[ny, nx] == 0) result = "Walkable";
                else result = "Blocked";
                sb.AppendLine($"{names[i]} ({nx}, {ny}) : {result}");
            }

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
            AssertUsesRealLoop("Ex02_CheckWalkableTile");
        }

        [TestCase(5, 5)]
        [TestCase(8, 6)]
        [TestCase(2, 2)]
        public void Ex03_SpawnChestsInCorners(int columns, int rows)
        {
            var chest = new GameObject("Chest");

            assignment.Ex03_SpawnChestsInCorners(columns, rows, chest);

            var expected = new StringBuilder();
            for (int y = rows - 1; y >= 0; y--)
            {
                var line = new StringBuilder();
                for (int x = 0; x < columns; x++)
                {
                    bool isCorner = (x == 0 || x == columns - 1) && (y == 0 || y == rows - 1);
                    line.Append(isCorner ? 'C' : '.');
                }
                expected.AppendLine(line.ToString());
            }
            expected.AppendLine("Spawned 4 chests at corners");
            TestUtils.AssertMultilineEqual(expected.ToString(), SimpleDebugConsole.GetOutput());
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
