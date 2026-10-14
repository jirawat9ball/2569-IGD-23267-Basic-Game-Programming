using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Teacher.Ex03
{
    public class Weapon : MonoBehaviour
    {
        public string weaponName = "Wooden Stick";
        public int attackPower = 10;
        public int durability = 20;

        public void Use()
        {
            if (durability > 0)
            {
                durability--;
            }
            Debug.Log($"{weaponName} used. Durability: {durability}");
        }
    }

    public class WeaponSpawner : MonoBehaviour
    {
        public GameObject SpawnWeapon(GameObject prefab, Vector3 spawnPosition)
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
    }
}
