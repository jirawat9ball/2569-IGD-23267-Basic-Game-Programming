using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    /// <summary>
    /// สคริปต์กับดักหนาม (Trap)
    /// ใช้สอนเรื่อง: การตรวจจับการชนด้วย OnTriggerEnter2D และการลดพลังชีวิตของผู้เล่นผ่าน player.TakeDamage()
    /// </summary>
    public class Trap : MonoBehaviour
    {
        #region 1. ข้อมูลของกับดัก (Variables / Fields)

        [Header("ข้อมูลทั่วไป")]
        public string Name = "Trap";

        [Header("ตำแหน่งบนแผนที่ (Grid Position)")]
        public int positionX;
        public int positionY;
        [HideInInspector] public MapGenerator mapGenerator;

        [Header("คุณสมบัติของกับดัก")]
        public int damage = 5; // ดาเมจที่จะสร้างแก่ผู้เล่นเมื่อเดินเหยียบ

        #endregion

        #region 2. การตรวจจับการชน (Trigger Detection)

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Guideline:
            // 1. แสดง Log แจ้งเตือนเมื่อเกิดการชน: "[Trigger] <gameObject.name> collided with <other.gameObject.name>"
            // 2. ใช้ other.GetComponent<Player>() เพื่อตรวจสอบว่าชนกับผู้เล่นหรือไม่
            // 3. ถ้าเป็นผู้เล่น ให้สั่ง:
            //    - แสดง Log: "⚠️ Stepped on <Name>! Trapped for 1 turn (-<damage> Energy)"
            //    - กำหนด player.isTrapped = true (ทำให้เดินไม่ได้ 1 ครั้ง)
            //    - เรียก player.TakeDamage(damage)

            // Student code starts HERE ...

            // Student code ends HERE ...
        }

        #endregion
    }
}
