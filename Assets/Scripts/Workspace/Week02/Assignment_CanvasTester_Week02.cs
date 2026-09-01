using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Week02
{
    public class Assignment_CanvasTester_Week02 : MonoBehaviour
    {
        [Header("อ้างอิงสคริปต์ของอาจารย์")]
        [Tooltip("ลาก GameObject ที่มีสคริปต์ Assignment_Student_Week02 มาใส่ช่องนี้")]
        public Assignment_Student_Week02 studentScript;

        [Header("โซน A: ระบบรักษาความปลอดภัย")]
        public Toggle isSixoClockToggle;
        public Button testAs01Button;
        public TMP_InputField stringPasswordInput;
        public Button testAs02Button;

        [Header("โซน B: ระบบตัวเลข")]
        public TMP_InputField comparisonNumberInput;
        public Button testAs03_04Button;

        [Header("โซน C: มินิเกมทายใจ")]
        public TMP_InputField guessingNumberInput;
        public TMP_InputField randomNumberInput;
        public Button testAs05Button;
        public Button testAs06Button;

        [Header("โซน D: ตรวจสอบสิทธิ์ VIP")]
        public TMP_InputField usernameInput;
        public TMP_InputField identityPasswordInput;
        public TMP_InputField ageInput;
        public Toggle isPaidToggle;
        public Button testAs07Button;

        private void Start()
        {
            if (studentScript == null)
            {
                Debug.LogError("❌ ลืมลาก Assignment_Student_Week02 มาใส่ช่อง studentScript หรือเปล่าครับ?");
                return;
            }

            // --- ผูกสายไฟดึงข้อมูลเมื่อ UI ขยับ ---
            
            if (isSixoClockToggle != null)
            {
                isSixoClockToggle.onValueChanged.AddListener((val) => studentScript.isSixoClock = val);
                studentScript.isSixoClock = isSixoClockToggle.isOn;
            }

            if (stringPasswordInput != null)
            {
                stringPasswordInput.onValueChanged.AddListener((val) => studentScript.stringPassword = val);
                studentScript.stringPassword = stringPasswordInput.text;
            }

            if (comparisonNumberInput != null)
            {
                comparisonNumberInput.onValueChanged.AddListener((val) => {
                    if (int.TryParse(val, out int num)) studentScript.comparisonNumber = num;
                });
                if (int.TryParse(comparisonNumberInput.text, out int initNum)) studentScript.comparisonNumber = initNum;
            }

            if (guessingNumberInput != null)
            {
                guessingNumberInput.onValueChanged.AddListener((val) => {
                    if (int.TryParse(val, out int num)) studentScript.guessingNumber = num;
                });
                if (int.TryParse(guessingNumberInput.text, out int initGuess)) studentScript.guessingNumber = initGuess;
            }

            if (randomNumberInput != null)
            {
                randomNumberInput.onValueChanged.AddListener((val) => {
                    if (int.TryParse(val, out int num)) studentScript.randomNumber = num;
                });
                if (int.TryParse(randomNumberInput.text, out int initRand)) studentScript.randomNumber = initRand;
            }

            if (usernameInput != null)
            {
                usernameInput.onValueChanged.AddListener((val) => studentScript.username = val);
                studentScript.username = usernameInput.text;
            }

            if (identityPasswordInput != null)
            {
                identityPasswordInput.onValueChanged.AddListener((val) => studentScript.identityPassword = val);
                studentScript.identityPassword = identityPasswordInput.text;
            }

            if (ageInput != null)
            {
                ageInput.onValueChanged.AddListener((val) => {
                    if (int.TryParse(val, out int num)) studentScript.age = num;
                });
                if (int.TryParse(ageInput.text, out int initAge)) studentScript.age = initAge;
            }

            if (isPaidToggle != null)
            {
                isPaidToggle.onValueChanged.AddListener((val) => studentScript.isPaid = val);
                studentScript.isPaid = isPaidToggle.isOn;
            }

            // --- ผูกปุ่มกดเทสต์ทีละข้อ (ฉลาดกว่าระบบดั้งเดิม) ---

            if (testAs01Button != null)
                testAs01Button.onClick.AddListener(() => {
                    ClearConsole();
                    studentScript.As01_SyntaxIf(studentScript.isSixoClock);
                });

            if (testAs02Button != null)
                testAs02Button.onClick.AddListener(() => {
                    ClearConsole();
                    studentScript.As02_StringComparisonExample(studentScript.stringPassword);
                });

            if (testAs03_04Button != null)
                testAs03_04Button.onClick.AddListener(() => {
                    ClearConsole();
                    studentScript.As03_NumberComparisonExample(studentScript.comparisonNumber);
                    studentScript.As04_AndOrOperatorExample(studentScript.comparisonNumber);
                });

            if (testAs05Button != null)
                testAs05Button.onClick.AddListener(() => {
                    ClearConsole();
                    studentScript.As05_GuessingNumberExample(studentScript.guessingNumber, studentScript.randomNumber);
                });

            if (testAs06Button != null)
                testAs06Button.onClick.AddListener(() => {
                    ClearConsole();
                    studentScript.As06_GuessingNumberMoreOrLessExample(studentScript.guessingNumber, studentScript.randomNumber);
                });

            if (testAs07Button != null)
                testAs07Button.onClick.AddListener(() => {
                    ClearConsole();
                    studentScript.As07_VerifyIdentityExample(studentScript.username, studentScript.identityPassword, studentScript.age, studentScript.isPaid);
                });
        }

        // ฟังก์ชันโกง: ใช้ลบข้อความใน Console ทิ้งก่อนรันเทสต์ใหม่ จะได้ไม่งง
        private void ClearConsole()
        {
#if UNITY_EDITOR
            var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
#endif
        }
    }
}
