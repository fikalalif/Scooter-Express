using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ScooterController : MonoBehaviour
{
    [Header("Kontrol Layar HP")]
    public FixedJoystick analogLayar; // Wadah buat masukin analognya

    [Header("Pengaturan Fisika Utama")]
    public float moveSpeed = 15f;
    public float turnSpeed = 90f;

    [Header("Komponen Visual")]
    public Transform stang;
    public Transform banDepan;
    public Transform banBelakang;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Mengecek apakah analog sudah dipasang di Unity
        if (analogLayar != null)
        {
            // Membaca pergerakan jempol dari layar HP
            moveInput = analogLayar.Vertical;
            turnInput = analogLayar.Horizontal;
        }
        else
        {
            // Fallback: Tetap bisa pakai WASD kalau dimainin di PC
            moveInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");
        }

        UpdateVisuals();
    }

    void FixedUpdate()
    {
        // Fisika motor majunya tetep sama persis
        Vector3 arahMaju = transform.forward * moveInput * moveSpeed;
        rb.linearVelocity = new Vector3(arahMaju.x, rb.linearVelocity.y, arahMaju.z);

        if (Mathf.Abs(moveInput) > 0.1f)
        {
            float sudutBelok = turnInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion belok = Quaternion.Euler(0f, sudutBelok, 0f);
            rb.MoveRotation(rb.rotation * belok);
        }
    }

    void UpdateVisuals()
    {
        if (stang != null) stang.localRotation = Quaternion.Euler(0, turnInput * 30f, 0);
        if (banDepan != null && banBelakang != null)
        {
            float kecepatanPutar = (moveInput * moveSpeed) * 10f;
            banDepan.Rotate(Vector3.right * kecepatanPutar * Time.deltaTime, Space.Self);
            banBelakang.Rotate(Vector3.right * kecepatanPutar * Time.deltaTime, Space.Self);
        }
    }
}