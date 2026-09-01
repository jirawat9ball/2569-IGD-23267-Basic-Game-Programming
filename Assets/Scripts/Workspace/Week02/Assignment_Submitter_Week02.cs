using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Assignment_Submitter_Week02 : MonoBehaviour
{
    [Header("ข้อมูลนักศึกษา (ห้ามเว้นว่าง)")]
    public string studentID = "รหัสนักศึกษา";
    public string studentName = "ชื่อ-นามสกุล";
    
    public enum StudentSection
    {
        Sec_001,
        Sec_101,
        Sec_002,
        Sec_102,
        Sec_003,
        Sec_103,
        Other
    }
    [Tooltip("เลือกกลุ่มเรียน")]
    public StudentSection section = StudentSection.Sec_001;
    
    [Tooltip("หากเลือก Other ให้ระบุกลุ่มเรียนที่นี่")]
    public string customSection = "";

    [HideInInspector]
    public string weekName = "Week02";
    
    // ซ่อนลิงก์ Web App URL ด้วยการเข้ารหัส Base64 (เหมือน Week 01)
    private string googleSheetWebAppURL
    {
        get
        {
            string _p1 = "aHR0cHM6Ly9zY3JpcHQuZ29vZ2";
            string _p2 = "xlLmNvbS9tYWNyb3Mvcy9BS2Z5";
            string _p3 = "Y2J5VUtWN3BoQUpjbTA0NHA5Z0";
            string _p4 = "w1MFgxVFFyOFhndlFhS3owQVVi";
            string _p5 = "UHl6ejRmamxrc1JTS0E0Z2w5TF";
            string _p6 = "JzcWZhV3hwTlIvZXhlYw==";
            byte[] _d = System.Convert.FromBase64String(_p1 + _p2 + _p3 + _p4 + _p5 + _p6);
            return System.Text.Encoding.UTF8.GetString(_d);
        }
    }

    /// <summary>
    /// ถูกเรียกจาก Editor Script หลังจาก NUnit Test Runner รันเสร็จและได้คะแนนแล้ว
    /// </summary>
    public void SendScoreToGoogleSheet(string score, string maxScore)
    {
        Debug.Log($"⏳ กำลังส่งงาน Week 02... (คะแนนที่ได้ {score}/{maxScore})");
        
        // ส่ง HTTP Request ผ่าน UnityEditor.EditorApplication.update เพื่อให้ทำงานใน EditMode ได้
#if UNITY_EDITOR
        UnityEditor.EditorApplication.CallbackFunction updateCallback = null;
        
        WWWForm form = new WWWForm();
        form.AddField("studentId", studentID);
        form.AddField("studentName", studentName);
        
        string sectionString = (section == StudentSection.Other) ? customSection : section.ToString().Replace("_", " ");
        form.AddField("section", sectionString);
        form.AddField("score", score);
        form.AddField("maxScore", maxScore);
        form.AddField("week", weekName);

        UnityWebRequest www = UnityWebRequest.Post(googleSheetWebAppURL, form);
        www.SendWebRequest();

        updateCallback = () =>
        {
            if (www.isDone)
            {
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("❌ การส่งข้อมูลล้มเหลว: " + www.error);
                }
                else
                {
                    Debug.Log("<color=green>✅ ส่งงานเรียบร้อยแล้ว!</color> ข้อมูลถูกบันทึกลง Google Sheet แท็บ Week02");
                }
                www.Dispose();
                UnityEditor.EditorApplication.update -= updateCallback;
            }
        };
        UnityEditor.EditorApplication.update += updateCallback;
#endif
    }
}
