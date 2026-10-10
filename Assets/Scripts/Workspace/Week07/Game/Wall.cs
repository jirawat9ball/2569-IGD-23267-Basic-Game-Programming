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
    public class Wall : MonoBehaviour
    {
        public string Name = "Wall";
        public int positionX;
        public int positionY;
        public int durability = 3;
        public MapGenerator mapGenerator;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Wall";
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                durability--;
                Debug.Log($"Hit Wall {Name}! Remaining durability: {durability}");
                if (durability <= 0)
                {
                    Debug.Log($"Wall {Name} destroyed!");
                    if (mapGenerator != null)
                    {
                        mapGenerator.mapData[positionX, positionY] = 0;
                    }
                    Destroy(gameObject);
                }
            }
        }

        // ===== student code starts HERE =====
        // 🎯 ข้อ 8: รีแฟกเตอร์จาก OnTriggerEnter2D มาเป็น Hit()
        public void Hit(Player player = null)
        {
            // Guideline: (ข้อ 8)
            // 1. ลดความทนทานลง 1: durability--;
            // 2. แสดงข้อความ: $"Hit Wall {Name}! Remaining durability: {durability}"
            // 3. ถ้า durability <= 0:
            //    - แสดงข้อความ: $"Wall {Name} destroyed!"
            //    - ตั้งค่าช่องในแผนที่เป็น 0: mapGenerator.mapData[positionX, positionY] = 0;
            //    - ทำลายตัวเองด้วย DestroySafe(gameObject);
        }
        // ===== student code ends HERE =====
    }
}
