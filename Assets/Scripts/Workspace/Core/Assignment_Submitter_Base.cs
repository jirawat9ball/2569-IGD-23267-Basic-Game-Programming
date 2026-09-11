using UnityEngine;
using UnityEngine.Networking;

namespace Workspace.Core
{
    /// <summary>
    /// คลาสแม่สำหรับการส่งการบ้านของทุกสัปดาห์ (Base Assignment Submitter)
    /// จัดการข้อมูลนักศึกษา, กลุ่มเรียน และการเชื่อมต่อไปยัง Google Sheet Web App
    /// </summary>
    public abstract class Assignment_Submitter_Base : MonoBehaviour
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

        [Tooltip("เลือกกลุ่มเรียน / Section")]
        public StudentSection section = StudentSection.Sec_001;

        [Tooltip("หากเลือก Other ให้ระบุกลุ่มเรียนที่นี่")]
        public string customSection = "";

        [HideInInspector]
        public string weekName = "Week01";

        // ดึง Web App URL ส่วนกลางจาก SubmissionConfig
        public string googleSheetWebAppURL => SubmissionConfig.GoogleSheetWebAppURL;

        /// <summary>
        /// แปลง Section เป็นข้อความที่ใช้แสดงผลและส่งเข้า Google Sheet
        /// </summary>
        public string GetSectionString()
        {
            return (section == StudentSection.Other) ? customSection : section.ToString().Replace("_", " ");
        }

        /// <summary>
        /// ตรวจสอบความถูกต้องของข้อมูลนักศึกษาก่อนส่งงาน
        /// </summary>
        public virtual bool ValidateStudentInfo()
        {
            if (string.IsNullOrEmpty(studentID) || studentID == "รหัสนักศึกษา"
                || string.IsNullOrEmpty(studentName) || studentName == "ชื่อ-นามสกุล")
            {
                Debug.LogError("❌ กรุณากรอก รหัสนักศึกษา และ ชื่อ-นามสกุล ให้เรียบร้อยก่อนส่งงาน");
                return false;
            }

            if (section == StudentSection.Other && string.IsNullOrEmpty(customSection))
            {
                Debug.LogError("❌ คุณเลือกกลุ่มเรียนเป็น 'Other' กรุณาระบุกลุ่มเรียนในช่อง Custom Section");
                return false;
            }

            return true;
        }

        /// <summary>
        /// ถูกเรียกจาก Editor Script หลังจาก NUnit Test Runner รันเสร็จและได้คะแนนแล้ว
        /// </summary>
        public virtual void SendScoreToGoogleSheet(string score, string maxScore)
        {
            Debug.Log($"⏳ กำลังส่งงาน {weekName}... (คะแนนที่ได้ {score}/{maxScore})");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.CallbackFunction updateCallback = null;

            WWWForm form = new WWWForm();
            form.AddField("studentId", studentID);
            form.AddField("studentName", studentName);
            form.AddField("section", GetSectionString());
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
                        Debug.LogError($"❌ การส่งข้อมูลล้มเหลว ({weekName}): " + www.error);
                    }
                    else
                    {
                        Debug.Log($"<color=green>✅ ส่งงานเรียบร้อยแล้ว!</color> คะแนน {score}/{maxScore} ถูกบันทึกลง Google Sheet แท็บ {weekName}");
                    }
                    www.Dispose();
                    UnityEditor.EditorApplication.update -= updateCallback;
                }
            };
            UnityEditor.EditorApplication.update += updateCallback;
#endif
        }
    }
}
