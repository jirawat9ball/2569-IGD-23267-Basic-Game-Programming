using System.Text;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Workspace.Core;

namespace Week08_Exam
{
    public abstract class TestBase
    {
        protected static readonly System.Reflection.BindingFlags AnyInstance =
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.NonPublic;

        // =========================================================================================
        // 🎯 สลับตรวจไฟล์ อ. หรือ นักเรียน: เปลี่ยนเป็น true เมื่อต้องการตรวจไฟล์เฉลยอาจารย์
        // =========================================================================================
        protected const bool isTeacherMode = false;

        protected GameObject testGo;

        protected static System.Type FindType(string typeName)
        {
            return System.Type.GetType($"{typeName}, Workspace")
                ?? System.Type.GetType($"{typeName}, Assembly-CSharp")
                ?? System.Type.GetType(typeName);
        }

        // Level 1: CharacterStatus
        protected static System.Type StatusType => (isTeacherMode ? FindType("Week08.Teacher.Ex01.CharacterStatus") : null) 
            ?? FindType("Week08.Ex01.CharacterStatus")
            ?? FindType("Week08.CharacterStatus")
            ?? FindType("CharacterStatus");

        // Level 2: Armor & ArmorSpawner
        protected static System.Type ArmorType => (isTeacherMode ? FindType("Week08.Teacher.Ex02.Armor") : null) 
            ?? (isTeacherMode ? FindType("Week08.Teacher.Ex03.Armor") : null) 
            ?? FindType("Week08.Ex02.Armor")
            ?? FindType("Week08.Ex03.Armor") 
            ?? FindType("Week08.Armor")
            ?? FindType("Armor")
            ?? (isTeacherMode ? FindType("Week08.Teacher.Ex02.Weapon") : null) 
            ?? FindType("Week08.Ex02.Weapon");

        protected static System.Type SpawnerType => (isTeacherMode ? FindType("Week08.Teacher.Ex02.ArmorSpawner") : null) 
            ?? (isTeacherMode ? FindType("Week08.Teacher.Ex03.ArmorSpawner") : null) 
            ?? FindType("Week08.Ex02.ArmorSpawner") 
            ?? FindType("Week08.Ex03.ArmorSpawner") 
            ?? FindType("Week08.ArmorSpawner")
            ?? FindType("ArmorSpawner")
            ?? (isTeacherMode ? FindType("Week08.Teacher.Ex02.WeaponSpawner") : null) 
            ?? FindType("Week08.Ex02.WeaponSpawner")
            ?? MapGenType;

        // Level 3: MapGenerator & Scanner
        protected static System.Type MapGenType => (isTeacherMode ? FindType("Week08.Teacher.Ex03.MapGenerator_Teacher") : null)
            ?? (isTeacherMode ? FindType("Week08.Teacher.Ex03.InventoryAndGridScanner") : null)
            ?? (isTeacherMode ? FindType("Week08.Teacher.Ex02.InventoryAndGridScanner") : null)
            ?? FindType("Week08.MapGenerator") 
            ?? FindType("Week08.Teacher.Ex03.MapGenerator_Teacher")
            ?? FindType("Week08.Teacher.Ex03.InventoryAndGridScanner")
            ?? FindType("Week08.Teacher.Ex02.InventoryAndGridScanner")
            ?? FindType("Week08.Ex03.InventoryAndGridScanner")
            ?? FindType("Week08.Ex02.InventoryAndGridScanner")
            ?? FindType("MapGenerator");
        protected static System.Type ScannerType => MapGenType;

        [SetUp]
        public void Setup()
        {
            testGo = new GameObject("Week08_Exam_TestRunner");
            SimpleDebugConsole.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            if (testGo != null)
                Object.DestroyImmediate(testGo);
        }
    }

