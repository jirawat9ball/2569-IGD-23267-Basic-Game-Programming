using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.HW04
{
    /// <summary>
    /// Homework ข้อ 4: การสร้างคลาส Chest (กล่องสมบัติ) สำหรับสร้างวัตถุใหม่ (Instantiate) และทำลายตัวเอง
    /// </summary>
    public class Chest : MonoBehaviour
    {
        // Guideline:
        // 1. ประกาศฟิลด์แบบ public ได้แก่:
        //    - Name (string) กำหนดค่าเริ่มต้นเป็น "Chest"
        //    - spawnPrefab (GameObject) สำหรับเก็บ Prefab วัตถุที่จะสร้างขึ้นมา
        public string Name = "Chest";
        public GameObject spawnPrefab;

        // Guideline:
        // 2. เขียนเมธอด OpenChest() แบบ public:
        //    - แสดงผลข้อความ "📦 Opened {Name}!"
        //    - ตรวจสอบว่า spawnPrefab != null หรือไม่ หากใช่ ให้สร้างวัตถุใหม่ด้วย:
        //      Instantiate(spawnPrefab, transform.position, Quaternion.identity);
        //    - เรียกใช้ Destroy(gameObject) เพื่อลบกล่องสมบัติออกจากฉาก
        public void OpenChest()
        {
            Debug.Log($"📦 Opened {Name}!");

            if (spawnPrefab != null)
            {
                Instantiate(spawnPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        // Guideline:
        // 3. เขียนเมธอด OnTriggerEnter2D(Collider2D other) แบบ private:
        //    - แสดงผลข้อความ "[Trigger] {gameObject.name} collided with {other.gameObject.name}"
        //    - ใช้ other.GetComponent<Player>() เพื่อตรวจหาตัวผู้เล่น
        //    - หากพบผู้เล่น ให้เรียกใช้ OpenChest()
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                OpenChest();
            }
        }
    }
}
