using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week08
{
    public class Assignment_Student_Week08 : MonoBehaviour, IAssignment
    {
        void Start()
        {
            Level1_StatusAndLogicDemo();
            Level2_InventoryAndGridDemo();
            Level3_MethodAndClassDemo();
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
            status.TakeDamage(20);
            status.IsAlive();
            status.CalculateDamage(30, 10);
            SafeDestroy(go);
        }

        public void Level2_InventoryAndGridDemo()
        {
            var go = new GameObject("InventoryScannerDemo");
            var scanner = go.AddComponent<Ex02.InventoryAndGridScanner>();
            var mapGen = FindAnyObjectByType<MapGenerator>();
            int[,] map = (mapGen != null && mapGen.mapGrid != null) ? mapGen.mapGrid : new int[,] {
                { 0, 1, 0, 3, 0 },
                { 2, 0, 0, 0, 0 },
                { 0, 0, 2, 0, 0 },
                { 0, 1, 0, 3, 0 },
                { 0, 0, 0, 0, 9 }
            };
            scanner.FindItemPosition(map, 3);
            scanner.CountItems(map, 3);
            scanner.ScanThreats(map, new Vector2Int(0, 0), 2);
            scanner.FindExitPosition(map, 9);
            SafeDestroy(go);
        }

        public void Level3_MethodAndClassDemo()
        {
            var go = new GameObject("SpawnerDemo");
            var spawner = go.AddComponent<Ex03.WeaponSpawner>();
            var dummyPrefab = new GameObject("Wooden Stick");
            var spawned = spawner.SpawnWeapon(dummyPrefab, new Vector3(1, 0, 0));

            var weapon = go.AddComponent<Ex03.Weapon>();
            weapon.Use();

            SafeDestroy(dummyPrefab);
            SafeDestroy(spawned);
            SafeDestroy(go);
        }
    }
}
