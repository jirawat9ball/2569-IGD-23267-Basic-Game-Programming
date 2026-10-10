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
    public class ItemSword : MonoBehaviour
    {
        public string Name = "Sword";
        public int positionX;
        public int positionY;
        public int attackBonus = 10;
        public MapGenerator mapGenerator;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Sword";
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"You got {Name} : {attackBonus}");
                if (mapGenerator != null && mapGenerator.player != null)
                {
                    mapGenerator.player.IncreaseAttack(attackBonus);
                    mapGenerator.mapData[positionX, positionY] = 0;
                }
                else
                {
                    player.IncreaseAttack(attackBonus);
                }
                Destroy(gameObject);
            }
        }

        // ===== student code starts HERE =====
        // 🎯 ข้อ 7: รีแฟกเตอร์จาก OnTriggerEnter2D มาเป็น Hit()
        
        // ===== student code ends HERE =====
    }
}
