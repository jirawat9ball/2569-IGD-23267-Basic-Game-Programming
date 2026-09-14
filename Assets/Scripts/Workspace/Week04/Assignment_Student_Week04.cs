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
        public GameObject[] foodTiles;

        [Header("As09 Variables")]
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
        public GameObject villageTile;

        [Header("Lv04 & Lv09 Variables")]
        public int size = 5;
        public GameObject riverTile;

        [Header("Lv05 Variables")]
        public int fromTable = 2;
        public int toTable = 4;

        [Header("Lv07 Variables")]
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

        /// <summary>
        /// เช็คว่าช่อง Prefab ใน Inspector ใส่มาครบหรือยัง ถ้ายังไม่ครบจะบอกเหตุผลใน Console
        /// (ไม่ใช่ส่วนของโจทย์ แค่กันไม่ให้ Play แล้ว error ตอนยังตั้งค่าไม่เสร็จ)
        /// </summary>
        private static bool HasPrefabs(GameObject[] prefabs, string fieldName, string methodName)
        {
            if (prefabs == null || prefabs.Length == 0)
            {
                Debug.Log("ข้าม " + methodName + " เพราะช่อง '" + fieldName + "' ใน Inspector ยังว่างอยู่");
                return false;
            }

            for (int i = 0; i < prefabs.Length; i++)
            {
                if (prefabs[i] == null)
                {
                    Debug.Log("ข้าม " + methodName + " เพราะช่อง '" + fieldName + "' Element " + i +
                              " ยังว่างอยู่ — ลด Size เหลือ " + i + " หรือใส่ Prefab ให้ครบ");
                    return false;
                }
            }
            return true;
        }

        void Start()
        {
            As01_Create2DArray();
            As02_ArraySize(rows, cols);
            As03_GetSet2DArray();
            As04_CreateWallRow(columns, wall);

            if (HasPrefabs(floorTiles, "Floor Tiles", "As05_CreateFloor"))
            {
                As05_CreateFloor(columns, mapRows, floorTiles);
            }

            if (wall != null)
            {
                As06_CreateWall(columns, mapRows, wall);
            }
            else
            {
                Debug.Log("ข้าม As06_CreateWall เพราะช่อง 'Wall' ใน Inspector ยังว่างอยู่");
            }

            if (Item != null)
            {
                As07_SetItemPosition(Item, ItemPosX, ItemPosY);
            }
            else
            {
                Debug.Log("ข้าม As07_SetItemPosition เพราะช่อง 'Item' ยังว่างอยู่ " +
                          "— ช่องนี้ต้องลาก GameObject ที่อยู่ในซีนมาใส่ ไม่ใช่ Prefab");
            }

            if (HasPrefabs(foodTiles, "Food Tiles", "As08_RandomFoodItem"))
            {
                As08_RandomFoodItem(columns, mapRows, foodTiles);
            }

            if (HasPrefabs(Items, "Items", "As09_CreateItemFromArray"))
            {
                As09_CreateItemFromArray(Items, foodPosX, foodPosY);
            }

            int[,] sampleMatrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Lv01_SumRow(sampleMatrix, row);
            Lv02_SumColumn(sampleMatrix, col);
            Lv03_BuildVillage(starColumns, starRows, villageTile);
            Lv04_BuildRiver(size, riverTile);
            Lv05_MultiplicationTableNested(fromTable, toTable);
            Lv06_FindMaxInMatrix(sampleMatrix);
            Lv07_CountTargetValue(sampleMatrix, targetValue);
            Lv08_SumAllElements(sampleMatrix);
            Lv09_BuildInvertedRiver(size, riverTile);
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

        public void Lv03_BuildVillage(int columns, int rows, GameObject villageTile)
        {
            // Guideline:
            // 1. ใช้ Nested Loop วนทุกช่องของพื้นที่หมู่บ้าน (กว้าง columns สูง rows)
            // 2. แต่ละช่องให้ Instantiate villageTile ลงไปที่ตำแหน่ง (x, y)
            // 3. เก็บสัญลักษณ์ * ของแถวนั้นไว้ แล้วพิมพ์ผังออกมาบรรทัดละแถว
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

        public void Lv04_BuildRiver(int size, GameObject riverTile)
        {
            // Guideline:
            // 1. แม่น้ำเป็นรูปสามเหลี่ยม แถวล่างสุดมี 1 ช่อง แถวถัดขึ้นไปเพิ่มทีละ 1 จนถึง size
            // 2. แถวที่ r จะมี r ช่อง ให้ Instantiate riverTile ที่ตำแหน่ง (i, r - 1)
            // 3. พิมพ์ผังออกมาบรรทัดละแถว
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

        public void Lv09_BuildInvertedRiver(int size, GameObject riverTile)
        {
            // Guideline:
            // 1. เหมือนข้อ Lv04 แต่กลับหัว แถวแรกกว้าง size แล้วลดลงทีละ 1 จนเหลือ 1
            // 2. ใช้ตัวแปร y นับแถวที่วางไปแล้ว เพื่อใช้เป็นตำแหน่งแกน Y
            // 3. พิมพ์ผังออกมาบรรทัดละแถว
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
            // Guideline:
            // 1. สร้างกระดาน char[3,3] เติมช่องว่าง ' ' ให้ครบทุกช่อง
            // 2. ไล่การเดินใน moves ทีละตา (แต่ละแถวคือ { แถว, คอลัมน์ }) เริ่มที่ผู้เล่น 'X'
            // 3. แต่ละตา: พิมพ์ "Player <X/O>:" แล้วพิมพ์พิกัดที่เดิน
            //    - ถ้าช่องนั้นมีคนลงแล้ว พิมพ์ "cannot set <แถว> <คอลัมน์>" แล้วข้ามไปตาถัดไป
            //    - ถ้าลงได้ ให้ใส่สัญลักษณ์ลงกระดาน แล้วพิมพ์กระดานออกมา
            // 4. หลังลงทุกครั้ง ให้เรียก Ex01_CheckWinner(board) เพื่อดูผล
            //    'X'/'O' -> พิมพ์ "<ผู้ชนะ> wins!" แล้วจบเกม · 'D' -> พิมพ์ "Draw!" แล้วจบเกม
            // 5. ถ้ายังไม่จบ ให้สลับตาเป็นอีกฝ่าย
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
            // Guideline:
            // 1. เช็ค 8 แนวที่ชนะได้ — แนวนอน 3 แนว, แนวตั้ง 3 แนว, แนวทแยง 2 แนว
            //    ช่องว่าง ' ' ไม่นับว่าชนะ ต้องเช็คก่อนว่าช่องแรกไม่ใช่ช่องว่าง
            // 2. ถ้าเจอผู้ชนะ ให้ return สัญลักษณ์ของคนนั้น ('X' หรือ 'O')
            // 3. ถ้ายังไม่มีใครชนะ แต่กระดานเต็มหมดแล้ว ให้ return 'D' (Draw = เสมอ)
            // 4. ถ้ายังมีช่องว่างเหลือ แปลว่าเกมยังไม่จบ ให้ return ' '
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
            // Guideline:
            // 1. พิมพ์หัวข้อว่ากำลังตรวจรอบตำแหน่งไหน: "Check around (x, y)"
            // 2. ตรวจ 4 ทิศรอบตัว ตามลำดับ Up, Down, Left, Right
            //    Up = y+1, Down = y-1, Left = x-1, Right = x+1
            // 3. แต่ละทิศพิมพ์ "<ทิศ> (x, y) : <ผล>" โดยผลมี 3 แบบ
            //    - ออกนอกแผนที่        -> Out of Bounds
            //    - ค่าในช่องเป็น 0     -> Walkable
            //    - ค่าในช่องไม่ใช่ 0   -> Blocked
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
            // Guideline:
            // 1. หีบสมบัติวางที่ 4 มุมของแผนที่ คือ (0,0), (columns-1,0), (0,rows-1), (columns-1,rows-1)
            // 2. Instantiate chestPrefab ที่ทุกมุม
            // 3. พิมพ์ผังแผนที่ออกมา มุมที่มีหีบใช้ C ช่องอื่นใช้ . (พิมพ์จากแถวบนลงล่าง)
            // 4. ปิดท้ายด้วยข้อความ "Spawned 4 chests at corners"
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
