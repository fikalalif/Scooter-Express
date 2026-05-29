using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    [Header("Referensi Visual")]
    public GameObject paketDiMotor;

    [Header("Status Kurir")]
    public bool sedangBawaPaket = false;

    void Start()
    {
        if (paketDiMotor != null)
        {
            paketDiMotor.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 1. LOGIKA AMBIL PAKET DI RESTORAN
        if (other.CompareTag("Restaurant") && !sedangBawaPaket)
        {
            sedangBawaPaket = true;

            if (paketDiMotor != null)
            {
                paketDiMotor.SetActive(true);
            }

            Debug.Log("Paket diambil!");
        }

        // 2. LOGIKA ANTAR PAKET DI RUMAH (ADA TAMBAHAN DI SINI)
        if (other.CompareTag("Rumah") && sedangBawaPaket)
        {
            sedangBawaPaket = false;

            if (paketDiMotor != null)
            {
                paketDiMotor.SetActive(false);
            }

            // --- HUBUNGKAN KE GAME MANAGER ---
            // Tambah ongkir Rp 15.000 tiap sukses nganter
            GameManager.instance.TambahUang(15000);

            // Kasih bonus waktu 20 detik biar pemain bisa lanjut cari orderan
            GameManager.instance.waktuBermain += 20f;

            Debug.Log("Paket sukses! Duit cair dan waktu nambah.");
        }
    }
}