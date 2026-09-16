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
        public GameObject[] wall;

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
            if (HasPrefabs(wall, "Wall", "As04_CreateWallRow"))
            {
                As04_CreateWallRow(columns, wall);
            }

            if (HasPrefabs(floorTiles, "Floor Tiles", "As05_CreateFloor"))
            {
                As05_CreateFloor(columns, mapRows, floorTiles);
            }

            if (HasPrefabs(wall, "Wall", "As06_CreateWall"))
            {
                As06_CreateWall(columns, mapRows, wall);
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
            // Guideline:
            // 1. สร้าง 2D Array int[,] my2DArray ขนาด 3x3 พร้อมกำหนดค่าเริ่มต้น:
            //    { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }
            // 2. ใช้ Debug.Log พิมพ์ค่าออกมาทีละแถว คั่นด้วยเว้นวรรค
            //    - แถวที่ 0: my2DArray[0, 0] + " " + my2DArray[0, 1] + " " + my2DArray[0, 2]
            //    - แถวที่ 1: my2DArray[1, 0] + " " + my2DArray[1, 1] + " " + my2DArray[1, 2]
            //    - แถวที่ 2: my2DArray[2, 0] + " " + my2DArray[2, 1] + " " + my2DArray[2, 2]
        }

        public void As02_ArraySize(int rows, int cols)
        {
            // Guideline:
            // 1. สร้าง 2D Array int[,] my2DArray ขนาด [rows, cols]
            // 2. หาจำนวนแถว (มิติที่ 1) ด้วย my2DArray.GetLength(0) แล้วพิมพ์ "rows = " + rows
            // 3. หาจำนวนคอลัมน์ (มิติที่ 2) ด้วย my2DArray.GetLength(1) แล้วพิมพ์ "cols = " + cols
        }

        public void As03_GetSet2DArray()
        {
            // Guideline:
            // 1. สร้าง 2D Array int[,] ขนาด 3x3: { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }
            //    และ string[,] ขนาด 2x3: { { "A", "B", "C" }, { "D", "E", "F" } }
            // 2. ดึงค่า (Get) จาก my2DArray แถวที่ 1 คอลัมน์ที่ 2 แล้วพิมพ์ "get : " + ค่าที่ได้
            // 3. เปลี่ยนค่า (Set) ใน my2DArray แถวที่ 1 คอลัมน์ที่ 2 ให้เป็น 70 แล้วพิมพ์ "set : 70"
            // 4. พิมพ์เส้นคั่น LineSeparator ("============================")
            // 5. ใช้ Nested Loop (for ซ้อน for) วนพิมพ์สมาชิกทั้งหมดของ my2DArray ทีละแถว คั่นด้วยช่องว่าง
            // 6. พิมพ์เส้นคั่น LineSeparator
            // 7. ดึงค่า (Get) จาก my2DStringArray แถวที่ 0 คอลัมน์ที่ 2 แล้วพิมพ์ "get : " + ค่าที่ได้
            // 8. เปลี่ยนค่า (Set) ใน my2DStringArray แถวที่ 0 คอลัมน์ที่ 2 ให้เป็น "Cat" แล้วพิมพ์ "set : Cat"
            // 9. พิมพ์เส้นคั่น LineSeparator
            // 10. ใช้ Nested Loop วนพิมพ์สมาชิกทั้งหมดของ my2DStringArray ทีละแถว คั่นด้วยช่องว่าง
        }

        public void As04_CreateWallRow(int columns, GameObject wall)
        {
            // Guideline:
            // 1. ใช้ลูป for วน x ตั้งแต่ 0 ถึง columns - 1
            // 2. ตรวจสอบว่า wall ไม่เป็น null ให้ Instantiate(wall, new Vector2(x, 0), Quaternion.identity)
            // 3. สะสมสตริงเครื่องหมาย "*" ของแต่ละช่อง
            // 4. เมื่อจบลูป ให้พิมพ์สตริงแถวกำแพงออกมา เช่น "*****"
        }

        public void As04_CreateWallRow(int columns, GameObject[] walls)
        {
            // Guideline: (Overload สำหรับสุ่มชนิดกำแพง)
            // 1. วนลูป x ตั้งแต่ 0 ถึง columns - 1
            // 2. สุ่มเลือก prefab จาก walls ด้วย walls[Random.Range(0, walls.Length)]
            // 3. Instantiate ที่ตำแหน่ง new Vector2(x, 0)
            // 4. พิมพ์สตริง "*" ออกมาทาง Console
        }

        public void As05_CreateFloor(int columns, int rows, GameObject[] floorTiles)
        {
            // Guideline:
            // 1. ใช้ Nested Loop: ลูปนอกวน y ตั้งแต่ 0 ถึง rows - 1, ลูปในวน x ตั้งแต่ 0 ถึง columns - 1
            // 2. ในแต่ละช่อง ให้สุ่มเลือกแผ่นพื้นจาก floorTiles ด้วย Random.Range(0, floorTiles.Length)
            // 3. Instantiate แผ่นพื้นที่สุ่มได้ที่พิกัด new Vector2(x, y)
            // 4. สะสมชื่อของแผ่นพื้น (tileChoice.name) ในแต่ละแถว แล้วพิมพ์ออกมาทีละแถว
        }

        public void As06_CreateWall(int columns, int rows, GameObject wall)
        {
            // Guideline:
            // 1. ขอบเขตกำแพงรอบนอกขยายออกไป 1 ช่อง: ลูป y จาก -1 ถึง rows, ลูป x จาก -1 ถึง columns
            // 2. ตรวจสอบเงื่อนไขว่าเป็นตำแหน่งขอบนอกหรือไม่: (x == -1 || x == columns || y == -1 || y == rows)
            // 3. ถ้าเป็นขอบ ให้ Instantiate(wall, new Vector2(x, y), Quaternion.identity) และสะสม "*"
            // 4. ถ้าไม่ใช่ขอบ (พื้นที่ภายใน) ให้สะสมช่องว่าง " "
            // 5. พิมพ์สตริงแต่ละแถวออกมาทาง Console
        }

        public void As06_CreateWall(int columns, int rows, GameObject[] walls)
        {
            // Guideline: (Overload สำหรับสุ่มชนิดกำแพงรอบนอก)
            // 1. ลูป y จาก -1 ถึง rows และ x จาก -1 ถึง columns
            // 2. ถ้าเป็นขอบนอก ให้สุ่มเลือก prefab จาก walls แล้ว Instantiate ที่ (x, y) พร้อมสะสม "*"
            // 3. ถ้าไม่ใช่ขอบ ให้สะสม " "
            // 4. พิมพ์แต่ละแถวออกมาทาง Console
        }

        public void As07_SetItemPosition(Transform item, int itemPosX, int itemPosY)
        {
            // Guideline:
            // 1. ตั้งค่า item.position เป็น new Vector2(itemPosX, itemPosY)
            // 2. พิมพ์พิกัด item.position ออกมาทาง Console
        }

        public void As08_RandomFoodItem(int columns, int rows, GameObject[] foodTiles)
        {
            // Guideline:
            // 1. สุ่มพิกัด x จาก 0 ถึง columns - 1 และ y จาก 0 ถึง rows - 1 ด้วย Random.Range
            // 2. สุ่มเลือกไอเทมอาหารจาก foodTiles ด้วย Random.Range(0, foodTiles.Length)
            // 3. Instantiate ไอเทมที่สุ่มได้ที่พิกัด (x, y)
            // 4. พิมพ์ข้อความ "{ชื่อไอเทม} at x: {x} y: {y}"
        }

        public void As09_CreateItemFromArray(GameObject[] items, int itemPosX, int itemPosY)
        {
            // Guideline:
            // 1. สร้าง 2D String Array my2DStringArray ขนาด 3x3:
            //    { { " ", "Soda", " " }, { " ", " ", " " }, { " ", " ", "Food" } }
            // 2. อ่านชื่อไอเทมจาก my2DStringArray ที่พิกัด [itemPosY, itemPosX]
            // 3. ถ้าพบชื่อไอเทม (ไม่ใช่ช่องว่าง):
            //    - วนลูปหา prefab ใน items ที่มี name ตรงกับชื่อที่ได้
            //    - ถ้าพบ ให้ Instantiate ที่พิกัด (itemPosX, itemPosY)
            //    - พิมพ์ "Create Item " + itemName + " at x: " + itemPosX + " y: " + itemPosY แล้ว return
            // 4. ถ้าไม่พบไอเทม ให้พิมพ์ "No items at x: " + itemPosX + " y: " + itemPosY
        }

        #endregion

        #region Homework

        #region Level 1: Simple

        public void Lv01_SumRow(int[,] matrix, int row)
        {
            // Guideline:
            // 1. ประกาศตัวแปร int sum = 0; เพื่อสะสมผลรวม
            // 2. ใช้ลูป for วนคอลัมน์ c ตั้งแต่ 0 ถึง matrix.GetLength(1) - 1
            // 3. นำค่า matrix[row, c] มาบวกเข้ากับ sum
            // 4. เมื่อจบลูป ให้พิมพ์ sum ออกมา
        }

        public void Lv02_SumColumn(int[,] matrix, int col)
        {
            // Guideline:
            // 1. ประกาศตัวแปร int sum = 0; เพื่อสะสมผลรวม
            // 2. ใช้ลูป for วนแถว r ตั้งแต่ 0 ถึง matrix.GetLength(0) - 1
            // 3. นำค่า matrix[r, col] มาบวกเข้ากับ sum
            // 4. เมื่อจบลูป ให้พิมพ์ sum ออกมา
        }

        public void Lv03_BuildVillage(int columns, int rows, GameObject villageTile)
        {
            // Guideline:
            // 1. ใช้ Nested Loop วน y ตั้งแต่ 0 ถึง rows - 1 และ x ตั้งแต่ 0 ถึง columns - 1
            // 2. แต่ละช่องให้ Instantiate villageTile ที่ตำแหน่ง new Vector2(x, y)
            // 3. สะสมสตริงสัญลักษณ์ "*" ในแต่ละแถว แล้วพิมพ์ออกมาทีละแถว
        }

        public void Lv04_BuildRiver(int size, GameObject riverTile)
        {
            // Guideline:
            // 1. สร้างแม่น้ำทรงสามเหลี่ยม ลูปแถว r ตั้งแต่ 1 ถึง size
            // 2. ในแถว r ให้มีจำนวนช่อง r ช่อง (ลูป i ตั้งแต่ 0 ถึง r - 1)
            // 3. Instantiate riverTile ที่ตำแหน่ง new Vector2(i, r - 1)
            // 4. สะสมสตริง "*" ในแต่ละแถว แล้วพิมพ์ออกมาทีละแถว
        }

        public void Lv05_MultiplicationTableNested(int fromTable, int toTable)
        {
            // Guideline:
            // 1. ลูปนอกวนตัวคูณ i ตั้งแต่ 1 ถึง 12
            // 2. ลูปในวนแม่สูตรคูณ table ตั้งแต่ fromTable ถึง toTable
            // 3. จัดรูปแบบข้อความ "{table} x {i} = {table * i}" คั่นระหว่างแม่ด้วย Tab "\t"
            // 4. พิมพ์สูตรคูณออกมาทีละบรรทัด
        }

        public void Lv06_FindMaxInMatrix(int[,] matrix)
        {
            // Guideline:
            // 1. กำหนดตัวแปร int max = matrix[0, 0]; และ int maxR = 0, maxC = 0;
            // 2. ใช้ Nested Loop วนทุกแถว r และทุกคอลัมน์ c ใน matrix
            // 3. ถ้า matrix[r, c] > max ให้ปรับค่า max = matrix[r, c], maxR = r, maxC = c
            // 4. เมื่อจบลูป ให้พิมพ์: "Max value " + max + " at [" + maxR + ", " + maxC + "]"
        }

        public void Lv07_CountTargetValue(int[,] matrix, int target)
        {
            // Guideline:
            // 1. กำหนดตัวแปร int count = 0; เพื่อนับจำนวน
            // 2. ใช้ Nested Loop วนทุกช่องใน matrix
            // 3. ถ้า matrix[r, c] == target ให้ count++
            // 4. พิมพ์: "Found target " + target + ": " + count + " cells"
        }

        public void Lv08_SumAllElements(int[,] matrix)
        {
            // Guideline:
            // 1. กำหนดตัวแปร int sum = 0; ไว้นอกลูป
            // 2. ใช้ Nested Loop วนทุกช่องใน matrix แล้วนำค่ามาบวกสะสมใน sum
            // 3. พิมพ์ sum ออกมา
        }

        public void Lv09_BuildInvertedRiver(int size, GameObject riverTile)
        {
            // Guideline:
            // 1. สร้างแม่น้ำสามเหลี่ยมกลับหัว แถวแรกกว้าง size แล้วลดลงทีละ 1 จนถึง 1 (ลูป r จาก size ลงมาถึง 1)
            // 2. ใช้ตัวแปร y นับตำแหน่งแนวตั้ง เริ่มจาก 0 และเพิ่มขึ้นทีละ 1 ทุกแถว
            // 3. ในแต่ละแถว ลูป i ตั้งแต่ 0 ถึง r - 1 ให้ Instantiate riverTile ที่ (i, y) พร้อมสะสม "*"
            // 4. พิมพ์สตริงแถวออกมาทีละบรรทัด
        }

        public void Lv10_PrintMainDiagonal(int[,] matrix)
        {
            // Guideline:
            // 1. หาขนาดแนวทแยง minDim = Mathf.Min(matrix.GetLength(0), matrix.GetLength(1))
            // 2. วนลูป i ตั้งแต่ 0 ถึง minDim - 1 ดึงค่าแนวทแยงหลัก matrix[i, i]
            // 3. นำค่ามาต่อเป็นข้อความบรรทัดเดียว คั่นด้วยช่องว่าง " " เช่น "1 5 9"
            // 4. พิมพ์ข้อความออกมา
        }

        #endregion

        #region Level 2: Moderate

        public void Ex01_TicTacToe(int[,] moves)
        {
            // Guideline:
            // 1. สร้างกระดาน char[3, 3] กำหนดค่าเริ่มต้นทุกช่องเป็น ' ' (ช่องว่าง)
            // 2. กำหนดผู้เล่นเริ่มต้น char current = 'X';
            // 3. วนลูปเดินตามตาใน moves (moves.GetLength(0))
            //    - ดึงพิกัด moveRow = moves[m, 0], moveCol = moves[m, 1]
            //    - พิมพ์ "Player " + current + ":" ตามด้วยพิกัด "moveRow moveCol"
            //    - ถ้าช่องนั้นไม่ว่าง (board[moveRow, moveCol] != ' ') ให้พิมพ์ "cannot set " + moveRow + " " + moveCol แล้วข้าม (continue)
            //    - ถ้าว่าง ให้วางหมาก board[moveRow, moveCol] = current แล้วเรียก PrintBoard(board)
            //    - ตรวจผลด้วย Ex01_CheckWinner(board)
            //      ถ้าได้ 'X' หรือ 'O' ให้พิมพ์ result + " wins!" แล้ว return
            //      ถ้าได้ 'D' ให้พิมพ์ "Draw!" แล้ว return
            //    - ถ้ายังไม่จบ ให้สลับผู้เล่น current = (current == 'X') ? 'O' : 'X';
        }

        public char Ex01_CheckWinner(char[,] board)
        {
            // Guideline:
            // 1. เช็คผู้ชนะ 8 แนว (แนวนอน 3 แถว, แนวตั้ง 3 คอลัมน์, แนวทแยง 2 เส้น)
            //    โดยช่องแรกต้องไม่เป็นช่องว่าง ' ' และทั้ง 3 ช่องต้องเหมือนกัน
            //    ถ้ามีแนวที่ตรงกัน ให้ return สัญลักษณ์ของผู้ชนะ ('X' หรือ 'O')
            // 2. ถ้ายังไม่มีใครชนะ ให้ตรวจว่ายังมีช่องว่าง ' ' เหลืออยู่หรือไม่
            //    ถ้ามีช่องว่างเหลือ แปลว่าเกมยังไม่จบ ให้ return ' '
            // 3. ถ้าไม่มีช่องว่างเหลือแล้ว และไม่มีใครชนะ ให้ return 'D' (Draw = เสมอ)
            return ' ';
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
            // 1. พิมพ์หัวข้อ "Check around (" + targetX + ", " + targetY + ")"
            // 2. ตรวจสอบ 4 ทิศรอบตัวตามลำดับ: Up (y + 1), Down (y - 1), Left (x - 1), Right (x + 1)
            // 3. เงื่อนไขผลลัพธ์ของแต่ละทิศ:
            //    - ถ้านอกขอบเขตแผนที่ (x < 0 หรือ x >= cols หรือ y < 0 หรือ y >= rows) -> "Out of Bounds"
            //    - ถ้าค่าในแผนที่เป็น 0 (map[y, x] == 0) -> "Walkable"
            //    - ถ้าค่าในแผนที่ไม่ใช่ 0 -> "Blocked"
            // 4. พิมพ์ผลแต่ละทิศในรูปแบบ: "{ทิศ} ({x}, {y}) : {ผลลัพธ์}"
        }

        public void Ex03_SpawnChestsInCorners(int columns, int rows, GameObject chestPrefab)
        {
            // Guideline:
            // 1. พิกัด 4 มุมของแผนที่คือ: (0, 0), (columns - 1, 0), (0, rows - 1), (columns - 1, rows - 1)
            // 2. Instantiate chestPrefab ที่ตำแหน่งทั้ง 4 มุม
            // 3. วนลูปพิมพ์ผังแผนที่จากแถวบนลงล่าง (y จาก rows - 1 ลงมาถึง 0, x จาก 0 ถึง columns - 1)
            //    - ถ้าเป็นตำแหน่ง 4 มุม ให้ใช้สัญลักษณ์ "C"
            //    - ตำแหน่งอื่น ให้ใช้สัญลักษณ์ "."
            // 4. พิมพ์ผังออกมาทีละแถว และปิดท้ายด้วยข้อความ "Spawned 4 chests at corners"
        }

        #endregion

        #endregion
    }
}
