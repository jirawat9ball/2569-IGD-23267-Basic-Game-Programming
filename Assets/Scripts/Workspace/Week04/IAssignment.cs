using UnityEngine;

namespace Week04
{
    public interface IAssignment
    {
        #region เรื่องที่ 1: รู้จัก 2D Array (ข้อ 1-3)

        void Ex01_Create2DArray();

        void Ex02_ArraySize(int rows, int cols);

        void Ex03_GetSet2DArray();

        #endregion

        #region เรื่องที่ 2: 2D Array กับวัตถุในเกม (ข้อ 4-5)

        void Ex04_SetItemPosition(Transform item, int itemPosX, int itemPosY);

        void Ex05_CreateItemFromArray(GameObject[] items, int itemPosX, int itemPosY);

        #endregion

        #region เรื่องที่ 3: วนลูปหาผลรวมในตาราง (ข้อ 6-7)

        void Ex06_SumRow(int[,] matrix, int row);

        void Ex07_SumColumn(int[,] matrix, int col);

        #endregion

        #region เรื่องที่ 4: Nested Loop สร้างรูปแบบและแผนที่ (ข้อ 8-13)

        void Ex08_StarPattern(int columns, int rows);

        void Ex09_RandomFloorMap(int columns, int rows, GameObject[] floorTiles);

        void Ex10_BuildOuterWall(int columns, int rows, GameObject wall);

        void Ex11_RandomFoodItem(int columns, int rows, GameObject[] foodTiles);

        void Ex12_TrianglePattern(int size);

        void Ex13_MultiplicationTableNested(int fromTable, int toTable);

        #endregion

        #region เรื่องที่ 5: โปรเจกต์รวม (ข้อ 14)

        void Ex14_TicTacToe(int[,] moves);

        #endregion
    }
}
