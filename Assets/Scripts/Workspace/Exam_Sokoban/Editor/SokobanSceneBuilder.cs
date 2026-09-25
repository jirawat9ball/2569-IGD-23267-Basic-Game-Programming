using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Exam_Sokoban.EditorTools
{
    public static class SokobanSceneBuilder
    {
        private const string ScenePath = "Assets/Scenes/Exam_Sokoban.unity";
        private const string PrefabFolder = "Assets/Prefabs/2D Array/";

        // ใช้สคริปต์เฉลยถ้ามีในโปรเจกต์ ถ้าไม่มี (แพ็กเกจนักศึกษา) จะใช้สคริปต์ของนักศึกษาแทน
        private const string TeacherTypeName = "Exam_Sokoban.Exam_Teacher_Sokoban, Workspace";

        [MenuItem("Tools/Build Sokoban Exam Scene")]
        public static void Build()
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;

            GameObject[] floors = LoadPrefabs("floor", "floor 1", "floor 2", "floor 3", "floor 4", "floor 5", "floor 6", "floor 7");
            GameObject[] walls = LoadPrefabs("Wall", "Wall 1", "Wall 2", "Wall 3", "Wall 4", "Wall 5", "Wall 6", "Wall 7", "Wall 8");
            GameObject player = LoadPrefab("Player");
            GameObject box = LoadPrefab("Chest");
            GameObject target = LoadPrefab("floor");

            if (floors == null || walls == null || player == null || box == null || target == null)
            {
                EditorUtility.DisplayDialog("Build Sokoban Exam Scene",
                    "หา prefab ใน " + PrefabFolder + " ไม่ครบ ดูรายละเอียดใน Console", "OK");
                return;
            }

            System.Type examType = System.Type.GetType(TeacherTypeName) ?? typeof(Exam_Student_Sokoban);

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            var root = new GameObject("Sokoban");
            Component exam = root.AddComponent(examType);
            SetField(exam, "floorTiles", floors);
            SetField(exam, "wallTiles", walls);
            SetField(exam, "playerPrefab", player);
            SetField(exam, "boxPrefab", box);
            SetField(exam, "targetPrefab", target);
            EditorUtility.SetDirty(exam);

            string[] map = (string[])examType.GetField("map").GetValue(exam);
            CreateCamera(map.Length, map[0].Length);

            Directory.CreateDirectory(Path.GetDirectoryName(ScenePath));
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene, ScenePath);
            AssetDatabase.Refresh();

            Debug.Log("สร้างซีน " + ScenePath + " แล้ว (ใช้สคริปต์ " + examType.Name + ") — กด Play แล้วเดินด้วยลูกศร/WASD");
        }

        private static void CreateCamera(int rows, int columns)
        {
            var camGo = new GameObject("Main Camera");
            camGo.tag = "MainCamera";
            camGo.transform.position = new Vector3((columns - 1) / 2f, (rows - 1) / 2f, -10f);

            Camera cam = camGo.AddComponent<Camera>();
            cam.orthographic = true;
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.12f, 0.12f, 0.15f);

            float fitHeight = rows / 2f + 0.5f;
            float fitWidth = (columns / 2f + 0.5f) / (16f / 9f);
            cam.orthographicSize = Mathf.Max(fitHeight, fitWidth);

            camGo.AddComponent<AudioListener>();
        }

        private static GameObject LoadPrefab(string name)
        {
            string path = PrefabFolder + name + ".prefab";
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) Debug.LogError("หา prefab ไม่เจอ: " + path);
            return prefab;
        }

        private static GameObject[] LoadPrefabs(params string[] names)
        {
            var result = new GameObject[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                result[i] = LoadPrefab(names[i]);
                if (result[i] == null) return null;
            }
            return result;
        }

        private static void SetField(Component target, string fieldName, object value)
        {
            FieldInfo field = target.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);
            if (field == null)
            {
                Debug.LogError("สคริปต์ " + target.GetType().Name + " ไม่มีตัวแปร public " + fieldName);
                return;
            }
            field.SetValue(target, value);
        }
    }
}
