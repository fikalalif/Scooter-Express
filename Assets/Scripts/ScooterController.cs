using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ScooterController : MonoBehaviour
{
    [Header("Pengaturan Fisika Utama")]
    public float moveSpeed = 15f;
    public float turnSpeed = 90f;

    [Header("Komponen Visual (Tarik dari Vario lu)")]
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
        // 1. Ambil input keyboard di Update (Kaidah wajib Unity)
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        // 2. Mainkan visual ban & stang (Tidak mengganggu fisika)
        UpdateVisuals();
    }

    void FixedUpdate()
    {
        // 3. MAJU & GRAVITASI
        // Kita hitung arah depan (Z positif) dari wrapper Player_Motor yang sumbunya sempurna
        Vector3 arahMaju = transform.forward * moveInput * moveSpeed;

        // rb.velocity.y dipertahankan agar motor tetap jatuh ditarik gravitasi bumi ke plane
        rb.linearVelocity = new Vector3(arahMaju.x, rb.linearVelocity.y, arahMaju.z);

        // 4. BELOK (Hanya bisa belok kalau motor sedang digas maju/mundur)
        if (Mathf.Abs(moveInput) > 0.1f)
        {
            float sudutBelok = turnInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion belok = Quaternion.Euler(0f, sudutBelok, 0f);
            rb.MoveRotation(rb.rotation * belok);
        }
    }

    void UpdateVisuals()
    {
        if (stang != null)
        {
            stang.localRotation = Quaternion.Euler(0, turnInput * 30f, 0);
        }

        if (banDepan != null && banBelakang != null)
        {
            float kecepatanPutar = (moveInput * moveSpeed) * 10f;
            banDepan.Rotate(Vector3.right * kecepatanPutar * Time.deltaTime, Space.Self);
            banBelakang.Rotate(Vector3.right * kecepatanPutar * Time.deltaTime, Space.Self);
        }
    }
}