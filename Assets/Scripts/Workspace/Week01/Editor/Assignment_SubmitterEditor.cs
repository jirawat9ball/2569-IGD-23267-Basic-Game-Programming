using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Assignment_Submitter))]
public class Assignment_SubmitterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // วาด Inspector เดิมตามปกติ (เช่น ให้กรอกข้อมูลนักศึกษา)
        DrawDefaultInspector();

        Assignment_Submitter submitter = (Assignment_Submitter)target;

        GUILayout.Space(20);

        // กำหนดสไตล์ของปุ่มให้ดูโดดเด่นและน่ากดมากขึ้น
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontStyle = FontStyle.Bold;
        buttonStyle.fontSize = 14;
        buttonStyle.fixedHeight = 40;
        
        // วาดปุ่มส่งงาน
        if (GUILayout.Button("🚀 ส่งงานเข้า Google Sheet", buttonStyle))
        {
            submitter.SubmitAssignment();
        }
    }
}
