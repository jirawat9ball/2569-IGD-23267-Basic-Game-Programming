using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

using NUnit.Framework;
using UnityEngine;

using Week05;
using SimpleDebugConsole = Workspace.Core.SimpleDebugConsole;

namespace Week05_Method
{
    /// <summary>
    /// ตัวช่วยเรียก Method ผ่าน Reflection โดยไม่ต้องบังคับให้ Assignment_Student_Week05 สืบทอด IAssignment
    /// ช่วยให้นักเรียนเริ่มไฟล์เปล่าได้โดยไม่มี Compiler Error CS0535
    /// </summary>
    public class AssignmentInvoker : IAssignment
    {
        private readonly object _target;
        private readonly Assignment_Student_Week05 _studentSync;

        public AssignmentInvoker(object target, Assignment_Student_Week05 studentSync = null)
        {
            _target = target;
            _studentSync = studentSync;
        }

        private void SyncFieldsBeforeInvoke()
        {
            if (_studentSync != null && _target is Assignment_Teacher_Week05 teacher)
            {
                teacher.transform.position = _studentSync.transform.position;
            }
        }

        private void SyncFieldsAfterInvoke()
        {
            if (_studentSync != null && _target is Assignment_Teacher_Week05 teacher)
            {
                _studentSync.transform.position = teacher.transform.position;
            }
        }

        private object Invoke(string methodName, params object[] args)
        {
            SyncFieldsBeforeInvoke();

            var type = _target.GetType();
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo targetMethod = null;

            // 1. ค้นหาตามชื่อตรงและจำนวน/ชนิดของ Parameter
            foreach (var m in methods)
            {
                if (m.Name == methodName)
                {
                    var parameters = m.GetParameters();
                    if (parameters.Length == args.Length)
                    {
                        bool match = true;
                        for (int i = 0; i < args.Length; i++)
                        {
                            if (args[i] != null && !parameters[i].ParameterType.IsAssignableFrom(args[i].GetType()))
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)
                        {
                            targetMethod = m;
                            break;
                        }
                    }
                }
            }

            // 2. รองรับ default parameter กรณี args น้อยกว่าจำนวน parameter ของ method
            if (targetMethod == null)
            {
                foreach (var m in methods)
                {
                    if (m.Name == methodName)
                    {
                        var parameters = m.GetParameters();
                        if (args.Length < parameters.Length)
                        {
                            bool canFillDefaults = true;
                            var fullArgs = new object[parameters.Length];
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                if (i < args.Length)
                                {
                                    fullArgs[i] = args[i];
                                }
                                else if (parameters[i].HasDefaultValue)
                                {
                                    fullArgs[i] = parameters[i].DefaultValue;
                                }
                                else
                                {
                                    canFillDefaults = false;
                                    break;
                                }
                            }

                            if (canFillDefaults)
                            {
                                targetMethod = m;
                                args = fullArgs;
                                break;
                            }
                        }
                    }
                }
            }

            // 3. ตรวจสอบกรณีสะกดชื่อผิดตัวพิมพ์เล็ก-ใหญ่ เพื่อแจ้ง error ชัดเจน
            if (targetMethod == null)
            {
                foreach (var m in methods)
                {
                    if (string.Equals(m.Name, methodName, System.StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Fail($"พบเมธอด '{m.Name}' แต่ตัวสะกดพิมพ์ใหญ่-เล็กไม่ตรงกับที่กำหนด (ต้องเป็น '{methodName}')");
                    }
                }

                Assert.Fail($"ไม่พบเมธอด '{methodName}' ใน {_target.GetType().Name} (กรุณาสร้างเมธอดตามโจทย์)");
                return null;
            }

            try
            {
                var result = targetMethod.Invoke(_target, args);
                SyncFieldsAfterInvoke();
                return result;
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                throw;
            }
        }

        public void UserNameIdentification() => Invoke(nameof(UserNameIdentification));
        public void UserNameIdentification(string name) => Invoke(nameof(UserNameIdentification), name);
        public void UserNameIdentification(string name, int age) => Invoke(nameof(UserNameIdentification), name, age);
        public void UserCountry(string country = "Thailand") => Invoke(nameof(UserCountry), country);
        public int Add(int a, int b) => (int)(Invoke(nameof(Add), a, b) ?? 0);
        public int GetStringLength(string text) => (int)(Invoke(nameof(GetStringLength), text) ?? 0);
        public bool ConvertInttoBool(int sex) => (bool)(Invoke(nameof(ConvertInttoBool), sex) ?? false);
        public int Lv01_CalculateDamage(int baseDamage, float multiplier) => (int)(Invoke(nameof(Lv01_CalculateDamage), baseDamage, multiplier) ?? 0);
        public bool Lv02_CanCastSpell(int currentMana, int manaCost) => (bool)(Invoke(nameof(Lv02_CanCastSpell), currentMana, manaCost) ?? false);
        public int Lv03_FindHighestScore(int[] scores) => (int)(Invoke(nameof(Lv03_FindHighestScore), (object)scores) ?? 0);
        public int Lv04_CalculateTotalScore(int[] scores) => (int)(Invoke(nameof(Lv04_CalculateTotalScore), (object)scores) ?? 0);
        public bool Lv05_CheckLevelUp(int currentExp, int requiredExp) => (bool)(Invoke(nameof(Lv05_CheckLevelUp), currentExp, requiredExp) ?? false);
        public int Lv06_ClampHealth(int currentHealth, int minHealth, int maxHealth) => (int)(Invoke(nameof(Lv06_ClampHealth), currentHealth, minHealth, maxHealth) ?? 0);
    }

