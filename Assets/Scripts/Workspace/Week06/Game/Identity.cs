using UnityEngine;

namespace Week06.Game
{
    /// <summary>
    /// คลาสแม่ของทุกอย่างที่วางอยู่บนแผนที่ (ตัวละคร, ยา, ดาบ)
    /// ไฟล์นี้ไม่ต้องแก้ — นักศึกษาแก้เฉพาะ Enemy.cs / ItemPotion.cs / ItemSword.cs / Character.cs
    /// </summary>
    public class Identity : MonoBehaviour
    {
        public string Name;
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;

        public virtual void Hit()
        {
        }

        /// <summary>
        /// เอาวัตถุออกจากเกม
        /// ตอนเล่นจริงใช้ Destroy ตามปกติ ส่วนตอนรันชุดทดสอบ (EditMode) ใช้ Destroy ไม่ได้
        /// จึงปิดการทำงานแทน เพื่อให้โค้ดที่อ่านค่าต่อจากวัตถุนั้นยังทำงานได้เหมือนตอนเล่นจริง
        /// </summary>
        protected static void DestroySafe(GameObject target)
        {
            if (target == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                Destroy(target);
            }
            else
            {
                target.SetActive(false);
            }
        }
    }
}
