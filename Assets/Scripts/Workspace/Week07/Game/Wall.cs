using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// กำแพงสิ่งกีดขวาง (Wall) - ยกมาจาก Week 06
    /// 
    /// 🎯 โจทย์ Week 07 (ข้อ 8): ปรับปรุงเป็น OOP
    /// 1. เปลี่ยนการสืบทอดจาก MonoBehaviour ให้สืบทอดจาก Identity:
    ///    public class Wall : Identity
    /// 2. มีค่าความทนทาน durability = 3;
    /// 3. เขียน override void Hit():
    ///    - ลดความทนทานลง 1: durability--;
    ///    - แสดงข้อความ: $"Hit Wall {Name}! Remaining durability: {durability}"
    ///    - ถ้า durability <= 0:
    ///      แสดงข้อความ: $"Wall {Name} destroyed!"
    ///      ตั้งค่าช่องในแผนที่เป็น 0: mapGenerator.mapData[positionX, positionY] = 0;
    ///      ทำลายตัวเองด้วย DestroySafe(gameObject);
    /// </summary>
    public class Wall : Identity
    {
        public int durability = 3;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Wall";
            }
        }

        // ===== student code starts HERE =====
        public override void Hit(Player player = null)
        {
            durability--;
            Debug.Log($"Hit Wall {Name}! Remaining durability: {durability}");
            if (durability <= 0)
            {
                Debug.Log($"Wall {Name} destroyed!");
                if (mapGenerator != null && mapGenerator.mapData != null)
                {
                    mapGenerator.mapData[positionX, positionY] = 0;
                }
                DestroySafe(gameObject);
            }else{
                player.RevertPosition();
            }
        }

       
        // ===== student code ends HERE =====
    }
}
