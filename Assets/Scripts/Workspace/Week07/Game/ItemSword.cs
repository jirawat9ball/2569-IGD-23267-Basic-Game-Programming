using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// ไอเทมดาบเพิ่มพลังโจมตี สืบทอดจาก Identity
    /// </summary>
    public class ItemSword : Identity
    {
        // Guideline: (ข้อ 6)
        // 1. ประกาศตัวแปร attackBonus แบบ public ชนิด int ค่าเริ่มต้น 10
        public int attackBonus = 10;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Sword";
            }
        }

        public override void Hit()
        {
            // ===== student code starts HERE =====
            // Guideline: (ข้อ 7)
            // 1. พิมพ์ข้อความ "You got <ชื่อไอเทม> : <attackBonus>"
            // 2. เพิ่มพลังโจมตีให้ผู้เล่น: mapGenerator.player.IncreaseAttack(attackBonus);
            // 3. เอาไอเทมออกจากแผนที่แบบเดียวกับ Potion (ตั้ง mapGenerator.mapData[positionX, positionY] = 0)
            // 4. ทำลายวัตถุทิ้งด้วย DestroySafe(gameObject);

            // ===== student code ends HERE =====
        }
    }
}
