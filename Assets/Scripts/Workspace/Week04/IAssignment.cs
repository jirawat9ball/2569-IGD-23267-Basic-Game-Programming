using UnityEngine;

namespace Week04
{
    public interface IAssignment
    {
        #region Lecture

        void As01_Create2DArray();

        void As02_ArraySize(int rows, int cols);

        void As03_GetSet2DArray();

        void As04_CreateWallRow(int columns, GameObject wall);

        void As05_CreateFloor(int columns, int rows, GameObject[] floorTiles);

        void As06_CreateWall(int columns, int rows, GameObject wall);

        void As07_SetItemPosition(Transform item, int itemPosX, int itemPosY);

        void As08_RandomFoodItem(int columns, int rows, GameObject[] foodTiles);

        void As09_CreateItemFromArray(GameObject[] items, int itemPosX, int itemPosY);

        #endregion

        #region Homework

        #region Level 1: Simple

        void Lv01_SumRow(int[,] matrix, int row);

        void Lv02_SumColumn(int[,] matrix, int col);

        void Lv03_StarPattern(int columns, int rows);

        void Lv04_TrianglePattern(int size);

        void Lv05_MultiplicationTableNested(int fromTable, int toTable);

        void Lv06_FindMaxInMatrix(int[,] matrix);

        void Lv07_CountTargetValue(int[,] matrix, int target);

        void Lv08_SumAllElements(int[,] matrix);

        void Lv09_InvertedTrianglePattern(int size);

        void Lv10_PrintMainDiagonal(int[,] matrix);

        #endregion

        #region Level 2: Moderate

        void Ex01_TicTacToe(int[,] moves);

        void Ex02_CheckWalkableTile(int[,] map, int targetX, int targetY);

        void Ex03_SpawnChestsInCorners(int columns, int rows, GameObject chestPrefab);

        #endregion

        #endregion
    }
}
