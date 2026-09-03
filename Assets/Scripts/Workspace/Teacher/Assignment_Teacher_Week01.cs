using UnityEngine;

public class Assignment_Teacher_Week01 : MonoBehaviour
{
    // ==========================================
    // เฉลยโจทย์ระดับ 1: แนะนำตัวละคร
    // ==========================================
    string characterName = "Hero";
    int level = 5;
    float moveSpeed = 4.5f;
    bool isAlive = true;

    // ==========================================
    // เฉลยโจทย์ระดับ 2: ปรับแต่งผ่าน Inspector
    // ==========================================
    public int maxHealth = 100;
    // หรือสามารถใช้ [SerializeField] private int maxHealth = 100; ก็ได้เช่นกัน
    
    private int currentHealth;

    // ==========================================
    // เฉลยโจทย์ระดับ 3: การคำนวณเบื้องต้น
    // ==========================================
    string NAME = "Somchai";
    string LASTNAME = "Unity";
    int HP = 100;
    int DAMAGE = 15;
    float SPEED = 10.5f;
    float TIME = 2.0f;
    float DISTANCE = 50.0f;

    // ==========================================
    // เฉลยโจทย์ระดับ 4: ตัวแปรจับเวลา
    // ==========================================
    float timer = 0f;

    // ==========================================
    // เฉลยโจทย์ระดับ 5: ตัวแปรของ Unity
    // ==========================================
    public Vector3 StartPosition;
    public Color colorPlayer;
    public MeshRenderer playerMesh;

    // ==========================================
    // เฉลยโจทย์ระดับ 6: การอ้างอิง Component และ GameObject
    // ==========================================
    public GameObject Heart;
    public Transform SpawnHeart;

    public FirstPersonMovement C1;
    public FirstPersonInterface C2;

    void Start()
    {
        // --- ส่วนที่ 1: แสดงผลชื่อตัวละคร ---
        Debug.Log("ตัวละครชื่อ: " + characterName + ", เลเวล: " + level + ", ความเร็ว: " + moveSpeed + ", สถานะมีชีวิต: " + isAlive);

        // --- ส่วนที่ 2: ตั้งค่าพลังชีวิตเริ่มต้น ---
        currentHealth = maxHealth;
        Debug.Log("เริ่มเกม! พลังชีวิตปัจจุบันคือ: " + currentHealth);

        // --- ส่วนที่ 3: การคำนวณเบื้องต้น ---
        Debug.Log(NAME + LASTNAME);
        Debug.Log(HP - DAMAGE);
        Debug.Log(SPEED * TIME);
        Debug.Log(DISTANCE / TIME);
        Debug.Log(HP % DAMAGE);

        // --- ส่วนที่ 5: ใช้งานตัวแปรของ Unity ---
        transform.position = StartPosition;
        C2.SetColorCapsule(colorPlayer, playerMesh);

        // --- ส่วนที่ 6: ใช้งาน Component ---
        if (C2 != null)
        {
            C2.SetUIName(characterName);
            C2.SetUIHp(currentHealth, Heart, SpawnHeart);
        }
    }

    void Update()
    {
        // --- ส่วนที่ 4: ระบบจับเวลา ---
        timer += Time.deltaTime; // หรือ timer = timer + Time.deltaTime;

        // เช็คว่าเวลาผ่านไป 3 วินาทีหรือยัง
        if (timer >= 3f)
        {
            Debug.Log("เวลาผ่านไป 3 วินาทีแล้ว!");
            
            // รีเซ็ตเวลาเพื่อให้นับใหม่
            timer = 0f; 
        }

        // --- ส่วนที่ 6: ใช้งาน Component ---
        if (C1 != null)
        {
            C1.Move(moveSpeed, isAlive);
        }
    }
}
