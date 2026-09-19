using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week04
{
    public class Assignment_Teacher_Week04 : MonoBehaviour, IAssignment
    {
        #region Lecture Variables

        [Header("As02 & Map Size Variables")]
        public int rows = 5;
        public int cols = 5;

        [Header("As05 - As07 Variables")]
        public GameObject[] floorTiles;
        public GameObject[] wall;

        [Header("As08 Variables")]
        public GameObject Item;
        public int ItemPosX = 1;
        public int ItemPosY = 0;

        [Header("As09 & As10 Variables")]
        public GameObject[] foodTiles;

        [Header("As11 & As12 Variables")]
        public GameObject player;
        public GameObject exitTile;

        #endregion

        #region Level 1 Variables

        [Header("Lv02 & Lv03 Variables")]
        public int row = 0;
        public int col = 0;

        [Header("Lv04 Variables")]
        public int starColumns = 5;
        public int starRows = 3;
        public GameObject villageTile;

        [Header("Lv05 & Lv10 Variables")]
        public int size = 5;
        public GameObject riverTile;

        [Header("Lv06 Variables")]
        public int fromTable = 2;
        public int toTable = 4;

        [Header("Lv08 Variables")]
        public int targetValue = 5;

        #endregion

        #region Level 2 Variables

        [Header("Ex02 Variables")]
        public int targetX = 1;
        public int targetY = 1;

        [Header("Ex03 Variables")]
        public GameObject chestPrefab;

        #endregion

        private const string LineSeparator = "============================";
        private const string BoardSeparator = "-------------";

        void Start()
        {
            As01_Create2DArray();
            As02_ArraySize(rows, cols);
            As03_GetSet2DArray();

            // =========================================================================
            // สร้างแผนที่และวางวัตถุ
            // =========================================================================

            // As05: สร้างแถวกำแพง
            for (int x = 0; x < cols; x++)
            {
                GameObject tileChoice = wall[Random.Range(0, wall.Length)];
                Instantiate(tileChoice, new Vector2(x, 0), Quaternion.identity);
            }

            // As06: สร้างพื้นแผนที่
            for (int y = 0; y < rows; y++)
            {
                string line = "";
                for (int x = 0; x < cols; x++)
                {
                    GameObject tileChoice = floorTiles[Random.Range(0, floorTiles.Length)];
                    GameObject instance = Instantiate(tileChoice, new Vector2(x, y), Quaternion.identity);
                    instance.name = $"Floor_{x}_{y}";
                    line += tileChoice.name;
                }
                Debug.Log(line);
            }

            // As07: สร้างกำแพงล้อมรอบแผนที่
            for (int y = -1; y <= rows; y++)
            {
                string line = "";
                for (int x = -1; x <= cols; x++)
                {
                    if (x == -1 || x == cols || y == -1 || y == rows)
                    {
                        GameObject tileChoice = wall[Random.Range(0, wall.Length)];
                        Instantiate(tileChoice, new Vector2(x, y), Quaternion.identity);
                        line += "*";
                    }
                    else
                    {
                        line += " ";
                    }
                }
                Debug.Log(line);
            }

            // As08: วางไอเทมเดี่ยวตามพิกัด
            Instantiate(Item, new Vector2(ItemPosX, ItemPosY), Quaternion.identity);
            Debug.Log(new Vector3(ItemPosX, ItemPosY, 0f));

            // As09: สุ่มวางอาหาร
            int foodX = Random.Range(0, cols);
            int foodY = Random.Range(0, rows);
            GameObject foodChoice = foodTiles[Random.Range(0, foodTiles.Length)];
            Instantiate(foodChoice, new Vector2(foodX, foodY), Quaternion.identity);
            Debug.Log(foodChoice.name + " at x: " + foodX + " y: " + foodY);

            // As10: สร้างไอเทมจาก 2D Array
            string[,] my2DStringArray = new string[3, 3] {
                { " ", "Soda", " " },
                { " ", " ", " " },
                { " ", " ", "Food" } };

            for (int y = 0; y < my2DStringArray.GetLength(0); y++)
            {
                for (int x = 0; x < my2DStringArray.GetLength(1); x++)
                {
                    string itemName = my2DStringArray[y, x];
                    if (!string.IsNullOrWhiteSpace(itemName))
                    {
                        for (int i = 0; i < foodTiles.Length; i++)
                        {
                            if (foodTiles[i].name == itemName)
                            {
                                Instantiate(foodTiles[i], new Vector2(x, y), Quaternion.identity);
                                Debug.Log("Create Item " + itemName + " at x: " + x + " y: " + y);
                                break;
                            }
                        }
                    }
                }
            }

            // As11 PlacePlayer: วางผู้เล่นที่มุมซ้ายล่าง (0, 0)
            Instantiate(player, new Vector2(0, 0), Quaternion.identity);

            // As12 PlaceExit: วางทางออกที่มุมขวาบน (cols - 1, rows - 1)
            Instantiate(exitTile, new Vector2(cols - 1, rows - 1), Quaternion.identity);

            Lv01_GetSet2DStringArray();
            int[,] sampleMatrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Lv02_SumRow(sampleMatrix, row);
            Lv03_SumColumn(sampleMatrix, col);
            Lv04_BuildVillage(starColumns, starRows, villageTile);
            Lv05_BuildRiver(size, riverTile);
            Lv06_MultiplicationTableNested(fromTable, toTable);
            Lv07_FindMaxInMatrix(sampleMatrix);
            Lv08_CountTargetValue(sampleMatrix, targetValue);
            Lv09_SumAllElements(sampleMatrix);
            Lv10_BuildInvertedRiver(size, riverTile);
            Lv11_PrintMainDiagonal(sampleMatrix);

            int[,] moves = new int[,] { { 1, 0, 2 }, { 0, 1, 0 }, { 2, 0, 1 } };
            Ex01_TicTacToe(moves);
            int[,] mapGrid = new int[,] { { 0, 1, 0 }, { 0, 0, 1 }, { 1, 0, 0 } };
            Ex02_CheckWalkableTile(mapGrid, targetX, targetY);
            Ex03_SpawnChestsInCorners(cols, rows, chestPrefab);
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
            Debug.Log("length = " + my2DArray.Length);
        }

        public void As03_GetSet2DArray()
        {
            int[,] my2DArray = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            Debug.Log("get : " + my2DArray[1, 2]);
            my2DArray[1, 2] = 70;
            Debug.Log("set : " + my2DArray[1, 2]);
            Debug.Log(LineSeparator);

            As04_Print2DArray(my2DArray);
        }

        public void As04_Print2DArray(int[,] array)
        {
            for (int r = 0; r < array.GetLength(0); r++)
            {
                string line = "";
                for (int c = 0; c < array.GetLength(1); c++)
                {
                    line += array[r, c];
                    if (c < array.GetLength(1) - 1)
                    {
                        line += " ";
                    }
                }
                Debug.Log(line);
            }
        }

        public void As04_Print2DArray(string[,] array)
        {
            for (int r = 0; r < array.GetLength(0); r++)
            {
                string line = "";
                for (int c = 0; c < array.GetLength(1); c++)
                {
                    line += array[r, c];
                    if (c < array.GetLength(1) - 1)
                    {
                        line += " ";
                    }
                }
                Debug.Log(line);
            }
        }
        #endregion

        #region Homework

        #region Level 1: Simple

        public void Lv01_GetSet2DStringArray()
        {
            string[,] my2DStringArray = new string[2, 3] { { "A", "B", "C" }, { "D", "E", "F" } };

            Debug.Log("get : " + my2DStringArray[0, 2]);
            my2DStringArray[0, 2] = "Cat";
            Debug.Log("set : " + my2DStringArray[0, 2]);
            Debug.Log(LineSeparator);

            As04_Print2DArray(my2DStringArray);
        }

        public void Lv02_SumRow(int[,] matrix, int row)
        {
            int sum = 0;
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                sum += matrix[row, c];
            }
            Debug.Log(sum);
        }

        public void Lv03_SumColumn(int[,] matrix, int col)
        {
            int sum = 0;
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                sum += matrix[r, col];
            }
            Debug.Log(sum);
        }

        public void Lv04_BuildVillage(int columns, int rows, GameObject villageTile)
        {
            for (int y = 0; y < rows; y++)
            {
                string line = "";
                for (int x = 0; x < columns; x++)
                {
                    if (villageTile != null)
                    {
                        Instantiate(villageTile, new Vector2(x, y), Quaternion.identity);
                    }
                    line += "*";
                }
                Debug.Log(line);
            }
        }

        public void Lv05_BuildRiver(int size, GameObject riverTile)
        {
            for (int r = 1; r <= size; r++)
            {
                string line = "";
                for (int i = 0; i < r; i++)
                {
                    if (riverTile != null)
                    {
                        Instantiate(riverTile, new Vector2(i, r - 1), Quaternion.identity);
                    }
                    line += "*";
                }
                Debug.Log(line);
            }
        }

        public void Lv06_MultiplicationTableNested(int fromTable, int toTable)
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

        public void Lv07_FindMaxInMatrix(int[,] matrix)
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

        public void Lv08_CountTargetValue(int[,] matrix, int target)
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

        public void Lv09_SumAllElements(int[,] matrix)
        {
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

        public void Lv10_BuildInvertedRiver(int size, GameObject riverTile)
        {
            int y = 0;
            for (int r = size; r >= 1; r--)
            {
                string line = "";
                for (int i = 0; i < r; i++)
                {
                    if (riverTile != null)
                    {
                        Instantiate(riverTile, new Vector2(i, y), Quaternion.identity);
                    }
                    line += "*";
                }
                Debug.Log(line);
                y++;
            }
        }

        public void Lv11_PrintMainDiagonal(int[,] matrix)
        {
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
                PrintBoard(board);

                char result = Ex01_CheckWinner(board);
                if (result == 'X' || result == 'O')
                {
                    Debug.Log(result + " wins!");
                    return;
                }
                if (result == 'D')
                {
                    Debug.Log("Draw!");
                    return;
                }

                current = (current == 'X') ? 'O' : 'X';
            }
        }

        public char Ex01_CheckWinner(char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != ' ' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return board[i, 0];
                }
                if (board[0, i] != ' ' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    return board[0, i];
                }
            }

            if (board[0, 0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return board[0, 0];
            }
            if (board[0, 2] != ' ' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return board[0, 2];
            }

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (board[r, c] == ' ')
                    {
                        return ' ';
                    }
                }
            }

            return 'D';
        }

        private void PrintBoard(char[,] board)
        {
            for (int r = 0; r < 3; r++)
            {
                Debug.Log(BoardSeparator);
                Debug.Log("| " + board[r, 0] + " | " + board[r, 1] + " | " + board[r, 2] + " |");
            }
        }
        public void Ex02_CheckWalkableTile(int[,] map, int targetX, int targetY)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            string[] directionNames = { "Up", "Down", "Left", "Right" };
            int[] offsetX = { 0, 0, -1, 1 };
            int[] offsetY = { 1, -1, 0, 0 };

            Debug.Log("Check around (" + targetX + ", " + targetY + ")");

            for (int i = 0; i < directionNames.Length; i++)
            {
                int nextX = targetX + offsetX[i];
                int nextY = targetY + offsetY[i];

                string result;
                if (nextX < 0 || nextX >= cols || nextY < 0 || nextY >= rows)
                {
                    result = "Out of Bounds";
                }
                else if (map[nextY, nextX] == 0)
                {
                    result = "Walkable";
                }
                else
                {
                    result = "Blocked";
                }

                Debug.Log(directionNames[i] + " (" + nextX + ", " + nextY + ") : " + result);
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

            for (int y = rows - 1; y >= 0; y--)
            {
                string line = "";
                for (int x = 0; x < columns; x++)
                {
                    bool isCorner = (x == 0 || x == columns - 1) && (y == 0 || y == rows - 1);
                    line += isCorner ? "C" : ".";
                }
                Debug.Log(line);
            }

            Debug.Log("Spawned 4 chests at corners");
        }

        #endregion

        #endregion
    }
}
