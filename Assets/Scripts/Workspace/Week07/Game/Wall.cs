using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// กำแพงสิ่งกีดขวางในแผนที่ สืบทอดจาก Identity
    /// มีค่าความทนทาน (durability) เมื่อถูกโจมตี/ชน จะลดความทนทานลง และทำลายตัวเองเมื่อ durability หมด
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

        public override void Hit()
        {
            // ===== student code starts HERE =====
            // Guideline: (ข้อ 8)
            // 1. ลดความทนทานลง 1: durability--;
            // 2. แสดงข้อความ: "Hit Wall <Name>! Remaining durability: <durability>"
            // 3. ถ้า durability <= 0:
            //    - แสดงข้อความ: "💥 Wall <Name> destroyed!"
            //    - ตั้งค่าช่องในแผนที่เป็น 0 (mapGenerator.mapData[positionX, positionY] = 0)
            //    - ทำลายวัตถุทิ้งด้วย DestroySafe(gameObject);

            // ===== student code ends HERE =====
        }
    }
}