    public class PlayerInvoker
    {
        private readonly Component _target;

        public PlayerInvoker(Component target)
        {
            _target = target;
        }

        public int Energy
        {
            get
            {
                var field = _target.GetType().GetField("energy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                return field != null ? (int)field.GetValue(_target) : 0;
            }
            set
            {
                var field = _target.GetType().GetField("energy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null) field.SetValue(_target, value);
            }
        }

        public Vector3 Position
        {
            get => _target.transform.position;
            set => _target.transform.position = value;
        }

        public Transform transform => _target.transform;

        private object Invoke(string methodName, params object[] args)
        {
            var type = _target.GetType();
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo targetMethod = null;

            foreach (var m in methods)
            {
                if (m.Name == methodName)
                {
                    var parameters = m.GetParameters();
                    if (parameters.Length == args.Length)
                    {
                        bool match = true;
                        for (int i = 0; i < args.Length; i++)
                        {
                            if (args[i] != null && !parameters[i].ParameterType.IsAssignableFrom(args[i].GetType()))
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)
                        {
                            targetMethod = m;
                            break;
                        }
                    }
                }
            }

            // 2. รองรับ default parameter กรณี args น้อยกว่าจำนวน parameter ของ method
            if (targetMethod == null)
            {
                foreach (var m in methods)
                {
                    if (m.Name == methodName)
                    {
                        var parameters = m.GetParameters();
                        if (args.Length < parameters.Length)
                        {
                            bool canFillDefaults = true;
                            var fullArgs = new object[parameters.Length];
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                if (i < args.Length)
                                {
                                    fullArgs[i] = args[i];
                                }
                                else if (parameters[i].HasDefaultValue)
                                {
                                    fullArgs[i] = parameters[i].DefaultValue;
                                }
                                else
                                {
                                    canFillDefaults = false;
                                    break;
                                }
                            }

                            if (canFillDefaults)
                            {
                                targetMethod = m;
                                args = fullArgs;
                                break;
                            }
                        }
                    }
                }
            }

            if (targetMethod == null)
            {
                foreach (var m in methods)
                {
                    if (string.Equals(m.Name, methodName, System.StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Fail($"พบเมธอด '{m.Name}' ใน Player แต่ตัวสะกดพิมพ์ใหญ่-เล็กไม่ตรงกับที่กำหนด (ต้องเป็น '{methodName}')");
                    }
                }

                Assert.Fail($"ไม่พบเมธอด '{methodName}' ใน Player (กรุณาสร้างเมธอดตามโจทย์ใน Player.cs)");
                return null;
            }

            try
            {
                return targetMethod.Invoke(_target, args);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                throw;
            }
        }

        public void Move(Vector2 direction) => Invoke(nameof(Move), direction);
        public void TakeDamage(int Damage) => Invoke(nameof(TakeDamage), Damage);
        public void Heal() => Invoke(nameof(Heal));
        public void Heal(int healPoint) => Invoke(nameof(Heal), healPoint);
        public bool CanMove() => (bool)(Invoke(nameof(CanMove)) ?? false);
    }

    public class MapGeneratorInvoker
    {
        private readonly Component _target;
        private readonly Assignment_Student_Week05 _studentSync;

        public MapGeneratorInvoker(Component target, Assignment_Student_Week05 studentSync = null)
        {
            _target = target;
            _studentSync = studentSync;
        }

        public int Columns
        {
            get => GetField<int>("columns");
            set => SetField("columns", value);
        }

        public int Rows
        {
            get => GetField<int>("rows");
            set => SetField("rows", value);
        }

        public GameObject[] FloorTiles
        {
            get => GetField<GameObject[]>("floorTiles");
            set => SetField("floorTiles", value);
        }

        public GameObject[] WallTiles
        {
            get => GetField<GameObject[]>("wallTiles");
            set => SetField("wallTiles", value);
        }

        public GameObject[] FoodTiles
        {
            get => GetField<GameObject[]>("foodTiles");
            set => SetField("foodTiles", value);
        }

        public int FoodCount
        {
            get => GetField<int>("foodCount");
            set => SetField("foodCount", value);
        }

        public GameObject Player
        {
            get => GetField<GameObject>("player");
            set => SetField("player", value);
        }

        public GameObject ExitTile
        {
            get => GetField<GameObject>("exitTile");
            set => SetField("exitTile", value);
        }

        private T GetField<T>(string fieldName)
        {
            var f = _target.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (f != null) return (T)f.GetValue(_target);
            if (_studentSync != null)
            {
                var sf = _studentSync.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (sf != null) return (T)sf.GetValue(_studentSync);
            }
            return default;
        }

        private void SetField(string fieldName, object value)
        {
            var f = _target.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (f != null) f.SetValue(_target, value);
            if (_studentSync != null)
            {
                var sf = _studentSync.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (sf != null) sf.SetValue(_studentSync, value);
            }
        }

        private object Invoke(string methodName, params object[] args)
        {
            var type = _target.GetType();
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo targetMethod = null;

            foreach (var m in methods)
            {
                if (m.Name == methodName && m.GetParameters().Length == args.Length)
                {
                    targetMethod = m;
                    break;
                }
            }

            if (targetMethod == null && _studentSync != null)
            {
                foreach (var m in _studentSync.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (m.Name == methodName && m.GetParameters().Length == args.Length)
                    {
                        return m.Invoke(_studentSync, args);
                    }
                }
            }

            if (targetMethod == null)
            {
                Assert.Fail($"ไม่พบเมธอด '{methodName}' ใน MapGenerator");
                return null;
            }

            try
            {
                return targetMethod.Invoke(_target, args);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null) throw ex.InnerException;
                throw;
            }
        }

        public void GenerateFloor() => Invoke(nameof(GenerateFloor));
        public void GenerateWalls() => Invoke(nameof(GenerateWalls));
        public void GenerateFoods() => Invoke(nameof(GenerateFoods));
        public void PlacePlayer() => Invoke(nameof(PlacePlayer));
        public void PlaceExit() => Invoke(nameof(PlaceExit));
    }

