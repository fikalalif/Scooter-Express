using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    [Header("Referensi Visual")]
    public GameObject paketDiMotor; // Taruh objek PaketVisual ke sini nanti

    [Header("Status Kurir")]
    public bool sedangBawaPaket = false; // Status awal kurir kosong

    void Start()
    {
        // Memastikan di awal game paket di motor tersembunyi
        if (paketDiMotor != null)
        {
            paketDiMotor.SetActive(false);
        }
    }

    // Fungsi bawaan Unity untuk mendeteksi sensor Trigger Collider
    void OnTriggerEnter(Collider other)
    {
        // 1. LOGIKA AMBIL PAKET DI RESTORAN
        if (other.CompareTag("Restaurant") && !sedangBawaPaket)
        {
            sedangBawaPaket = true;

            if (paketDiMotor != null)
            {
                paketDiMotor.SetActive(true); // Paket muncul di belakang motor
            }

            Debug.Log("Paket berhasil diambil dari Restoran! Antar ke Rumah Tujuan.");
            // Nanti di sini kita bisa tambahin efek suara "ting!"
        }

        // 2. LOGIKA ANTAR PAKET DI RUMAH
        if (other.CompareTag("Rumah") && sedangBawaPaket)
        {
            sedangBawaPaket = false;

            if (paketDiMotor != null)
            {
                paketDiMotor.SetActive(false); // Paket hilang karena sudah diserahkan
            }

            Debug.Log("Paket sukses diantarkan! Anda mendapatkan tip uang.");
            // Nanti di sini tempat kita nambahin skor atau uang UAS lu
        }
    }
}