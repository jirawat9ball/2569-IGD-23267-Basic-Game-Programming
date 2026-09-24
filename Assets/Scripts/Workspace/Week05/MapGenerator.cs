using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week05
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("ตัวแปรสำหรับสร้างแผนที่")]
        public int columns = 3;
        public int rows = 4;
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;
        public int foodCount = 3;
        public GameObject player;
        public GameObject exitTile;

        void Start()
        {
            // =========================================================================
            // โค้ดสร้างฉากจาก Week 4 (สำหรับให้นักเรียนฝึก Refactor แยกโค้ดเหล่านี้ออกไปเป็น Method)
            // เมื่อสร้าง Method เสร็จแล้ว ให้คอมเมนต์โค้ดด้านล่างแล้วเปลี่ยนมาเรียก Method แทน:
            // GenerateFloor();
            // GenerateWalls();
            // GenerateFoods();
            // PlacePlayer();
            // PlaceExit();
            // =========================================================================

            // --- 1. โค้ดสร้างพื้นแผนที่ (ย้ายไปใส่ใน Method GenerateFloor()) ---
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
                }
            }

            // --- 2. โค้ดสร้างกำแพงล้อมรอบ (ย้ายไปใส่ใน Method GenerateWalls()) ---
            for (int y = -1; y <= rows; y++)
            {
                for (int x = -1; x <= columns; x++)
                {
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        GameObject toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
                        Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
                    }
                }
            }

            // --- 3. โค้ดสุ่มวางอาหาร (ย้ายไปใส่ใน Method GenerateFoods()) ---
            for (int i = 0; i < foodCount; i++)
            {
                GameObject toInstantiate = foodTiles[Random.Range(0, foodTiles.Length)];
                Vector2 position = new Vector2(Random.Range(0, columns), Random.Range(0, rows));
                Instantiate(toInstantiate, position, Quaternion.identity);
            }

            // --- 4. โค้ดวางผู้เล่น (ย้ายไปใส่ใน Method PlacePlayer()) ---
            Instantiate(player, new Vector2(0, 0), Quaternion.identity);

            // --- 5. โค้ดวางทางออก (ย้ายไปใส่ใน Method PlaceExit()) ---
            Instantiate(exitTile, new Vector2(columns - 1, rows - 1), Quaternion.identity);
        }

        #region Method สำหรับสร้างแผนที่

        // Guideline ข้อ 3.1:
        // สร้าง Method ชื่อ GenerateFloor แบบ public void ไม่รับพารามิเตอร์
        // - ใช้ Nested Loop วน y ตั้งแต่ 0 ถึง rows-1 และ x ตั้งแต่ 0 ถึง columns-1
        // - แต่ละช่องสุ่มพื้นจาก floorTiles ด้วย Random.Range
        // - Instantiate พื้นที่ตำแหน่ง (x, y) ด้วย Quaternion.identity

        // Guideline ข้อ 3.2:
        // สร้าง Method ชื่อ GenerateWalls แบบ public void ไม่รับพารามิเตอร์
        // - วน x ตั้งแต่ -1 ถึง columns และ y ตั้งแต่ -1 ถึง rows (ขยายออกไปด้านละ 1 ช่องเพื่อทำขอบ)
        // - สร้างกำแพงเฉพาะช่องที่อยู่ขอบนอกเท่านั้น (เงื่อนไขขอบคือ x == -1 || x == columns || y == -1 || y == rows)
        // - สุ่มกำแพงจาก wallTiles แล้ว Instantiate ที่ตำแหน่งนั้น

        // Guideline ข้อ 3.3:
        // สร้าง Method ชื่อ GenerateFoods แบบ public void ไม่รับพารามิเตอร์
        // - วนลูปจำนวน foodCount รอบ
        // - แต่ละรอบสุ่มอาหารจาก foodTiles และสุ่มตำแหน่ง x (0 ถึง columns-1), y (0 ถึง rows-1)
        // - Instantiate อาหารที่ตำแหน่งที่สุ่มได้

        // Guideline ข้อ 3.4:
        // สร้าง Method ชื่อ PlacePlayer แบบ public void ไม่รับพารามิเตอร์
        // - Instantiate ตัวละคร player ไว้ที่มุมซ้ายล่างของแผนที่ คือตำแหน่ง (0, 0)

        // Guideline ข้อ 3.5:
        // สร้าง Method ชื่อ PlaceExit แบบ public void ไม่รับพารามิเตอร์
        // - Instantiate ทางออก exitTile ไว้ที่มุมขวาบนของแผนที่ คือตำแหน่ง (columns-1, rows-1)

        #endregion
    }
}
