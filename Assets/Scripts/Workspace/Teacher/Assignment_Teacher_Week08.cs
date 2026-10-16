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
            var go = new GameObject("TeacherArmorDemo");
            var armor = go.AddComponent<Teacher.Ex02.Armor>();
            Debug.Log($"Armor: {armor.Name}, Defense Bonus: {armor.defenseBonus}");

            SafeDestroy(go);
        }

        public void Level3_MapGeneratorDemo()
        {
            var existingMapGen = FindAnyObjectByType<Teacher.Ex03.MapGenerator_Teacher>() ?? FindAnyObjectByType<MapGenerator>() as Teacher.Ex03.MapGenerator_Teacher;
            if (existingMapGen != null)
            {
                // หากมี MapGenerator ในฉาก แผนที่จะถูกสร้างใน Start() เรียบร้อยแล้ว จึงทดสอบเฉพาะการสแกนหาภัยคุกคาม
                existingMapGen.ScanThreats(new Vector2Int(0, 0), 2);
                return;
            }

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
