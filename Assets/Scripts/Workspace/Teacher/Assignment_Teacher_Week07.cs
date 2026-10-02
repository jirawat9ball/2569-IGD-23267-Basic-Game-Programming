using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07
{
    public class Assignment_Teacher_Week07 : MonoBehaviour, IAssignment
    {
        public void Ex01_CarDemo()
        {
            // Guideline: (โค้ดของ Car ทำงานใน Start() และ Update() ของไฟล์ Ex01_Car.cs)
        }

        public void Ex02_DogDemo()
        {
            new Teacher.Ex02.AS02_ClassConstructor().Start();
        }

        public void Ex03_InheritanceDemo()
        {
            new Teacher.Ex03.AS03_Inheritance().Start();
        }

        public void Ex04_AccessModifierDemo()
        {
            new Teacher.Ex04.AS04_AccessModifier().Start();
        }

        public void Ex05_VirtualOverrideDemo()
        {
            new Teacher.Ex05.AS05_VirtualOverride().Start();
        }

        public void Ex06_BattleDemo()
        {
            Game.MapGenerator map = Game.MapGenerator.CreateDemoMap();
            Game.Character player = map.player;
            Game.Enemy enemy = map.enemies[3, 3];

            player.Move(Vector2.up);      // (0,1) ช่องว่าง -> energy 99
            player.Move(Vector2.up);      // (0,2) ช่องว่าง -> energy 98
            player.Move(Vector2.right);   // (1,2) ช่องว่าง -> energy 97
            player.Move(Vector2.right);   // (2,2) ยา       -> energy 117
            player.Move(Vector2.right);   // (3,2) ดาบ      -> attack 20

            Debug.Log($"Player energy after picking up potion: {player.energy}");
            Debug.Log($"Player attack point after picking up sword: {player.attackPoint}");

            Debug.Log("first attack ...");
            player.Move(Vector2.up);
            Debug.Log($"Player energy after attack: {player.energy}");
            Debug.Log($"Enemy energy after attack: {enemy.energy}");

            Debug.Log("second attack ...");
            player.Move(Vector2.up);
            Debug.Log($"Player energy after attack: {player.energy}");
            Debug.Log($"Enemy energy after attack: {enemy.energy}");

            Debug.Log("thrid attack ...");
            player.Move(Vector2.up);
            Debug.Log($"Player energy after attack: {player.energy}");
            Debug.Log($"Enemy energy after attack: {enemy.energy}");

            map.ClearMap();
        }

        public void Ex07_PotionDemo()
        {
            Game.MapGenerator map = Game.MapGenerator.CreateDemoMap();
            Game.Character player = map.player;

            player.Move(Vector2.up);
            player.Move(Vector2.up);
            player.Move(Vector2.right);
            player.Move(Vector2.right);   // เหยียบยา

            Debug.Log($"Player energy after picking up potion: {player.energy}");

            map.ClearMap();
        }

        public void Ex08_SwordDemo()
        {
            Game.MapGenerator map = Game.MapGenerator.CreateDemoMap();
            Game.Character player = map.player;

            player.Move(Vector2.up);
            player.Move(Vector2.up);
            player.Move(Vector2.right);
            player.Move(Vector2.right);   // เหยียบยา
            player.Move(Vector2.right);   // เหยียบดาบ

            Debug.Log($"Player attack point after picking up sword: {player.attackPoint}");

            map.ClearMap();
        }
    }
}
