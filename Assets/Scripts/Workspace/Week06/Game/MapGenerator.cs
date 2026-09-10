using System.Collections.Generic;
using UnityEngine;

namespace Week06.Game
{
    /// <summary>
    /// แผนที่ของเกม เก็บว่าช่องไหนมีอะไรอยู่ และเก็บตัวอ้างอิงของศัตรู/ยา/ดาบ
    /// ไฟล์นี้ไม่ต้องแก้
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        public const int MapSize = 10;

        public int empty = 0;
        public int demonWall = 1;
        public int enemy = 2;
        public int sword = 3;
        public int potion = 4;
        public int exit = 5;

        public int[,] mapData;
        public Enemy[,] enemies;
        public ItemPotion[,] potions;
        public ItemSword[,] swords;
        public Character player;

        private readonly List<GameObject> spawned = new List<GameObject>();

        public int GetMapData(int x, int y)
        {
            if (x < 0 || x >= MapSize || y < 0 || y >= MapSize)
            {
                return demonWall;
            }
            return mapData[x, y];
        }

        /// <summary>
        /// สร้างแผนที่ 10x10 ตามฉากตัวอย่างในโจทย์
        /// Player (0,0) energy 100 attack 10 · Potion (2,2) heal 20 · Sword (3,2) bonus 10 · Enemy (3,3) energy 60 attack 5
        /// </summary>
        public static MapGenerator CreateDemoMap()
        {
            var go = new GameObject("MapGenerator");
            var map = go.AddComponent<MapGenerator>();
            map.Build();
            return map;
        }

        public void Build()
        {
            mapData = new int[MapSize, MapSize];
            enemies = new Enemy[MapSize, MapSize];
            potions = new ItemPotion[MapSize, MapSize];
            swords = new ItemSword[MapSize, MapSize];

            player = Spawn<Player>("Player", 0, 0);
            player.energy = 100;
            player.attackPoint = 10;

            var thePotion = Spawn<ItemPotion>("Potion1", 2, 2);
            thePotion.healPoint = 20;
            potions[2, 2] = thePotion;
            mapData[2, 2] = potion;

            var theSword = Spawn<ItemSword>("Sword1", 3, 2);
            theSword.attackBonus = 10;
            swords[3, 2] = theSword;
            mapData[3, 2] = sword;

            var theEnemy = Spawn<Enemy>("Enemy1", 3, 3);
            theEnemy.energy = 60;
            theEnemy.attackPoint = 5;
            enemies[3, 3] = theEnemy;
            mapData[3, 3] = enemy;
        }

        private T Spawn<T>(string objectName, int x, int y) where T : Identity
        {
            var go = new GameObject(objectName);
            go.transform.position = new Vector3(x, y, 0);

            var identity = go.AddComponent<T>();
            identity.Name = objectName;
            identity.positionX = x;
            identity.positionY = y;
            identity.mapGenerator = this;

            spawned.Add(go);
            return identity;
        }

        /// <summary>เก็บกวาดวัตถุทั้งหมดที่สร้างไว้ (ใช้ตอนจบชุดทดสอบ)</summary>
        public void ClearMap()
        {
            foreach (var go in spawned)
            {
                if (go == null) continue;
                if (Application.isPlaying) Destroy(go);
                else DestroyImmediate(go);
            }
            spawned.Clear();

            if (Application.isPlaying) Destroy(gameObject);
            else DestroyImmediate(gameObject);
        }
    }
}
