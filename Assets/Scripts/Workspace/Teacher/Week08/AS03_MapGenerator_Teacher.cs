using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Teacher.Ex03
{
    // =========================================================================
    // ระดับ 3: การสร้างฉากและจัดวางตำแหน่งออบเจกต์ (MapGenerator Object Placement) - Teacher Solution
    // =========================================================================
    public class MapGenerator_Teacher : MapGenerator
    {
        // 1. กำหนดตำแหน่งทางออก (Exit) และเสกสร้าง Exit Prefab
        public override void PlaceExit()
        {
            if (exitPrefab != null)
            {
                var exitObj = Instantiate(exitPrefab, new Vector3(exitPos.y, exitPos.x, 0), Quaternion.identity);
                exitObj.name = "Exit";
            }
        }

        // 2. วางไอเทมเกราะลงในฉาก
        public override void PlaceArmor()
        {
            if (armorPrefab == null) return;

            for (int i = 0; i < armorCount; i++)
            {
                int X = Random.Range(0, rows);
                int Y = Random.Range(0, columns);
                var armorObj = Instantiate(armorPrefab, new Vector3(Y, X, 0), Quaternion.identity, itemParent);
                if (armorObj != null)
                {
                    armorObj.name = $"Armor_{X}_{Y}";
                }
            }
        }

        // 3. สร้างกำแพงรูปตัว L (L-shaped Wall) สูงครึ่งหนึ่งของแถว (rows) และกว้างครึ่งหนึ่งของคอลัมน์ (columns)
        public override void GenerateLWall()
        {
            int wallHeight = rows / 2;
            int wallWidth = columns / 2;
            var wallPrefab = (wallPrefabs != null && wallPrefabs.Length > 1) ? wallPrefabs[1] : (wallPrefabs != null && wallPrefabs.Length > 0 ? wallPrefabs[0] : null);

            for (int r = 0; r <= wallHeight; r++)
            {
                if (wallPrefab != null)
                {
                    Instantiate(wallPrefab, new Vector3(0, r, 0), Quaternion.identity, wallParent);
                }
            }

            for (int c = 0; c <= wallWidth; c++)
            {
                if (wallPrefab != null)
                {
                    Instantiate(wallPrefab, new Vector3(c, 0, 0), Quaternion.identity, wallParent);
                }
            }
        }

        // 4. สแกนหาภัยคุกคามรอบตัวผู้เล่นในรัศมี 1 ช่อง (8 ทิศทาง)
        public override int ScanThreats(int[,] grid, Vector2Int playerPos, int threatCode = 2)
        {
            if (grid == null) return 0;

            int count = 0;
            for (int r = playerPos.x - 1; r <= playerPos.x + 1; r++)
            {
                for (int c = playerPos.y - 1; c <= playerPos.y + 1; c++)
                {
                    if (r == playerPos.x && c == playerPos.y) continue;

                    if (IsInBounds(grid, r, c))
                    {
                        if (grid[r, c] == threatCode)
                        {
                            count++;
                        }
                    }
                }
            }

            Debug.Log($"Found {count} threats around player at {playerPos}");
            return count;
        }
    }

    // Alias for backward compatibility
    public class InventoryAndGridScanner : MapGenerator_Teacher { }
}

namespace Week08.Teacher.Ex02
{
    // Alias for backward compatibility if resolved via Ex02
    public class InventoryAndGridScanner : Ex03.MapGenerator_Teacher { }
}
