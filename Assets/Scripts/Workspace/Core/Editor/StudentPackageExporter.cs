using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Workspace.Core.Editor
{
    /// <summary>
    /// เครื่องมือช่วย Export ไฟล์การบ้าน/สื่อการสอนสำหรับนักเรียนเป็น .unitypackage
    /// ครอบคลุมทุกสัปดาห์ (Week 01 - Week 07) พร้อมระบบตัดโฟลเดอร์เฉลยอาจารย์ (Teacher) ออกโดยอัตโนมัติ
    /// </summary>
    public class StudentPackageExporter : EditorWindow
    {
        public class WeekExportConfig
        {
            public int weekNumber;
            public string weekId;
            public string title;
            public string topic;
            public bool isSelected = true;
            public string scriptFolder;
            public string[] scenePaths;
            public string[] prefabFolders;

            public string PackageFileName => !string.IsNullOrEmpty(topic)
                ? $"{weekId}_{topic}_Student.unitypackage"
                : $"{weekId}_Student.unitypackage";

            public WeekExportConfig(int number, string id, string title, string topic, string scriptFolder, string[] scenePaths, string[] prefabFolders = null)
            {
                this.weekNumber = number;
                this.weekId = id;
                this.title = title;
                this.topic = topic;
                this.scriptFolder = scriptFolder;
                this.scenePaths = scenePaths ?? Array.Empty<string>();
                this.prefabFolders = prefabFolders ?? Array.Empty<string>();
            }
        }

        public static readonly List<WeekExportConfig> WeekConfigs = new List<WeekExportConfig>()
        {
            new WeekExportConfig(
                1, "Week01", "Week 01: Value & Variables", "Value_and_Variables",
                "Assets/Scripts/Workspace/Week01",
                new[] { "Assets/Scenes/Week01_Value.unity" },
                new[] { "Assets/Prefabs/Value" }
            ),
            new WeekExportConfig(
                2, "Week02", "Week 02: If-Else Condition", "If_Else_Condition",
                "Assets/Scripts/Workspace/Week02",
                new[] { "Assets/Scenes/Week02_If.unity" },
                null
            ),
            new WeekExportConfig(
                3, "Week03", "Week 03: Array 1D", "Array_1D",
                "Assets/Scripts/Workspace/Week03",
                new[] { "Assets/Scenes/Week03_Array.unity" },
                new[] { "Assets/Prefabs/Array" }
            ),
            new WeekExportConfig(
                4, "Week04", "Week 04: Array 2D & TicTacToe", "Array_2D_and_TicTacToe",
                "Assets/Scripts/Workspace/Week04",
                new[] { "Assets/Scenes/Week04_2D_Array.unity", "Assets/Scenes/Week04_XO.unity" },
                new[] { "Assets/Prefabs/2D Array" }
            ),
            new WeekExportConfig(
                5, "Week05", "Week 05: Method & Grid Map", "Method_and_Grid_Map",
                "Assets/Scripts/Workspace/Week05",
                new[] { "Assets/Scenes/Week05_Method.unity" },
                new[] { "Assets/Prefabs/Method" }
            ),
            new WeekExportConfig(
                6, "Week06", "Week 06: Class & Grid Game", "Class_and_Grid_Game",
                "Assets/Scripts/Workspace/Week06",
                new[] { "Assets/Scenes/Week06_Class.unity" },
                new[] { "Assets/Prefabs/Class" }
            ),
            new WeekExportConfig(
                7, "Week07", "Week 07: Object-Oriented Programming (OOP)", "OOP_and_Grid_Game",
                "Assets/Scripts/Workspace/Week07",
                new[] { "Assets/Scenes/Week07_OOP.unity" },
                new[] { "Assets/Prefabs/OOP" }
            ),
        };

        private string exportDirectory = "Exports";
        private bool includeCore = true;
        private bool includeComponents = true;
        private bool includeSprites = true;
        private bool includeTextMeshPro = true;
        private bool includeSettings = true;
        private bool includeDependencies = true;
        private bool openFolderAfterExport = true;
        private Vector2 scrollPos;

        [MenuItem("Assignment/Export Student Packages...")]
        [MenuItem("Tools/Assignment Exporter/Open Exporter Window")]
        public static void OpenWindow()
        {
            var window = GetWindow<StudentPackageExporter>("Student Exporter");
            window.minSize = new Vector2(550, 680);
            window.Show();
        }

        #region Quick Export Menu Items

        [MenuItem("Assignment/Quick Export/Week 01 - Value & Variables")]
        public static void QuickExportWeek01() => ExportWeekByNumber(1);

        [MenuItem("Assignment/Quick Export/Week 02 - If-Else Condition")]
        public static void QuickExportWeek02() => ExportWeekByNumber(2);

        [MenuItem("Assignment/Quick Export/Week 03 - Array 1D")]
        public static void QuickExportWeek03() => ExportWeekByNumber(3);

        [MenuItem("Assignment/Quick Export/Week 04 - Array 2D & TicTacToe")]
        public static void QuickExportWeek04() => ExportWeekByNumber(4);

        [MenuItem("Assignment/Quick Export/Week 05 - Method & Grid Map")]
        public static void QuickExportWeek05() => ExportWeekByNumber(5);

        [MenuItem("Assignment/Quick Export/Week 06 - Class & Grid Game")]
        public static void QuickExportWeek06() => ExportWeekByNumber(6);

        [MenuItem("Assignment/Quick Export/Week 07 - OOP & Grid Game")]
        public static void QuickExportWeek07() => ExportWeekByNumber(7);

        [MenuItem("Assignment/Quick Export/Export All Weeks (Separate Packages)")]
        public static void QuickExportAllWeeksSeparate()
        {
            ExportWeeks(WeekConfigs, "Exports", true, true, true, true, true, true, true);
        }

        [MenuItem("Assignment/Quick Export/Export All Weeks (Single Combined Package)")]
        public static void QuickExportAllWeeksCombined()
        {
            ExportCombinedWeeks(WeekConfigs, "Exports", "All_Weeks_Student.unitypackage", true, true, true, true, true, true, true);
        }

        private static void ExportWeekByNumber(int weekNumber)
        {
            var config = WeekConfigs.FirstOrDefault(w => w.weekNumber == weekNumber);
            if (config != null)
            {
                ExportSingleWeek(config, "Exports", true, true, true, true, true, true, true);
            }
        }

        #endregion

        private void OnEnable()
        {
            exportDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Exports").Replace('\\', '/');
        }

        private void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            DrawHeader();
            EditorGUILayout.Space(8);

            DrawOutputDirectorySection();
            EditorGUILayout.Space(8);

            DrawOptionsSection();
            EditorGUILayout.Space(8);

            DrawWeeksSelectionSection();
            EditorGUILayout.Space(12);

            DrawActionButtons();
            EditorGUILayout.Space(10);

            EditorGUILayout.EndScrollView();
        }

        private void DrawHeader()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("📦 ตัวส่งออกไฟล์การบ้านสำหรับนักเรียน (Student Package Exporter)", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(
                "สร้างไฟล์ .unitypackage แยกตามสัปดาห์ (Week 01 - Week 07) หรือรวมทุกสัปดาห์เป็นไฟล์เดียว\n" +
                "🔒 ปลอดภัยสูงสุด: ระบบจะตัดโฟลเดอร์เฉลยอาจารย์ (Teacher) ออกโดยอัตโนมัติ เพื่อป้องกันคำตอบรั่วไหล",
                EditorStyles.wordWrappedLabel
            );
            EditorGUILayout.EndVertical();
        }

        private void DrawOutputDirectorySection()
        {
            EditorGUILayout.LabelField("📁 โฟลเดอร์ปลายทาง (Export Output)", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            exportDirectory = EditorGUILayout.TextField(exportDirectory);
            if (GUILayout.Button("เลือก...", GUILayout.Width(70)))
            {
                string chosen = EditorUtility.OpenFolderPanel("เลือกโฟลเดอร์สำหรับบันทึกไฟล์ Export", exportDirectory, "");
                if (!string.IsNullOrEmpty(chosen))
                {
                    exportDirectory = chosen;
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawOptionsSection()
        {
            EditorGUILayout.LabelField("⚙️ ตัวเลือกไฟล์ส่วนกลางที่ต้องแนบไปด้วย (Shared Assets)", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            includeCore = EditorGUILayout.ToggleLeft("รวม Core Scripts (SimpleDebugConsole, IAssignment, Submitter, Workspace.asmdef)", includeCore);
            includeComponents = EditorGUILayout.ToggleLeft("รวม Components Scripts (Assets/Scripts/Components)", includeComponents);
            includeSprites = EditorGUILayout.ToggleLeft("รวมโฟลเดอร์รูปภาพ (Assets/Sprite)", includeSprites);
            includeTextMeshPro = EditorGUILayout.ToggleLeft("รวมโฟลเดอร์ TextMesh Pro (Assets/TextMesh Pro)", includeTextMeshPro);
            includeSettings = EditorGUILayout.ToggleLeft("รวม Fonts และ Project Settings (Assets/Settings)", includeSettings);
            includeDependencies = EditorGUILayout.ToggleLeft("ให้ Unity ค้นหา Dependency ที่เกี่ยวข้องอัตโนมัติ", includeDependencies);
            openFolderAfterExport = EditorGUILayout.ToggleLeft("เปิดโฟลเดอร์ปลายทางเมื่อ Export เสร็จ", openFolderAfterExport);

            EditorGUILayout.EndVertical();
        }

        private void DrawWeeksSelectionSection()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("📚 เลือกสัปดาห์ที่ต้องการ Export", EditorStyles.boldLabel);
            if (GUILayout.Button("เลือกทั้งหมด", GUILayout.Width(90)))
            {
                foreach (var w in WeekConfigs) w.isSelected = true;
            }
            if (GUILayout.Button("ยกเลิกทั้งหมด", GUILayout.Width(90)))
            {
                foreach (var w in WeekConfigs) w.isSelected = false;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            foreach (var week in WeekConfigs)
            {
                EditorGUILayout.BeginHorizontal();
                week.isSelected = EditorGUILayout.ToggleLeft($"[{week.weekId}] {week.title}", week.isSelected);

                if (GUILayout.Button($"Export {week.weekId}", GUILayout.Width(110)))
                {
                    ExportSingleWeek(week, exportDirectory, includeCore, includeComponents, includeSprites, includeTextMeshPro, includeSettings, includeDependencies, openFolderAfterExport);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.LabelField($"    ↳ ไฟล์: {week.PackageFileName}", EditorStyles.miniLabel);
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawActionButtons()
        {
            GUIStyle bigButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 12,
                fixedHeight = 36
            };

            var selectedWeeks = WeekConfigs.Where(w => w.isSelected).ToList();

            EditorGUILayout.LabelField("🚀 การส่งออกข้อมูลแบบกลุ่ม (Batch Export)", EditorStyles.boldLabel);

            // Button 1: Separate packages
            GUI.backgroundColor = new Color(0.25f, 0.7f, 1f);
            if (GUILayout.Button($"🚀 Export สัปดาห์ที่เลือก ({selectedWeeks.Count} สัปดาห์) เป็น .unitypackage แยกแต่ละไฟล์", bigButtonStyle))
            {
                if (selectedWeeks.Count == 0)
                {
                    EditorUtility.DisplayDialog("แจ้งเตือน", "กรุณาเลือกอย่างน้อย 1 สัปดาห์", "ตกลง");
                    return;
                }
                ExportWeeks(selectedWeeks, exportDirectory, includeCore, includeComponents, includeSprites, includeTextMeshPro, includeSettings, includeDependencies, openFolderAfterExport);
            }

            // Button 2: Combined single package
            GUI.backgroundColor = new Color(0.95f, 0.65f, 0.25f);
            if (GUILayout.Button($"📦 Export สัปดาห์ที่เลือก รวมเป็น 1 ไฟล์เดียว (All-in-One Package)", bigButtonStyle))
            {
                if (selectedWeeks.Count == 0)
                {
                    EditorUtility.DisplayDialog("แจ้งเตือน", "กรุณาเลือกอย่างน้อย 1 สัปดาห์", "ตกลง");
                    return;
                }
                string combinedFileName = selectedWeeks.Count == WeekConfigs.Count ? "All_Weeks_Student.unitypackage" : $"Selected_{selectedWeeks.Count}Weeks_Student.unitypackage";
                ExportCombinedWeeks(selectedWeeks, exportDirectory, combinedFileName, includeCore, includeComponents, includeSprites, includeTextMeshPro, includeSettings, includeDependencies, openFolderAfterExport);
            }

            EditorGUILayout.Space(6);
            EditorGUILayout.LabelField("⚡ ส่งออกรายสัปดาห์แบบด่วน (1-Click Quick Export):", EditorStyles.boldLabel);

            GUI.backgroundColor = Color.white;
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < WeekConfigs.Count; i++)
            {
                var w = WeekConfigs[i];
                if (GUILayout.Button(w.weekId, GUILayout.Height(28)))
                {
                    ExportSingleWeek(w, exportDirectory, includeCore, includeComponents, includeSprites, includeTextMeshPro, includeSettings, includeDependencies, openFolderAfterExport);
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(6);
            GUI.backgroundColor = Color.white;
            if (GUILayout.Button("📂 เปิดโฟลเดอร์ Exports ปลายทาง", GUILayout.Height(30)))
            {
                EnsureDirectory(exportDirectory);
                EditorUtility.RevealInFinder(exportDirectory);
            }
        }

        #region Export Implementations

        public static void ExportSingleWeek(
            WeekExportConfig week,
            string targetDir,
            bool withCore,
            bool withComponents,
            bool withSprites,
            bool withTmp,
            bool withSettings,
            bool withDependencies,
            bool revealWhenDone)
        {
            ExportWeeks(
                new List<WeekExportConfig> { week },
                targetDir,
                withCore,
                withComponents,
                withSprites,
                withTmp,
                withSettings,
                withDependencies,
                revealWhenDone
            );
        }

        public static void ExportWeeks(
            List<WeekExportConfig> weeks,
            string targetDir,
            bool withCore,
            bool withComponents,
            bool withSprites,
            bool withTmp,
            bool withSettings,
            bool withDependencies,
            bool revealWhenDone)
        {
            EnsureDirectory(targetDir);

            int total = weeks.Count;
            int successCount = 0;
            string lastExportedFile = null;

            try
            {
                for (int i = 0; i < total; i++)
                {
                    var week = weeks[i];
                    float progress = (float)i / total;
                    EditorUtility.DisplayProgressBar("Exporting Student Packages", $"Exporting {week.weekId}: {week.title}...", progress);

                    string fileName = week.PackageFileName;
                    string destinationPath = Path.Combine(targetDir, fileName).Replace('\\', '/');

                    var assetList = CollectAssetsForWeeks(new[] { week }, withCore, withComponents, withSprites, withTmp, withSettings);

                    if (assetList.Count == 0)
                    {
                        Debug.LogWarning($"⚠️ ไม่พบ Asset สำหรับ {week.weekId} ข้ามการ Export");
                        continue;
                    }

                    // Strict exclusion filter: never include Teacher files or directories
                    var safeAssets = assetList
                        .Where(path => !IsTeacherAsset(path))
                        .Distinct()
                        .ToArray();

                    ExportPackageOptions options = ExportPackageOptions.Recurse;
                    if (withDependencies)
                    {
                        options |= ExportPackageOptions.IncludeDependencies;
                    }

                    AssetDatabase.ExportPackage(safeAssets, destinationPath, options);

                    Debug.Log($"<color=green>✅ Export สำเร็จ:</color> {fileName} ({safeAssets.Length} รายการ) -> {destinationPath}");
                    successCount++;
                    lastExportedFile = destinationPath;
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            if (successCount > 0)
            {
                EditorUtility.DisplayDialog(
                    "Export สำเร็จ",
                    $"สร้างไฟล์สำหรับนักเรียนสำเร็จทั้งหมด {successCount} แพ็กเกจ!\n" +
                    $"ตำแหน่ง: {targetDir}\n\n" +
                    $"🔒 ตรวจสอบแล้ว: ตัดโฟลเดอร์เฉลยอาจารย์ (Teacher) ออกทั้งหมดเรียบร้อย",
                    "ตกลง"
                );

                if (revealWhenDone && !string.IsNullOrEmpty(lastExportedFile) && File.Exists(lastExportedFile))
                {
                    EditorUtility.RevealInFinder(lastExportedFile);
                }
            }
            else
            {
                EditorUtility.DisplayDialog("แจ้งเตือน", "ไม่มีไฟล์ที่ถูก Export", "ตกลง");
            }
        }

        public static void ExportCombinedWeeks(
            List<WeekExportConfig> weeks,
            string targetDir,
            string fileName,
            bool withCore,
            bool withComponents,
            bool withSprites,
            bool withTmp,
            bool withSettings,
            bool withDependencies,
            bool revealWhenDone)
        {
            EnsureDirectory(targetDir);

            try
            {
                EditorUtility.DisplayProgressBar("Exporting Combined Package", "กำลังรวบรวมไฟล์สำหรับทุกสัปดาห์...", 0.5f);

                string destinationPath = Path.Combine(targetDir, fileName).Replace('\\', '/');
                var assetList = CollectAssetsForWeeks(weeks, withCore, withComponents, withSprites, withTmp, withSettings);

                var safeAssets = assetList
                    .Where(path => !IsTeacherAsset(path))
                    .Distinct()
                    .ToArray();

                if (safeAssets.Length == 0)
                {
                    EditorUtility.DisplayDialog("แจ้งเตือน", "ไม่พบ Asset ที่จะ Export", "ตกลง");
                    return;
                }

                ExportPackageOptions options = ExportPackageOptions.Recurse;
                if (withDependencies)
                {
                    options |= ExportPackageOptions.IncludeDependencies;
                }

                AssetDatabase.ExportPackage(safeAssets, destinationPath, options);
                Debug.Log($"<color=green>✅ Export แพ็กเกจรวมสำเร็จ:</color> {fileName} ({safeAssets.Length} รายการ) -> {destinationPath}");

                EditorUtility.DisplayDialog(
                    "Export รวมทุกสัปดาห์สำเร็จ",
                    $"สร้างแพ็กเกจรวม {weeks.Count} สัปดาห์เรียบร้อยแล้ว!\n" +
                    $"ไฟล์: {fileName}\n" +
                    $"ตำแหน่ง: {destinationPath}\n\n" +
                    $"🔒 ตัดโฟลเดอร์เฉลยอาจารย์ (Teacher) ออกเรียบร้อย",
                    "ตกลง"
                );

                if (revealWhenDone && File.Exists(destinationPath))
                {
                    EditorUtility.RevealInFinder(destinationPath);
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private static List<string> CollectAssetsForWeeks(
            IEnumerable<WeekExportConfig> weeks,
            bool withCore,
            bool withComponents,
            bool withSprites,
            bool withTmp,
            bool withSettings)
        {
            var assets = new List<string>();

            foreach (var week in weeks)
            {
                // 1. Week specific scripts
                if (!string.IsNullOrEmpty(week.scriptFolder) && AssetExists(week.scriptFolder))
                {
                    assets.Add(week.scriptFolder);
                }

                // 2. Week specific scenes
                if (week.scenePaths != null)
                {
                    foreach (var scene in week.scenePaths)
                    {
                        if (AssetExists(scene))
                        {
                            assets.Add(scene);
                        }
                    }
                }

                // 3. Week specific prefabs
                if (week.prefabFolders != null)
                {
                    foreach (var prefab in week.prefabFolders)
                    {
                        if (AssetExists(prefab))
                        {
                            assets.Add(prefab);
                        }
                    }
                }
            }

            // 4. Shared Core Assets
            if (withCore)
            {
                if (AssetExists("Assets/Scripts/Workspace/Core"))
                {
                    assets.Add("Assets/Scripts/Workspace/Core");
                }
                if (AssetExists("Assets/Scripts/Workspace/Workspace.asmdef"))
                {
                    assets.Add("Assets/Scripts/Workspace/Workspace.asmdef");
                }
            }

            // 5. Shared Components
            if (withComponents && AssetExists("Assets/Scripts/Components"))
            {
                assets.Add("Assets/Scripts/Components");
            }

            // 6. Shared Sprites
            if (withSprites && AssetExists("Assets/Sprite"))
            {
                assets.Add("Assets/Sprite");
            }

            // 7. TextMesh Pro
            if (withTmp && AssetExists("Assets/TextMesh Pro"))
            {
                assets.Add("Assets/TextMesh Pro");
            }

            // 8. Settings & Fonts
            if (withSettings && AssetExists("Assets/Settings"))
            {
                assets.Add("Assets/Settings");
            }

            return assets;
        }

        private static bool IsTeacherAsset(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            string normalized = path.Replace('\\', '/');
            return normalized.Contains("/Teacher/") || normalized.EndsWith("/Teacher");
        }

        private static bool AssetExists(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            return AssetDatabase.IsValidFolder(path) || !string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path));
        }

        private static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion
    }
}
