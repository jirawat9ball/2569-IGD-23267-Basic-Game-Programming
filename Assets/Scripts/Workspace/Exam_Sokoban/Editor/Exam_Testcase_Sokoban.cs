using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

using Exam_Sokoban;
using SimpleDebugConsole = Workspace.Core.SimpleDebugConsole;

// =============================================================================================
// ข้อสอบปฏิบัติ Sokoban Mini — เกณฑ์คะแนน (ต้องตรงกับตารางใน Instruction-th.md)
//   ทุกเทสมีค่า 5 คะแนนเท่ากัน
//   ข้อ 1 ทำฉาก                 5 เทส = 25 คะแนน
//   ข้อ 2 ควบคุมตัวละคร + อินพุต   5 เทส = 25 คะแนน
//   ข้อ 3 ผลักกล่อง + ตรวจชนะ     7 เทส = 35 คะแนน
//   ข้อ 4 โบนัส พื้นถล่ม           3 เทส = 15 คะแนน
//   รวม 85 คะแนน + โบนัส 15 = เต็ม 100
// =============================================================================================
namespace Exam_Sokoban_Tests
{
    public class TestBase
    {
        // =========================================================================================
        // 🎯 สลับตรวจไฟล์ อ. หรือ นักเรียน: เปลี่ยนเป็น true เมื่อต้องการตรวจไฟล์เฉลยอาจารย์
        // =========================================================================================
        protected const bool isTeacherMode = false;

        protected const string StudentPath = "Assets/Scripts/Workspace/Exam_Sokoban/Exam_Student_Sokoban.cs";
        protected const string TeacherPath = "Assets/Scripts/Workspace/Teacher/Exam_Teacher_Sokoban.cs";

        // หาคลาสเฉลยด้วยชื่อแทนการอ้างตรง ๆ แพ็กเกจของนักศึกษาที่ไม่มีไฟล์เฉลยจะได้คอมไพล์ผ่าน
        protected const string TeacherTypeName = "Exam_Sokoban.Exam_Teacher_Sokoban, Workspace";

        protected static string CurrentTargetFilePath => isTeacherMode ? TeacherPath : StudentPath;

        protected const string FloorName = "T_Floor";
        protected const string WallName = "T_Wall";
        protected const string PlayerName = "T_Player";
        protected const string BoxName = "T_Box";
        protected const string TargetName = "T_Target";

        protected GameObject testGo;
        protected MonoBehaviour examComponent;
        protected IExam_Sokoban exam;

        private readonly List<GameObject> stand_ins = new List<GameObject>();

        [SetUp]
        public void Setup()
        {
            SimpleDebugConsole.Clear();
            testGo = new GameObject("Sokoban_TestRunner");

            if (isTeacherMode)
            {
                System.Type teacherType = System.Type.GetType(TeacherTypeName);
                Assert.IsNotNull(teacherType, "เปิด isTeacherMode ไว้ แต่หาคลาส Exam_Teacher_Sokoban ไม่เจอ");
                examComponent = (MonoBehaviour)testGo.AddComponent(teacherType);
            }
            else
            {
                examComponent = testGo.AddComponent<Exam_Student_Sokoban>();
            }

            exam = (IExam_Sokoban)examComponent;

            SetField("floorTiles", new[] { MakeStandIn(FloorName) });
            SetField("wallTiles", new[] { MakeStandIn(WallName) });
            SetField("playerPrefab", MakeStandIn(PlayerName));
            SetField("boxPrefab", MakeStandIn(BoxName));
            SetField("targetPrefab", MakeStandIn(TargetName));
            SetField("collapseMode", false);
        }

        [TearDown]
        public void TearDown()
        {
            if (testGo != null) Object.DestroyImmediate(testGo);
            foreach (var go in stand_ins)
            {
                if (go != null) Object.DestroyImmediate(go);
            }
            stand_ins.Clear();

            foreach (var go in AllSceneObjects())
            {
                if (go != null && go.name.Contains("(Clone)")) Object.DestroyImmediate(go);
            }
        }

        // ทุก object ในซีนที่ active อยู่ รวมลูกที่ถูกจับไปไว้ใต้ parent ด้วย
        protected static List<GameObject> AllSceneObjects()
        {
            var list = new List<GameObject>();
            foreach (GameObject root in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                foreach (Transform t in root.GetComponentsInChildren<Transform>())
                {
                    list.Add(t.gameObject);
                }
            }
            return list;
        }

