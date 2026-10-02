using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    public class ItemSword : MonoBehaviour
    {
        public string Name = "Sword";
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;

        public int attackBonus = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Hit();
            }
        }

        public void Hit()
        {
            Debug.Log($"You got {Name} : {attackBonus}");

            if (mapGenerator != null && mapGenerator.player != null)
            {
                mapGenerator.player.IncreaseAttack(attackBonus);
            }

            if (mapGenerator != null && mapGenerator.mapdata != null)
            {
                mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
            }

            DestroySafe(gameObject);
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

