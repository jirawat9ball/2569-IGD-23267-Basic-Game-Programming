using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week04
{
    public class Assignment_Student_Week04 : MonoBehaviour, IAssignment
    {
        #region Lecture Variables

        [Header("As02 Variables")]
        public int rows = 3;
        public int cols = 5;

        [Header("As04 - As06 Variables")]
        public int columns = 5;
        public int mapRows = 5;
        public GameObject[] floorTiles;
        public GameObject wall;

        [Header("As07 Variables")]
        public Transform Item;
        public int ItemPosX = 1;
        public int ItemPosY = 0;

        [Header("As08 Variables")]
        public GameObject[] Items;
        public int foodPosX = 1;
        public int foodPosY = 0;

        #endregion

        #region Level 1 Variables

        [Header("Lv01 & Lv02 Variables")]
        public int row = 0;
        public int col = 0;

        [Header("Lv03 Variables")]
        public int starColumns = 5;
        public int starRows = 3;

        [Header("Lv04 & Lv05 Variables")]
        public int size = 5;
        public int fromTable = 2;
        public int toTable = 4;

        [Header("Lv07 Variables")]
        public int targetValue = 5;

        #endregion

        #region Level 2 Variables

        [Header("Ex01 Variables")]
        public GameObject[] foodTiles;

        [Header("Ex03 Variables")]
        public int targetX = 1;
        public int targetY = 1;

        [Header("Ex04 Variables")]
        public GameObject chestPrefab;

        #endregion

        private const string LineSeparator = "============================";
        private const string BoardSeparator = "-------------";

        void Start()
        {
            As01_Create2DArray();
            As02_ArraySize(rows, cols);
            As03_GetSet2DArray();
            As04_CreateWallRow(columns, wall);
            As05_CreateFloor(columns, mapRows, floorTiles);
            As06_CreateWall(columns, mapRows, wall);
            if (Item != null)
            {
                As07_SetItemPosition(Item, ItemPosX, ItemPosY);
            }
            As08_RandomFoodItem(columns, mapRows, foodTiles);
            As09_CreateItemFromArray(Items, foodPosX, foodPosY);

            int[,] sampleMatrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Lv01_SumRow(sampleMatrix, row);
            Lv02_SumColumn(sampleMatrix, col);
            Lv03_StarPattern(starColumns, starRows);
            Lv04_TrianglePattern(size);
            Lv05_MultiplicationTableNested(fromTable, toTable);
            Lv06_FindMaxInMatrix(sampleMatrix);
            Lv07_CountTargetValue(sampleMatrix, targetValue);
            Lv08_SumAllElements(sampleMatrix);
            Lv09_InvertedTrianglePattern(size);
            Lv10_PrintMainDiagonal(sampleMatrix);

            int[,] moves = new int[,] { { 1, 0, 2 }, { 0, 1, 0 }, { 2, 0, 1 } };
            Ex01_TicTacToe(moves);
            int[,] mapGrid = new int[,] { { 0, 1, 0 }, { 0, 0, 1 }, { 1, 0, 0 } };
            Ex02_CheckWalkableTile(mapGrid, targetX, targetY);
            Ex03_SpawnChestsInCorners(columns, mapRows, chestPrefab);
        }

        #region Lecture

        public void As01_Create2DArray()
        {
            int[,] my2DArray = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            Debug.Log(my2DArray[0, 0] + " " + my2DArray[0, 1] + " " + my2DArray[0, 2]);
            Debug.Log(my2DArray[1, 0] + " " + my2DArray[1, 1] + " " + my2DArray[1, 2]);
            Debug.Log(my2DArray[2, 0] + " " + my2DArray[2, 1] + " " + my2DArray[2, 2]);
        }

        public void As02_ArraySize(int rows, int cols)
        {
            int[,] my2DArray = new int[rows, cols];
            Debug.Log("rows = " + my2DArray.GetLength(0));
            Debug.Log("cols = " + my2DArray.GetLength(1));
        }

        public void As03_GetSet2DArray()
        {
            int[,] my2DArray = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            string[,] my2DStringArray = new string[2, 3] { { "A", "B", "C" }, { "D", "E", "F" } };

            Debug.Log("get : " + my2DArray[1, 2]);
            my2DArray[1, 2] = 70;
            Debug.Log("set : " + my2DArray[1, 2]);
            Debug.Log(LineSeparator);

            for (int r = 0; r < my2DArray.GetLength(0); r++)
            {
                string line = "";
                for (int c = 0; c < my2DArray.GetLength(1); c++)
                {
                    line += my2DArray[r, c];
                    if (c < my2DArray.GetLength(1) - 1)
                    {
                        line += " ";
                    }
                }
                Debug.Log(line);
            }

            Debug.Log(LineSeparator);
            Debug.Log("get : " + my2DStringArray[0, 2]);
            my2DStringArray[0, 2] = "Cat";
            Debug.Log("set : " + my2DStringArray[0, 2]);
            Debug.Log(LineSeparator);

            for (int r = 0; r < my2DStringArray.GetLength(0); r++)
            {
                string line = "";
                for (int c = 0; c < my2DStringArray.GetLength(1); c++)
                {
                    line += my2DStringArray[r, c];
                    if (c < my2DStringArray.GetLength(1) - 1)
                    {
                        line += " ";
                    }
                }
                Debug.Log(line);
            }
        }

        public void As04_CreateWallRow(int columns, GameObject wall)
        {
            string line = "";
            for (int x = 0; x < columns; x++)
            {
                if (wall != null)
                {
                    Instantiate(wall, new Vector2(x, 0), Quaternion.identity);
                }
                line += "*";
            }
            Debug.Log(line);
        }

        public void As05_CreateFloor(int columns, int rows, GameObject[] floorTiles)
        {
            for (int y = 0; y < rows; y++)
            {
                string line = "";
                for (int x = 0; x < columns; x++)
                {
                    GameObject tileChoice = floorTiles[Random.Range(0, floorTiles.Length)];
                    Instantiate(tileChoice, new Vector2(x, y), Quaternion.identity);
                    line += tileChoice.name;
                }
                Debug.Log(line);
            }
        }

        public void As06_CreateWall(int columns, int rows, GameObject wall)
        {
            for (int y = -1; y <= rows; y++)
            {
                string line = "";
                for (int x = -1; x <= columns; x++)
                {
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        Instantiate(wall, new Vector2(x, y), Quaternion.identity);
                        line += "*";
                    }
                    else
                    {
                        line += " ";
                    }
                }
                Debug.Log(line);
            }
        }

        public void As07_SetItemPosition(Transform item, int itemPosX, int itemPosY)
        {
            item.position = new Vector2(itemPosX, itemPosY);
            Debug.Log(item.position);
        }

        public void As08_RandomFoodItem(int columns, int rows, GameObject[] foodTiles)
        {
            int x = Random.Range(0, columns);
            int y = Random.Range(0, rows);
            GameObject tileChoice = foodTiles[Random.Range(0, foodTiles.Length)];
            Instantiate(tileChoice, new Vector2(x, y), Quaternion.identity);
            Debug.Log(tileChoice.name + " at x: " + x + " y: " + y);
        }

        public void As09_CreateItemFromArray(GameObject[] items, int itemPosX, int itemPosY)
        {
            string[,] my2DStringArray = new string[3, 3] {
                { " ", "Soda", " " },
                { " ", " ", " " },
                { " ", " ", "Food" } };

            string itemName = my2DStringArray[itemPosY, itemPosX];

            if (!string.IsNullOrWhiteSpace(itemName))
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null && items[i].name == itemName)
                    {
                        Instantiate(items[i], new Vector2(itemPosX, itemPosY), Quaternion.identity);
                        Debug.Log("Create Item " + itemName + " at x: " + itemPosX + " y: " + itemPosY);
                        return;
                    }
                }
            }

            Debug.Log("No items at x: " + itemPosX + " y: " + itemPosY);
        }

        #endregion

        #region Homework

        #region Level 1: Simple

        public void Lv01_SumRow(int[,] matrix, int row)
        {
            int sum = 0;
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                sum += matrix[row, c];
            }
            Debug.Log(sum);
        }

        public void Lv02_SumColumn(int[,] matrix, int col)
        {
            int sum = 0;
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                sum += matrix[r, col];
            }
            Debug.Log(sum);
        }

        public void Lv03_StarPattern(int columns, int rows)
        {
            for (int y = 0; y < rows; y++)
            {
                string line = "";
                for (int x = 0; x < columns; x++)
                {
                    line += "*";
                }
                Debug.Log(line);
            }
        }

        public void Lv04_TrianglePattern(int size)
        {
            for (int r = 1; r <= size; r++)
            {
                string line = "";
                for (int i = 0; i < r; i++)
                {
                    line += "*";
                }
                Debug.Log(line);
            }
        }

        public void Lv05_MultiplicationTableNested(int fromTable, int toTable)
        {
            for (int i = 1; i <= 12; i++)
            {
                string line = "";
                for (int table = fromTable; table <= toTable; table++)
                {
                    line += table + " x " + i + " = " + (table * i);
                    if (table < toTable)
                    {
                        line += "\t";
                    }
                }
                Debug.Log(line);
            }
        }

        public void Lv06_FindMaxInMatrix(int[,] matrix)
        {
            int max = matrix[0, 0];
            int maxR = 0;
            int maxC = 0;

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] > max)
                    {
                        max = matrix[r, c];
                        maxR = r;
                        maxC = c;
                    }
                }
            }

            Debug.Log("Max value " + max + " at [" + maxR + ", " + maxC + "]");
        }

        public void Lv07_CountTargetValue(int[,] matrix, int target)
        {
            int count = 0;
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] == target)
                    {
                        count++;
                    }
                }
            }

            Debug.Log("Found target " + target + ": " + count + " cells");
        }

        public void Lv08_SumAllElements(int[,] matrix)
        {
            // TODO: หาผลรวมของสมาชิกทุกช่องใน matrix
            // แสดงผล: Debug.Log(sum)
            int sum = 0;
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    sum += matrix[r, c];
                }
            }

            Debug.Log(sum);
        }

        public void Lv09_InvertedTrianglePattern(int size)
        {
            // TODO: วาดสามเหลี่ยมดาวกลับด้าน (size แถว โดยแถวแรกลดจาก size ตัวลงมาถึง 1 ตัว)
            // ตัวอย่าง size = 3:
            // ***
            // **
            // *
            for (int r = size; r >= 1; r--)
            {
                string line = "";
                for (int i = 0; i < r; i++)
                {
                    line += "*";
                }
                Debug.Log(line);
            }
        }

        public void Lv10_PrintMainDiagonal(int[,] matrix)
        {
            // TODO: พิมพ์ค่าแนวทแยงมุมหลัก (matrix[0,0], matrix[1,1], ...) ในบรรทัดเดียวคั่นด้วยช่องว่าง
            // ตัวอย่าง: "1 5 9"
            int minDim = Mathf.Min(matrix.GetLength(0), matrix.GetLength(1));
            string line = "";
            for (int i = 0; i < minDim; i++)
            {
                line += matrix[i, i];
                if (i < minDim - 1)
                {
                    line += " ";
                }
            }

            Debug.Log(line);
        }

        #endregion

        #region Level 2: Moderate

        public void Ex01_TicTacToe(int[,] moves)
        {
            char[,] board = new char[3, 3];
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    board[r, c] = ' ';
                }
            }

            char current = 'X';
            int placed = 0;

            for (int m = 0; m < moves.GetLength(0); m++)
            {
                int moveRow = moves[m, 0];
                int moveCol = moves[m, 1];

                Debug.Log("Player " + current + ":");
                Debug.Log(moveRow + " " + moveCol);

                if (board[moveRow, moveCol] != ' ')
                {
                    Debug.Log("cannot set " + moveRow + " " + moveCol);
                    continue;
                }

                board[moveRow, moveCol] = current;
                placed++;
                PrintBoard(board);

                if (HasWinner(board, current))
                {
                    Debug.Log(current + " wins!");
                    return;
                }

                if (placed == 9)
                {
                    Debug.Log("Draw!");
                    return;
                }

                current = (current == 'X') ? 'O' : 'X';
            }
        }

        private void PrintBoard(char[,] board)
        {
            for (int r = 0; r < 3; r++)
            {
                Debug.Log(BoardSeparator);
                Debug.Log("| " + board[r, 0] + " | " + board[r, 1] + " | " + board[r, 2] + " |");
            }
        }

        private bool HasWinner(char[,] board, char player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                {
                    return true;
                }
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                {
                    return true;
                }
            }

            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
            {
                return true;
            }
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
            {
                return true;
            }

            return false;
        }

        public void Ex02_CheckWalkableTile(int[,] map, int targetX, int targetY)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            if (targetX < 0 || targetX >= cols || targetY < 0 || targetY >= rows)
            {
                Debug.Log("Position (" + targetX + ", " + targetY + ") is Out of Bounds");
                return;
            }

            if (map[targetY, targetX] == 0)
            {
                Debug.Log("Position (" + targetX + ", " + targetY + ") is Walkable");
            }
            else if (map[targetY, targetX] == 1)
            {
                Debug.Log("Position (" + targetX + ", " + targetY + ") is Blocked by Wall");
            }
            else
            {
                Debug.Log("Position (" + targetX + ", " + targetY + ") is Blocked");
            }
        }

        public void Ex03_SpawnChestsInCorners(int columns, int rows, GameObject chestPrefab)
        {
            Vector2[] corners = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(columns - 1, 0),
                new Vector2(0, rows - 1),
                new Vector2(columns - 1, rows - 1)
            };

            for (int i = 0; i < corners.Length; i++)
            {
                if (chestPrefab != null)
                {
                    Instantiate(chestPrefab, corners[i], Quaternion.identity);
                }
            }

            Debug.Log("Spawned 4 chests at corners");
        }

        #endregion

        #endregion
    }
}
