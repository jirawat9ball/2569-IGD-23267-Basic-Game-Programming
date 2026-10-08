using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Teacher.Game
{
    public class Chest : Week07.Game.Identity
    {
        public GameObject spawnPrefab;
        public bool isOpen = false;

        private void Awake()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "Chest";
            }
        }

        public override void Hit()
        {
            OpenChest();
        }

        public void OpenChest()
        {
            if (isOpen) return;
            isOpen = true;

            Debug.Log($"📦 Opened {Name}!");

            if (spawnPrefab != null)
            {
                Vector3 spawnPosition = transform.position + Vector3.up;
                var spawned = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
                var map = mapGenerator as MapGenerator;
                if (map != null)
                {
                    map.TrackSpawned(spawned);
                }
            }

            if (mapGenerator != null && mapGenerator.mapData != null)
            {
                mapGenerator.mapData[positionX, positionY] = 0;
            }

            DestroySafe(gameObject);
        }
    }
}

namespace Week07.Teacher.Ex09
{
    public class Chest : Week07.Teacher.Game.Chest { }
}
