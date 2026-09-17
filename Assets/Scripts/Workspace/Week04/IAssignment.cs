using UnityEngine;

namespace Week04
{
    public interface IAssignment
    {
        #region Lecture

        void As01_Create2DArray();

        void As02_ArraySize(int rows, int cols);

        void As03_GetSet2DArray();
        void Print2DArray(int[,] array);
        void Print2DArray(string[,] array);

        void As04_CreateWallRow(int columns, GameObject[] walls);

        void As05_CreateFloor(int columns, int rows, GameObject[] floorTiles);

        void As06_CreateWall(int columns, int rows, GameObject[] walls);

        void As07_SetItemPosition(GameObject item, int itemPosX, int itemPosY);

        void As08_RandomFoodItem(int columns, int rows, GameObject[] foodTiles);

        void As09_CreateItemFromArray(GameObject[] items);

        #endregion

        #region Homework

        #region Level 1: Simple

        void Lv01_GetSet2DStringArray();

        void Lv02_SumRow(int[,] matrix, int row);

        void Lv03_SumColumn(int[,] matrix, int col);

        void Lv04_BuildVillage(int columns, int rows, GameObject villageTile);

        void Lv05_BuildRiver(int size, GameObject riverTile);

        void Lv06_MultiplicationTableNested(int fromTable, int toTable);

        void Lv07_FindMaxInMatrix(int[,] matrix);

        void Lv08_CountTargetValue(int[,] matrix, int target);

        void Lv09_SumAllElements(int[,] matrix);

        void Lv10_BuildInvertedRiver(int size, GameObject riverTile);

        void Lv11_PrintMainDiagonal(int[,] matrix);

        #endregion

        #region Level 2: Moderate

        void Ex01_TicTacToe(int[,] moves);

        char Ex01_CheckWinner(char[,] board);

        void Ex02_CheckWalkableTile(int[,] map, int targetX, int targetY);

        void Ex03_SpawnChestsInCorners(int columns, int rows, GameObject chestPrefab);

        #endregion

        #endregion
    }
}
