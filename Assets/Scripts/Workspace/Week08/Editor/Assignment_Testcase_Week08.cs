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

        // Level 2: MapGenerator & Scanner
        protected static System.Type MapGenType => (isTeacherMode ? FindType("Week08.Teacher.Ex02.InventoryAndGridScanner") : null)
            ?? FindType("Week08.MapGenerator") 
            ?? FindType("Week08.Teacher.Ex02.InventoryAndGridScanner")
            ?? FindType("Week08.Ex02.InventoryAndGridScanner")
            ?? FindType("MapGenerator");
        protected static System.Type ScannerType => MapGenType;

        // Level 3: Armor & ArmorSpawner
        protected static System.Type ArmorType => (isTeacherMode ? FindType("Week08.Teacher.Ex03.Armor") : null) 
            ?? FindType("Week08.Ex03.Armor") 
            ?? FindType("Week08.Armor")
            ?? FindType("Armor")
            ?? (isTeacherMode ? FindType("Week08.Teacher.Ex03.Weapon") : null) 
            ?? FindType("Week08.Ex03.Weapon");

        protected static System.Type SpawnerType => (isTeacherMode ? FindType("Week08.Teacher.Ex03.ArmorSpawner") : null) 
            ?? FindType("Week08.Ex03.ArmorSpawner") 
            ?? FindType("Week08.ArmorSpawner")
            ?? FindType("ArmorSpawner")
            ?? (isTeacherMode ? FindType("Week08.Teacher.Ex03.WeaponSpawner") : null) 
            ?? FindType("Week08.Ex03.WeaponSpawner");

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
        public void Lv1_03_IsAlive_And_CalculateDamage_ReturnMethods()
        {
            var comp = testGo.AddComponent(StatusType);
            var isAliveMethod = StatusType.GetMethod("IsAlive", AnyInstance);
            var calcMethod = StatusType.GetMethod("CalculateDamage", AnyInstance);

            Assert.IsNotNull(isAliveMethod, "ต้องมีเมธอด IsAlive()");
            Assert.AreEqual(typeof(bool), isAliveMethod.ReturnType, "เมธอด IsAlive ต้องคืนค่าเป็น bool");

            Assert.IsNotNull(calcMethod, "ต้องมีเมธอด CalculateDamage(int, int)");
            Assert.AreEqual(typeof(int), calcMethod.ReturnType, "เมธอด CalculateDamage ต้องคืนค่าเป็น int");

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
    public class Level2_MapGenerator_Tests : TestBase
    {
        [Test]
        public void Lv2_01_MapGenerator_FindItemPosition()
        {
            Assert.IsNotNull(MapGenType, "ต้องมีคลาส MapGenerator หรือ InventoryAndGridScanner");
            Assert.IsTrue(typeof(MonoBehaviour).IsAssignableFrom(MapGenType), "คลาสต้องสืบทอดจาก MonoBehaviour");

            var scanner = testGo.AddComponent(MapGenType);
            var mapGen = scanner as Week08.MapGenerator;
            Assert.IsNotNull(mapGen, "คอมโพเนนต์ต้องเป็นหรือสืบทอดจาก MapGenerator");

            int[,] map = new int[,]
            {
                { 0, 3, 0 },
                { 1, 0, 0 },
                { 0, 0, 3 }
            };
            mapGen.mapGrid = map;

            // 1. เรียกใช้งานผ่าน MapGenerator (ดึงข้อมูลจาก mapGrid ภายใน)
            Vector2Int posDirect = mapGen.FindItemPosition(3);
            Assert.AreEqual(new Vector2Int(0, 1), posDirect, "FindItemPosition(3) บน MapGenerator ต้องหาเจอไอเทมชิ้นแรกที่ (0, 1)");

            Vector2Int posNotFound = mapGen.FindItemPosition(999);
            Assert.AreEqual(new Vector2Int(-1, -1), posNotFound, "หากหาไอเทมไม่เจอ ต้องคืนค่า (-1, -1)");

            // 2. เรียกใช้งานผ่านพารามิเตอร์ตาราง 2 มิติ (Overload)
            Vector2Int posOverload = mapGen.FindItemPosition(map, 3);
            Assert.AreEqual(new Vector2Int(0, 1), posOverload, "FindItemPosition(map, 3) ต้องหาเจอที่ (0, 1)");
        }

        [Test]
        public void Lv2_02_MapGenerator_CountItems()
        {
            Assert.IsNotNull(MapGenType, "ต้องมีคลาส MapGenerator หรือ InventoryAndGridScanner");

            var scanner = testGo.AddComponent(MapGenType);
            var mapGen = scanner as Week08.MapGenerator;
            Assert.IsNotNull(mapGen, "คอมโพเนนต์ต้องเป็นหรือสืบทอดจาก MapGenerator");

            int[,] map = new int[,]
            {
                { 0, 3, 0, 0 },
                { 2, 0, 3, 0 },
                { 0, 0, 0, 3 }
            };
            mapGen.mapGrid = map;

            // 1. เรียกใช้งานผ่าน MapGenerator (ดึงข้อมูลจาก mapGrid ภายใน)
            int countDirect = mapGen.CountItems(3);
            Assert.AreEqual(3, countDirect, "CountItems(3) บน MapGenerator ต้องนับไอเทมรหัส 3 ได้ทั้งหมด 3 ชิ้น");

            int countZero = mapGen.CountItems(50);
            Assert.AreEqual(0, countZero, "ถ้าไม่มีไอเทมรหัส 50 ต้องคืนค่า 0");

            // 2. เรียกใช้งานผ่านพารามิเตอร์ตาราง 2 มิติ (Overload)
            int countOverload = mapGen.CountItems(map, 3);
            Assert.AreEqual(3, countOverload, "CountItems(map, 3) ต้องนับไอเทมได้ 3 ชิ้น");
        }

        [Test]
        public void Lv2_03_MapGenerator_ScanThreats()
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
        public void Lv2_04_MapGenerator_FindPlayerPosition()
        {
            Assert.IsNotNull(MapGenType, "ต้องมีคลาส MapGenerator หรือ InventoryAndGridScanner");

            var scanner = testGo.AddComponent(MapGenType);
            var mapGen = scanner as Week08.MapGenerator;
            Assert.IsNotNull(mapGen, "คอมโพเนนต์ต้องเป็นหรือสืบทอดจาก MapGenerator");
            mapGen.playerStartPos = new Vector2Int(2, 3);

            // 1. ทดสอบ FindPlayerPosition() จาก MapGenerator
            Vector2Int posDirect = mapGen.FindPlayerPosition();
            Assert.AreEqual(new Vector2Int(2, 3), posDirect, "FindPlayerPosition() ต้องดึงตำแหน่ง playerStartPos ได้ถูกต้อง");

            // 2. ทดสอบ FindPlayerPosition(MapGenerator mapGen)
            Vector2Int posFromGen = mapGen.FindPlayerPosition(mapGen);
            Assert.AreEqual(new Vector2Int(2, 3), posFromGen, "FindPlayerPosition(mapGen) ต้องดึงค่า playerStartPos ได้");

            Vector2Int nullGen = mapGen.FindPlayerPosition((Week08.MapGenerator)null);
            Assert.AreEqual(new Vector2Int(-1, -1), nullGen, "เมื่อ MapGenerator เป็น null ต้องคืนค่า (-1, -1)");

            // 3. ทดสอบ FindPlayerPosition(int[,] grid, int playerCode)
            int[,] map = new int[,]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 0 }
            };
            mapGen.mapGrid = map;

            Vector2Int playerPos = mapGen.FindPlayerPosition(map, 1);
            Assert.AreEqual(new Vector2Int(1, 2), playerPos, "ผู้เล่นรหัส 1 อยู่ที่แถว 1 คอลัมน์ 2");

            Vector2Int notFound = mapGen.FindPlayerPosition(map, 99);
            Assert.AreEqual(new Vector2Int(-1, -1), notFound, "หากไม่พบผู้เล่นรหัส 99 ต้องคืนค่า (-1, -1)");
        }
    }

    [TestFixture]
    [Category("Week08")]
    [Category("Week08_Exam")]
    public class Level3_MethodAndClass_Tests : TestBase
    {
        [Test]
        public void Lv3_01_Armor_FieldsAndTrigger()
        {
            Assert.IsNotNull(ArmorType, "ต้องมีคลาส Armor ใน namespace Week08.Ex03 (นักศึกษาต้องเขียนประกาศคลาส Armor : MonoBehaviour ขึ้นมาเอง)");
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

            var triggerMethod = ArmorType.GetMethod("OnTriggerEnter2D", AnyInstance);
            Assert.IsNotNull(triggerMethod, "Armor ต้องมีเมธอด OnTriggerEnter2D()");

            // ทดสอบชน Player
            var playerGo = new GameObject("TestPlayer");
            var col = playerGo.AddComponent<BoxCollider2D>();
            var statusComp = playerGo.AddComponent(StatusType);
            var player = playerGo.AddComponent<Week08.Player>();
            player.defensePoint = 0;

            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex("Destroy may not be called from edit mode"));

            SimpleDebugConsole.Clear();
            triggerMethod.Invoke(armor, new object[] { col });

            Assert.AreEqual(10, player.defensePoint, "เมื่อเก็บไอเทมเกราะ ค่า defensePoint ของ Player ต้องเพิ่มขึ้นตาม defenseBonus (+10)");
            Assert.IsTrue(SimpleDebugConsole.GetOutput().Contains("Picked up Iron Armor! +10 Defense"), "ต้องพิมพ์ข้อความการเก็บเกราะ เช่น 'Picked up Iron Armor! +10 Defense'");

            Object.DestroyImmediate(playerGo);
        }

        [Test]
        public void Lv3_02_ArmorSpawner_NullPrefab()
        {
            Assert.IsNotNull(SpawnerType, "ต้องมีคลาส ArmorSpawner");
            Assert.AreEqual(typeof(MonoBehaviour), SpawnerType.BaseType, "คลาส ArmorSpawner ต้องสืบทอดจาก MonoBehaviour");

            var spawner = testGo.AddComponent(SpawnerType);
            var spawnMethod = SpawnerType.GetMethod("SpawnArmor", AnyInstance) ?? SpawnerType.GetMethod("SpawnWeapon", AnyInstance);
            Assert.IsNotNull(spawnMethod, "ต้องมีเมธอด SpawnArmor(GameObject, Vector3)");

            SimpleDebugConsole.Clear();
            var result = spawnMethod.Invoke(spawner, new object[] { null, Vector3.zero });
            Assert.IsNull(result, "หาก prefab เป็น null เมธอด SpawnArmor ต้องคืนค่า null");
            Assert.IsTrue(SimpleDebugConsole.GetOutput().Contains("Prefab is null"), "หาก prefab เป็น null ต้องพิมพ์ 'Prefab is null'");
        }

        [Test]
        public void Lv3_03_ArmorSpawner_InstantiateArmorPrefab()
        {
            Assert.IsNotNull(SpawnerType, "ต้องมีคลาส ArmorSpawner");

            var spawner = testGo.AddComponent(SpawnerType);
            var spawnMethod = SpawnerType.GetMethod("SpawnArmor", AnyInstance) ?? SpawnerType.GetMethod("SpawnWeapon", AnyInstance);
            Assert.IsNotNull(spawnMethod, "ต้องมีเมธอด SpawnArmor(GameObject, Vector3)");

            GameObject armorPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Exam/Iron Armor.prefab");
            Assert.IsNotNull(armorPrefab, "ไม่พบไฟล์ Prefab 'Assets/Prefabs/Exam/Iron Armor.prefab'! ให้นักศึกษาสร้าง Prefab เกราะขึ้นมาเองใน Unity Editor แล้วบันทึกไว้ที่ Assets/Prefabs/Exam/Iron Armor.prefab");

            Vector3 spawnPos = new Vector3(2f, 3f, 0f);
            SimpleDebugConsole.Clear();
            GameObject spawnedObj = (GameObject)spawnMethod.Invoke(spawner, new object[] { armorPrefab, spawnPos });

            try
            {
                Assert.IsNotNull(spawnedObj, "SpawnArmor ต้องเสกสร้าง GameObject ออกมา");
                Assert.IsTrue(spawnedObj.name.StartsWith("Iron Armor"), "ชื่อของ Object ที่ถูกเสกต้องขึ้นต้นด้วยชื่อ Prefab ('Iron Armor')");
                Assert.AreEqual(spawnPos.x, spawnedObj.transform.position.x, 0.01f, "พิกัด X ของ Object ต้องตรงกับ spawnPosition");
                Assert.AreEqual(spawnPos.y, spawnedObj.transform.position.y, 0.01f, "พิกัด Y ของ Object ต้องตรงกับ spawnPosition");
                Assert.IsTrue(SimpleDebugConsole.GetOutput().Contains("Spawned Iron Armor"), "SpawnArmor ต้องพิมพ์ข้อความการเสกสร้างวัตถุ เช่น 'Spawned Iron Armor at ...'");
            }
            finally
            {
                if (spawnedObj != null)
                    Object.DestroyImmediate(spawnedObj);
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
