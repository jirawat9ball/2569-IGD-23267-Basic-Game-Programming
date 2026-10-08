using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// กล่องสมบัติในแผนที่ สืบทอดจาก Identity
    /// เมื่อถูกเปิด (Hit หรือ OpenChest) จะเสกไอเทมจาก spawnPrefab ออกมาและทำลายกล่อง
    /// </summary>
    public class Chest : Identity
    {
        public GameObject spawnPrefab;
        public bool isOpen = false;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Chest";
            }
        }

        public override void Hit()
        {
            // ===== student code starts HERE =====
            // Guideline: (ข้อ 9)
            // เรียก OpenChest();

            // ===== student code ends HERE =====
        }

        public void OpenChest()
        {
            // ===== student code starts HERE =====
            // Guideline: (ข้อ 9)
            // 1. ถ้า isOpen เป็นจริง ให้ return ทันที (ไม่เปิดซ้ำ)
            // 2. ตั้งค่า isOpen = true;
            // 3. แสดงข้อความ: "📦 Opened <Name>!"
            // 4. ถ้า spawnPrefab != null:
            //    - สร้างวัตถุใหม่ที่ตำแหน่ง transform.position + Vector3.up ด้วย Instantiate
            //    - บันทึกการเกิดวัตถุด้วย mapGenerator.TrackSpawned(...) ถ้า mapGenerator ไม่เป็น null
            // 5. ตั้งค่าช่องในแผนที่เป็น 0 (mapGenerator.mapData[positionX, positionY] = 0)
            // 6. ทำลายวัตถุทิ้งด้วย DestroySafe(gameObject);

            // ===== student code ends HERE =====
        }
    }
}
