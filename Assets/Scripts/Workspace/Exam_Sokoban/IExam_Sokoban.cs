using UnityEngine;

namespace Exam_Sokoban
{
    public interface IExam_Sokoban
    {
        // ข้อ 1
        void BuildLevel(string[] map);

        // ข้อ 2 + 3 + 4
        bool TryMove(Vector2Int direction);

        // ข้อ 3
        bool IsSolved();
    }
}
