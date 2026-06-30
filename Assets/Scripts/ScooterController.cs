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

    public bool sedangSlip = false;

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

    // Timpa FixedUpdate lu yang lama dengan ini
    void FixedUpdate()
    {
        if (sedangSlip)
        {
            // 1. Bikin setir ngebanting ke kiri atau kanan secara acak dan brutal
            float setirNgaco = Random.Range(-300f, 300f);
            Quaternion belokAcak = Quaternion.Euler(0f, setirNgaco * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * belokAcak);

            // 2. Motor tetep meluncur, tapi kecepatannya ngedrop jadi 40% (0.4f)
            // Arahnya bakal ngacak banget karena ngikutin bodinya yang lagi muter liar
            Vector3 arahMeluncur = transform.forward * (moveSpeed * 0.4f);
            rb.linearVelocity = new Vector3(arahMeluncur.x, rb.linearVelocity.y, arahMeluncur.z);
        }
        else
        {
            // Kontrol normal analog
            Vector3 arahMaju = transform.forward * moveInput * moveSpeed;
            rb.linearVelocity = new Vector3(arahMaju.x, rb.linearVelocity.y, arahMaju.z);

            if (Mathf.Abs(moveInput) > 0.1f)
            {
                float sudutBelok = turnInput * turnSpeed * Time.fixedDeltaTime;
                Quaternion belok = Quaternion.Euler(0f, sudutBelok, 0f);
                rb.MoveRotation(rb.rotation * belok);
            }
        }
    }

    public void TerkenaOli(float durasi)
    {
        sedangSlip = true;
        Invoke("SelesaiSlip", durasi); // Panggil fungsi SelesaiSlip setelah durasi habis
    }

    void SelesaiSlip()
    {
        sedangSlip = false;
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