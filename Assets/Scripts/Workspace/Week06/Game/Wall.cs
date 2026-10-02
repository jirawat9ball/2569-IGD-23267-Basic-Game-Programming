using UnityEngine;

namespace Week06.Game
{
    public class Wall : MonoBehaviour
    {
        public string Name = "Wall";
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;

        public void Hit()
        {
            // สามารถเพิ่มตรรกะเมื่อกำแพงโดนตีได้ เช่น ลดความทนทาน หรือทำลายกำแพง
        }

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

