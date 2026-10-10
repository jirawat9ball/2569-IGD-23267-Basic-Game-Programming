using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// ไอเทมยาฟื้นฟูพลังงาน (ItemPotion) - ยกมาจาก Week 06
    /// 
    /// 🎯 โจทย์ Week 07 (ข้อ 6): ปรับปรุงเป็น OOP
    /// 1. เปลี่ยนการสืบทอดจาก MonoBehaviour ให้สืบทอดจาก Identity:
    ///    public class ItemPotion : Identity
    /// 2. คงตัวแปร public int healPoint = 10; ไว้
    /// 3. เขียน override void Hit() ทำงานเมื่อถูกเดินชน:
    ///    - แสดงข้อความ: $"You got {Name} : {healPoint}"
    ///    - เพิ่มพลังงานให้ผู้เล่น: mapGenerator.player.Heal(healPoint);
    ///    - ตั้งค่าช่องในแผนที่เป็น 0: mapGenerator.mapData[positionX, positionY] = 0;
    ///    - ทำลายตัวเอง: DestroySafe(gameObject);
    /// </summary>
    public class ItemPotion : Identity
    {
        public int healPoint = 10;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Potion";
            }
        }

        // ===== student code starts HERE =====
        public override void Hit(Player player = null)
        {
            Debug.Log($"You got {Name} : {healPoint}");

            if (player == null && mapGenerator != null)
            {
                player = mapGenerator.player;
            }

            if (player != null)
            {
                player.Heal(healPoint);
            }

            if (mapGenerator != null && mapGenerator.mapData != null)
            {
                mapGenerator.mapData[positionX, positionY] = 0;
            }

            DestroySafe(gameObject);
        }
        // ===== student code ends HERE =====
    }
}
