using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Teacher.Ex02
{
    // =========================================================================
    // ระดับ 2: การค้นหาและเรดาร์สำรวจดันเจี้ยนบนตาราง (Dungeon Grid Radar) - Teacher Solution
    // =========================================================================
    public class InventoryAndGridScanner : MapGenerator
    {
        public override Vector2Int FindItemPosition(int[,] grid, int itemCode)
        {
            if (grid == null) return new Vector2Int(-1, -1);

            int rLength = grid.GetLength(0);
            int cLength = grid.GetLength(1);

            for (int r = 0; r < rLength; r++)
            {
                for (int c = 0; c < cLength; c++)
                {
                    if (grid[r, c] == itemCode)
                    {
                        return new Vector2Int(r, c);
                    }
                }
            }

            return new Vector2Int(-1, -1);
        }

        public override int CountItems(int[,] grid, int itemCode)
        {
            if (grid == null) return 0;

            int count = 0;
            int rLength = grid.GetLength(0);
            int cLength = grid.GetLength(1);

            for (int r = 0; r < rLength; r++)
            {
                for (int c = 0; c < cLength; c++)
                {
                    if (grid[r, c] == itemCode)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public override int ScanThreats(int[,] grid, Vector2Int playerPos, int threatCode = 2)
        {
            if (grid == null) return 0;

            int count = 0;
            int rLength = grid.GetLength(0);
            int cLength = grid.GetLength(1);

            for (int r = playerPos.x - 1; r <= playerPos.x + 1; r++)
            {
                for (int c = playerPos.y - 1; c <= playerPos.y + 1; c++)
                {
                    if (r >= 0 && r < rLength && c >= 0 && c < cLength)
                    {
                        if (r == playerPos.x && c == playerPos.y) continue;

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

        public override Vector2Int FindPlayerPosition(MapGenerator mapGen)
        {
            if (mapGen == null) return new Vector2Int(-1, -1);
            return mapGen.playerStartPos;
        }

        public override Vector2Int FindPlayerPosition(int[,] grid, int playerCode)
        {
            if (grid == null) return new Vector2Int(-1, -1);

            int rLength = grid.GetLength(0);
            int cLength = grid.GetLength(1);

            for (int r = 0; r < rLength; r++)
            {
                for (int c = 0; c < cLength; c++)
                {
                    if (grid[r, c] == playerCode)
                    {
                        return new Vector2Int(r, c);
                    }
                }
            }

            return new Vector2Int(-1, -1);
        }
    }
}
