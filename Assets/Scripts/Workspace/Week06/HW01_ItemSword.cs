using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.HW01
{
    /// <summary>
    /// Homework ข้อ 1: การสร้างคลาส ItemSword สำหรับเพิ่มพลังโจมตีของผู้เล่น
    /// </summary>
    public class ItemSword : MonoBehaviour
    {
        // Guideline:
        // 1. ประกาศฟิลด์แบบ public ได้แก่:
        //    - Name (string) กำหนดค่าเริ่มต้นเป็น "Sword"
        //    - attackBonus (int) กำหนดค่าเริ่มต้นเป็น 10
        public string Name = "Sword";
        public int attackBonus = 10;

        // Guideline:
        // 2. เขียนเมธอด OnTriggerEnter2D(Collider2D other) แบบ private:
        //    - แสดงผลข้อความ "[Trigger] {gameObject.name} collided with {other.gameObject.name}"
        //    - ใช้ other.GetComponent<Player>() เพื่อตรวจหาตัวผู้เล่น
        //    - หากพบผู้เล่น ให้เรียกใช้ player.IncreaseAttack(attackBonus)
        //    - แสดงผลข้อความ "⚔️ Picked up {Name}! +{attackBonus} Attack"
        //    - เรียกใช้ Destroy(gameObject) เพื่อลบไอเทมออกจากฉาก
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"⚔️ Picked up {Name}! +{attackBonus} Attack");
                player.IncreaseAttack(attackBonus);
                Destroy(gameObject);
            }
        }
    }
}