    [TestFixture]
    [Category("Week08")]
    [Category("Week08_Exam")]
    public class Level1_StatusAndLogic_Tests : TestBase
    {
        [Test]
        public void Lv1_01_CharacterStatus_FieldsExistAndDefaultValues()
        {
            Assert.IsNotNull(StatusType, "ต้องมีคลาส CharacterStatus ใน namespace Week08.Ex01");
            Assert.AreEqual(typeof(MonoBehaviour), StatusType.BaseType, "คลาส CharacterStatus ต้องสืบทอดจาก MonoBehaviour");

            var comp = testGo.AddComponent(StatusType);

            var charNameField = StatusType.GetField("characterName", AnyInstance);
            var maxHpField = StatusType.GetField("maxHealth", AnyInstance);
            var curHpField = StatusType.GetField("currentHealth", AnyInstance);
            var isAliveField = StatusType.GetField("isAlive", AnyInstance);

            var atkField = StatusType.GetField("attackPoint", AnyInstance);
            var defField = StatusType.GetField("defensePoint", AnyInstance);

            Assert.IsNotNull(charNameField, "CharacterStatus ต้องมีฟิลด์ characterName");
            Assert.IsNotNull(maxHpField, "CharacterStatus ต้องมีฟิลด์ maxHealth");
            Assert.IsNotNull(curHpField, "CharacterStatus ต้องมีฟิลด์ currentHealth");
            Assert.IsNotNull(isAliveField, "CharacterStatus ต้องมีฟิลด์ isAlive");
            Assert.IsNotNull(atkField, "CharacterStatus ต้องมีฟิลด์ attackPoint");
            Assert.IsNotNull(defField, "CharacterStatus ต้องมีฟิลด์ defensePoint");

            Assert.AreEqual("Hero", charNameField.GetValue(comp), "ค่าเริ่มต้นของ characterName ควรเป็น 'Hero'");
            Assert.AreEqual(100, (int)maxHpField.GetValue(comp), "ค่าเริ่มต้นของ maxHealth ควรเป็น 100");
            Assert.AreEqual(100, (int)curHpField.GetValue(comp), "ค่าเริ่มต้นของ currentHealth ควรเป็น 100");
            Assert.AreEqual(true, (bool)isAliveField.GetValue(comp), "ค่าเริ่มต้นของ isAlive ควรเป็น true");
            Assert.AreEqual(10, (int)atkField.GetValue(comp), "ค่าเริ่มต้นของ attackPoint ควรเป็น 10");
            Assert.AreEqual(0, (int)defField.GetValue(comp), "ค่าเริ่มต้นของ defensePoint ควรเป็น 0");
        }

        [Test]
        public void Lv1_02_TakeDamage_VoidMethod()
        {
            var comp = testGo.AddComponent(StatusType);
            var takeDamageMethod = StatusType.GetMethod("TakeDamage", AnyInstance);
            Assert.IsNotNull(takeDamageMethod, "ต้องมีเมธอด TakeDamage(int)");
            Assert.AreEqual(typeof(void), takeDamageMethod.ReturnType, "เมธอด TakeDamage ต้องเป็น void (ไม่คืนค่า)");

            var curHpField = StatusType.GetField("currentHealth", AnyInstance);
            var isAliveField = StatusType.GetField("isAlive", AnyInstance);

            // 1. โดนดาเมจ 30: เลือดต้องลดจาก 100 เหลือ 70
            takeDamageMethod.Invoke(comp, new object[] { 30 });
            Assert.AreEqual(70, (int)curHpField.GetValue(comp), "โดนดาเมจ 30 เลือดต้องเหลือ 70");
            Assert.IsTrue((bool)isAliveField.GetValue(comp), "เลือดยังเหลือ ตัวละครต้องยังมีชีวิต (isAlive = true)");

            // 2. โดนดาเมจเกินเลือดที่เหลือ (100): เลือดต้องเหลือ 0 และ isAlive ต้องเป็น false
            takeDamageMethod.Invoke(comp, new object[] { 100 });
            Assert.AreEqual(0, (int)curHpField.GetValue(comp), "เมื่อเลือดหมด currentHealth ต้องเป็น 0 (ไม่ติดลบ)");
            Assert.IsFalse((bool)isAliveField.GetValue(comp), "เมื่อ currentHealth <= 0 ตัวแปร isAlive ต้องเป็น false");
        }

