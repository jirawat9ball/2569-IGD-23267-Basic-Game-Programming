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
            if (mapGenerator == null || mapGenerator.mapGrid == null) return;
            Vector2Int gridPos = new Vector2Int(positionY, positionX);

            var scanner = GetComponent<Ex02.InventoryAndGridScanner>();
            if (scanner != null)
            {
                int threats = scanner.ScanThreats(mapGenerator.mapGrid, gridPos, 2);
                Debug.Log($"[Radar] Found {threats} threats around {characterName} at ({positionX}, {positionY})");
                return;
            }

            var teacherScanner = GetComponent<Teacher.Ex02.InventoryAndGridScanner>();
            if (teacherScanner != null)
            {
                int threats = teacherScanner.ScanThreats(mapGenerator.mapGrid, gridPos, 2);
                Debug.Log($"[Radar] Found {threats} threats around {characterName} at ({positionX}, {positionY})");
                return;
            }
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

        public void UseEquippedWeapon()
        {
            var weapon = GetComponent<Ex03.Weapon>();
            if (weapon != null)
            {
                weapon.Use();
                return;
            }

            var teacherWeapon = GetComponent<Teacher.Ex03.Weapon>();
            if (teacherWeapon != null)
            {
                teacherWeapon.Use();
                return;
            }

            Debug.Log($"{characterName} attacks with bare hands! (Damage: 5)");
        }

        public void CheckStatus()
        {
            var status = GetComponent<Ex01.CharacterStatus>();
            if (status != null)
            {
                bool alive = status.IsAlive();
                Debug.Log($"[{status.characterName}] HP: {status.currentHealth}/{status.maxHealth}, IsAlive: {alive}");
                return;
            }

            var teacherStatus = GetComponent<Teacher.Ex01.CharacterStatus>();
            if (teacherStatus != null)
            {
                bool alive = teacherStatus.IsAlive();
                Debug.Log($"[{teacherStatus.characterName}] HP: {teacherStatus.currentHealth}/{teacherStatus.maxHealth}, IsAlive: {alive}");
                return;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other == null) return;

            // เก็บไอเทมไม้สติ๊ก
            if (other.name.Contains("Wooden Stick"))
            {
                Debug.Log($"Picked up {other.name}!");
                var weapon = GetComponent<Ex03.Weapon>();
                if (weapon == null)
                {
                    gameObject.AddComponent<Ex03.Weapon>();
                }
                Destroy(other.gameObject);
            }
            // ชนทางออก
            else if (other.name.Contains("Exit") || other.CompareTag("Finish"))
            {
                Debug.Log("Dungeon Cleared! You reached the Exit!");
            }
        }
    }
}
