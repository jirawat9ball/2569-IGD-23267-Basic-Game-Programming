namespace Workspace.Core
{
    /// <summary>
    /// การตั้งค่าส่วนกลางสำหรับการส่งคะแนนการบ้าน (Centralized Submission Configuration)
    /// </summary>
    public static class SubmissionConfig
    {
        // ซ่อนลิงก์ Web App URL ด้วยการเข้ารหัส Base64 และแยกส่วน (Obfuscation) เพื่อป้องกันการค้นหาเจอในโค้ดดิบ
        public static string GoogleSheetWebAppURL
        {
            get
            {
                string _p1 = "aHR0cHM6Ly9zY3JpcHQuZ29vZ2";
                string _p2 = "xlLmNvbS9tYWNyb3Mvcy9BS2Z5";
                string _p3 = "Y2J4OVRsUHBqMXVRU2JDbEZG";
                string _p4 = "d3RuS29aMy1DYWY5OE5BZnpH";
                string _p5 = "c3lMY2ViMGkzamoySVNMZmNK";
                string _p6 = "YjlLdlZ1eHVVM1c0R0MvZXhlYw==";
                byte[] _d = System.Convert.FromBase64String(_p1 + _p2 + _p3 + _p4 + _p5 + _p6);
                return System.Text.Encoding.UTF8.GetString(_d);
            }
        }
    }
}