        [Test]
        public void Lv1_03_IsAlive_ReturnMethod()
        {
            var comp = testGo.AddComponent(StatusType);
            var isAliveMethod = StatusType.GetMethod("IsAlive", AnyInstance);

            Assert.IsNotNull(isAliveMethod, "ต้องมีเมธอด IsAlive()");
            Assert.AreEqual(typeof(bool), isAliveMethod.ReturnType, "เมธอด IsAlive ต้องคืนค่าเป็น bool");

            var curHpField = StatusType.GetField("currentHealth", AnyInstance);
            var isAliveField = StatusType.GetField("isAlive", AnyInstance);

            // ทดสอบ IsAlive() เมื่อมีชีวิต
            curHpField.SetValue(comp, 50);
            isAliveField.SetValue(comp, true);
            bool alive = (bool)isAliveMethod.Invoke(comp, null);
            Assert.IsTrue(alive, "เมื่อ currentHealth > 0 เมธอด IsAlive() ต้องคืนค่า true");

            // ทดสอบ IsAlive() เมื่อตาย
            curHpField.SetValue(comp, 0);
            isAliveField.SetValue(comp, false);
            bool dead = (bool)isAliveMethod.Invoke(comp, null);
            Assert.IsFalse(dead, "เมื่อ currentHealth <= 0 เมธอด IsAlive() ต้องคืนค่า false");
        }

        [Test]
        public void Lv1_04_CalculateDamage_ReturnMethod()
        {
            var comp = testGo.AddComponent(StatusType);
            var calcMethod = StatusType.GetMethod("CalculateDamage", AnyInstance);

            Assert.IsNotNull(calcMethod, "ต้องมีเมธอด CalculateDamage(int, int)");
            Assert.AreEqual(typeof(int), calcMethod.ReturnType, "เมธอด CalculateDamage ต้องคืนค่าเป็น int");

            // ทดสอบ CalculateDamage(rawDamage, defense)
            int dmg1 = (int)calcMethod.Invoke(comp, new object[] { 50, 20 });
            Assert.AreEqual(30, dmg1, "CalculateDamage(50, 20) ดาเมจสุทธิต้องเท่ากับ 30 (50 - 20)");

            int dmg2 = (int)calcMethod.Invoke(comp, new object[] { 15, 30 });
            Assert.AreEqual(0, dmg2, "CalculateDamage(15, 30) เมื่อ defense มากกว่า rawDamage ดาเมจสุทธิต้องไม่ติดลบ (คืนค่า 0)");
        }
    }

    [TestFixture]
    [Category("Week08")]
    [Category("Week08_Exam")]
    public class Level2_MethodAndClass_Tests : TestBase
    {
        [Test]
        public void Lv2_01_Armor_Fields()
        {
            Assert.IsNotNull(ArmorType, "ต้องมีคลาส Armor (นักศึกษาต้องเขียนประกาศคลาส Armor : MonoBehaviour ขึ้นมาเอง)");
            Assert.AreEqual(typeof(MonoBehaviour), ArmorType.BaseType, "คลาส Armor ต้องสืบทอดจาก MonoBehaviour");

            var armor = testGo.AddComponent(ArmorType);
            var nameField = ArmorType.GetField("Name", AnyInstance) ?? ArmorType.GetField("name", AnyInstance) ?? ArmorType.GetField("armorName", AnyInstance);
            var bonusField = ArmorType.GetField("defenseBonus", AnyInstance) ?? ArmorType.GetField("defense", AnyInstance);

            Assert.IsNotNull(nameField, "Armor ต้องมีฟิลด์ Name");
            Assert.IsNotNull(bonusField, "Armor ต้องมีฟิลด์ defenseBonus");
            Assert.AreEqual(typeof(string), nameField.FieldType, "Name ต้องเป็น string");
            Assert.AreEqual(typeof(int), bonusField.FieldType, "defenseBonus ต้องเป็น int");

            Assert.AreEqual("Iron Armor", nameField.GetValue(armor), "Name เริ่มต้นต้องเป็น 'Iron Armor'");
            Assert.AreEqual(10, (int)bonusField.GetValue(armor), "defenseBonus เริ่มต้นต้องเป็น 10");
        }

