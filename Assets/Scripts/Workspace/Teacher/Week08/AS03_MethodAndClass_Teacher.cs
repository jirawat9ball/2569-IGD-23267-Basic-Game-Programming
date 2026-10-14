using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Teacher.Ex03
{
    public class Armor : MonoBehaviour
    {
        public string Name = "Iron Armor";
        public int defenseBonus = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"Picked up {Name}! +{defenseBonus} Defense");
                player.IncreaseDefense(defenseBonus);
                Destroy(gameObject);
            }
        }
    }

    public class ArmorSpawner : MonoBehaviour
    {
        public GameObject SpawnArmor(GameObject prefab, Vector3 spawnPosition)
        {
            if (prefab == null)
            {
                Debug.Log("Prefab is null");
                return null;
            }

            GameObject spawned = Instantiate(prefab, spawnPosition, Quaternion.identity);
            Debug.Log($"Spawned {prefab.name} at {spawnPosition}");
            return spawned;
        }

        public GameObject SpawnWeapon(GameObject prefab, Vector3 spawnPosition) => SpawnArmor(prefab, spawnPosition);
    }
}
