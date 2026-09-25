using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

// ปิดคำเตือน "ตัวแปรยังไม่ถูกใช้" ระหว่างที่ยังเขียนไม่เสร็จ
#pragma warning disable 0169, 0414, 0649

namespace Exam_Sokoban
{
    public class Exam_Student_Sokoban : MonoBehaviour, IExam_Sokoban
    {
        [Header("Prefab (ลากใส่ใน Inspector)")]
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject playerPrefab;
        public GameObject boxPrefab;
        public GameObject targetPrefab;

        [Header("ด่าน (map[0] คือแถวบนสุด)")]
        public string[] map =
        {
            "##########",
            "#........#",
            "#..B..X..#",
            "#..P.....#",
            "#.....B..#",
            "#..X.....#",
            "##########",
        };

        [Header("ข้อ 4 (โบนัส): พื้นถล่ม")]
        public bool collapseMode = false;

        // ===================================================================================
        // ข้อมูลของด่าน — ประกาศไว้ให้แล้ว ใช้ร่วมกันในข้อ 1–4 (ห้ามเปลี่ยนชื่อ)
        // ทุกตัวใช้ index แบบ [x, y] ตามพิกัดในฉาก ไม่ใช่ [row, col]
        // ===================================================================================
        private int columns;                // ความกว้างของด่าน = map[0].Length
        private int rows;                   // ความสูงของด่าน   = map.Length
        private bool[,] isWall;             // isWall[x, y]   = ช่องนี้เป็นกำแพงไหม
        private bool[,] isTarget;           // isTarget[x, y] = ช่องนี้เป็นเป้าหมายไหม
        private bool[,] isHole;             // isHole[x, y]   = ช่องนี้กลายเป็นหลุมแล้วไหม (ข้อ 4)
        private GameObject[,] boxes;        // boxes[x, y]    = กล่องที่อยู่ในช่องนี้ (null = ไม่มีกล่อง)
        private GameObject[,] floors;       // floors[x, y]   = พื้น/เป้าหมายที่สร้างไว้ในช่องนี้ (ใช้ตอนพื้นถล่ม)
        private GameObject player;          // ตัวผู้เล่นในฉาก
        private Vector2Int playerPos;       // ผู้เล่นยืนอยู่ช่องไหน (x, y)

        void Start()
        {
            BuildLevel(map);
        }

        #region ข้อ 1: ทำฉาก (25 คะแนน)

        public void BuildLevel(string[] map)
        {
            // Guideline:
            // 1. หาขนาดด่าน: rows = map.Length และ columns = map[0].Length
            // 2. สร้าง array เก็บข้อมูลทุกตัวด้วยขนาด [columns, rows]
            //    เช่น isWall = new bool[columns, rows];  boxes = new GameObject[columns, rows];
            //    (ทำให้ครบ: isWall, isTarget, isHole, boxes, floors)
            // 3. ใช้ Nested Loop: ลูปนอกวน row จาก 0 ถึง rows - 1, ลูปในวน col จาก 0 ถึง columns - 1
            //    อ่านตัวอักษรทีละช่องด้วย char symbol = map[row][col];
            // 4. แปลงเป็นพิกัดในฉาก (สำคัญที่สุด อ่านใน Instruction ให้เข้าใจก่อน):
            //       int x = col;
            //       int y = rows - 1 - row;     <- แถวบนสุดของ map ต้องอยู่สูงสุดในฉาก
            // 5. แยกตามสัญลักษณ์:
            //    '#' -> isWall[x, y] = true แล้ว Instantiate กำแพง (สุ่มจาก wallTiles) ที่ new Vector2(x, y)
            //           ช่องกำแพงไม่ต้องมีพื้น
            //    'X' -> isTarget[x, y] = true แล้ว Instantiate targetPrefab เก็บไว้ใน floors[x, y]
            //           จากนั้นเรียก SetColor(floors[x, y], Color.yellow);
            //    อื่น ๆ ('.', 'P', 'B') -> Instantiate พื้น (สุ่มจาก floorTiles) เก็บไว้ใน floors[x, y]
            //    'B' -> สร้างกล่องจาก boxPrefab ทับบนพื้น เก็บไว้ใน boxes[x, y]
            //           แล้วเรียก SetSortingOrder(boxes[x, y], 2); ให้กล่องอยู่เหนือพื้น
            //    'P' -> สร้างผู้เล่นจาก playerPrefab ทับบนพื้น เก็บไว้ใน player
            //           แล้วเรียก SetSortingOrder(player, 3); และจำตำแหน่ง playerPos = new Vector2Int(x, y);
        }

        #endregion

        #region ข้อ 2: ควบคุมตัวละคร + รับอินพุต (25 คะแนน)

