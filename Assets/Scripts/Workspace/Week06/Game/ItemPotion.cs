using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// สคริปต์ไอเทมยาฟื้นฟูพลังงาน (ItemPotion)
    /// ใช้สอนเรื่อง: การตรวจจับการชนด้วย OnTriggerEnter2D และเรียกใช้เมธอด Heal() ของ Player ผ่าน GetComponent
    /// </summary>
    public class ItemPotion : MonoBehaviour
    {
        #region 1. ข้อมูลของไอเทม (Variables / Fields)

        [Header("ข้อมูลทั่วไป")]
        public string Name = "Potion";

        [Header("ตำแหน่งบนแผนที่ (Grid Position)")]
        public int positionX;
        public int positionY;
        [HideInInspector] public MapGenerator mapGenerator;

        [Header("คุณสมบัติของไอเทม")]
        public int healPoint = 10; // ปริมาณพลังงานที่จะฟื้นฟูให้ผู้เล่น

        #endregion

        #region 2. การตรวจจับการชน (Trigger Detection)

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Guideline:
            // 1. แสดง Log แจ้งเตือนเมื่อเกิดการชน: "[Trigger] <gameObject.name> collided with <other.gameObject.name>"
            // 2. ใช้ other.GetComponent<Player>() เพื่อตรวจสอบว่าชนกับผู้เล่นหรือไม่
            // 3. ถ้าเป็นผู้เล่น ให้สั่ง:
            //    - แสดง Log: "✨ Picked up <Name>! +<healPoint> Energy"
            //    - เรียก player.Heal(healPoint)
            //    - เรียก Destroy(gameObject) เพื่อลบไอเทมออกจากฉาก

            // Student code starts HERE ...

            // Student code ends HERE ...
        }

        #endregion
    }
}
