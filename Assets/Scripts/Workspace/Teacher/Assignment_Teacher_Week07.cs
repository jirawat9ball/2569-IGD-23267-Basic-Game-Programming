using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week07
{
    public class Assignment_Teacher_Week07 : MonoBehaviour, IAssignment
    {
        public void Ex01_InheritanceDemo()
        {
            new Teacher.Ex01.AS01_Inheritance().Start();
        }

        public void Ex02_AccessModifierDemo()
        {
            new Teacher.Ex02.AS02_AccessModifier().Start();
        }

        public void Ex03_VirtualOverrideDemo()
        {
            new Teacher.Ex03.AS03_VirtualOverride().Start();
        }

        public void Ex04_PlayerDemo()
        {
            Game.MapGenerator map = Game.MapGenerator.CreateDemoMap();
            Game.Player player = map.player as Game.Player;

            player.Move(Vector2.right);
            Debug.Log($"Player position: ({player.positionX}, {player.positionY})");
            Debug.Log($"Player energy: {player.energy}");

            map.ClearMap();
        }

        public void Ex05_BattleDemo()
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

        public void Ex06_PotionDemo()
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

        public void Ex07_SwordDemo()
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

        public void Ex08_WallDemo()
        {
            Game.MapGenerator map = Game.MapGenerator.CreateDemoMap();
            Game.Wall wall = map.walls[1, 3];

            wall.Hit();
            wall.Hit();
            wall.Hit();

            map.ClearMap();
        }

        public void Ex09_ChestDemo()
        {
            Game.MapGenerator map = Game.MapGenerator.CreateDemoMap();
            Game.Chest chest = map.chests[1, 1];

            chest.OpenChest();

            map.ClearMap();
        }
    }
}