        // ---------------------------------------------------------------------------------------
        // ตัวช่วยตั้งค่า
        // ---------------------------------------------------------------------------------------

        private GameObject MakeStandIn(string name)
        {
            var go = new GameObject(name, typeof(SpriteRenderer));
            stand_ins.Add(go);
            return go;
        }

        protected void SetField(string fieldName, object value)
        {
            FieldInfo field = examComponent.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);
            Assert.IsNotNull(field, $"ไม่พบตัวแปร public {fieldName} ในสคริปต์ — ห้ามลบหรือเปลี่ยนชื่อตัวแปรที่ให้มา");
            field.SetValue(examComponent, value);
        }

        protected void Build(string[] map)
        {
            try
            {
                exam.BuildLevel(map);
            }
            catch (System.Exception e)
            {
                Assert.Fail($"BuildLevel เกิด error: {e.GetType().Name} — {e.Message}");
            }
        }

        protected bool Move(Vector2Int direction, string context)
        {
            try
            {
                return exam.TryMove(direction);
            }
            catch (System.IndexOutOfRangeException)
            {
                Assert.Fail($"{context}: TryMove({direction}) เกิด IndexOutOfRangeException — " +
                            "ต้องเช็คก่อนว่าช่องถัดไปอยู่ในแผนที่ ก่อนจะอ่าน array");
            }
            catch (System.Exception e)
            {
                Assert.Fail($"{context}: TryMove({direction}) เกิด error: {e.GetType().Name} — {e.Message}");
            }
            return false;
        }

        protected void MoveExpect(Vector2Int direction, bool expected, string context)
        {
            bool actual = Move(direction, context);
            Assert.AreEqual(expected, actual,
                $"{context}: TryMove({direction}) คาดหวังให้คืน {expected} แต่ได้ {actual}");
        }

        // ---------------------------------------------------------------------------------------
        // ตัวช่วยอ่านฉาก
        // ---------------------------------------------------------------------------------------

        // กติกาพิกัดของข้อสอบ: map[row][col] อยู่ในฉากที่ (col, rows - 1 - row)
        protected static Vector2 Cell(string[] map, int row, int col)
        {
            return new Vector2(col, map.Length - 1 - row);
        }

        protected static List<GameObject> Clones(string prefabName)
        {
            var list = new List<GameObject>();
            string cloneName = prefabName + "(Clone)";
            foreach (var go in AllSceneObjects())
            {
                if (go != null && go.name == cloneName) list.Add(go);
            }
            return list;
        }

        protected static int CountAt(string prefabName, Vector2 pos)
        {
            int n = 0;
            foreach (var go in Clones(prefabName))
            {
                Vector3 p = go.transform.position;
                if (Mathf.Approximately(p.x, pos.x) && Mathf.Approximately(p.y, pos.y)) n++;
            }
            return n;
        }

        protected static string Positions(string prefabName)
        {
            var parts = new List<string>();
            foreach (var go in Clones(prefabName))
            {
                parts.Add($"({go.transform.position.x}, {go.transform.position.y})");
            }
            return parts.Count == 0 ? "ไม่มีเลย" : string.Join(" ", parts);
        }

        protected static int CountSymbol(string[] map, char symbol)
        {
            int n = 0;
            foreach (string line in map)
            {
                foreach (char c in line)
                {
                    if (c == symbol) n++;
                }
            }
            return n;
        }

        protected static void AssertPlayerAt(Vector2 expected, string context)
        {
            var players = Clones(PlayerName);
            Assert.AreEqual(1, players.Count, $"{context}: คาดหวังผู้เล่น 1 ตัวในฉาก แต่เจอ {players.Count} ตัว");
            Vector3 p = players[0].transform.position;
            Assert.IsTrue(Mathf.Approximately(p.x, expected.x) && Mathf.Approximately(p.y, expected.y),
                $"{context}: คาดหวังผู้เล่นอยู่ที่ ({expected.x}, {expected.y}) แต่ได้ ({p.x}, {p.y})");
        }

        protected static void AssertBoxAt(Vector2 expected, string context)
        {
            Assert.AreEqual(1, CountAt(BoxName, expected),
                $"{context}: คาดหวังกล่องที่ ({expected.x}, {expected.y}) แต่กล่องที่เจออยู่ที่ {Positions(BoxName)}");
        }

