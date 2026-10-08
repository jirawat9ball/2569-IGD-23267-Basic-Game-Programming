using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// ไอเทมยาฟื้นฟูพลังงาน สืบทอดจาก Identity
    /// </summary>
    public class ItemPotion : Identity
    {
        // Guideline: (ข้อ 5)
        // 1. ประกาศตัวแปร healPoint แบบ public ชนิด int ค่าเริ่มต้น 10
        public int healPoint = 10;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Potion";
            }
        }

        public override void Hit()
        {
            // ===== student code starts HERE =====
            // Guideline: (ข้อ 6)
            // 1. พิมพ์ข้อความ "You got <ชื่อไอเทม> : <healPoint>"
            // 2. เพิ่มเลือดให้ผู้เล่น: mapGenerator.player.Heal(healPoint);
            // 3. เอาไอเทมออกจากแผนที่ โดยตั้งค่าช่องนั้นเป็น 0 ใน mapGenerator.mapData
            // 4. ทำลายวัตถุทิ้งด้วย DestroySafe(gameObject);

            // ===== student code ends HERE =====
        }
    }
}