    public class TestBase
    {
        // =========================================================================================
        // 🎯 สลับตรวจไฟล์ อ. หรือ นักเรียน: เปลี่ยนเป็น true เมื่อต้องการตรวจไฟล์เฉลยอาจารย์
        // =========================================================================================
        protected const bool isTeacherMode = false;

        protected const string StudentPath = "Assets/Scripts/Workspace/Week05/Assignment_Student_Week05.cs";
        protected const string TeacherPath = "Assets/Scripts/Workspace/Teacher/Week05/Assignment_Teacher_Week05.cs";

        protected const string StudentPlayerPath = "Assets/Scripts/Workspace/Week05/Player.cs";
        protected const string TeacherPlayerPath = "Assets/Scripts/Workspace/Teacher/Week05/Player_Teacher_Week05.cs";

        protected const string StudentMapPath = "Assets/Scripts/Workspace/Week05/MapGenerator.cs";
        protected const string TeacherMapPath = "Assets/Scripts/Workspace/Teacher/Week05/MapGenerator_Teacher_Week05.cs";

        protected static string CurrentTargetFilePath => isTeacherMode ? TeacherPath : StudentPath;
        protected static string CurrentPlayerFilePath => isTeacherMode ? TeacherPlayerPath : StudentPlayerPath;
        protected static string CurrentMapFilePath => isTeacherMode ? TeacherMapPath : StudentMapPath;

        protected IAssignment assignment;
        protected Assignment_Student_Week05 student;
        protected Assignment_Teacher_Week05 teacher;

        protected PlayerInvoker player;
        protected MapGeneratorInvoker mapGenerator;
        protected GameObject testGo;
        protected GameObject playerGo;
        protected GameObject mapGeneratorGo;

