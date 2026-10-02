using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.Teacher.HW03
{
    public class Wall : MonoBehaviour
    {
        public string Name = "Wall";
        public int durability = 2;

        public void Hit()
        {
            durability--;
            Debug.Log($"🧱 {Name} was hit! Remaining durability: {durability}");

            if (durability <= 0)
            {
                Debug.Log($"💥 {Name} destroyed!");
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Hit();
            }
        }
    }
}
