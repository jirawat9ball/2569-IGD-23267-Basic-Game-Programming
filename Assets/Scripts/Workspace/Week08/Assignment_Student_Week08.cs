using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08
{
    public class Assignment_Student_Week08 : MonoBehaviour, IAssignment
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
            var go = new GameObject("PlayerStatusDemo");
            var status = go.AddComponent<Ex01.CharacterStatus>();
            var type = status.GetType();
            type.GetMethod("TakeDamage")?.Invoke(status, new object[] { 20 });
            type.GetMethod("IsAlive")?.Invoke(status, null);
            type.GetMethod("CalculateDamage")?.Invoke(status, new object[] { 30, 10 });
            SafeDestroy(go);
        }

        public void Level2_MethodAndClassDemo()
        {
            var go = new GameObject("SpawnerDemo");
            var spawnerType = System.Type.GetType("Week08.Ex02.ArmorSpawner, Workspace")
                ?? System.Type.GetType("Week08.Ex03.ArmorSpawner, Workspace")
                ?? System.Type.GetType("Week08.ArmorSpawner, Workspace")
                ?? System.Type.GetType("ArmorSpawner, Workspace")
                ?? System.Type.GetType("Week08.Teacher.Ex02.ArmorSpawner, Workspace")
                ?? System.Type.GetType("Week08.Teacher.Ex03.ArmorSpawner, Workspace");

            if (spawnerType != null)
            {
                var spawner = go.AddComponent(spawnerType);
                var dummyPrefab = new GameObject("Iron Armor");
                var method = System.Array.Find(spawnerType.GetMethods(), m => (m.Name == "SpawnArmor" || m.Name == "SpawnWeapon") && m.GetParameters().Length == 2);
                var spawned = method?.Invoke(spawner, new object[] { dummyPrefab, new Vector3(1, 0, 0) }) as GameObject;

                SafeDestroy(dummyPrefab);
                SafeDestroy(spawned);
            }

            var armorType = System.Type.GetType("Week08.Ex02.Armor, Workspace") 
                ?? System.Type.GetType("Week08.Ex03.Armor, Workspace") 
                ?? System.Type.GetType("Week08.Armor, Workspace")
                ?? System.Type.GetType("Armor, Workspace")
                ?? System.Type.GetType("Week08.Teacher.Ex02.Armor, Workspace")
                ?? System.Type.GetType("Week08.Teacher.Ex03.Armor, Workspace");

            if (armorType != null)
            {
                var armor = go.AddComponent(armorType);
                var nameField = armorType.GetField("Name") ?? armorType.GetField("name");
                var defField = armorType.GetField("defenseBonus") ?? armorType.GetField("defense");
                Debug.Log($"Armor: {nameField?.GetValue(armor)}, Defense Bonus: {defField?.GetValue(armor)}");
            }

            SafeDestroy(go);
        }

        public void Level3_MapGeneratorDemo()
        {
            var go = new GameObject("MapGeneratorDemo");
            var mapGen = FindAnyObjectByType<MapGenerator>() ?? go.AddComponent<MapGenerator>();
            if (mapGen.mapGrid == null) mapGen.mapGrid = new int[5, 5];
            mapGen.PlaceExit();
            mapGen.PlaceArmor();
            mapGen.GenerateLWall();
            mapGen.ScanThreats(new Vector2Int(0, 0), 2);
            SafeDestroy(go);
        }
    }
}
