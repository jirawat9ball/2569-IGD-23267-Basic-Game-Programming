using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// สคริปต์กล่องสมบัติ (Chest)
    /// ใช้สอนเรื่อง: การตรวจจับการชน และการเสกสร้างวัตถุใหม่ลงในฉากด้วย Instantiate()
    /// </summary>
    public class Chest : MonoBehaviour
    {
        #region 1. ข้อมูลของกล่องสมบัติ (Variables / Fields)

        [Header("ข้อมูลทั่วไป")]
        public string Name = "Chest";

        [Header("ตำแหน่งบนแผนที่ (Grid Position)")]
        public int positionX;
        public int positionY;
        [HideInInspector] public MapGenerator mapGenerator;

        [Header("Prefab ไอเทมที่จะเสก")]
        public GameObject spawnPrefab; // เช่น Prefab ของ ItemPotion หรือ ItemSword

        #endregion

        #region 2. การตรวจจับการชน (Trigger Detection)

        private void OnTriggerEnter2D(Collider2D other)
        {
            // แสดง Log แจ้งเตือนเมื่อเกิดการชน
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            // ตรวจสอบว่าผู้เล่นเดินมาเปิดกล่องหรือไม่
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                OpenChest();
            }
        }

        #endregion

        #region 3. การเปิดกล่องและสร้างวัตถุ (Open & Instantiate)

        /// <summary>
        /// เปิดกล่อง เสกวัตถุใหม่ และทำลายกล่องทิ้ง
        /// </summary>
        public void OpenChest()
        {
            Debug.Log($"📦 Opened {Name}!");

            // หากมีการระบุ Prefab ให้เสกสร้างวัตถุใหม่ลงไป ณ ตำแหน่งของกล่อง
            if (spawnPrefab != null)
            {
                Instantiate(spawnPrefab, transform.position, Quaternion.identity);
            }

            // ทำลายกล่องสมบัติออกจากฉาก
            Destroy(gameObject);
        }

        #endregion
    }
}
