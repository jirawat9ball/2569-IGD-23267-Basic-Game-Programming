using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Exam_Sokoban
{
    public class Exam_Teacher_Sokoban : MonoBehaviour, IExam_Sokoban
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

        private int columns;
        private int rows;
        private bool[,] isWall;
        private bool[,] isTarget;
        private bool[,] isHole;
        private GameObject[,] boxes;
        private GameObject[,] floors;
        private GameObject player;
        private Vector2Int playerPos;

        private bool finished;

        void Start()
        {
            BuildLevel(map);
        }

        #region ข้อ 1: ทำฉาก

        public void BuildLevel(string[] map)
        {
            rows = map.Length;
            columns = map[0].Length;

            isWall = new bool[columns, rows];
            isTarget = new bool[columns, rows];
            isHole = new bool[columns, rows];
            boxes = new GameObject[columns, rows];
            floors = new GameObject[columns, rows];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    char symbol = map[row][col];
                    int x = col;
                    int y = rows - 1 - row;
                    Vector2 position = new Vector2(x, y);

                    if (symbol == '#')
                    {
                        isWall[x, y] = true;
                        GameObject wallChoice = wallTiles[Random.Range(0, wallTiles.Length)];
                        Instantiate(wallChoice, position, Quaternion.identity);
                        continue;
                    }

                    if (symbol == 'X')
                    {
                        isTarget[x, y] = true;
                        floors[x, y] = Instantiate(targetPrefab, position, Quaternion.identity);
                        SetColor(floors[x, y], Color.yellow);
                    }
                    else
                    {
                        GameObject floorChoice = floorTiles[Random.Range(0, floorTiles.Length)];
                        floors[x, y] = Instantiate(floorChoice, position, Quaternion.identity);
                    }

                    if (symbol == 'B')
                    {
                        boxes[x, y] = Instantiate(boxPrefab, position, Quaternion.identity);
                        SetSortingOrder(boxes[x, y], 2);
                    }
                    else if (symbol == 'P')
                    {
                        player = Instantiate(playerPrefab, position, Quaternion.identity);
                        SetSortingOrder(player, 3);
                        playerPos = new Vector2Int(x, y);
                    }
                }
            }
        }

        #endregion

        #region ข้อ 2: ควบคุมตัวละคร + รับอินพุต

        void Update()
        {
            if (finished) return;

            Vector2Int direction = Vector2Int.zero;

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) direction = Vector2Int.up;
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) direction = Vector2Int.down;
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) direction = Vector2Int.left;
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) direction = Vector2Int.right;

            if (direction == Vector2Int.zero) return;

            if (TryMove(direction) && IsSolved())
            {
                finished = true;
            }
        }

        public bool TryMove(Vector2Int direction)
        {
            Vector2Int next = playerPos + direction;
            if (!CanEnter(next)) return false;

            // ข้อ 3: ช่องถัดไปมีกล่อง -> ดูช่องหลังกล่องอีกชั้น
            GameObject box = boxes[next.x, next.y];
            if (box != null)
            {
                Vector2Int beyond = next + direction;
                if (!CanEnter(beyond) || boxes[beyond.x, beyond.y] != null) return false;

                boxes[next.x, next.y] = null;
                boxes[beyond.x, beyond.y] = box;
                box.transform.position = new Vector3(beyond.x, beyond.y, 0f);
            }

            Vector2Int from = playerPos;
            playerPos = next;
            player.transform.position = new Vector3(next.x, next.y, 0f);

            // ข้อ 4: ช่องที่เพิ่งเดินออกมากลายเป็นหลุม
            if (collapseMode)
            {
                isHole[from.x, from.y] = true;
                RemoveObject(floors[from.x, from.y]);
                floors[from.x, from.y] = null;
            }

            return true;
        }

        private bool CanEnter(Vector2Int cell)
        {
            if (cell.x < 0 || cell.x >= columns || cell.y < 0 || cell.y >= rows) return false;
            if (isWall[cell.x, cell.y]) return false;
            if (isHole[cell.x, cell.y]) return false;
            return true;
        }

        #endregion

        #region ข้อ 3: ตรวจว่าชนะหรือยัง

        public bool IsSolved()
        {
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (isTarget[x, y] && boxes[x, y] == null) return false;
                }
            }

            Debug.Log("You Win");
            return true;
        }

        #endregion

        #region ตัวช่วย (เขียนไว้ให้แล้ว ไม่ต้องแก้)

        private void RemoveObject(GameObject target)
        {
            if (target == null) return;
            if (Application.isPlaying) Destroy(target);
            else DestroyImmediate(target);
        }

        private void SetColor(GameObject target, Color color)
        {
            if (target == null) return;
            SpriteRenderer sr = target.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = color;
        }

        private void SetSortingOrder(GameObject target, int order)
        {
            if (target == null) return;
            SpriteRenderer sr = target.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sortingOrder = order;
        }

        #endregion
    }
}
