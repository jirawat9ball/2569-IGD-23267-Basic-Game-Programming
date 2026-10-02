using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.HW02
{
    /// <summary>
    /// Homework ข้อ 2: การสร้างคลาส Trap (กับดักหนาม) สำหรับสร้างความเสียหายแก่ผู้เล่น
    /// </summary>
    public class Trap : MonoBehaviour
    {
        // Guideline:
        // 1. ประกาศฟิลด์แบบ public ได้แก่:
        //    - Name (string) กำหนดค่าเริ่มต้นเป็น "Trap"
        //    - damage (int) กำหนดค่าเริ่มต้นเป็น 5
        public string Name = "Trap";
        public int damage = 5;

        // Guideline:
        // 2. เขียนเมธอด OnTriggerEnter2D(Collider2D other) แบบ private:
        //    - แสดงผลข้อความ "[Trigger] {gameObject.name} collided with {other.gameObject.name}"
        //    - ใช้ other.GetComponent<Player>() เพื่อตรวจหาตัวผู้เล่น
        //    - หากพบผู้เล่น ให้เรียกใช้ player.TakeDamage(damage)
        //    - แสดงผลข้อความ "⚠️ Stepped on {Name}! -{damage} Energy"
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"⚠️ Stepped on {Name}! -{damage} Energy");
                player.TakeDamage(damage);
            }
        }
    }
}
