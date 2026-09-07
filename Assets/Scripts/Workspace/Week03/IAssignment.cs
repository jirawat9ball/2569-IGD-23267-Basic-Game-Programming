using UnityEngine;

namespace Week03
{
    /// <summary>
    /// Week 03 - Array, For Loop, While Loop, Instantiate, Translate
    /// signature + รูปแบบ output อ้างอิงจาก Instruction-th.md
    /// </summary>
    public interface IAssignment
    {
        #region Lecture

        void As01_IronManSuit();

        void As02_SpiderManAndBatMan();

        void As03_RandomItemDrop(GameObject[] items);

        void As04_ForLoopBasic();

        void As05_ForLoopN(int n);

        void As06_ForLoopWithArray(string[] suiteNames);

        void As07_WhileLoopBasic();

        #endregion

        #region Homework

        #region Level 1: Simple

        void Lv01_AttackTarget(int[] enemyHP, int damage, int target);

        void Lv02_MultiplicationTable(int n);

        void Lv03_WhileLoopN(int n);

        void Lv04_WhileLoopStep(string[] suiteNames);

        void Lv05_WhileLoopSum(int n);

        #endregion

        #region Level 2: Moderate

        void Ex01_HealTarget(int[] enemyHP, int heal, int target);

        void Ex02_RandomDialogue(string[] npc1Dialogues);

        void Ex03_InstantiateEnemies(GameObject Enemy, int[] HpEnemy);

        void Ex04_MoveToTarget(Transform positionToMove, float speed);

        #endregion
        #endregion
    }
}