        void Update()
        {
            // Guideline:
            // 1. อ่านปุ่มด้วย Input.GetKeyDown (กด 1 ครั้ง = เดิน 1 ช่อง)
            //    ลูกศรขึ้น หรือ W -> ทิศ Vector2Int.up        ลูกศรลง หรือ S -> ทิศ Vector2Int.down
            //    ลูกศรซ้าย หรือ A -> ทิศ Vector2Int.left      ลูกศรขวา หรือ D -> ทิศ Vector2Int.right
            //    ตัวอย่าง: if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) { ... }
            // 2. ถ้ามีการกดปุ่ม ให้เรียก TryMove(ทิศที่ได้)
            // 3. ถ้า TryMove คืน true (เดินได้จริง) ให้เรียก IsSolved() เพื่อเช็คว่าชนะหรือยัง
            // * ห้ามเขียนการเดินไว้ใน Update ตรง ๆ — ลอจิกการเดินต้องอยู่ใน TryMove เท่านั้น
        }

        public bool TryMove(Vector2Int direction)
        {
            // Guideline ข้อ 2 (เดินธรรมดา):
            // 1. ช่องถัดไป: Vector2Int next = playerPos + direction;
            // 2. ถ้า next อยู่นอกแผนที่ (x < 0, x >= columns, y < 0, y >= rows) -> return false
            // 3. ถ้า next เป็นกำแพง (isWall[next.x, next.y]) -> return false
            // 4. ถ้าเดินได้: อัปเดต playerPos = next;
            //    แล้วย้ายตัวในฉาก player.transform.position = new Vector3(next.x, next.y, 0f);
            //    แล้ว return true
            //
            // Guideline ข้อ 3 (ผลักกล่อง) — แทรกก่อนขั้นตอนที่ 4:
            // 5. ถ้า next มีกล่อง (boxes[next.x, next.y] != null) ให้ดูช่องหลังกล่อง:
            //       Vector2Int beyond = next + direction;
            //    - ถ้า beyond อยู่นอกแผนที่ / เป็นกำแพง / มีกล่องอีกใบ -> return false (ไม่ขยับทั้งคู่)
            //    - ถ้าผลักได้: ย้ายกล่องในข้อมูล boxes[beyond.x, beyond.y] = กล่อง; boxes[next.x, next.y] = null;
            //      และย้ายกล่องในฉาก กล่อง.transform.position = new Vector3(beyond.x, beyond.y, 0f);
            //      จากนั้นผู้เล่นเดินตามไปที่ next ตามขั้นตอนที่ 4
            //
            // Guideline ข้อ 4 (โบนัส พื้นถล่ม) — ทำเฉพาะเมื่อ collapseMode เป็น true:
            // 6. จำช่องเดิมไว้ก่อนย้าย (Vector2Int from = playerPos;)
            // 7. หลังเดินสำเร็จ: isHole[from.x, from.y] = true;
            //    ลบพื้นช่องเดิมด้วย RemoveObject(floors[from.x, from.y]); แล้วตั้ง floors[from.x, from.y] = null;
            // 8. เพิ่มเงื่อนไข: ช่องที่เป็นหลุม (isHole) ห้ามผู้เล่นเดินเข้า และห้ามผลักกล่องลงไป
            //
            // Tip: ขั้นตอน 2, 3, 8 ใช้ซ้ำกันหลายที่ เขียนเป็นเมธอดช่วย เช่น bool CanEnter(Vector2Int cell) ก็ได้
            return false;
        }

        #endregion

        #region ข้อ 3: ตรวจว่าชนะหรือยัง (ส่วนหนึ่งของ 35 คะแนน)

        public bool IsSolved()
        {
            // Guideline:
            // 1. ใช้ Nested Loop วนทุกช่อง x (0 ถึง columns - 1) และ y (0 ถึง rows - 1)
            // 2. ถ้าเจอช่องที่เป็นเป้าหมาย (isTarget[x, y]) แต่ไม่มีกล่อง (boxes[x, y] == null) -> return false
            // 3. ถ้าวนครบแล้วทุกเป้าหมายมีกล่องหมด -> Debug.Log("You Win"); แล้ว return true
            return false;
        }

        #endregion

        #region ตัวช่วย (เขียนไว้ให้แล้ว ไม่ต้องแก้)

        // ลบ object ออกจากฉาก (ใช้ได้ทั้งตอนกด Play และตอนรัน Test Runner)
        private void RemoveObject(GameObject target)
        {
            if (target == null) return;
            if (Application.isPlaying) Destroy(target);
            else DestroyImmediate(target);
        }

        // เปลี่ยนสีของ Sprite
        private void SetColor(GameObject target, Color color)
        {
            if (target == null) return;
            SpriteRenderer sr = target.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = color;
        }

        // ตั้งลำดับการวาด ตัวเลขมากกว่าจะอยู่ด้านบน
        private void SetSortingOrder(GameObject target, int order)
        {
            if (target == null) return;
            SpriteRenderer sr = target.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sortingOrder = order;
        }

        #endregion
    }
}
