using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week04
{
    public class Assignment_Teacher_Week04 : MonoBehaviour, IAssignment
    {
        private const string LineSeparator = "============================";
        private const string BoardSeparator = "-------------";

        #region เรื่องที่ 1: รู้จัก 2D Array (ข้อ 1-3)

        public void Ex01_Create2DArray()
        {
            int[,] my2DArray = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

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
        }

        public void Ex02_ArraySize(int rows, int cols)
        {
            int[,] my2DArray = new int[rows, cols];
            Debug.Log("rows = " + my2DArray.GetLength(0));
            Debug.Log("cols = " + my2DArray.GetLength(1));
        }

        public void Ex03_GetSet2DArray()
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

        #endregion

        #region เรื่องที่ 2: 2D Array กับวัตถุในเกม (ข้อ 4-5)

        public void Ex04_SetItemPosition(Transform item, int itemPosX, int itemPosY)
        {
            item.position = new Vector2(itemPosX, itemPosY);
            Debug.Log(item.position);
        }

        public void Ex05_CreateItemFromArray(GameObject[] items, int itemPosX, int itemPosY)
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

        #region เรื่องที่ 3: วนลูปหาผลรวมในตาราง (ข้อ 6-7)

        public void Ex06_SumRow(int[,] matrix, int row)
        {
            int sum = 0;
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                sum += matrix[row, c];
            }
            Debug.Log(sum);
        }

        public void Ex07_SumColumn(int[,] matrix, int col)
        {
            int sum = 0;
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                sum += matrix[r, col];
            }
            Debug.Log(sum);
        }

        #endregion

        #region เรื่องที่ 4: Nested Loop สร้างรูปแบบและแผนที่ (ข้อ 8-13)

        public void Ex08_StarPattern(int columns, int rows)
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

        public void Ex09_RandomFloorMap(int columns, int rows, GameObject[] floorTiles)
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

        public void Ex10_BuildOuterWall(int columns, int rows, GameObject wall)
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

        public void Ex11_RandomFoodItem(int columns, int rows, GameObject[] foodTiles)
        {
            int x = Random.Range(0, columns);
            int y = Random.Range(0, rows);
            GameObject tileChoice = foodTiles[Random.Range(0, foodTiles.Length)];
            Instantiate(tileChoice, new Vector2(x, y), Quaternion.identity);
            Debug.Log(tileChoice.name + " at x: " + x + " y: " + y);
        }

        public void Ex12_TrianglePattern(int size)
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

        public void Ex13_MultiplicationTableNested(int fromTable, int toTable)
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

        #endregion

        #region เรื่องที่ 5: โปรเจกต์รวม (ข้อ 14)

        public void Ex14_TicTacToe(int[,] moves)
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

        #endregion
    }
}
