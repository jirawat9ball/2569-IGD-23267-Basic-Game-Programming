using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.Ex05
{
    /// <summary>
    /// Assignment ข้อ 5: การสร้างคลาส ItemPotion สำหรับฟื้นฟูพลังงานของผู้เล่น
    /// </summary>
    public class ItemPotion : MonoBehaviour
    {
        // Guideline:
        // 1. ประกาศฟิลด์แบบ public ได้แก่:
        //    - name (string) กำหนดค่าเริ่มต้นเป็น "Potion"
        //    - healPoint (int) กำหนดค่าเริ่มต้นเป็น 10
        public string name = "Potion";
        public int healPoint = 10;

        // Guideline:
        // 2. เขียนเมธอด OnTriggerEnter2D(Collider2D other) แบบ private:
        //    - แสดงผลข้อความ "[Trigger] {gameObject.name} collided with {other.gameObject.name}"
        //    - ใช้ other.GetComponent<Player>() เพื่อตรวจหาตัวผู้เล่น
        //    - หากพบผู้เล่น ให้เรียกใช้ player.Heal(healPoint)
        //    - แสดงผลข้อความ "✨ Picked up {name}! +{healPoint} Energy"
        //    - เรียกใช้ Destroy(gameObject) เพื่อลบไอเทมออกจากฉาก
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"✨ Picked up {name}! +{healPoint} Energy");
                player.Heal(healPoint);
                Destroy(gameObject);
            }
        }
    }
}
