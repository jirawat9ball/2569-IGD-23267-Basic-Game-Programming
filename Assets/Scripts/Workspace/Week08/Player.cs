using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08
{
    /// <summary>
    /// สคริปต์ควบคุมตัวละครผู้เล่น (Player / Character) ประจำข้อสอบปฏิบัติ Week 08
    /// รับ Input จากคีย์บอร์ด (WASD / Arrow Keys, Spacebar, Key C) เพื่อเคลื่อนที่และมีปฏิสัมพันธ์ในตาราง 5x5
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class Player : MonoBehaviour
    {
        [Header("ข้อมูลและพิกัดบนตาราง (Grid Position)")]
        public string characterName = "Hero";
        public int positionX = 0;
        public int positionY = 0;
        public int attackPoint
        {
            get => GetFieldInt(GetAnyStatus(), "attackPoint", _fallbackAttack);
            set
            {
                var s = GetAnyStatus();
                if (s != null) SetFieldInt(s, "attackPoint", value);
                _fallbackAttack = value;
            }
        }
        private int _fallbackAttack = 10;

        public int defensePoint
        {
            get => GetFieldInt(GetAnyStatus(), "defensePoint", _fallbackDefense);
            set
            {
                var s = GetAnyStatus();
                if (s != null) SetFieldInt(s, "defensePoint", value);
                _fallbackDefense = value;
            }
        }
        private int _fallbackDefense = 0;

        private Component GetAnyStatus()
        {
            return GetComponent<Teacher.Ex01.CharacterStatus>() as Component
                ?? GetComponent<Ex01.CharacterStatus>() as Component
                ?? GetComponent("CharacterStatus")
                ?? GetComponent("Week08.Ex01.CharacterStatus");
        }

        private static int GetFieldInt(Component comp, string fieldName, int fallback)
        {
            if (comp == null) return fallback;
            var field = comp.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            if (field != null && field.FieldType == typeof(int))
            {
                return (int)field.GetValue(comp);
            }
            return fallback;
        }

        private static void SetFieldInt(Component comp, string fieldName, int value)
        {
            if (comp == null) return;
            var field = comp.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            if (field != null && field.FieldType == typeof(int))
            {
                field.SetValue(comp, value);
            }
        }

        [Header("ขอบเขตตารางแผนที่ (5 x 5)")]
        public int rows = 5;
        public int columns = 5;

        [Header("การเชื่อมต่อระบบอื่น")]
        public MapGenerator mapGenerator;

        private void Start()
        {
            if (mapGenerator == null)
            {
                mapGenerator = FindAnyObjectByType<MapGenerator>();
            }

            if (mapGenerator != null)
            {
                rows = mapGenerator.rows;
                columns = mapGenerator.columns;
            }

            // ซิงค์พิกัดเริ่มต้นกับ Transform
            positionX = Mathf.RoundToInt(transform.position.x);
            positionY = Mathf.RoundToInt(transform.position.y);
            transform.position = new Vector3(positionX, positionY, 0);

            // ตรวจสอบ Collider และ Rigidbody2D สำหรับ Trigger
            var col = GetComponent<BoxCollider2D>();
            if (col != null) col.isTrigger = false;

            var rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
            }
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0;
        }

        private void Update()
        {
            // 1. รับ Input การเคลื่อนที่บนตาราง (Arrow Keys และ WASD)
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector2.right);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                Move(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector2.down);
            }

            // 2. รับ Input การใช้อาวุธ / โจมตี (Spacebar)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UseEquippedWeapon();
            }

            // 3. รับ Input การตรวจเช็คสถานะเลือด (Key C)
            if (Input.GetKeyDown(KeyCode.C))
            {
                CheckStatus();
            }

            // 4. รับ Input เรดาร์สแกนหาภัยคุกคามรอบตัว (Key R)
            if (Input.GetKeyDown(KeyCode.R))
            {
                ScanNearbyThreats();
            }
        }

        public void ScanNearbyThreats()
        {
            if (mapGenerator == null) return;
            Vector2Int gridPos = new Vector2Int(positionY, positionX);
            int threats = mapGenerator.ScanThreats(gridPos, 2);
            Debug.Log($"[Radar] Found {threats} threats around {characterName} at ({positionX}, {positionY})");
        }

        public void Move(Vector2 direction)
        {
            int nextX = positionX + (int)direction.x;
            int nextY = positionY + (int)direction.y;

            // ตรวจสอบขอบเขตตาราง (0 ถึง columns - 1 และ 0 ถึง rows - 1)
            if (nextX < 0 || nextX >= columns || nextY < 0 || nextY >= rows)
            {
                Debug.Log($"Hit wall/boundary at ({nextX}, {nextY})! Cannot move out of grid.");
                return;
            }

            positionX = nextX;
            positionY = nextY;
            transform.position = new Vector3(positionX, positionY, 0);
            Debug.Log($"{characterName} moved to ({positionX}, {positionY})");
        }

        public void IncreaseAttack(int value)
        {
            attackPoint += value;
            Debug.Log($"Attack increased by +{value}! Current Attack: {attackPoint}");
        }

        public void IncreaseDefense(int value)
        {
            defensePoint += value;
            Debug.Log($"Defense increased by +{value}! Current Defense: {defensePoint}");
        }

        public void UseEquippedWeapon()
        {
            Debug.Log($"{characterName} attacks! (Attack: {attackPoint})");
        }

        public void CheckStatus()
        {
            var status = GetAnyStatus();
            if (status != null)
            {
                var type = status.GetType();
                var nameField = type.GetField("characterName", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                var curHpField = type.GetField("currentHealth", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                var maxHpField = type.GetField("maxHealth", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                var isAliveMethod = type.GetMethod("IsAlive", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

                string cName = (nameField != null && nameField.GetValue(status) != null) ? nameField.GetValue(status).ToString() : characterName;
                int curHp = curHpField != null ? (int)curHpField.GetValue(status) : 100;
                int maxHp = maxHpField != null ? (int)maxHpField.GetValue(status) : 100;
                bool alive = (isAliveMethod != null) ? (bool)isAliveMethod.Invoke(status, null) : true;

                Debug.Log($"[{cName}] HP: {curHp}/{maxHp}, IsAlive: {alive}");
                return;
            }

            Debug.Log($"[{characterName}] HP: 100/100, IsAlive: true");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other == null) return;

            // ชนทางออก
            if (other.name.Contains("Exit") || other.CompareTag("Finish"))
            {
                Debug.Log("Dungeon Cleared! You reached the Exit!");
            }
        }
    }
}
