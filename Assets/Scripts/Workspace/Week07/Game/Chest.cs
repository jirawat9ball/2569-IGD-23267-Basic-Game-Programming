using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07.Game
{
    /// <summary>
    /// กล่องสมบัติ (Chest) - ยกมาจาก Week 06
    /// 
    /// 🎯 โจทย์ Week 07 (ข้อ 9): ปรับปรุงเป็น OOP
    /// 1. เปลี่ยนการสืบทอดจาก MonoBehaviour ให้สืบทอดจาก Identity:
    ///    public class Chest : Identity
    /// 2. มีฟิลด์ spawnPrefab และ isOpen = false
    /// 3. เขียน override void Hit() ให้เรียก OpenChest()
    /// 4. เขียนเมธอด OpenChest():
    ///    - ถ้า isOpen ให้ return
    ///    - ตั้ง isOpen = true
    ///    - แสดงข้อความ: $"Opened {Name}!"
    ///    - เสกสร้างวัตถุใหม่ที่ตำแหน่งด้านบน: Instantiate(spawnPrefab, transform.position + Vector3.up, Quaternion.identity);
    ///    - ตั้งค่าช่องในแผนที่เป็น 0: mapGenerator.mapData[positionX, positionY] = 0;
    ///    - ทำลายตัวเอง: DestroySafe(gameObject);
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

        // ===== student code starts HERE =====
        public override void Hit(Player player = null)
        {
            if(player != null){
                OpenChest();
            }
            
        }

        public void OpenChest()
        {
            if (isOpen) return;
            isOpen = true;

            Debug.Log($"Opened {Name}!");

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
        // ===== student code ends HERE =====
    }
}
