using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    public class Enemy : MonoBehaviour
    {
        public string Name = "Enemy";
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;

        public int energy = 20;
        public int attackPoint = 5;

        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleCollision(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleCollision(other.gameObject);
        }

        private void HandleCollision(GameObject other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Hit();
            }
        }

        public void Hit()
        {
            if (energy <= 0)
            {
                return;
            }

            if (mapGenerator != null && mapGenerator.player != null)
            {
                Attack(mapGenerator.player, attackPoint);
            }
        }

        public void Attack(Player target, int damage)
        {
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        public void TakeDamage(int damage)
        {
            energy -= damage;
            CheckDead();
        }

        protected void CheckDead()
        {
            if (energy <= 0)
            {
                DestroySafe(gameObject);
            }
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

