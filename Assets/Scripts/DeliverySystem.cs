using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    [Header("Referensi Visual")]
    public GameObject paketDiMotor;

    [Header("Sistem Titik Lokasi Acak (Terpisah)")]
    public GameObject zonaRestoran;
    public GameObject zonaRumah;

    // Kita pisah kolomnya di Inspector biar gak ketuker tema bangunannya
    public Transform[] daftarTitikRestoran;
    public Transform[] daftarTitikRumah;

    [Header("Status Kurir")]
    public bool sedangBawaPaket = false;

    void Start()
    {
        if (paketDiMotor != null) paketDiMotor.SetActive(false);

        // Awal game: Restoran HANYA boleh pindah ke titik khusus restoran
        PindahKeLokasiAcak(zonaRestoran, daftarTitikRestoran);
        zonaRestoran.SetActive(true);

        zonaRumah.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // 1. LOGIKA AMBIL PAKET DI RESTORAN
        if (other.CompareTag("Restaurant") && !sedangBawaPaket)
        {
            sedangBawaPaket = true;
            if (paketDiMotor != null) paketDiMotor.SetActive(true);

            zonaRestoran.SetActive(false);

            // Rumah HANYA boleh pindah ke titik khusus perumahan warga
            PindahKeLokasiAcak(zonaRumah, daftarTitikRumah);
            zonaRumah.SetActive(true);

            Debug.Log("Paket diambil dari Restoran! Antar ke Rumah Pelanggan!");
        }

        // 2. LOGIKA ANTAR PAKET DI RUMAH
        if (other.CompareTag("Rumah") && sedangBawaPaket)
        {
            sedangBawaPaket = false;
            if (paketDiMotor != null) paketDiMotor.SetActive(false);

            GameManager.instance.TambahUang(15000);
            GameManager.instance.waktuBermain += 60f;

            zonaRumah.SetActive(false);

            // Munculin restoran baru, tetep di area khusus restoran lagi
            PindahKeLokasiAcak(zonaRestoran, daftarTitikRestoran);
            zonaRestoran.SetActive(true);

            Debug.Log("Paket sukses diantar ke Rumah! Cari orderan restoran baru!");
        }
    }

    // Fungsi kita upgrade agar menerima parameter kumpulan array yang spesifik
    void PindahKeLokasiAcak(GameObject targetObjek, Transform[] kumpulanTitik)
    {
        if (kumpulanTitik.Length > 0)
        {
            int indeksAcak = Random.Range(0, kumpulanTitik.Length);
            targetObjek.transform.position = kumpulanTitik[indeksAcak].position;
        }
    }
}