        [SetUp]
        public void Setup()
        {
            testGo = new GameObject("Week05_TestRunner");
            playerGo = new GameObject("Week05_Player");
            mapGeneratorGo = new GameObject("Week05_MapGenerator");

            if (isTeacherMode)
            {
                teacher = testGo.AddComponent<Assignment_Teacher_Week05>();
                student = testGo.AddComponent<Assignment_Student_Week05>();
                assignment = new AssignmentInvoker(teacher, student);
                var teacherPlayer = playerGo.AddComponent<Player_Teacher_Week05>();
                player = new PlayerInvoker(teacherPlayer);
                var teacherMap = mapGeneratorGo.AddComponent<MapGenerator_Teacher_Week05>();
                mapGenerator = new MapGeneratorInvoker(teacherMap);
            }
            else
            {
                student = testGo.AddComponent<Assignment_Student_Week05>();
                assignment = new AssignmentInvoker(student);
                var studentPlayer = playerGo.AddComponent<Player>();
                player = new PlayerInvoker(studentPlayer);
                var studentMap = mapGeneratorGo.AddComponent<MapGenerator>();
                mapGenerator = new MapGeneratorInvoker(studentMap, student);
            }
            SimpleDebugConsole.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            if (testGo != null)
                Object.DestroyImmediate(testGo);
            if (playerGo != null)
                Object.DestroyImmediate(playerGo);
            if (mapGeneratorGo != null)
                Object.DestroyImmediate(mapGeneratorGo);

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
            if (sig == -1)
            {
                sig = src.IndexOf(signature, System.StringComparison.OrdinalIgnoreCase);
            }
            if (sig == -1)
            {
                var match = Regex.Match(signature, @"(\w+)\s*\(");
                if (match.Success)
                {
                    string methodName = match.Groups[1].Value;
                    var mMatch = Regex.Match(src, $@"\b{methodName}\s*\(");
                    if (mMatch.Success) sig = mMatch.Index;
                }
            }

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
            string src = ReadStudentSourceStripped();
            if (src.Contains(signature)) return;

            var match = Regex.Match(signature, @"(public|private|protected)\s+([\w<>\[\], ]+?)\s+(\w+)\s*\((.*?)\)");
            if (match.Success)
            {
                string access = match.Groups[1].Value;
                string returnType = Regex.Escape(match.Groups[2].Value.Trim());
                string methodName = match.Groups[3].Value;
                string rawParams = match.Groups[4].Value.Trim();

                string pattern;
                if (string.IsNullOrEmpty(rawParams))
                {
                    pattern = $@"\b{access}\s+{returnType}\s+{methodName}\s*\(\s*\)";
                }
                else
                {
                    var paramParts = rawParams.Split(',');
                    var typePatterns = new System.Collections.Generic.List<string>();
                    foreach (var p in paramParts)
                    {
                        var tokens = p.Trim().Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                        if (tokens.Length > 0)
                        {
                            string pType = Regex.Escape(tokens[0]);
                            typePatterns.Add($@"{pType}\s+\w+");
                        }
                    }
                    pattern = $@"\b{access}\s+{returnType}\s+{methodName}\s*\(\s*" + string.Join(@"\s*,\s*", typePatterns) + @"\s*\)";
                }

                if (Regex.IsMatch(src, pattern, RegexOptions.IgnoreCase))
                    return;
            }

            StringAssert.Contains(signature, src, $"ต้องประกาศเมธอดตามรูปแบบ '{signature}'");
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

        // ---- อ่าน source ของ Player เพื่อกัน hardcode ----

        private static string ReadPlayerSourceStripped()
        {
            string path = CurrentPlayerFilePath;
            Assert.IsTrue(File.Exists(path),
                $"หาไฟล์เป้าหมายไม่เจอที่ '{path}' (cwd={Directory.GetCurrentDirectory()})");

            string src = File.ReadAllText(path);
            src = Regex.Replace(src, @"//.*?$", "", RegexOptions.Multiline);
            src = Regex.Replace(src, @"/\*.*?\*/", "", RegexOptions.Singleline);
            src = Regex.Replace(src, "\"([^\"\\\\]|\\\\.)*\"", "\"\"");
            src = Regex.Replace(src, "'([^'\\\\]|\\\\.)*'", "' '");
            return src;
        }

        protected static string GetPlayerMethodBody(string signature)
        {
            string src = ReadPlayerSourceStripped();

            int sig = src.IndexOf(signature, System.StringComparison.Ordinal);
            if (sig == -1)
            {
                sig = src.IndexOf(signature, System.StringComparison.OrdinalIgnoreCase);
            }
            if (sig == -1)
            {
                var match = Regex.Match(signature, @"(\w+)\s*\(");
                if (match.Success)
                {
                    string methodName = match.Groups[1].Value;
                    var mMatch = Regex.Match(src, $@"\b{methodName}\s*\(");
                    if (mMatch.Success) sig = mMatch.Index;
                }
            }

            Assert.Greater(sig, -1, $"ไม่พบเมธอด '{signature}' ในไฟล์เป้าหมาย ({CurrentPlayerFilePath})");

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

        protected static void AssertPlayerSignatureExists(string signature)
        {
            string src = ReadPlayerSourceStripped();
            if (src.Contains(signature)) return;

            var match = Regex.Match(signature, @"(public|private|protected)\s+([\w<>\[\], ]+?)\s+(\w+)\s*\((.*?)\)");
            if (match.Success)
            {
                string access = match.Groups[1].Value;
                string returnType = Regex.Escape(match.Groups[2].Value.Trim());
                string methodName = match.Groups[3].Value;
                string rawParams = match.Groups[4].Value.Trim();

                string pattern;
                if (string.IsNullOrEmpty(rawParams))
                {
                    pattern = $@"\b{access}\s+{returnType}\s+{methodName}\s*\(\s*\)";
                }
                else
                {
                    var paramParts = rawParams.Split(',');
                    var typePatterns = new System.Collections.Generic.List<string>();
                    foreach (var p in paramParts)
                    {
                        var tokens = p.Trim().Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                        if (tokens.Length > 0)
                        {
                            string pType = Regex.Escape(tokens[0]);
                            typePatterns.Add($@"{pType}\s+\w+");
                        }
                    }
                    pattern = $@"\b{access}\s+{returnType}\s+{methodName}\s*\(\s*" + string.Join(@"\s*,\s*", typePatterns) + @"\s*\)";
                }

                if (Regex.IsMatch(src, pattern, RegexOptions.IgnoreCase))
                    return;
            }

            StringAssert.Contains(signature, src, $"ต้องประกาศเมธอดตามรูปแบบ '{signature}' ใน {CurrentPlayerFilePath}");
        }

        protected static void AssertPlayerBodyContains(string signature, string needle, string reason)
        {
            StringAssert.Contains(needle, GetPlayerMethodBody(signature), $"{signature}: {reason}");
        }

        protected static void AssertPlayerRawSourceContains(string needle, string reason)
        {
            string path = CurrentPlayerFilePath;
            Assert.IsTrue(File.Exists(path), $"หาไฟล์เป้าหมายไม่เจอที่ '{path}'");
            StringAssert.Contains(needle, File.ReadAllText(path), reason);
        }

        // ---- อ่าน source ของ MapGenerator เพื่อกัน hardcode ----

        private static string ReadMapSourceStripped()
        {
            string path = CurrentMapFilePath;
            if (!File.Exists(path) && !isTeacherMode)
            {
                path = StudentPath;
            }
            Assert.IsTrue(File.Exists(path), $"หาไฟล์เป้าหมายไม่เจอที่ '{path}'");

            string src = File.ReadAllText(path);
            src = Regex.Replace(src, @"//.*?$", "", RegexOptions.Multiline);
            src = Regex.Replace(src, @"/\*.*?\*/", "", RegexOptions.Singleline);
            src = Regex.Replace(src, "\"([^\"\\\\]|\\\\.)*\"", "\"\"");
            src = Regex.Replace(src, "'([^'\\\\]|\\\\.)*'", "' '");
            return src;
        }

        protected static string GetMapMethodBody(string signature)
        {
            string src = ReadMapSourceStripped();

            int sig = src.IndexOf(signature, System.StringComparison.Ordinal);
            if (sig == -1)
            {
                sig = src.IndexOf(signature, System.StringComparison.OrdinalIgnoreCase);
            }
            if (sig == -1)
            {
                var match = Regex.Match(signature, @"(\w+)\s*\(");
                if (match.Success)
                {
                    string methodName = match.Groups[1].Value;
                    var mMatch = Regex.Match(src, $@"\b{methodName}\s*\(");
                    if (mMatch.Success) sig = mMatch.Index;
                }
            }

            if (sig == -1 && !isTeacherMode)
            {
                src = ReadStudentSourceStripped();
                sig = src.IndexOf(signature, System.StringComparison.Ordinal);
                if (sig == -1) sig = src.IndexOf(signature, System.StringComparison.OrdinalIgnoreCase);
                if (sig == -1)
                {
                    var match = Regex.Match(signature, @"(\w+)\s*\(");
                    if (match.Success)
                    {
                        string methodName = match.Groups[1].Value;
                        var mMatch = Regex.Match(src, $@"\b{methodName}\s*\(");
                        if (mMatch.Success) sig = mMatch.Index;
                    }
                }
            }

            Assert.Greater(sig, -1, $"ไม่พบเมธอด '{signature}' ในไฟล์เป้าหมาย ({CurrentMapFilePath})");

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

        protected static void AssertMapUsesRealLoop(string signature, int minLoops = 1)
        {
            string body = GetMapMethodBody(signature);
            int loops = Regex.Matches(body, @"\bfor\s*\(").Count
                      + Regex.Matches(body, @"\bforeach\s*\(").Count
                      + Regex.Matches(body, @"\bwhile\s*\(").Count;

            Assert.GreaterOrEqual(loops, minLoops,
                $"{signature}: ต้องใช้ลูปจริงอย่างน้อย {minLoops} ลูป (ห้าม hardcode พิมพ์ทีละบรรทัด)");
        }

        protected static void AssertMapBodyContains(string signature, string needle, string reason)
        {
            StringAssert.Contains(needle, GetMapMethodBody(signature), $"{signature}: {reason}");
        }
    }


    public class Lecture : TestBase
    {
        // ============ ข้อ 1: Method แบบ void และ Parameter (Overloading) ============

        [Test]
        public void As01_01_UserNameIdentification_NoParameter()
        {
            assignment.UserNameIdentification();
            TestUtils.AssertMultilineEqual("user name is UntitleUser", SimpleDebugConsole.GetOutput());
            AssertSignatureExists("public void UserNameIdentification()");
        }

        [TestCase("boy")]
        [TestCase("Anna")]
        [TestCase("นักศึกษา")]
        public void As01_02_UserNameIdentification_WithName(string name)
        {
            assignment.UserNameIdentification(name);
            TestUtils.AssertMultilineEqual("user name is " + name, SimpleDebugConsole.GetOutput());
            AssertSignatureExists("public void UserNameIdentification(string name)");
        }

        [TestCase("big", 18)]
        [TestCase("Tom", 7)]
        [TestCase("Ann", 0)]
        public void As01_03_UserNameIdentification_WithNameAndAge(string name, int age)
        {
            assignment.UserNameIdentification(name, age);
            TestUtils.AssertMultilineEqual($"user name is {name} age is {age}", SimpleDebugConsole.GetOutput());
            AssertSignatureExists("public void UserNameIdentification(string name, int age)");
        }

        [Test]
        public void As01_04_UserCountry_UsesDefaultValue()
        {
            assignment.UserCountry();
            TestUtils.AssertMultilineEqual("Thailand", SimpleDebugConsole.GetOutput());
            AssertRawSourceContains("string country = \"Thailand\"",
                "ต้องกำหนดค่าเริ่มต้นของพารามิเตอร์เป็น \"Thailand\"");
        }

        [TestCase("Japan")]
        [TestCase("Laos")]
        public void As01_05_UserCountry_WithValue(string country)
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
        public void As02_01_Add(int a, int b, int expected)
        {
            Assert.AreEqual(expected, assignment.Add(a, b), $"Add({a}, {b}) ต้อง return {expected}");
            AssertSignatureExists("public int Add(int a, int b)");
        }

        [TestCase("hello", 5)]
        [TestCase("", 0)]
        [TestCase("Unity Engine", 12)]
        [TestCase("a", 1)]
        public void As02_02_GetStringLength(string text, int expected)
        {
            Assert.AreEqual(expected, assignment.GetStringLength(text), $"GetStringLength(\"{text}\") ต้อง return {expected}");
            AssertSignatureExists("public int GetStringLength(string text)");
        }

        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(2, false)]
        [TestCase(-1, false)]
        public void As02_03_ConvertInttoBool(int sex, bool expected)
        {
            Assert.AreEqual(expected, assignment.ConvertInttoBool(sex), $"ConvertInttoBool({sex}) ต้อง return {expected}");
            AssertSignatureExists("public bool ConvertInttoBool(int sex)");
        }

        // ============ ข้อ 3: Move ============

        [TestCase(1, 0, 5)]
        [TestCase(0, -1, 1)]
        [TestCase(2, 3, 4)]
        public void As03_01_Move_SingleDirection(int dirX, int dirY, int times)
        {
            player.Energy = 20;
            player.Position = Vector3.zero;

            for (int i = 0; i < times; i++) player.Move(new Vector2(dirX, dirY));

            Assert.AreEqual(dirX * times, player.Position.x, 0.0001f);
            Assert.AreEqual(dirY * times, player.Position.y, 0.0001f);
            Assert.AreEqual(20 - times, player.Energy, "energy ต้องลดลง 1 ต่อการเดิน 1 ครั้ง");
        }

        [Test]
        public void As03_02_Move_RightThreeThenUpThree()
        {
            player.Energy = 20;
            player.Position = Vector3.zero;

            for (int i = 0; i < 3; i++) player.Move(Vector2.right);
            for (int i = 0; i < 3; i++) player.Move(Vector2.up);

            Assert.AreEqual(3f, player.Position.x, 0.0001f, "เดินขวา 3 ครั้ง x ต้องเป็น 3");
            Assert.AreEqual(3f, player.Position.y, 0.0001f, "เดินขึ้น 3 ครั้ง y ต้องเป็น 3");
            Assert.AreEqual(14, player.Energy, "เดิน 6 ครั้ง energy ต้องลดจาก 20 เหลือ 14");

            AssertPlayerSignatureExists("public void Move(Vector2 direction)");
        }

        // ============ ข้อ 4: TakeDamage ============

        [Test]
        public void As04_01_TakeDamage_ReducesEnergy()
        {
            player.Energy = 20;

            player.TakeDamage(4);
            player.TakeDamage(5);
            player.TakeDamage(6);

            Assert.AreEqual(5, player.Energy, "โดน 4 + 5 + 6 จาก 20 ต้องเหลือ 5");
            AssertPlayerSignatureExists("public void TakeDamage(int Damage)");
        }

        [TestCase(10, 50)]
        [TestCase(20, 20)]
        [TestCase(1, 999)]
        public void As04_02_TakeDamage_NeverBelowZero(int startEnergy, int damage)
        {
            player.Energy = startEnergy;

            player.TakeDamage(damage);

            Assert.AreEqual(0, player.Energy, "energy ต้องไม่ต่ำกว่า 0");
        }

        // ============ ข้อ 5: CheckDead ============

        [Test]
        public void As05_01_CheckDead_NotDeadYet()
        {
            player.Energy = 30;
            SimpleDebugConsole.Clear();

            player.TakeDamage(10);

            TestUtils.AssertMultilineEqual("Current Energy : 20", SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As05_02_CheckDead_PrintsYouLoseWhenEnergyRunsOut()
        {
            player.Energy = 40;
            SimpleDebugConsole.Clear();

            player.TakeDamage(10);
            player.TakeDamage(10);
            player.TakeDamage(10);
            player.TakeDamage(10);

            var sb = new StringBuilder();
            sb.AppendLine("Current Energy : 30");
            sb.AppendLine("Current Energy : 20");
            sb.AppendLine("Current Energy : 10");
            sb.AppendLine("Current Energy : 0");
            sb.AppendLine("You Lose");

            TestUtils.AssertMultilineEqual(sb.ToString(), SimpleDebugConsole.GetOutput());
        }

        [Test]
        public void As05_03_CheckDead_IsPrivateAndCalledFromTakeDamage()
        {
            AssertPlayerSignatureExists("private void CheckDead()");
            AssertPlayerBodyContains("public void TakeDamage(int Damage)", "CheckDead",
                "TakeDamage ต้องเรียก CheckDead() หลังลด energy");
        }

        // ============ ข้อ 6: Heal & Default Parameter ============

        [TestCase(20, 4, 24)]
        [TestCase(0, 10, 10)]
        [TestCase(5, 0, 5)]
        [TestCase(100, 250, 350)]
        public void As06_01_Heal(int startEnergy, int healPoint, int expected)
        {
            player.Energy = startEnergy;

            player.Heal(healPoint);

            Assert.AreEqual(expected, player.Energy, $"Heal({healPoint}) จาก {startEnergy} ต้องได้ {expected}");
            AssertPlayerSignatureExists("public void Heal(int healPoint)");
        }

        [Test]
        public void As06_02_Heal_UsesDefaultValue()
        {
            player.Energy = 15;
            player.Heal(); // ไม่ส่งพารามิเตอร์ ต้องใช้ default = 10

            Assert.AreEqual(25, player.Energy, "Heal() แบบไม่ระบุพารามิเตอร์ ต้องเพิ่ม energy 10 เป็นค่าเริ่มต้น");
            AssertPlayerRawSourceContains("healPoint = 10",
                "ต้องกำหนดค่าเริ่มต้นของพารามิเตอร์ healPoint เป็น 10 เช่น Heal(int healPoint = 10)");
        }

        // ============ ข้อ 7: CanMove ============

        [TestCase(20, true)]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-5, false)]
        public void As07_01_CanMove(int currentEnergy, bool expected)
        {
            player.Energy = currentEnergy;
            Assert.AreEqual(expected, player.CanMove(), $"energy = {currentEnergy} CanMove() ต้อง return {expected}");
            AssertPlayerSignatureExists("public bool CanMove()");
        }
    }

    public class Homework : TestBase
    {
        // ============ Ex01: แยกโค้ดสร้างแผนที่ออกเป็น Method (Refactoring) ============

        [TestCase(3, 4)]
        [TestCase(1, 1)]
        [TestCase(5, 2)]
        public void Ex01_GenerateFloor(int columns, int rows)
        {
            var tiles = MakePrefabs("Floor");
            mapGenerator.Columns = columns;
            mapGenerator.Rows = rows;
            mapGenerator.FloorTiles = tiles;

            mapGenerator.GenerateFloor();

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
            AssertMapUsesRealLoop("public void GenerateFloor()", minLoops: 2);
            AssertMapBodyContains("public void GenerateFloor()", "Instantiate", "ต้อง Instantiate พื้นจริง");
        }

        [TestCase(3, 4)]
        [TestCase(1, 1)]
        [TestCase(5, 2)]
        public void Ex01_GenerateWalls(int columns, int rows)
        {
            var tiles = MakePrefabs("Wall");
            mapGenerator.Columns = columns;
            mapGenerator.Rows = rows;
            mapGenerator.WallTiles = tiles;

            mapGenerator.GenerateWalls();

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
            AssertMapUsesRealLoop("public void GenerateWalls()", minLoops: 2);
            AssertMapBodyContains("public void GenerateWalls()", "Instantiate", "ต้อง Instantiate กำแพงจริง");
        }

        [TestCase(3, 4, 3)]
        [TestCase(5, 5, 1)]
        [TestCase(2, 2, 6)]
        public void Ex01_GenerateFoods(int columns, int rows, int foodCount)
        {
            var tiles = MakePrefabs("Food");
            mapGenerator.Columns = columns;
            mapGenerator.Rows = rows;
            mapGenerator.FoodCount = foodCount;
            mapGenerator.FoodTiles = tiles;

            mapGenerator.GenerateFoods();

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
            AssertMapUsesRealLoop("public void GenerateFoods()");
            AssertMapBodyContains("public void GenerateFoods()", "Instantiate", "ต้อง Instantiate อาหารจริง");
        }

        [Test]
        public void Ex01_PlacePlayer()
        {
            var prefab = new GameObject("Player");
            mapGenerator.Player = prefab;

            mapGenerator.PlacePlayer();

            var clones = ClonesNamed("Player");
            Assert.AreEqual(1, clones.Count, "ต้องสร้างตัวละคร 1 ตัว");
            Assert.AreEqual(0f, clones[0].transform.position.x, 0.0001f, "ตัวละครต้องอยู่ที่ x = 0");
            Assert.AreEqual(0f, clones[0].transform.position.y, 0.0001f, "ตัวละครต้องอยู่ที่ y = 0");

            DestroyAll(prefab);
            AssertMapBodyContains("public void PlacePlayer()", "Instantiate", "ต้อง Instantiate ตัวละครจริง");
        }

        [TestCase(3, 4)]
        [TestCase(1, 1)]
        [TestCase(6, 2)]
        public void Ex01_PlaceExit(int columns, int rows)
        {
            var prefab = new GameObject("Exit");
            mapGenerator.Columns = columns;
            mapGenerator.Rows = rows;
            mapGenerator.ExitTile = prefab;

            mapGenerator.PlaceExit();

            var clones = ClonesNamed("Exit");
            Assert.AreEqual(1, clones.Count, "ต้องสร้างทางออก 1 อัน");
            Assert.AreEqual(columns - 1, clones[0].transform.position.x, 0.0001f, $"ทางออกต้องอยู่ที่ x = {columns - 1}");
            Assert.AreEqual(rows - 1, clones[0].transform.position.y, 0.0001f, $"ทางออกต้องอยู่ที่ y = {rows - 1}");

            DestroyAll(prefab);
            AssertMapBodyContains("public void PlaceExit()", "Instantiate", "ต้อง Instantiate ทางออกจริง");
        }

        // ============ Level 1: Simple (โจทย์การบ้าน Method พื้นฐาน & ระบบเกม) ============

        [TestCase(100, 1.5f, 150)]
        [TestCase(50, 2.0f, 100)]
        [TestCase(80, 0.5f, 40)]
        [TestCase(10, 1.25f, 12)]
        [TestCase(0, 2.5f, 0)]
        public void Lv01_CalculateDamage(int baseDamage, float multiplier, int expected)
        {
            AssertSignatureExists("public int Lv01_CalculateDamage(int baseDamage, float multiplier)");
            int actual = assignment.Lv01_CalculateDamage(baseDamage, multiplier);
            Assert.AreEqual(expected, actual, $"Lv01_CalculateDamage({baseDamage}, {multiplier}f) ต้องได้ {expected}");
        }

        [TestCase(50, 30, true)]
        [TestCase(30, 30, true)]
        [TestCase(20, 30, false)]
        [TestCase(0, 10, false)]
        [TestCase(100, 0, true)]
        public void Lv02_CanCastSpell(int currentMana, int manaCost, bool expected)
        {
            AssertSignatureExists("public bool Lv02_CanCastSpell(int currentMana, int manaCost)");
            bool actual = assignment.Lv02_CanCastSpell(currentMana, manaCost);
            Assert.AreEqual(expected, actual, $"Lv02_CanCastSpell({currentMana}, {manaCost}) ต้องได้ {expected}");
        }

        [Test]
        public void Lv03_FindHighestScore()
        {
            AssertSignatureExists("public int Lv03_FindHighestScore(int[] scores)");

            int[] test1 = new int[] { 10, 45, 99, 23, 7 };
            Assert.AreEqual(99, assignment.Lv03_FindHighestScore(test1), "อาร์เรย์ [10, 45, 99, 23, 7] คะแนนสูงสุดต้องเป็น 99");

            int[] test2 = new int[] { -5, -20, -2, -10 };
            Assert.AreEqual(-2, assignment.Lv03_FindHighestScore(test2), "อาร์เรย์ [-5, -20, -2, -10] คะแนนสูงสุดต้องเป็น -2");

            int[] test3 = new int[] { 50 };
            Assert.AreEqual(50, assignment.Lv03_FindHighestScore(test3), "อาร์เรย์ [50] คะแนนสูงสุดต้องเป็น 50");

            int[] testEmpty = new int[] { };
            Assert.AreEqual(0, assignment.Lv03_FindHighestScore(testEmpty), "อาร์เรย์ว่าง ต้องคืนค่า 0");

            Assert.AreEqual(0, assignment.Lv03_FindHighestScore(null), "อาร์เรย์ null ต้องคืนค่า 0");
        }

        [Test]
        public void Lv04_CalculateTotalScore()
        {
            AssertSignatureExists("public int Lv04_CalculateTotalScore(int[] scores)");

            int[] test1 = new int[] { 10, 20, 30 };
            Assert.AreEqual(60, assignment.Lv04_CalculateTotalScore(test1), "อาร์เรย์ [10, 20, 30] ผลรวมต้องเป็น 60");

            int[] test2 = new int[] { 5, -5, 10 };
            Assert.AreEqual(10, assignment.Lv04_CalculateTotalScore(test2), "อาร์เรย์ [5, -5, 10] ผลรวมต้องเป็น 10");

            int[] test3 = new int[] { 100 };
            Assert.AreEqual(100, assignment.Lv04_CalculateTotalScore(test3), "อาร์เรย์ [100] ผลรวมต้องเป็น 100");

            int[] testEmpty = new int[] { };
            Assert.AreEqual(0, assignment.Lv04_CalculateTotalScore(testEmpty), "อาร์เรย์ว่าง ผลรวมต้องเป็น 0");

            Assert.AreEqual(0, assignment.Lv04_CalculateTotalScore(null), "อาร์เรย์ null ผลรวมต้องเป็น 0");
        }

        [TestCase(120, 100, true)]
        [TestCase(100, 100, true)]
        [TestCase(99, 100, false)]
        [TestCase(0, 50, false)]
        [TestCase(500, 200, true)]
        public void Lv05_CheckLevelUp(int currentExp, int requiredExp, bool expected)
        {
            AssertSignatureExists("public bool Lv05_CheckLevelUp(int currentExp, int requiredExp)");
            bool actual = assignment.Lv05_CheckLevelUp(currentExp, requiredExp);
            Assert.AreEqual(expected, actual, $"Lv05_CheckLevelUp({currentExp}, {requiredExp}) ต้องได้ {expected}");
        }

        [TestCase(120, 0, 100, 100)]
        [TestCase(-10, 0, 100, 0)]
        [TestCase(50, 0, 100, 50)]
        [TestCase(0, 0, 100, 0)]
        [TestCase(100, 0, 100, 100)]
        [TestCase(25, 20, 80, 25)]
        [TestCase(15, 20, 80, 20)]
        [TestCase(95, 20, 80, 80)]
        public void Lv06_ClampHealth(int currentHealth, int minHealth, int maxHealth, int expected)
        {
            AssertSignatureExists("public int Lv06_ClampHealth(int currentHealth, int minHealth, int maxHealth)");
            int actual = assignment.Lv06_ClampHealth(currentHealth, minHealth, maxHealth);
            Assert.AreEqual(expected, actual, $"Lv06_ClampHealth({currentHealth}, {minHealth}, {maxHealth}) ต้องได้ {expected}");
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
