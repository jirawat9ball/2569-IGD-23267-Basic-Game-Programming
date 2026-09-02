using UnityEngine;

namespace Week03
{
    /// <summary>
    /// Week 03 - Array, For Loop, While Loop, Instantiate, Translate
    /// signature + รูปแบบ output อ้างอิงจาก Instruction-th.md
    /// </summary>
    public interface IAssignment
    {
        #region Array (ข้อ 1-6)

        void Ex01_IronManSuit();

        void Ex02_SpiderManAndBatMan();

        void Ex03_AttackTarget(int[] enemyHP, int damage, int target);

        void Ex04_RandomItemDrop(GameObject[] items);

        void Ex05_HealTarget(int[] enemyHP, int heal, int target);

        void Ex06_RandomDialogue(string[] npc1Dialogues);

        #endregion

        #region For Loop (ข้อ 7-10)

        void Ex07_ForLoopBasic();

        void Ex08_ForLoopN(int n);

        void Ex09_ForLoopStep(string[] suiteNames);

        void Ex10_MultiplicationTable(int n);

        #endregion

        #region While Loop (ข้อ 11-14)

        void Ex11_WhileLoopBasic();

        void Ex12_WhileLoopN(int n);

        void Ex13_WhileLoopStep(string[] suiteNames);

        void Ex14_WhileLoopSum(int n);

        #endregion

        #region Instantiate & Translate (ข้อ 15-16)

        void Ex15_InstantiateEnemies(GameObject Enemy, int[] HpEnemy);

        void Ex16_MoveToTarget(Transform positionToMove, float speed);

        #endregion
    }
}
