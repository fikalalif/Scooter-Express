using UnityEngine;
using UnityEngine.SceneManagement; // Wajib dipanggil buat mindahin level

public class MainMenuManager : MonoBehaviour
{
    // Fungsi ini dipanggil pas tombol MULAI diklik
    public void TombolMulai()
    {
        // Pastikan nama ini sama persis dengan nama Scene map kota lu
        SceneManager.LoadScene("Level-1");
    }

    // Fungsi ini dipanggil pas tombol KELUAR diklik
    public void TombolKeluar()
    {
        Debug.Log("Keluar dari Game!");
        Application.Quit(); // Menutup aplikasi (hanya berefek saat sudah di-build jadi .apk/.exe)
    }
}