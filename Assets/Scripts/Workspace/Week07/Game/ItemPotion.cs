using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// ไอเทมยาฟื้นฟูพลังงาน (ItemPotion) - ยกมาจาก Week 06
    /// 
    /// 🎯 โจทย์ Week 07 (ข้อ 6): ปรับปรุงเป็น OOP
    /// 1. เปลี่ยนการสืบทอดจาก MonoBehaviour ให้สืบทอดจาก Identity:
    ///    public class ItemPotion : Identity
    /// 2. คงตัวแปร public int healPoint = 10; ไว้
    /// 3. เขียน override void Hit() ทำงานเมื่อถูกเดินชน:
    ///    - แสดงข้อความ: $"You got {Name} : {healPoint}"
    ///    - เพิ่มพลังงานให้ผู้เล่น: mapGenerator.player.Heal(healPoint);
    ///    - ตั้งค่าช่องในแผนที่เป็น 0: mapGenerator.mapData[positionX, positionY] = 0;
    ///    - ทำลายตัวเอง: DestroySafe(gameObject);
    /// </summary>
    public class ItemPotion : MonoBehaviour
    {
        public string Name = "Potion";
        public int positionX;
        public int positionY;
        public int healPoint = 10;
        public MapGenerator mapGenerator;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Potion";
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"You got {Name} : {healPoint}");
                if (mapGenerator != null && mapGenerator.player != null)
                {
                    mapGenerator.player.Heal(healPoint);
                    mapGenerator.mapData[positionX, positionY] = 0;
                }
                else
                {
                    player.Heal(healPoint);
                }
                Destroy(gameObject);
            }
        }

        // ===== student code starts HERE =====
        // 🎯 ข้อ 6: รีแฟกเตอร์จาก OnTriggerEnter2D มาเป็น Hit()
        
        // ===== student code ends HERE =====
    }
}
