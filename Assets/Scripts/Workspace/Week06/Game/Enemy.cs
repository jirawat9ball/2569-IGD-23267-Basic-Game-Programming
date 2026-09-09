namespace Week06.Game
{
    public class Enemy : Character
    {
        public override void Hit()
        {
            // Guideline: (ข้อ 6)
            // 1. ตรวจก่อนว่า energy ของศัตรูหมดหรือยัง
            //    ถ้า energy <= 0 แปลว่าตายแล้ว ไม่ต้องทำอะไรต่อ ให้ return ออกไปเลย
            // 2. ถ้ายังไม่ตาย ให้ศัตรูตีผู้เล่นกลับ
            //    ใช้ this.Attack(mapGenerator.player, attackPoint);
            if (energy <= 0)
            {
                return;
            }

            this.Attack(mapGenerator.player, attackPoint);
        }
    }
}
