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

        // 3. เมธอด ScanThreats(int[,] mapGrid, int threatCode) คืนค่า int
        //    - ใช้ Nested Loop วนอ่านค่าทุกแถวและคอลัมน์ใน mapGrid
        //      โดยใช้ mapGrid.GetLength(0) และ mapGrid.GetLength(1)
        //    - นับจำนวนช่องทั้งหมดที่มีค่าเท่ากับ threatCode
        //    - พิมพ์ $"Found {threatCount} threats of code {threatCode}"
        //    - คืนค่า threatCount
        public int ScanThreats(int[,] mapGrid, int threatCode)
        {
            // Student code starts HERE ...
            return 0;
            // Student code ends HERE ...
        }

        // 4. เมธอด FindExitPosition(int[,] mapGrid, int exitCode) คืนค่า Vector2Int
        //    - ใช้ Nested Loop ค้นหาพิกัดช่องแรกที่มีค่าเท่ากับ exitCode
        //    - หากพบ ให้คืนค่า new Vector2Int(row, col) ทันที
        //    - หากไม่พบ ให้คืนค่า new Vector2Int(-1, -1)
        public Vector2Int FindExitPosition(int[,] mapGrid, int exitCode)
        {
            // Student code starts HERE ...
            return new Vector2Int(-1, -1);
            // Student code ends HERE ...
        }
    }
}
