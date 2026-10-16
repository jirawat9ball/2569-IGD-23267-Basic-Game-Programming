using UnityEngine;

namespace Week07.Teacher.Game
{
    /// <summary>
    /// คลาสแม่ของทุกอย่างที่วางอยู่บนแผนที่ (ตัวละคร, ยา, ดาบ) - ฉบับเฉลยอาจารย์
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class Identity : MonoBehaviour
    {
        public string Name;
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;

        public virtual void Hit(Week07.Game.Player player = null)
        {
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
        }

        public static void DestroySafe(GameObject target)
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
