using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Teacher.Ex02
{
    public class InventoryAndGridScanner : MonoBehaviour
    {
        public Vector2Int FindItemPosition(int[,] mapGrid, int itemCode)
        {
            if (mapGrid == null) return new Vector2Int(-1, -1);

            int rows = mapGrid.GetLength(0);
            int cols = mapGrid.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (mapGrid[r, c] == itemCode)
                    {
                        return new Vector2Int(r, c);
                    }
                }
            }

            return new Vector2Int(-1, -1);
        }

        public int CountItems(int[,] mapGrid, int itemCode)
        {
            if (mapGrid == null) return 0;

            int count = 0;
            int rows = mapGrid.GetLength(0);
            int cols = mapGrid.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (mapGrid[r, c] == itemCode)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public int ScanThreats(int[,] mapGrid, int threatCode)
        {
            if (mapGrid == null) return 0;

            int threatCount = 0;
            int rows = mapGrid.GetLength(0);
            int cols = mapGrid.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (mapGrid[r, c] == threatCode)
                    {
                        threatCount++;
                    }
                }
            }

            Debug.Log($"Found {threatCount} threats of code {threatCode}");
            return threatCount;
        }

        public Vector2Int FindExitPosition(int[,] mapGrid, int exitCode)
        {
            if (mapGrid == null) return new Vector2Int(-1, -1);

            int rows = mapGrid.GetLength(0);
            int cols = mapGrid.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (mapGrid[r, c] == exitCode)
                    {
                        return new Vector2Int(r, c);
                    }
                }
            }

            return new Vector2Int(-1, -1);
        }
    }
}
