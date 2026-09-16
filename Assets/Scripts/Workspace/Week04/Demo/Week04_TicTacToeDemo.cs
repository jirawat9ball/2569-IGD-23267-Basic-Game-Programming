using UnityEngine;

namespace Week04.Demo
{
    /// <summary>
    /// ซีนเกม XO — คลิกเมาส์เล่นได้จริง
    ///
    /// สคริปต์นี้ดูแลแค่ "หน้าตา" คือวาดตาราง รับคลิกเมาส์ และวางสัญลักษณ์
    /// ส่วน "การตัดสินว่าใครชนะ" เรียกใช้เมธอด Ex01_CheckWinner() ที่นักศึกษาเขียนเอง
    /// ถ้านักศึกษาเขียนผิด เกมนี้จะตัดสินผลผิดตามไปด้วย
    ///
    /// วิธีใช้: สร้างซีนใหม่ → GameObject เปล่า → ใส่ทั้ง Assignment_Student_Week04 และสคริปต์นี้ → กด Play
    /// </summary>
    public class Week04_TicTacToeDemo : MonoBehaviour
    {
        private const int Size = 3;

        [Tooltip("เว้นว่างได้ ถ้าสคริปต์ Assignment_Student_Week04 อยู่บน GameObject เดียวกัน")]
        public Assignment_Student_Week04 student;

        [Header("สี")]
        public Color boardColor = new Color(0.18f, 0.20f, 0.24f);
        public Color cellColor = new Color(0.92f, 0.92f, 0.88f);
        public Color xColor = new Color(0.85f, 0.22f, 0.22f);
        public Color oColor = new Color(0.20f, 0.45f, 0.85f);

        private char[,] board;
        private char current;
        private bool finished;
        private readonly GameObject[,] marks = new GameObject[Size, Size];

        private Sprite squareSprite;
        private Sprite xSprite;
        private Sprite oSprite;

        void Start()
        {
            if (student == null)
            {
                student = GetComponent<Assignment_Student_Week04>();
            }

            if (student == null)
            {
                Debug.LogError("[Week04_TicTacToeDemo] ไม่พบ Assignment_Student_Week04 " +
                               "— ลากใส่ช่อง Student หรือวางสคริปต์นี้ไว้บน GameObject เดียวกัน");
                enabled = false;
                return;
            }

            squareSprite = MakeSprite(DrawSquare);
            xSprite = MakeSprite(DrawX);
            oSprite = MakeSprite(DrawO);

            ResetGame();
            DrawGrid();
            PointCamera();
            Debug.Log("คลิกที่ช่องเพื่อเล่น · กด R เพื่อเริ่มใหม่");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ClearMarks();
                ResetGame();
                Debug.Log("เริ่มเกมใหม่");
                return;
            }

            if (finished || !Input.GetMouseButtonDown(0)) return;

            var cam = Camera.main;
            if (cam == null) return;

            Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
            int col = Mathf.RoundToInt(world.x);
            int row = Mathf.RoundToInt(world.y);

            if (col < 0 || col >= Size || row < 0 || row >= Size) return;
            if (board[row, col] != ' ')
            {
                Debug.Log($"cannot set {row} {col}");
                return;
            }

            board[row, col] = current;
            PlaceMark(row, col, current);
            Debug.Log($"Player {current}: {row} {col}");

            // เรียกโค้ดที่นักศึกษาเขียนมาตัดสินผล
            char result = student.Ex01_CheckWinner(board);

            if (result == 'X' || result == 'O')
            {
                Debug.Log($"{result} wins!");
                finished = true;
                return;
            }

            if (result == 'D')
            {
                Debug.Log("Draw!");
                finished = true;
                return;
            }

            current = current == 'X' ? 'O' : 'X';
        }

        private void ResetGame()
        {
            board = new char[Size, Size];
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    board[r, c] = ' ';
                }
            }
            current = 'X';
            finished = false;
        }

        private void DrawGrid()
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    NewSprite($"Cell_{r}_{c}", squareSprite, new Vector3(c, r, 0f), 0.92f, cellColor, 0);
                }
            }
        }

        private void PlaceMark(int row, int col, char player)
        {
            var sprite = player == 'X' ? xSprite : oSprite;
            var color = player == 'X' ? xColor : oColor;
            marks[row, col] = NewSprite($"Mark_{player}_{row}_{col}", sprite,
                new Vector3(col, row, 0f), 0.70f, color, 1);
        }

        private static GameObject NewSprite(string objectName, Sprite sprite, Vector3 position,
                                            float scale, Color color, int sortingOrder)
        {
            var go = new GameObject(objectName);
            go.transform.position = position;
            go.transform.localScale = new Vector3(scale, scale, 1f);

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;
            sr.color = color;
            sr.sortingOrder = sortingOrder;

            return go;
        }

        private void ClearMarks()
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    if (marks[r, c] != null)
                    {
                        Destroy(marks[r, c]);
                        marks[r, c] = null;
                    }
                }
            }
        }

        // ---------- สร้างรูปเองตอนรัน ไม่ต้องมีไฟล์ภาพ ----------

        private const int Res = 64;

        private delegate bool PixelTest(int x, int y);

        private static Sprite MakeSprite(PixelTest test)
        {
            var tex = new Texture2D(Res, Res, TextureFormat.RGBA32, false);
            tex.filterMode = FilterMode.Bilinear;

            for (int y = 0; y < Res; y++)
            {
                for (int x = 0; x < Res; x++)
                {
                    tex.SetPixel(x, y, test(x, y) ? Color.white : Color.clear);
                }
            }
            tex.Apply();

            return Sprite.Create(tex, new Rect(0, 0, Res, Res), new Vector2(0.5f, 0.5f), Res);
        }

        private static bool DrawSquare(int x, int y)
        {
            return true;
        }

        private static bool DrawX(int x, int y)
        {
            const float thickness = 6f;
            const float margin = 10f;

            if (x < margin || x > Res - margin || y < margin || y > Res - margin) return false;

            bool onMainDiagonal = Mathf.Abs(x - y) <= thickness;
            bool onAntiDiagonal = Mathf.Abs(x + y - (Res - 1)) <= thickness;
            return onMainDiagonal || onAntiDiagonal;
        }

        private static bool DrawO(int x, int y)
        {
            float cx = (Res - 1) * 0.5f;
            float cy = (Res - 1) * 0.5f;
            float dist = Mathf.Sqrt((x - cx) * (x - cx) + (y - cy) * (y - cy));

            const float outer = 26f;
            const float inner = 17f;
            return dist <= outer && dist >= inner;
        }

        private void PointCamera()
        {
            var cam = Camera.main;
            if (cam == null) return;

            cam.orthographic = true;
            cam.orthographicSize = 2.5f;
            cam.transform.position = new Vector3(1f, 1f, -10f);
            cam.transform.rotation = Quaternion.identity;
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = boardColor;
        }
    }
}