        [Test]
        public void Lv2_02_Armor_OnTriggerEnter2D()
        {
            Assert.IsNotNull(ArmorType, "ต้องมีคลาส Armor");
            var armorGo = new GameObject("TestArmor");
            var armor = armorGo.AddComponent(ArmorType);

            var triggerMethod = ArmorType.GetMethod("OnTriggerEnter2D", AnyInstance);
            Assert.IsNotNull(triggerMethod, "Armor ต้องมีเมธอด OnTriggerEnter2D(Collider2D other)");

            // ทดสอบชน Player
            var playerGo = new GameObject("TestPlayer");
            var col = playerGo.AddComponent<BoxCollider2D>();
            var statusComp = playerGo.AddComponent(StatusType);
            var player = playerGo.AddComponent<Week08.Player>();
            player.defensePoint = 0;

            var capturedLogs = new System.Text.StringBuilder();
            Application.LogCallback onLog = (condition, stackTrace, type) => capturedLogs.AppendLine(condition);
            Application.logMessageReceived += onLog;

            LogAssert.ignoreFailingMessages = true;
            try
            {
                SimpleDebugConsole.Clear();
                triggerMethod.Invoke(armor, new object[] { col });

                Assert.AreEqual(10, player.defensePoint, "เมื่อเก็บไอเทมเกราะ ค่า defensePoint ของ Player ต้องเพิ่มขึ้นตาม defenseBonus (+10)");
                string allOutput = SimpleDebugConsole.GetOutput() + "\n" + capturedLogs.ToString();
                bool hasMsg = allOutput.Contains("Picked up Iron Armor! +10 Defense")
                    || (allOutput.Contains("Picked up") && allOutput.Contains("Iron Armor") && allOutput.Contains("10"));
                Assert.IsTrue(hasMsg, "ต้องพิมพ์ข้อความการเก็บเกราะ เช่น 'Picked up Iron Armor! +10 Defense'");
            }
            finally
            {
                Application.logMessageReceived -= onLog;
                if (armorGo != null) Object.DestroyImmediate(armorGo);
                if (playerGo != null) Object.DestroyImmediate(playerGo);
            }
        }

        [Test]
        public void Lv2_03_Armor_Prefab_Configuration()
        {
            GameObject armorPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Exam/Iron Armor.prefab");
            Assert.IsNotNull(armorPrefab, "ไม่พบไฟล์ Prefab 'Assets/Prefabs/Exam/Iron Armor.prefab'! ให้นักศึกษาสร้าง Prefab เกราะขึ้นมาเองใน Unity Editor แล้วบันทึกไว้ที่ Assets/Prefabs/Exam/Iron Armor.prefab");

            // 1. ตรวจสอบ SpriteRenderer
            var spriteRenderer = armorPrefab.GetComponent<SpriteRenderer>();
            Assert.IsNotNull(spriteRenderer, "Prefab 'Iron Armor' ต้องมีคอมโพเนนต์ SpriteRenderer");
            Assert.IsNotNull(spriteRenderer.sprite, "SpriteRenderer ของ Prefab 'Iron Armor' ต้องมีการกำหนด Sprite รูปภาพ");

            // 2. ตรวจสอบ BoxCollider2D และ Is Trigger
            var collider = armorPrefab.GetComponent<BoxCollider2D>();
            Assert.IsNotNull(collider, "Prefab 'Iron Armor' ต้องมีคอมโพเนนต์ BoxCollider2D");
            Assert.IsTrue(collider.isTrigger, "BoxCollider2D บน Prefab 'Iron Armor' ต้องติ๊กถูกที่ช่อง 'Is Trigger'");

            // 3. ตรวจสอบการแนบคอมโพเนนต์ Armor
            Assert.IsNotNull(ArmorType, "ต้องมีคลาส Armor ในสคริปต์");
            var armorComp = armorPrefab.GetComponent(ArmorType);
            Assert.IsNotNull(armorComp, "Prefab 'Iron Armor' ต้องมีคอมโพเนนต์ Armor แนบอยู่");

            var nameField = ArmorType.GetField("Name", AnyInstance) ?? ArmorType.GetField("name", AnyInstance) ?? ArmorType.GetField("armorName", AnyInstance);
            var bonusField = ArmorType.GetField("defenseBonus", AnyInstance) ?? ArmorType.GetField("defense", AnyInstance);

            if (nameField != null)
            {
                Assert.AreEqual("Iron Armor", nameField.GetValue(armorComp), "ค่า Name บน Prefab ต้องเป็น 'Iron Armor'");
            }
            if (bonusField != null)
            {
                Assert.AreEqual(10, (int)bonusField.GetValue(armorComp), "ค่า defenseBonus บน Prefab ต้องเป็น 10");
            }
        }
    }

