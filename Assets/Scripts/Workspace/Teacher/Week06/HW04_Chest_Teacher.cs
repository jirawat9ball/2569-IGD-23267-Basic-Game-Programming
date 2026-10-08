using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.Teacher.HW04
{
    public class Chest : MonoBehaviour
    {
        public string Name = "Chest";
        public GameObject spawnPrefab;

        public void OpenChest()
        {
            Debug.Log($"Opened {Name}!");

            if (spawnPrefab != null)
            {
                Vector3 spawnPosition = transform.position + Vector3.up;
                Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                OpenChest();
            }
        }
    }
}
