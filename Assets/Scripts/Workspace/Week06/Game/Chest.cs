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
            // Guideline:
            // 1. แสดง Log แจ้งเตือนเมื่อเกิดการชน: "[Trigger] <gameObject.name> collided with <other.gameObject.name>"
            // 2. ใช้ other.GetComponent<Player>() เพื่อตรวจสอบว่าชนกับผู้เล่นหรือไม่
            // 3. ถ้าเป็นผู้เล่น ให้เรียก OpenChest()

            // Student code starts HERE ...

            // Student code ends HERE ...
        }

        #endregion

        #region 3. การเปิดกล่องและสร้างวัตถุ (Open & Instantiate)

        /// <summary>
        /// เปิดกล่อง เสกวัตถุใหม่ และทำลายกล่องทิ้ง
        /// </summary>
        public void OpenChest()
        {
            // Guideline:
            // 1. แสดง Log: "📦 Opened <Name>!"
            // 2. หาก spawnPrefab != null ให้เสกสร้างวัตถุใหม่ลงไปด้านบน 1 ช่อง (y + 1):
            //    Vector3 spawnPosition = transform.position + Vector3.up;
            //    Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
            // 3. เรียก Destroy(gameObject) เพื่อทำลายกล่องสมบัติออกจากฉาก

            // Student code starts HERE ...

            // Student code ends HERE ...
        }

        #endregion
    }
}
