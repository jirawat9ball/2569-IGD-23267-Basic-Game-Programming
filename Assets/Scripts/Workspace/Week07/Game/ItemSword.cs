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
            // Guideline: (ข้อ 6)
            // 2. พิมพ์ข้อความ "You got <ชื่อไอเทม> : <attackBonus>"
            // 3. เพิ่มพลังโจมตีให้ผู้เล่น: mapGenerator.player.IncreaseAttack(attackBonus);
            // 4. เอาไอเทมออกจากแผนที่ แบบเดียวกับ Potion
            Debug.Log($"You got {Name} : {attackBonus}");

            if (mapGenerator != null && mapGenerator.player != null)
            {
                mapGenerator.player.IncreaseAttack(attackBonus);
            }

            if (mapGenerator != null && mapGenerator.mapData != null)
            {
                mapGenerator.mapData[positionX, positionY] = 0;
            }

            DestroySafe(gameObject);
        }
    }
}