    [TestFixture]
    [Category("Week08")]
    [Category("Week08_Exam")]
    public class Level3_MapGenerator_Tests : TestBase
    {
        [Test]
        public void Lv3_01_MapGenerator_PlaceExit()
        {
            Assert.IsNotNull(MapGenType, "ต้องมีคลาส MapGenerator หรือ InventoryAndGridScanner");

            var scanner = testGo.AddComponent(MapGenType);
            var mapGen = scanner as Week08.MapGenerator;
            Assert.IsNotNull(mapGen, "คอมโพเนนต์ต้องเป็นหรือสืบทอดจาก MapGenerator");

            mapGen.rows = 5;
            mapGen.columns = 5;
            mapGen.exitPos = new Vector2Int(4, 4);

            var dummyExitPrefab = new GameObject("ExitPrefab");
            mapGen.exitPrefab = dummyExitPrefab;

            try
            {
                mapGen.PlaceExit();
                var spawnedExit = GameObject.Find("Exit");
                Assert.IsNotNull(spawnedExit, "PlaceExit() ต้องเสกสร้าง GameObject ทางออกที่มีชื่อว่า 'Exit'");
                Assert.AreEqual(4f, spawnedExit.transform.position.x, 0.01f, "พิกัด X ของ GameObject ต้องตรงกับ exitPos.y");
                Assert.AreEqual(4f, spawnedExit.transform.position.y, 0.01f, "พิกัด Y ของ GameObject ต้องตรงกับ exitPos.x");
            }
            finally
            {
                var spawnedExit = GameObject.Find("Exit");
                if (spawnedExit != null) Object.DestroyImmediate(spawnedExit);
                Object.DestroyImmediate(dummyExitPrefab);
            }
        }

        [Test]
        public void Lv3_02_MapGenerator_PlaceArmor()
        {
            Assert.IsNotNull(MapGenType, "ต้องมีคลาส MapGenerator หรือ InventoryAndGridScanner");

            var scanner = testGo.AddComponent(MapGenType);
            var mapGen = scanner as Week08.MapGenerator;
            Assert.IsNotNull(mapGen, "คอมโพเนนต์ต้องเป็นหรือสืบทอดจาก MapGenerator");

            mapGen.rows = 5;
            mapGen.columns = 5;
            mapGen.armorCount = 2;

            var dummyArmorPrefab = new GameObject("Iron Armor");
            mapGen.armorPrefab = dummyArmorPrefab;

            var itemsFolder = new GameObject("Items");
            mapGen.itemParent = itemsFolder.transform;

            try
            {
                mapGen.PlaceArmor();
                var armors = itemsFolder.GetComponentsInChildren<Transform>();
                Assert.IsTrue(armors.Length > 1, "PlaceArmor() ต้องเสกสร้าง GameObject ไอเทมเกราะลงใน itemParent");
            }
            finally
            {
                Object.DestroyImmediate(itemsFolder);
                Object.DestroyImmediate(dummyArmorPrefab);
            }
        }

