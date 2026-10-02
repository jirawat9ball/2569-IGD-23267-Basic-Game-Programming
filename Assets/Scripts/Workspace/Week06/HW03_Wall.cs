using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.HW03
{
    /// <summary>
    /// Homework ข้อ 3: การสร้างคลาส Wall (กำแพงพังได้) ที่มีความทนทานและถูกทำลายเมื่อค่าความทนทานหมด
    /// </summary>
    public class Wall : MonoBehaviour
    {
        // Guideline:
        // 1. ประกาศฟิลด์แบบ public ได้แก่:
        //    - Name (string) กำหนดค่าเริ่มต้นเป็น "Wall"
        //    - durability (int) กำหนดค่าเริ่มต้นเป็น 2
        public string Name = "Wall";
        public int durability = 2;

        // Guideline:
        // 2. เขียนเมธอด Hit() แบบ public:
        //    - ลดค่าความทนทานลง 1 (durability--)
        //    - แสดงผลข้อความ "🧱 {Name} was hit! Remaining durability: {durability}"
        //    - หาก durability <= 0 ให้แสดงข้อความ "💥 {Name} destroyed!" และเรียกใช้ Destroy(gameObject)
        public void Hit()
        {
            durability--;
            Debug.Log($"🧱 {Name} was hit! Remaining durability: {durability}");

            if (durability <= 0)
            {
                Debug.Log($"💥 {Name} destroyed!");
                Destroy(gameObject);
            }
        }

        // Guideline:
        // 3. เขียนเมธอด OnTriggerEnter2D(Collider2D other) แบบ private:
        //    - แสดงผลข้อความ "[Trigger] {gameObject.name} collided with {other.gameObject.name}"
        //    - ใช้ other.GetComponent<Player>() เพื่อตรวจหาตัวผู้เล่น
        //    - หากพบผู้เล่น ให้เรียกใช้ Hit()
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Hit();
            }
        }
    }
}
