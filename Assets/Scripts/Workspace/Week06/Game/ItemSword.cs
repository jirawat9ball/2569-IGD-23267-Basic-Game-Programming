using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    public class ItemSword : Identity
    {
        // Guideline: (ข้อ 8)
        // 1. ประกาศตัวแปร attackBonus แบบ public ชนิด int ค่าเริ่มต้น 10
        public int attackBonus = 10;

        public override void Hit()
        {
            // Guideline: (ข้อ 8)
            // 2. พิมพ์ข้อความ "You got <ชื่อไอเทม> : <attackBonus>"
            // 3. เพิ่มพลังโจมตีให้ผู้เล่น: mapGenerator.player.IncreaseAttack(attackBonus);
            // 4. เอาไอเทมออกจากแผนที่ แบบเดียวกับ Potion
            Debug.Log($"You got {Name} : {attackBonus}");

            mapGenerator.player.IncreaseAttack(attackBonus);

            mapGenerator.mapData[positionX, positionY] = 0;
            DestroySafe(gameObject);
        }
    }
}
