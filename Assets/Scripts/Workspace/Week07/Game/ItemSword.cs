using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// ไอเทมดาบเพิ่มพลังโจมตี (ItemSword) - ยกมาจาก Week 06
    /// 
    /// 🎯 โจทย์ Week 07 (ข้อ 7): ปรับปรุงเป็น OOP
    /// 1. เปลี่ยนการสืบทอดจาก MonoBehaviour ให้สืบทอดจาก Identity:
    ///    public class ItemSword : Identity
    /// 2. คงตัวแปร public int attackBonus = 10; ไว้
    /// 3. เขียน override void Hit() ทำงานเมื่อถูกเดินชน:
    ///    - แสดงข้อความ: $"You got {Name} : {attackBonus}"
    ///    - เพิ่มพลังโจมตีให้ผู้เล่น: mapGenerator.player.IncreaseAttack(attackBonus);
    ///    - ตั้งค่าช่องในแผนที่เป็น 0: mapGenerator.mapData[positionX, positionY] = 0;
    ///    - ทำลายตัวเอง: DestroySafe(gameObject);
    /// </summary>
    public class ItemSword : Identity
    {
        public int attackBonus = 10;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Sword";
            }
        }

        // ===== student code starts HERE =====
        public override void Hit(Player player = null)
        {
            Debug.Log($"You got {Name} : {attackBonus}");

            if (player == null && mapGenerator != null)
            {
                player = mapGenerator.player;
            }

            if (player != null)
            {
                player.IncreaseAttack(attackBonus);
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
