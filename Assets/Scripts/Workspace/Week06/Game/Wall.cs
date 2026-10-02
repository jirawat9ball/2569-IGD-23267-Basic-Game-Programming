using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// สคริปต์กำแพง / สิ่งกีดขวาง (Wall)
    /// ใช้สอนเรื่อง: การตรวจจับการชนด้วย OnTriggerEnter2D และการลดค่าความทนทาน (Durability)
    /// </summary>
    public class Wall : MonoBehaviour
    {
        #region 1. ข้อมูลของกำแพง (Variables / Fields)

        [Header("ข้อมูลทั่วไป")]
        public string Name = "Wall";

        [Header("ตำแหน่งบนแผนที่ (Grid Position)")]
        public int positionX;
        public int positionY;
        [HideInInspector] public MapGenerator mapGenerator;

        [Header("ค่าสถานะ")]
        public int durability = 2; // ความทนทานของกำแพง (ตี 2 ครั้งพัง)

        #endregion

        #region 2. การตรวจจับการชน (Trigger Detection)

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Guideline:
            // 1. แสดง Log แจ้งเตือนเมื่อเกิดการชน: "[Trigger] <gameObject.name> collided with <other.gameObject.name>"
            // 2. ใช้ other.GetComponent<Player>() เพื่อตรวจสอบว่าชนกับผู้เล่นหรือไม่
            // 3. ถ้าเป็นผู้เล่น ให้สั่ง:
            //    - เรียก Hit()
            //    - เรียก player.RevertPosition() (เพื่อให้ผู้เล่นกลับไปอยู่ที่เดิม เดินผ่านไม่ได้)

            // Student code starts HERE ...

            // Student code ends HERE ...
        }

        #endregion

        #region 3. ความเสียหายและการทำลาย (Durability & Destruction)

        /// <summary>
        /// เมื่อกำแพงถูกโจมตี จะลดค่าความทนทานลง
        /// </summary>
        public void Hit()
        {
            // Guideline:
            // 1. ลดค่า durability ลง 1
            // 2. แสดง Log: "🧱 <Name> was hit! Remaining durability: <durability>"
            // 3. หาก durability <= 0 ให้แสดง Log "💥 <Name> destroyed!" และเรียก Destroy(gameObject)

            // Student code starts HERE ...

            // Student code ends HERE ...
        }

        #endregion
    }
}
