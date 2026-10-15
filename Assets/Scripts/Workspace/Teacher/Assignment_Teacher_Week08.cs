using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08
{
    public class Assignment_Teacher_Week08 : MonoBehaviour, IAssignment
    {
        void Start()
        {
            Level1_StatusAndLogicDemo();
            Level2_MethodAndClassDemo();
            Level3_MapGeneratorDemo();
        }

        private static void SafeDestroy(GameObject go)
        {
            if (go == null) return;
            if (Application.isPlaying)
                Destroy(go);
            else
                DestroyImmediate(go);
        }

        public void Level1_StatusAndLogicDemo()
        {
            var go = new GameObject("TeacherPlayerStatusDemo");
            var status = go.AddComponent<Teacher.Ex01.CharacterStatus>();
            status.TakeDamage(20);
            status.IsAlive();
            status.CalculateDamage(30, 10);
            SafeDestroy(go);
        }

        public void Level2_MethodAndClassDemo()
        {
            var go = new GameObject("TeacherSpawnerDemo");
            var spawner = go.AddComponent<Teacher.Ex02.ArmorSpawner>();
            var dummyPrefab = new GameObject("Iron Armor");
            var spawned = spawner.SpawnArmor(dummyPrefab, new Vector3(1, 0, 0));

            var armor = go.AddComponent<Teacher.Ex02.Armor>();
            Debug.Log($"Armor: {armor.Name}, Defense Bonus: {armor.defenseBonus}");

            SafeDestroy(dummyPrefab);
            SafeDestroy(spawned);
            SafeDestroy(go);
        }

        public void Level3_MapGeneratorDemo()
        {
            var go = new GameObject("TeacherMapGeneratorDemo");
            var mapGen = go.AddComponent<Teacher.Ex03.MapGenerator_Teacher>();
            if (mapGen.mapGrid == null) mapGen.mapGrid = new int[5, 5];
            mapGen.PlaceExit();
            mapGen.PlaceArmor();
            mapGen.GenerateLWall();
            mapGen.ScanThreats(new Vector2Int(0, 0), 2);
            SafeDestroy(go);
        }
    }
}
