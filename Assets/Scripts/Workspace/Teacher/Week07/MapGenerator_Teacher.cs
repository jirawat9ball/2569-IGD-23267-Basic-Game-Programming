using System.Collections.Generic;
using UnityEngine;

namespace Week07.Teacher.Game
{
    public class MapGenerator : Week07.Game.MapGenerator
    {
        public new Player player;
        public new Enemy[,] enemies;
        public new ItemPotion[,] potions;
        public new ItemSword[,] swords;
        public new Wall[,] walls;
        public new Chest[,] chests;

        private readonly List<GameObject> teacherSpawned = new List<GameObject>();

        public new static MapGenerator CreateDemoMap()
        {
            var go = new GameObject("TeacherMapGenerator");
            var map = go.AddComponent<MapGenerator>();
            map.Build();
            return map;
        }

        public new void Build()
        {
            Row = MapSize;
            Col = MapSize;
            mapData = new int[MapSize, MapSize];
            enemies = new Enemy[MapSize, MapSize];
            potions = new ItemPotion[MapSize, MapSize];
            swords = new ItemSword[MapSize, MapSize];
            walls = new Wall[MapSize, MapSize];
            chests = new Chest[MapSize, MapSize];

            player = SpawnTeacher<Player>("Player", 0, 0);
            player.energy = 100;
            player.attackPoint = 10;
            base.player = null; // ensure teacher player is distinct

            var thePotion = SpawnTeacher<ItemPotion>("Potion1", 2, 2);
            thePotion.healPoint = 20;
            potions[2, 2] = thePotion;
            mapData[2, 2] = potion;

            var theSword = SpawnTeacher<ItemSword>("Sword1", 3, 2);
            theSword.attackBonus = 10;
            swords[3, 2] = theSword;
            mapData[3, 2] = sword;

            var theEnemy = SpawnTeacher<Enemy>("Enemy1", 3, 3);
            theEnemy.energy = 60;
            theEnemy.attackPoint = 5;
            enemies[3, 3] = theEnemy;
            mapData[3, 3] = enemy;

            var theWall = SpawnTeacher<Wall>("Wall1", 1, 3);
            theWall.durability = 3;
            walls[1, 3] = theWall;
            mapData[1, 3] = demonWall;

            var theChest = SpawnTeacher<Chest>("Chest1", 1, 1);
            chests[1, 1] = theChest;
            mapData[1, 1] = chest;
        }

        public T SpawnTeacher<T>(string objectName, int x, int y) where T : Week07.Game.Identity
        {
            var go = new GameObject(objectName);
            go.transform.position = new Vector3(x, y, 0);

            var identity = go.AddComponent<T>();
            identity.name = objectName;
            identity.Name = objectName;
            identity.positionX = x;
            identity.positionY = y;
            identity.mapGenerator = this;

            teacherSpawned.Add(go);
            return identity;
        }

        public new void TrackSpawned(GameObject go)
        {
            if (go != null)
            {
                teacherSpawned.Add(go);
            }
        }

        public new void ClearMap()
        {
            foreach (var go in teacherSpawned)
            {
                if (go == null) continue;
                if (Application.isPlaying) Destroy(go);
                else DestroyImmediate(go);
            }
            teacherSpawned.Clear();

            if (Application.isPlaying) Destroy(gameObject);
            else DestroyImmediate(gameObject);
        }
    }
}
