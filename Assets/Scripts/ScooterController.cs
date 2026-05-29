using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ScooterController : MonoBehaviour
{
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

    // Variabel state untuk Oli
    private bool sedangTerpeleset = false;
    private float rotasiTerpeleset = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Kalau lagi terpeleset, input keyboard belok diabaikan
        if (!sedangTerpeleset)
        {
            moveInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");
        }
        else
        {
            // Tetap bisa gas maju dikit tapi gak bisa dikendalikan arahnya
            moveInput = Input.GetAxis("Vertical");
            turnInput = 0f;
        }

        UpdateVisuals();
    }

    void FixedUpdate()
    {
        Vector3 arahMaju = transform.forward * moveInput * moveSpeed;
        rb.linearVelocity = new Vector3(arahMaju.x, rb.linearVelocity.y, arahMaju.z);

        if (sedangTerpeleset)
        {
            // Maksa motor berputar melintir sendiri di sumbu Y
            Quaternion putaranLiar = Quaternion.Euler(0f, rotasiTerpeleset * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * putaranLiar);
        }
        else if (Mathf.Abs(moveInput) > 0.1f)
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

    // Deteksi Trigger Nabrak Oli
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Oli") && !sedangTerpeleset)
        {
            StartCoroutine(ProsesTerpeleset());
        }
    }

    IEnumerator ProsesTerpeleset()
    {
        sedangTerpeleset = true;
        // Pilih arah muter acak, ke kanan atau ke kiri biar realistis
        rotasiTerpeleset = Random.Range(150f, 300f) * (Random.value > 0.5f ? 1f : -1f);

        Debug.Log("Waduh, Vario lu kepleset oli kotor!");

        yield return new WaitForSeconds(1.5f); // Durasi efek licin (1.5 detik)

        sedangTerpeleset = false;
    }
}