        [Test]
        public void Lv3_03_MapGenerator_GenerateLWall()
        {
            Assert.IsNotNull(MapGenType, "ต้องมีคลาส MapGenerator หรือ InventoryAndGridScanner");
            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(MapGenType), "คลาสต้องสืบทอดจาก MonoBehaviour");

            var scanner = testGo.AddComponent(MapGenType);
            var mapGen = scanner as Week08.MapGenerator;
            Assert.IsNotNull(mapGen, "คอมโพเนนต์ต้องเป็นหรือสืบทอดจาก MapGenerator");

            mapGen.rows = 6;
            mapGen.columns = 6;
            mapGen.mapGrid = new int[6, 6];

            var dummyWallPrefab = new GameObject("WallPrefab");
            mapGen.wallPrefabs = new GameObject[] { dummyWallPrefab };
            mapGen.wallParent = testGo.transform;

            try
            {
                mapGen.GenerateLWall();

                // ตรวจสอบว่ามีการเสกสร้าง GameObject กำแพงลงในฉากใต้ wallParent
                var spawnedWalls = testGo.GetComponentsInChildren<Transform>();
                Assert.IsTrue(spawnedWalls.Length > 1, "GenerateLWall() ต้องเสกสร้าง GameObject กำแพงลงใน wallParent");

                // ตรวจสอบตำแหน่งกำแพง: มีทั้งแนวดิ่ง (X=wallWidth หรือ X=0) และแนวนอน (Y=wallHeight หรือ Y=0)
                int wallHeight = mapGen.rows / 2;
                int wallWidth = mapGen.columns / 2;
                bool hasVertical = false;
                bool hasHorizontal = false;
                foreach (var t in spawnedWalls)
                {
                    if (t == testGo.transform) continue;
                    if ((Mathf.Approximately(t.position.x, wallWidth) || Mathf.Approximately(t.position.x, 0f)) && t.position.y >= 0f) hasVertical = true;
                    if ((Mathf.Approximately(t.position.y, wallHeight) || Mathf.Approximately(t.position.y, 0f)) && t.position.x >= 0f) hasHorizontal = true;
                }
                Assert.IsTrue(hasVertical, "GenerateLWall() ต้องเสกสร้างกำแพงแนวดิ่ง");
                Assert.IsTrue(hasHorizontal, "GenerateLWall() ต้องเสกสร้างกำแพงแนวนอน");
            }
            finally
            {
                Object.DestroyImmediate(dummyWallPrefab);
            }
        }

        [Test]
        public void Lv3_04_MapGenerator_ScanThreats()
        {
            Assert.IsNotNull(MapGenType, "ต้องมีคลาส MapGenerator หรือ InventoryAndGridScanner");

            var scanner = testGo.AddComponent(MapGenType);
            var mapGen = scanner as Week08.MapGenerator;
            Assert.IsNotNull(mapGen, "คอมโพเนนต์ต้องเป็นหรือสืบทอดจาก MapGenerator");

            // แผนที่ 5x5:
            // ผู้เล่นอยู่ที่ (1, 1): มี Threat (2) รอบตัวที่ (0, 1) และ (1, 2) รวม 2 ตัว
            // และมี Threat นอกระยะที่ (3, 3) ซึ่งไม่ควรถูกนับ
            int[,] map = new int[,]
            {
                { 0, 2, 0, 0, 0 },
                { 0, 0, 2, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 2, 0 },
                { 0, 0, 0, 0, 9 }
            };
            mapGen.mapGrid = map;

            // 1. สแกนรอบตัวผู้เล่นที่ (1, 1)
            int threatsAround = mapGen.ScanThreats(new Vector2Int(1, 1), 2);
            Assert.AreEqual(2, threatsAround, "ต้องนับรหัสภัยคุกคาม 2 รอบตัวผู้เล่นที่ (1, 1) ได้ 2 ช่อง (ไม่นับช่องนอกระยะ)");

            // 2. ทดสอบที่มุม (0, 0): ป้องกัน Out of Bounds
            int threatsCorner = mapGen.ScanThreats(new Vector2Int(0, 0), 2);
            Assert.AreEqual(1, threatsCorner, "ที่มุม (0, 0) มี Threat ที่ (0, 1) เพียง 1 ตัว และต้องไม่เกิด Out of Bounds Error");

            // 3. เรียกใช้งานผ่านพารามิเตอร์ตาราง 2 มิติ (Overload)
            int threatsOverload = mapGen.ScanThreats(map, new Vector2Int(1, 1), 2);
            Assert.AreEqual(2, threatsOverload, "ScanThreats(map, ...) ต้องนับได้ 2 ช่อง");
        }

