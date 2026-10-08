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
            // Guideline: (ข้อ 5)
            // 2. พิมพ์ข้อความ "You got <ชื่อไอเทม> : <healPoint>"
            // 3. เพิ่มเลือดให้ผู้เล่น: mapGenerator.player.Heal(healPoint);
            // 4. เอาไอเทมออกจากแผนที่ โดยตั้งค่าช่องนั้นเป็น 0 แล้วทำลายวัตถุทิ้ง
            Debug.Log($"You got {Name} : {healPoint}");

            if (mapGenerator != null && mapGenerator.player != null)
            {
                mapGenerator.player.Heal(healPoint);
            }

            if (mapGenerator != null && mapGenerator.mapData != null)
            {
                mapGenerator.mapData[positionX, positionY] = 0;
            }

            DestroySafe(gameObject);
        }
    }
}
