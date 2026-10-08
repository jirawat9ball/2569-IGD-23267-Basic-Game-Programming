using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// กล่องสมบัติในแผนที่ สืบทอดจาก Identity
    /// เมื่อถูกเปิด (Hit หรือ OpenChest) จะเสกไอเทมจาก spawnPrefab ออกมาและทำลายกล่อง
    /// </summary>
    public class Chest : Identity
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
                if (mapGenerator != null)
                {
                    mapGenerator.TrackSpawned(spawned);
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
