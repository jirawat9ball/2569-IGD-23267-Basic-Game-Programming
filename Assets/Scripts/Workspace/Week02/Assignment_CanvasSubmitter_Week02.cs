using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

namespace Week02
{
    public class Assignment_CanvasSubmitter_Week02 : MonoBehaviour
    {
        [Header("UI References")]
        [Tooltip("ช่องกรอกรหัสนักศึกษา")]
        public TMP_InputField studentIdInput;
        
        [Tooltip("ช่องกรอกชื่อ-นามสกุล")]
        public TMP_InputField studentNameInput;
        
        [Tooltip("Dropdown เลือกกลุ่มเรียน")]
        public TMP_Dropdown sectionDropdown;
        
        [Tooltip("ช่องกรอกกลุ่มเรียนอื่นๆ (กรณีเลือก Other)")]
        public TMP_InputField customSectionInput;
        
        [Tooltip("ปุ่มกดส่งงาน")]
        public Button submitButton;
        
        [Tooltip("ข้อความแสดงสถานะ (เช่น กำลังส่ง, ส่งสำเร็จ, หรือแจ้งเตือนต่างๆ)")]
        public TextMeshProUGUI statusText;

        private string weekName = "Week02";

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

        private void Start()
        {
            if (submitButton != null)
            {
                submitButton.onClick.AddListener(OnSubmitClicked);
            }

            if (statusText != null)
            {
                statusText.text = "พร้อมส่งงาน (กรุณารัน Test Runner ให้ผ่านก่อนกดส่ง)";
                statusText.color = Color.white;
            }
        }

        public void OnSubmitClicked()
        {
            // 1. ตรวจสอบข้อมูลที่กรอก
            if (string.IsNullOrEmpty(studentIdInput.text) || string.IsNullOrEmpty(studentNameInput.text))
            {
                UpdateStatus("❌ กรุณากรอกรหัสและชื่อให้ครบถ้วน!", Color.red);
                return;
            }

            string sectionStr = sectionDropdown.options[sectionDropdown.value].text;
            if (sectionStr == "Other" || sectionStr.Contains("อื่น"))
            {
                if (customSectionInput == null || string.IsNullOrEmpty(customSectionInput.text))
                {
                    UpdateStatus("❌ กรุณาระบุ Section ในช่อง Custom Section!", Color.red);
                    return;
                }
                sectionStr = customSectionInput.text;
            }

            // 2. ไปดึงคะแนนจากไฟล์ที่ Test Runner แอบเซฟไว้ (ฉลาดสุดๆ)
            string resultFilePath = "Library/PlayModeTestResults.xml";
            if (!File.Exists(resultFilePath))
            {
                UpdateStatus("❌ ไม่พบผลคะแนน! กรุณาเปิดหน้าต่าง Test Runner แล้วกดรัน PlayMode ให้เสร็จก่อนกดส่งงาน", Color.red);
                return;
            }

            string xmlContent = File.ReadAllText(resultFilePath);
            
            // หาบรรทัด <test-run ... passed="145" ...>
            int passCount = ExtractXmlAttribute(xmlContent, "passed=\"", "\"");
            int totalCount = ExtractXmlAttribute(xmlContent, "total=\"", "\"");

            if (totalCount == 0 || passCount == -1)
            {
                UpdateStatus("❌ อ่านผลคะแนนไม่สำเร็จ รบกวนรัน Test Runner ใหม่อีกรอบครับ", Color.red);
                return;
            }

            // 3. ยิงขึ้น Google Sheet
            UpdateStatus($"⏳ กำลังส่งงาน... (คะแนน: {passCount}/{totalCount})", Color.yellow);
            submitButton.interactable = false;
            
            StartCoroutine(SendScoreCoroutine(studentIdInput.text, studentNameInput.text, sectionStr, passCount.ToString(), totalCount.ToString()));
        }

        private IEnumerator SendScoreCoroutine(string id, string name, string section, string score, string maxScore)
        {
            WWWForm form = new WWWForm();
            form.AddField("studentId", id);
            form.AddField("studentName", name);
            form.AddField("section", section);
            form.AddField("score", score);
            form.AddField("maxScore", maxScore);
            form.AddField("week", weekName);

            using (UnityWebRequest www = UnityWebRequest.Post(googleSheetWebAppURL, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    UpdateStatus("❌ ส่งข้อมูลล้มเหลว: " + www.error, Color.red);
                    submitButton.interactable = true;
                }
                else
                {
                    UpdateStatus($"✅ ส่งงานสำเร็จ! ({name} ได้ {score}/{maxScore} คะแนน)", Color.green);
                }
            }
        }

        private void UpdateStatus(string message, Color color)
        {
            Debug.Log(message);
            if (statusText != null)
            {
                statusText.text = message;
                statusText.color = color;
            }
        }

        // ฟังก์ชันบ้านๆ สำหรับดึงตัวเลขออกจาก XML แบบไม่ต้องใช้ไลบรารีใหญ่
        private int ExtractXmlAttribute(string xml, string prefix, string suffix)
        {
            int startIndex = xml.IndexOf(prefix);
            if (startIndex == -1) return -1;
            
            startIndex += prefix.Length;
            int endIndex = xml.IndexOf(suffix, startIndex);
            if (endIndex == -1) return -1;
            
            string valueStr = xml.Substring(startIndex, endIndex - startIndex);
            if (int.TryParse(valueStr, out int value))
            {
                return value;
            }
            return -1;
        }
    }
}
