using UnityEngine;

namespace Week07.Game
{
    /// <summary>
    /// สคริปต์ศัตรู (Enemy) สืบทอดจาก Character (ซึ่งสืบทอดจาก Identity)
    /// มีความสามารถเหมือนตัวละครทุกอย่าง และ override Hit() เพื่อตีสวนผู้เล่น
    /// </summary>
    public class Enemy : Character
    {
        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Enemy";
            }
            if (energy <= 0)
            {
                energy = 10;
            }
            if (attackPoint <= 0)
            {
                attackPoint = 5;
            }
        }

        public override void Hit()
        {
            // ===== student code starts HERE =====
            // Guideline: (ข้อ 5)
            // 1. ตรวจก่อนว่า energy ของศัตรูหมดหรือยัง
            //    ถ้า energy <= 0 แปลว่าตายแล้ว ไม่ต้องทำอะไรต่อ ให้ return ออกไปเลย
            // 2. ถ้ายังไม่ตาย ให้ศัตรูตีผู้เล่นกลับ
            //    ใช้ this.Attack(mapGenerator.player, attackPoint);

            // ===== student code ends HERE =====
        }
    }
}
