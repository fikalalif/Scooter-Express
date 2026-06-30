using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Referensi UI Setting")]
    public GameObject panelSetting;
    public TextMeshProUGUI teksTombolSuara;

    // Status suara
    private bool suaraMenyala = true;

    void Start()
    {
        // Pastikan panel setting mati di awal
        if (panelSetting != null) panelSetting.SetActive(false);

        // (Opsional) Mengambil data memori HP apakah sebelumnya di-mute atau nggak
        suaraMenyala = PlayerPrefs.GetInt("SuaraMute", 0) == 0;
        UpdateVisualSuara();
    }

    // --- FUNGSI BAWAAN MENU SEBELUMNYA ---
    public void TombolMulai()
    {
        SceneManager.LoadScene("Level-1");
    }

    public void TombolKeluar()
    {
        Debug.Log("Keluar dari Game!");
        Application.Quit();
    }

    // --- FUNGSI BARU UNTUK SETTING ---
    public void BukaSetting()
    {
        if (panelSetting != null) panelSetting.SetActive(true);
    }

    public void TutupSetting()
    {
        if (panelSetting != null) panelSetting.SetActive(false);
    }

    public void ToggleSuara()
    {
        // Membalikkan status (Kalau On jadi Off, kalau Off jadi On)
        suaraMenyala = !suaraMenyala;

        // Simpan setingan ke memori perangkat
        PlayerPrefs.SetInt("SuaraMute", suaraMenyala ? 0 : 1);

        UpdateVisualSuara();
    }

    private void UpdateVisualSuara()
    {
        if (suaraMenyala)
        {
            AudioListener.volume = 1f; // Menyalakan semua suara di game
            if (teksTombolSuara != null) teksTombolSuara.text = "SUARA: ON";
        }
        else
        {
            AudioListener.volume = 0f; // Membisukan (mute) semua suara di game
            if (teksTombolSuara != null) teksTombolSuara.text = "SUARA: OFF";
        }
    }
}