using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Workspace.Core.Editor
{
    /// <summary>
    /// เครื่องมือช่วยจัด Format Encoding ของไฟล์ C# Script ให้เป็น UTF-8 with BOM
    /// เพื่อให้สามารถอ่านและแสดงผลภาษาไทย (คอมเมนต์ และ ข้อความ String) ได้ถูกต้อง
    /// ทั้งใน Visual Studio, Visual Studio Code และ Unity Editor / Console
    /// </summary>
    public static class ScriptEncodingFixer
    {
        private static readonly Encoding Utf8WithBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);

        [MenuItem("Tools/Encoding/Convert All Scripts to UTF-8 with BOM")]
        [MenuItem("Assignment/Fix Script Encoding (UTF-8 with BOM)")]
        public static void ConvertAllScriptsMenu()
        {
            string projectPath = Application.dataPath;
            string[] csFiles = Directory.GetFiles(projectPath, "*.cs", SearchOption.AllDirectories);

            int total = csFiles.Length;
            int convertedCount = 0;
            var convertedFiles = new List<string>();

            try
            {
                for (int i = 0; i < total; i++)
                {
                    string file = csFiles[i];
                    string relativePath = "Assets" + file.Substring(projectPath.Length).Replace('\\', '/');

                    EditorUtility.DisplayProgressBar(
                        "Converting Scripts Encoding",
                        $"Processing ({i + 1}/{total}): {Path.GetFileName(file)}",
                        (float)i / total
                    );

                    if (EnsureUtf8WithBom(file))
                    {
                        convertedCount++;
                        convertedFiles.Add(relativePath);
                    }
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            if (convertedCount > 0)
            {
                AssetDatabase.Refresh();
                Debug.Log($"<color=#4CAF50><b>[ScriptEncodingFixer]</b></color> แปลง Encoding เป็น UTF-8 with BOM สำเร็จทั้งหมด <b>{convertedCount}</b> ไฟล์:\n- " +
                          string.Join("\n- ", convertedFiles));

                EditorUtility.DisplayDialog(
                    "Fix Script Encoding",
                    $"แปลง Encoding เป็น UTF-8 with BOM เรียบร้อยแล้ว {convertedCount} ไฟล์!\n\nตอนนี้ Visual Studio และ Unity Editor จะสามารถอ่านคอมเมนต์ภาษาไทยได้ถูกต้อง",
                    "ตกลง"
                );
            }
            else
            {
                Debug.Log("<color=#2196F3><b>[ScriptEncodingFixer]</b></color> ไฟล์สคริปต์ C# ทั้งหมดเป็น UTF-8 with BOM อยู่แล้ว ไม่พบไฟล์ที่ต้องแก้ไข");
                EditorUtility.DisplayDialog(
                    "Fix Script Encoding",
                    "ไฟล์ C# Scripts ทั้งหมดเป็น UTF-8 with BOM อยู่แล้ว\nภาษาไทยสามารถแสดงผลได้สมบูรณ์",
                    "ตกลง"
                );
            }
        }

        [MenuItem("Assets/Encoding/Convert Selected to UTF-8 with BOM", false, 20)]
        public static void ConvertSelectedMenu()
        {
            var selectedObjects = Selection.objects;
            if (selectedObjects == null || selectedObjects.Length == 0)
                return;

            int convertedCount = 0;
            var convertedFiles = new List<string>();

            foreach (var obj in selectedObjects)
            {
                string assetPath = AssetDatabase.GetAssetPath(obj);
                if (string.IsNullOrEmpty(assetPath))
                    continue;

                if (Directory.Exists(assetPath))
                {
                    string[] files = Directory.GetFiles(assetPath, "*.cs", SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        if (EnsureUtf8WithBom(file))
                        {
                            convertedCount++;
                            convertedFiles.Add(file.Replace('\\', '/'));
                        }
                    }
                }
                else if (assetPath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                {
                    if (EnsureUtf8WithBom(assetPath))
                    {
                        convertedCount++;
                        convertedFiles.Add(assetPath);
                    }
                }
            }

            if (convertedCount > 0)
            {
                AssetDatabase.Refresh();
                Debug.Log($"<color=#4CAF50><b>[ScriptEncodingFixer]</b></color> แปลงไฟล์ที่เลือกเป็น UTF-8 with BOM สำเร็จ {convertedCount} ไฟล์:\n- " +
                          string.Join("\n- ", convertedFiles));
            }
            else
            {
                Debug.Log("<color=#2196F3><b>[ScriptEncodingFixer]</b></color> ไฟล์ที่เลือกเป็น UTF-8 with BOM อยู่แล้ว");
            }
        }

        [MenuItem("Assets/Encoding/Convert Selected to UTF-8 with BOM", true)]
        public static bool ValidateConvertSelectedMenu()
        {
            var selectedObjects = Selection.objects;
            if (selectedObjects == null || selectedObjects.Length == 0)
                return false;

            foreach (var obj in selectedObjects)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                if (Directory.Exists(path) || path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// ตรวจสอบและแปลงไฟล์ให้อยู่ในรูปแบบ UTF-8 with BOM
        /// คืนค่า true หากมีการแก้ไข/บันทึกไฟล์ใหม่
        /// </summary>
        public static bool EnsureUtf8WithBom(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            try
            {
                byte[] bytes = File.ReadAllBytes(filePath);

                // ตรวจสอบ BOM ของ UTF-8 (EF BB BF)
                if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
                {
                    return false; // มี UTF-8 BOM อยู่แล้ว ไม่ต้องทำอะไร
                }

                // พยายามอ่านเนื้อหา โดยลอง UTF-8 ก่อน
                string content;
                var utf8Strict = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
                try
                {
                    content = utf8Strict.GetString(bytes);
                }
                catch (DecoderFallbackException)
                {
                    // หากไม่ใช่ UTF-8 ที่สมบูรณ์ ให้ลองอ่านแบบ Windows-874 (Thai ANSI)
                    try
                    {
                        var thaiEncoding = Encoding.GetEncoding(874);
                        content = thaiEncoding.GetString(bytes);
                    }
                    catch
                    {
                        content = Encoding.Default.GetString(bytes);
                    }
                }

                // บันทึกกลับลงไฟล์ด้วย UTF-8 with BOM
                File.WriteAllText(filePath, content, Utf8WithBom);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[ScriptEncodingFixer] ไม่สามารถแปลงไฟล์ {filePath}: {ex.Message}");
                return false;
            }
        }
    }

    /// <summary>
    /// ตรวจสอบอัตโนมัติเมื่อมีการบันทึกไฟล์สคริปต์ใน Unity Editor
    /// </summary>
    public class ScriptEncodingSaveProcessor : UnityEditor.AssetModificationProcessor
    {
        public static string[] OnWillSaveAssets(string[] paths)
        {
            if (paths == null) return paths;

            foreach (string path in paths)
            {
                if (path != null && path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                {
                    ScriptEncodingFixer.EnsureUtf8WithBom(path);
                }
            }

            return paths;
        }
    }

    /// <summary>
    /// ตรวจสอบอัตโนมัติเมื่อมีการสร้างหรือ Import ไฟล์สคริปต์ใหม่เข้ามาใน Unity
    /// </summary>
    public class ScriptEncodingPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            if (importedAssets == null) return;

            foreach (string path in importedAssets)
            {
                if (path != null && path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                {
                    ScriptEncodingFixer.EnsureUtf8WithBom(path);
                }
            }
        }
    }
}
