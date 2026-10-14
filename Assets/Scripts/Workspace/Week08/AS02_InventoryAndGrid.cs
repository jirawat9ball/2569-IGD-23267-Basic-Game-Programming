using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08.Ex02
{
    public class InventoryAndGridScanner : MonoBehaviour
    {
        // =========================================================================
        // ระดับ 2: การค้นหาและเรดาร์สำรวจดันเจี้ยนบนตาราง (Dungeon Grid Radar)
        // =========================================================================

        // 1. เมธอด FindItemPosition(int[,] mapGrid, int itemCode) คืนค่า Vector2Int
        //    - ใช้ Nested Loop ค้นหาพิกัดช่องแรกใน mapGrid ที่มีค่าเท่ากับ itemCode (เช่น ไม้สติ๊กรหัส 3)
        //    - หากพบ ให้คืนค่า new Vector2Int(row, col) ทันที
        //    - หากวนจนครบแล้วไม่พบ ให้คืนค่า new Vector2Int(-1, -1)
        public Vector2Int FindItemPosition(int[,] mapGrid, int itemCode)
        {
            // Student code starts HERE ...
            return new Vector2Int(-1, -1);
            // Student code ends HERE ...
        }

        // 2. เมธอด CountItems(int[,] mapGrid, int itemCode) คืนค่า int
        //    - ใช้ Nested Loop วนอ่านค่าทุกช่องใน mapGrid
        //    - นับจำนวนช่องทั้งหมดที่มีค่าเท่ากับ itemCode
        //    - คืนค่าจำนวนไอเทมทั้งหมดที่นับได้
        public int CountItems(int[,] mapGrid, int itemCode)
        {
            // Student code starts HERE ...
            return 0;
            // Student code ends HERE ...
        }

        // 3. เมธอด ScanThreats(int[,] mapGrid, Vector2Int playerPos, int threatCode) คืนค่า int
        //    - สแกนหาภัยคุกคามรอบตัวผู้เล่นในรัศมี 1 ช่อง (พื้นที่ 3x3 หรือ 8 ทิศทางรอบ playerPos)
        //    - วนลูป r ตั้งแต่ playerPos.x - 1 ถึง playerPos.x + 1
        //      และ c ตั้งแต่ playerPos.y - 1 ถึง playerPos.y + 1
        //    - ตรวจสอบว่าพิกัดอยู่ในขอบเขตแผนที่หรือไม่ (r >= 0 && r < mapGrid.GetLength(0) && c >= 0 && c < mapGrid.GetLength(1))
        //    - ข้ามช่องที่ตัวผู้เล่นยืนอยู่ (r == playerPos.x && c == playerPos.y)
        //    - นับจำนวนช่องที่มีค่าเท่ากับ threatCode
        //    - พิมพ์ $"Found {threatCount} threats around player"
        //    - คืนค่า threatCount
        public int ScanThreats(int[,] mapGrid, Vector2Int playerPos, int threatCode)
        {
            // Student code starts HERE ...
            return 0;
            // Student code ends HERE ...
        }

        // 4. เมธอด FindPlayerPosition(int[,] mapGrid, int playerCode) คืนค่า Vector2Int
        //    - ใช้ Nested Loop ค้นหาพิกัดช่องแรกที่มีค่าเท่ากับ playerCode (เช่น รหัส 1)
        //    - หากพบ ให้คืนค่า new Vector2Int(row, col) ทันที
        //    - หากไม่พบ ให้คืนค่า new Vector2Int(-1, -1)
        public Vector2Int FindPlayerPosition(int[,] mapGrid, int playerCode)
        {
            // Student code starts HERE ...
            return new Vector2Int(-1, -1);
            // Student code ends HERE ...
        }
    }
}
