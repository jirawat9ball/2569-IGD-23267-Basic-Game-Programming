using System.Text;
using NUnit.Framework;
using UnityEngine;
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
        protected static System.Type StatusType => (isTeacherMode ? FindType("Week08.Teacher.Ex01.CharacterStatus") : null) ?? FindType("Week08.Ex01.CharacterStatus");

        // Level 2: InventoryAndGridScanner
        protected static System.Type ScannerType => (isTeacherMode ? FindType("Week08.Teacher.Ex02.InventoryAndGridScanner") : null) ?? FindType("Week08.Ex02.InventoryAndGridScanner");

        // Level 3: Weapon & WeaponSpawner
        protected static System.Type WeaponType => (isTeacherMode ? FindType("Week08.Teacher.Ex03.Weapon") : null) ?? FindType("Week08.Ex03.Weapon");
        protected static System.Type SpawnerType => (isTeacherMode ? FindType("Week08.Teacher.Ex03.WeaponSpawner") : null) ?? FindType("Week08.Ex03.WeaponSpawner");

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

            Assert.IsNotNull(charNameField, "CharacterStatus ต้องมีฟิลด์ characterName");
            Assert.IsNotNull(maxHpField, "CharacterStatus ต้องมีฟิลด์ maxHealth");
            Assert.IsNotNull(curHpField, "CharacterStatus ต้องมีฟิลด์ currentHealth");
            Assert.IsNotNull(isAliveField, "CharacterStatus ต้องมีฟิลด์ isAlive");

            Assert.AreEqual("Hero", charNameField.GetValue(comp), "ค่าเริ่มต้นของ characterName ควรเป็น 'Hero'");
            Assert.AreEqual(100, (int)maxHpField.GetValue(comp), "ค่าเริ่มต้นของ maxHealth ควรเป็น 100");
            Assert.AreEqual(100, (int)curHpField.GetValue(comp), "ค่าเริ่มต้นของ currentHealth ควรเป็น 100");
            Assert.AreEqual(true, (bool)isAliveField.GetValue(comp), "ค่าเริ่มต้นของ isAlive ควรเป็น true");
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
    public class Level2_InventoryAndGrid_Tests : TestBase
    {
        [Test]
        public void Lv2_01_FindItemPosition_2DGrid()
        {
            Assert.IsNotNull(ScannerType, "ต้องมีคลาส InventoryAndGridScanner");
            Assert.AreEqual(typeof(MonoBehaviour), ScannerType.BaseType, "คลาส InventoryAndGridScanner ต้องสืบทอดจาก MonoBehaviour");

            var scanner = testGo.AddComponent(ScannerType);
            var method = ScannerType.GetMethod("FindItemPosition", AnyInstance);
            Assert.IsNotNull(method, "ต้องมีเมธอด FindItemPosition(int[,], int)");

            int[,] map = new int[,]
            {
                { 0, 3, 0 },
                { 1, 0, 0 },
                { 0, 0, 3 }
            };

            Vector2Int posFound = (Vector2Int)method.Invoke(scanner, new object[] { map, 3 });
            Assert.AreEqual(new Vector2Int(0, 1), posFound, "ต้องหาเจอไอเทมรหัส 3 ตัวแรกที่ตำแหน่ง (0, 1)");

            Vector2Int posNotFound = (Vector2Int)method.Invoke(scanner, new object[] { map, 999 });
            Assert.AreEqual(new Vector2Int(-1, -1), posNotFound, "หากหาไอเทมไม่เจอ ต้องคืนค่า (-1, -1)");
        }

        [Test]
        public void Lv2_02_CountItems_2DGrid()
        {
            var scanner = testGo.AddComponent(ScannerType);
            var method = ScannerType.GetMethod("CountItems", AnyInstance);
            Assert.IsNotNull(method, "ต้องมีเมธอด CountItems(int[,], int)");

            int[,] map = new int[,]
            {
                { 0, 3, 0, 0 },
                { 2, 0, 3, 0 },
                { 0, 0, 0, 3 }
            };

            int count = (int)method.Invoke(scanner, new object[] { map, 3 });
            Assert.AreEqual(3, count, "ต้องนับไอเทมรหัส 3 บนตารางได้ทั้งหมด 3 ชิ้น");

            int countZero = (int)method.Invoke(scanner, new object[] { map, 50 });
            Assert.AreEqual(0, countZero, "ถ้าไม่มีไอเทมรหัส 50 ต้องคืนค่า 0");
        }

        [Test]
        public void Lv2_03_ScanThreats_AroundPlayer()
        {
            var scanner = testGo.AddComponent(ScannerType);
            var method = ScannerType.GetMethod("ScanThreats", AnyInstance);
            Assert.IsNotNull(method, "ต้องมีเมธอด ScanThreats(int[,], Vector2Int, int)");

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

            int threatsAround = (int)method.Invoke(scanner, new object[] { map, new Vector2Int(1, 1), 2 });
            Assert.AreEqual(2, threatsAround, "ต้องนับรหัสภัยคุกคาม 2 รอบตัวผู้เล่นที่ (1, 1) ได้ 2 ช่อง (ไม่นับช่องนอกระยะ)");

            // ทดสอบที่มุม (0, 0): ตรวจสอบการกัน Out of Bounds
            int threatsCorner = (int)method.Invoke(scanner, new object[] { map, new Vector2Int(0, 0), 2 });
            Assert.AreEqual(1, threatsCorner, "ที่มุม (0, 0) มี Threat ที่ (0, 1) เพียง 1 ตัว และต้องไม่เกิด Out of Bounds Error");
        }

        [Test]
        public void Lv2_04_FindExitPosition_2DGrid()
        {
            var scanner = testGo.AddComponent(ScannerType);
            var method = ScannerType.GetMethod("FindExitPosition", AnyInstance);
            Assert.IsNotNull(method, "ต้องมีเมธอด FindExitPosition(int[,], int)");

            int[,] map = new int[,]
            {
                { 0, 1, 0, 0 },
                { 0, 0, 9, 0 },
                { 1, 0, 0, 0 }
            };

            Vector2Int exitPos = (Vector2Int)method.Invoke(scanner, new object[] { map, 9 });
            Assert.AreEqual(new Vector2Int(1, 2), exitPos, "ทางออกรหัส 9 อยู่ที่แถว 1 คอลัมน์ 2");

            Vector2Int notFound = (Vector2Int)method.Invoke(scanner, new object[] { map, 99 });
            Assert.AreEqual(new Vector2Int(-1, -1), notFound, "หากไม่พบทางออก ต้องคืนค่า (-1, -1)");
        }
    }

    [TestFixture]
    [Category("Week08")]
    [Category("Week08_Exam")]
    public class Level3_MethodAndClass_Tests : TestBase
    {
        [Test]
        public void Lv3_01_Weapon_ClassAndUse()
        {
            Assert.IsNotNull(WeaponType, "ต้องมีคลาส Weapon");
            Assert.AreEqual(typeof(MonoBehaviour), WeaponType.BaseType, "คลาส Weapon ต้องสืบทอดจาก MonoBehaviour");

            var weapon = testGo.AddComponent(WeaponType);
            var nameField = WeaponType.GetField("weaponName", AnyInstance);
            var atkField = WeaponType.GetField("attackPower", AnyInstance);
            var durField = WeaponType.GetField("durability", AnyInstance);

            Assert.IsNotNull(nameField, "Weapon ต้องมีฟิลด์ weaponName");
            Assert.IsNotNull(atkField, "Weapon ต้องมีฟิลด์ attackPower");
            Assert.IsNotNull(durField, "Weapon ต้องมีฟิลด์ durability");

            Assert.AreEqual("Wooden Stick", nameField.GetValue(weapon), "weaponName เริ่มต้นต้องเป็น 'Wooden Stick'");
            Assert.AreEqual(10, (int)atkField.GetValue(weapon), "attackPower เริ่มต้นต้องเป็น 10");
            Assert.AreEqual(20, (int)durField.GetValue(weapon), "durability เริ่มต้นต้องเป็น 20");

            var useMethod = WeaponType.GetMethod("Use", AnyInstance);
            Assert.IsNotNull(useMethod, "คลาส Weapon ต้องมีเมธอด Use()");
            SimpleDebugConsole.Clear();
            useMethod.Invoke(weapon, null);
            Assert.AreEqual(19, (int)durField.GetValue(weapon), "เมื่อเรียก Use() durability ต้องลดลง 1");
            Assert.IsTrue(SimpleDebugConsole.GetOutput().Contains("Wooden Stick used. Durability: 19"), "Use() ต้องพิมพ์ข้อความระบุ durability ล่าสุด");
        }

        [Test]
        public void Lv3_02_WeaponSpawner_NullPrefab()
        {
            Assert.IsNotNull(SpawnerType, "ต้องมีคลาส WeaponSpawner");
            Assert.AreEqual(typeof(MonoBehaviour), SpawnerType.BaseType, "คลาส WeaponSpawner ต้องสืบทอดจาก MonoBehaviour");

            var spawner = testGo.AddComponent(SpawnerType);
            var spawnMethod = SpawnerType.GetMethod("SpawnWeapon", AnyInstance);
            Assert.IsNotNull(spawnMethod, "ต้องมีเมธอด SpawnWeapon(GameObject, Vector3)");

            SimpleDebugConsole.Clear();
            var result = spawnMethod.Invoke(spawner, new object[] { null, Vector3.zero });
            Assert.IsNull(result, "หาก prefab เป็น null เมธอด SpawnWeapon ต้องคืนค่า null");
            Assert.IsTrue(SimpleDebugConsole.GetOutput().Contains("Prefab is null"), "หาก prefab เป็น null ต้องพิมพ์ 'Prefab is null'");
        }

        [Test]
        public void Lv3_03_WeaponSpawner_InstantiateStickPrefab()
        {
            Assert.IsNotNull(SpawnerType, "ต้องมีคลาส WeaponSpawner");

            var spawner = testGo.AddComponent(SpawnerType);
            var spawnMethod = SpawnerType.GetMethod("SpawnWeapon", AnyInstance);
            Assert.IsNotNull(spawnMethod, "ต้องมีเมธอด SpawnWeapon(GameObject, Vector3)");

            GameObject stickPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Exam/Wooden Stick.prefab");
            bool isTempPrefab = false;
            if (stickPrefab == null)
            {
                stickPrefab = new GameObject("Wooden Stick");
                isTempPrefab = true;
            }

            Vector3 spawnPos = new Vector3(2f, 3f, 0f);
            SimpleDebugConsole.Clear();
            GameObject spawnedObj = (GameObject)spawnMethod.Invoke(spawner, new object[] { stickPrefab, spawnPos });

            try
            {
                Assert.IsNotNull(spawnedObj, "SpawnWeapon ต้องเสกสร้าง GameObject ออกมา");
                Assert.IsTrue(spawnedObj.name.StartsWith("Wooden Stick"), "ชื่อของ Object ที่ถูกเสกต้องขึ้นต้นด้วยชื่อ Prefab");
                Assert.AreEqual(spawnPos.x, spawnedObj.transform.position.x, 0.01f, "พิกัด X ของ Object ต้องตรงกับ spawnPosition");
                Assert.AreEqual(spawnPos.y, spawnedObj.transform.position.y, 0.01f, "พิกัด Y ของ Object ต้องตรงกับ spawnPosition");
                Assert.IsTrue(SimpleDebugConsole.GetOutput().Contains("Spawned Wooden Stick"), "SpawnWeapon ต้องพิมพ์ข้อความการเสกสร้างวัตถุ");
            }
            finally
            {
                if (spawnedObj != null)
                    Object.DestroyImmediate(spawnedObj);
                if (isTempPrefab && stickPrefab != null)
                    Object.DestroyImmediate(stickPrefab);
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