        [Test]
        public void Lv3_05_MapGenerator_DisableSpawnItemPotions()
        {
            Assert.IsNotNull(MapGenType, "ต้องมีคลาส MapGenerator หรือ InventoryAndGridScanner");

            var scanner = testGo.AddComponent(MapGenType);
            var mapGen = scanner as Week08.MapGenerator;
            Assert.IsNotNull(mapGen, "คอมโพเนนต์ต้องเป็นหรือสืบทอดจาก MapGenerator");

            mapGen.rows = 5;
            mapGen.columns = 5;

            // จัดเตรียม Prefabs พื้นฐานเพื่อป้องกัน NullReferenceException ในขั้นตอน GenerateLWall/PlaceExit/PlaceArmor
            var dummyWallPrefab = new GameObject("DummyWall");
            mapGen.wallPrefabs = new GameObject[] { dummyWallPrefab };
            mapGen.wallParent = testGo.transform;

            var dummyExitPrefab = new GameObject("Exit");
            mapGen.exitPrefab = dummyExitPrefab;

            var dummyArmorPrefab = new GameObject("DummyArmor");
            mapGen.armorPrefab = dummyArmorPrefab;

            // โหลด Prefab โพชั่นของจริงจากโปรเจกต์ หรือสร้าง Dummy ขึ้นมาทดสอบ
            GameObject bluePrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Exam/Blue Potion.prefab");
            GameObject redPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Exam/Red Potion.prefab");
            bool createdBlue = false;
            bool createdRed = false;

            if (bluePrefab == null)
            {
                bluePrefab = new GameObject("Blue Potion");
                createdBlue = true;
            }
            if (redPrefab == null)
            {
                redPrefab = new GameObject("Red Potion");
                createdRed = true;
            }

            mapGen.itemPotions = new GameObject[] { bluePrefab, redPrefab };

            var itemsFolder = new GameObject("Items");
            mapGen.itemParent = itemsFolder.transform;

            try
            {
                mapGen.GenerateMap();

                // ตรวจสอบว่าในฉากต้องไม่มีทั้ง Blue Potion และ Red Potion ถูกเสกออกมา
                var allTransforms = Object.FindObjectsOfType<Transform>();
                var spawnedPotions = System.Array.FindAll(
                    allTransforms,
                    t => t != null && 
                         (t.name.Contains("Blue Potion") || t.name.Contains("Red Potion")) &&
                         t.gameObject != bluePrefab && t.gameObject != redPrefab
                );

                Assert.AreEqual(0, spawnedPotions.Length, "ต้องคอมเมนต์ปิดการเรียกใช้ SpawnItemPotions() ใน GenerateMap() เพื่อไม่ให้มี Blue Potion หรือ Red Potion ในซีน");
            }
            finally
            {
                Object.DestroyImmediate(itemsFolder);
                Object.DestroyImmediate(dummyWallPrefab);
                Object.DestroyImmediate(dummyExitPrefab);
                Object.DestroyImmediate(dummyArmorPrefab);
                if (createdBlue) Object.DestroyImmediate(bluePrefab);
                if (createdRed) Object.DestroyImmediate(redPrefab);
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