        // ---------------------------------------------------------------------------------------
        // ตัวช่วยตรวจโค้ด
        // ---------------------------------------------------------------------------------------

        protected static string GetMethodBody(string methodName)
        {
            string path = CurrentTargetFilePath;
            Assert.IsTrue(File.Exists(path), $"หาไฟล์ไม่เจอที่ '{path}' (cwd={Directory.GetCurrentDirectory()})");

            string src = File.ReadAllText(path);
            src = Regex.Replace(src, @"//.*?$", "", RegexOptions.Multiline);
            src = Regex.Replace(src, @"/\*.*?\*/", "", RegexOptions.Singleline);
            src = Regex.Replace(src, "\"([^\"\\\\]|\\\\.)*\"", "\"\"");
            src = Regex.Replace(src, "'([^'\\\\]|\\\\.)*'", "' '");

            Match sig = Regex.Match(src, @"\b(?:void|bool)\s+" + methodName + @"\s*\(");
            Assert.IsTrue(sig.Success, $"ไม่พบเมธอด {methodName} ในไฟล์");

            int open = src.IndexOf('{', sig.Index);
            Assert.Greater(open, -1, $"เมธอด {methodName} ไม่มี body");

            int depth = 0;
            for (int i = open; i < src.Length; i++)
            {
                if (src[i] == '{') depth++;
                else if (src[i] == '}')
                {
                    depth--;
                    if (depth == 0) return src.Substring(open + 1, i - open - 1);
                }
            }
            Assert.Fail($"บอดี้เมธอด {methodName} ปีกกาไม่ครบ");
            return null;
        }

        protected static void AssertBodyContains(string methodName, string needle, string reason)
        {
            StringAssert.Contains(needle, GetMethodBody(methodName), $"{methodName}: {reason}");
        }

        protected static void AssertBodyMatches(string methodName, string pattern, string reason)
        {
            Assert.IsTrue(Regex.IsMatch(GetMethodBody(methodName), pattern), $"{methodName}: {reason}");
        }
    }

    // ============================================================================================
    // ข้อ 1 — ทำฉาก (25 คะแนน)
    // ============================================================================================
    public class Q1_BuildLevel : TestBase
    {
        // แผนที่ไม่เป็นสี่เหลี่ยมจัตุรัส จะจับได้ทันทีถ้าสลับ row/col หรือลืมกลับแกน y
        static readonly string[] TestMap =
        {
            "#######",
            "#P..X.#",
            "#..B..#",
            "#.....#",
            "#######",
        };

        static readonly string[] SampleLevel =
        {
            "##########",
            "#........#",
            "#..B..X..#",
            "#..P.....#",
            "#.....B..#",
            "#..X.....#",
            "##########",
        };

        [Test]
        public void Q1_01_ObjectCount()
        {
            Build(SampleLevel);

            int walls = CountSymbol(SampleLevel, '#');
            int floors = CountSymbol(SampleLevel, '.') + CountSymbol(SampleLevel, 'P') + CountSymbol(SampleLevel, 'B');
            int targets = CountSymbol(SampleLevel, 'X');

            Assert.AreEqual(walls, Clones(WallName).Count, $"คาดหวังกำแพง {walls} ชิ้น แต่ได้ {Clones(WallName).Count}");
            Assert.AreEqual(floors, Clones(FloorName).Count,
                $"คาดหวังพื้น {floors} ชิ้น (ช่อง '.', 'P', 'B' — ช่อง 'X' ใช้ targetPrefab แทนพื้น) แต่ได้ {Clones(FloorName).Count}");
            Assert.AreEqual(targets, Clones(TargetName).Count, $"คาดหวังเป้าหมาย {targets} ชิ้น แต่ได้ {Clones(TargetName).Count}");
            Assert.AreEqual(2, Clones(BoxName).Count, $"คาดหวังกล่อง 2 ใบ แต่ได้ {Clones(BoxName).Count}");
            Assert.AreEqual(1, Clones(PlayerName).Count, $"คาดหวังผู้เล่น 1 ตัว แต่ได้ {Clones(PlayerName).Count}");
        }

        [Test]
        public void Q1_02_WallPositions()
        {
            Build(TestMap);

            Assert.AreEqual(CountSymbol(TestMap, '#'), Clones(WallName).Count,
                $"คาดหวังกำแพง {CountSymbol(TestMap, '#')} ชิ้น แต่ได้ {Clones(WallName).Count}");

            for (int row = 0; row < TestMap.Length; row++)
            {
                for (int col = 0; col < TestMap[row].Length; col++)
                {
                    if (TestMap[row][col] != '#') continue;
                    Vector2 pos = Cell(TestMap, row, col);
                    Assert.AreEqual(1, CountAt(WallName, pos),
                        $"map[{row}][{col}] เป็น '#' ต้องมีกำแพงที่ ({pos.x}, {pos.y}) แต่ไม่เจอ");
                }
            }
        }

        [Test]
        public void Q1_03_PlayerAndBoxPositions()
        {
            Build(TestMap);

            AssertPlayerAt(Cell(TestMap, 1, 1), "map[1][1] เป็น 'P'");
            AssertBoxAt(Cell(TestMap, 2, 3), "map[2][3] เป็น 'B'");
        }

        [Test]
        public void Q1_04_FloorAndTargetPositions()
        {
            Build(TestMap);

            for (int row = 0; row < TestMap.Length; row++)
            {
                for (int col = 0; col < TestMap[row].Length; col++)
                {
                    char symbol = TestMap[row][col];
                    if (symbol == '#') continue;

                    Vector2 pos = Cell(TestMap, row, col);
                    int floor = CountAt(FloorName, pos);
                    int target = CountAt(TargetName, pos);

                    if (symbol == 'X')
                    {
                        Assert.AreEqual(1, target, $"map[{row}][{col}] เป็น 'X' ต้องมีเป้าหมายที่ ({pos.x}, {pos.y}) แต่ได้ {target} ชิ้น");
                        Assert.AreEqual(0, floor, $"map[{row}][{col}] เป็น 'X' ใช้ targetPrefab แทนพื้น ไม่ต้องสร้างพื้นซ้อน แต่เจอพื้น {floor} ชิ้น");
                    }
                    else
                    {
                        Assert.AreEqual(1, floor, $"map[{row}][{col}] เป็น '{symbol}' ต้องมีพื้น 1 ชิ้นที่ ({pos.x}, {pos.y}) แต่ได้ {floor} ชิ้น");
                        Assert.AreEqual(0, target, $"map[{row}][{col}] เป็น '{symbol}' ไม่ใช่เป้าหมาย แต่เจอเป้าหมาย {target} ชิ้น");
                    }
                }
            }
        }

        [Test]
        public void Q1_05_UsesNestedLoopAndInstantiate()
        {
            AssertBodyContains("BuildLevel", "Instantiate", "ต้องสร้างของในฉากด้วย Instantiate");

            string body = GetMethodBody("BuildLevel");
            int loops = Regex.Matches(body, @"\bfor\s*\(").Count
                      + Regex.Matches(body, @"\bforeach\s*\(").Count
                      + Regex.Matches(body, @"\bwhile\s*\(").Count;
            Assert.GreaterOrEqual(loops, 2, $"BuildLevel: ต้องใช้ Nested Loop (ลูปซ้อนลูปอย่างน้อย 2 ลูป) แต่เจอ {loops} ลูป");
        }
    }

    // ============================================================================================
    // ข้อ 2 — ควบคุมตัวละคร + รับอินพุต (25 คะแนน)
    // ============================================================================================
    public class Q2_Movement : TestBase
    {
        static readonly string[] Q2Map =
        {
            "#####",
            "#P..#",
            "#.#.#",
            "#####",
        };

        [Test]
        public void Q2_01_MoveOntoFloor()
        {
            Build(Q2Map);

            MoveExpect(Vector2Int.right, true, "เดินขวาไปพื้นว่าง");
            AssertPlayerAt(new Vector2(2, 2), "หลังเดินขวา 1 ครั้ง");
        }

        [Test]
        public void Q2_02_BlockedByWall()
        {
            Build(Q2Map);

            MoveExpect(Vector2Int.up, false, "เดินขึ้นชนกำแพง");
            AssertPlayerAt(new Vector2(1, 2), "หลังเดินชนกำแพงด้านบน ต้องอยู่ที่เดิม");

            MoveExpect(Vector2Int.left, false, "เดินซ้ายชนกำแพง");
            AssertPlayerAt(new Vector2(1, 2), "หลังเดินชนกำแพงด้านซ้าย ต้องอยู่ที่เดิม");
        }

        [Test]
        public void Q2_03_OneStepPerMoveAndUpIsPlusY()
        {
            Build(Q2Map);

            MoveExpect(Vector2Int.down, true, "เดินลง");
            AssertPlayerAt(new Vector2(1, 1), "Vector2Int.down ต้องทำให้ y ลดลง 1");

            MoveExpect(Vector2Int.up, true, "เดินขึ้น");
            AssertPlayerAt(new Vector2(1, 2), "Vector2Int.up ต้องทำให้ y เพิ่มขึ้น 1");

            MoveExpect(Vector2Int.right, true, "เดินขวาครั้งที่ 1");
            MoveExpect(Vector2Int.right, true, "เดินขวาครั้งที่ 2");
            AssertPlayerAt(new Vector2(3, 2), "เดินขวา 2 ครั้ง ต้องขยับทีละ 1 ช่อง รวม 2 ช่อง");

            MoveExpect(Vector2Int.down, true, "เดินลงที่มุมขวา");
            MoveExpect(Vector2Int.left, false, "เดินซ้ายเข้ากำแพงกลางแผนที่");
            AssertPlayerAt(new Vector2(3, 1), "หลังชนกำแพงกลางแผนที่ ต้องอยู่ที่เดิม");
        }

        [Test]
        public void Q2_04_OutsideMapIsBlocked()
        {
            string[] noBorder = { "P." };
            Build(noBorder);

            MoveExpect(Vector2Int.left, false, "เดินออกนอกแผนที่ทางซ้าย");
            MoveExpect(Vector2Int.up, false, "เดินออกนอกแผนที่ทางบน");
            MoveExpect(Vector2Int.down, false, "เดินออกนอกแผนที่ทางล่าง");
            MoveExpect(Vector2Int.right, true, "เดินขวาไปพื้นว่าง");
            MoveExpect(Vector2Int.right, false, "เดินออกนอกแผนที่ทางขวา");
            AssertPlayerAt(new Vector2(1, 0), "หลังพยายามเดินออกนอกแผนที่");
        }

        [Test]
        public void Q2_05_UpdateReadsKeysWithGetKeyDown()
        {
            AssertBodyContains("Update", "GetKeyDown", "ต้องอ่านปุ่มด้วย Input.GetKeyDown (กด 1 ครั้ง = เดิน 1 ช่อง)");
            AssertBodyContains("Update", "TryMove", "ต้องเรียก TryMove เมื่อมีการกดปุ่ม");

            string[] keys = { "UpArrow", "DownArrow", "LeftArrow", "RightArrow", "W", "A", "S", "D" };
            foreach (string key in keys)
            {
                AssertBodyMatches("Update", @"KeyCode\s*\.\s*" + key + @"\b", $"ต้องรับปุ่ม KeyCode.{key} (ลูกศรและ WASD)");
            }

            string tryMove = GetMethodBody("TryMove");
            Assert.IsFalse(tryMove.Contains("Input."),
                "TryMove: ห้ามอ่านปุ่มใน TryMove — อ่านปุ่มใน Update แล้วส่งทิศทางเข้ามาแทน");
        }
    }

    // ============================================================================================
    // ข้อ 3 — ผลักกล่อง + ตรวจชนะ (35 คะแนน)
    // ============================================================================================
    public class Q3_PushBox : TestBase
    {
        [Test]
        public void Q3_01_PushBoxOntoFloor()
        {
            string[] map =
            {
                "###",
                "#.#",
                "#B#",
                "#P#",
                "###",
            };
            Build(map);

            MoveExpect(Vector2Int.up, true, "ผลักกล่องขึ้นไปพื้นว่าง");
            AssertBoxAt(new Vector2(1, 3), "หลังผลักกล่องขึ้น");
            AssertPlayerAt(new Vector2(1, 2), "หลังผลักกล่องขึ้น ผู้เล่นต้องเดินตามไปช่องที่กล่องเคยอยู่");
            Assert.AreEqual(1, Clones(BoxName).Count, "ต้องย้ายกล่องใบเดิม ห้าม Instantiate กล่องใหม่");
        }

        [Test]
        public void Q3_02_PushBoxIntoWallIsBlocked()
        {
            string[] map =
            {
                "#####",
                "#.PB#",
                "#####",
            };
            Build(map);

            MoveExpect(Vector2Int.right, false, "ผลักกล่องชนกำแพง");
            AssertBoxAt(new Vector2(3, 1), "ผลักกล่องชนกำแพง กล่องต้องอยู่ที่เดิม");
            AssertPlayerAt(new Vector2(2, 1), "ผลักกล่องชนกำแพง ผู้เล่นต้องอยู่ที่เดิม");
        }

        [Test]
        public void Q3_03_PushBoxIntoBoxIsBlocked()
        {
            string[] map =
            {
                "######",
                "#PBB.#",
                "######",
            };
            Build(map);

            MoveExpect(Vector2Int.right, false, "ผลักกล่องชนกล่องอีกใบ");
            AssertBoxAt(new Vector2(2, 1), "ผลักกล่องชนกล่อง กล่องใบแรกต้องอยู่ที่เดิม");
            AssertBoxAt(new Vector2(3, 1), "ผลักกล่องชนกล่อง กล่องใบที่สองต้องอยู่ที่เดิม");
            AssertPlayerAt(new Vector2(1, 1), "ผลักกล่องชนกล่อง ผู้เล่นต้องอยู่ที่เดิม");
        }

        [Test]
        public void Q3_04_PushBoxOntoTargetAndOffAgain()
        {
            string[] map =
            {
                "######",
                "#PBX.#",
                "######",
            };
            Build(map);

            MoveExpect(Vector2Int.right, true, "ผลักกล่องเข้าเป้าหมาย");
            AssertBoxAt(new Vector2(3, 1), "หลังผลักกล่องเข้าเป้าหมาย");

            MoveExpect(Vector2Int.right, true, "ผลักกล่องออกจากเป้าหมาย (ต้องทำได้)");
            AssertBoxAt(new Vector2(4, 1), "หลังผลักกล่องออกจากเป้าหมาย");
            AssertPlayerAt(new Vector2(3, 1), "ผู้เล่นต้องยืนบนเป้าหมายได้");
        }

        [Test]
        public void Q3_05_PlayerCanStandOnTarget()
        {
            string[] map =
            {
                "#####",
                "#PX.#",
                "#####",
            };
            Build(map);

            MoveExpect(Vector2Int.right, true, "เดินขึ้นไปยืนบนเป้าหมาย");
            AssertPlayerAt(new Vector2(2, 1), "ผู้เล่นต้องยืนบนเป้าหมายได้");

            MoveExpect(Vector2Int.right, true, "เดินออกจากเป้าหมาย");
            MoveExpect(Vector2Int.left, true, "เดินกลับมายืนบนเป้าหมาย");
            AssertPlayerAt(new Vector2(2, 1), "เดินกลับมายืนบนเป้าหมาย");
        }

        [Test]
        public void Q3_06_NotSolvedUntilAllTargetsCovered()
        {
            string[] map =
            {
                "########",
                "#PB.X..#",
                "#....X.#",
                "########",
            };
            Build(map);

            Assert.IsFalse(exam.IsSolved(), "ตอนเริ่มเกมยังไม่มีกล่องบนเป้าหมาย IsSolved() ต้องคืน false");

            MoveExpect(Vector2Int.right, true, "ผลักกล่องครั้งที่ 1");
            MoveExpect(Vector2Int.right, true, "ผลักกล่องครั้งที่ 2");
            AssertBoxAt(new Vector2(4, 2), "หลังผลักกล่องเข้าเป้าหมายแรก");

            Assert.IsFalse(exam.IsSolved(), "มีเป้าหมาย 2 ช่องแต่มีกล่องทับแค่ 1 ช่อง IsSolved() ต้องคืน false");
            StringAssert.DoesNotContain("You Win", SimpleDebugConsole.GetOutput(),
                "ยังไม่ชนะ ห้ามพิมพ์ \"You Win\"");
        }

        [Test]
        public void Q3_07_SolvedWhenAllTargetsCoveredPrintsYouWin()
        {
            string[] map =
            {
                "######",
                "#PB.X#",
                "######",
            };
            Build(map);

            Assert.IsFalse(exam.IsSolved(), "ตอนเริ่มเกมยังไม่มีกล่องบนเป้าหมาย IsSolved() ต้องคืน false");
            StringAssert.DoesNotContain("You Win", SimpleDebugConsole.GetOutput(), "ยังไม่ชนะ ห้ามพิมพ์ \"You Win\"");

            MoveExpect(Vector2Int.right, true, "ผลักกล่องครั้งที่ 1");
            MoveExpect(Vector2Int.right, true, "ผลักกล่องครั้งที่ 2");
            AssertBoxAt(new Vector2(4, 1), "หลังผลักกล่องเข้าเป้าหมาย");

            Assert.IsTrue(exam.IsSolved(), "ทุกเป้าหมายมีกล่องแล้ว IsSolved() ต้องคืน true");
            StringAssert.Contains("You Win", SimpleDebugConsole.GetOutput(), "ชนะแล้วต้อง Debug.Log(\"You Win\")");
        }
    }

    // ============================================================================================
    // ข้อ 4 — โบนัส: พื้นถล่ม (15 คะแนน)
    // ============================================================================================
    public class Q4_Bonus_Collapse : TestBase
    {
        [Test]
        public void Q4_01_LeftCellBecomesHole()
        {
            SetField("collapseMode", true);
            string[] map =
            {
                "#####",
                "#P..#",
                "#####",
            };
            Build(map);

            MoveExpect(Vector2Int.right, true, "เดินขวา (collapseMode เปิด)");
            Assert.AreEqual(0, CountAt(FloorName, new Vector2(1, 1)),
                "ช่อง (1, 1) ที่เพิ่งเดินออกมาต้องกลายเป็นหลุม (ลบพื้นทิ้ง) แต่พื้นยังอยู่");
            Assert.AreEqual(1, CountAt(FloorName, new Vector2(2, 1)),
                "ช่อง (2, 1) ที่ผู้เล่นยืนอยู่ตอนนี้ พื้นต้องยังอยู่");
        }

        [Test]
        public void Q4_02_CannotEnterOrPushIntoHole()
        {
            SetField("collapseMode", true);
            string[] map =
            {
                "######",
                "#P.B.#",
                "#....#",
                "######",
            };
            Build(map);

            MoveExpect(Vector2Int.right, true, "เดินขวา");
            MoveExpect(Vector2Int.left, false, "เดินกลับลงหลุม");
            AssertPlayerAt(new Vector2(2, 2), "เดินลงหลุมไม่ได้ ต้องอยู่ที่เดิม");

            // เดินอ้อมไปอีกฝั่งของกล่อง ให้หลุมอยู่หน้ากล่องพอดี
            MoveExpect(Vector2Int.down, true, "เดินอ้อม: ลง");
            MoveExpect(Vector2Int.right, true, "เดินอ้อม: ขวา 1");
            MoveExpect(Vector2Int.right, true, "เดินอ้อม: ขวา 2");
            MoveExpect(Vector2Int.up, true, "เดินอ้อม: ขึ้น");
            AssertPlayerAt(new Vector2(4, 2), "หลังเดินอ้อมมาอีกฝั่งของกล่อง");

            MoveExpect(Vector2Int.left, false, "ผลักกล่องลงหลุม");
            AssertBoxAt(new Vector2(3, 2), "ผลักกล่องลงหลุมไม่ได้ กล่องต้องอยู่ที่เดิม");
            AssertPlayerAt(new Vector2(4, 2), "ผลักกล่องลงหลุมไม่ได้ ผู้เล่นต้องอยู่ที่เดิม");
        }

        [Test]
        public void Q4_03_CollapseOffWorksLikeQ3()
        {
            SetField("collapseMode", false);
            string[] map =
            {
                "#####",
                "#P..#",
                "#####",
            };
            Build(map);

            MoveExpect(Vector2Int.right, true, "เดินขวา (collapseMode ปิด)");
            MoveExpect(Vector2Int.left, true, "เดินกลับ (collapseMode ปิด ต้องเดินกลับได้)");
            AssertPlayerAt(new Vector2(1, 1), "collapseMode ปิด เดินไปกลับได้ปกติ");
            Assert.AreEqual(1, CountAt(FloorName, new Vector2(1, 1)),
                "collapseMode ปิด พื้นต้องไม่หาย");
        }
    }
}
