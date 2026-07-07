using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Referensi UI HUD")]
    public TextMeshProUGUI teksTimer;
    public TextMeshProUGUI teksUang;
    public GameObject panelGameOver;
    public GameObject panelMenang;

    [Header("Referensi UI Baru (Tugas Dosen)")]
    public GameObject panelInstruksi;
    public GameObject panelPause;
    public GameObject panelSetting;
    public GameObject tombolPauseKecil; // TAMBAHAN: Kita daftarin tombol pausenya
    public GameObject uiMinimap;

    [Header("Pengaturan Game")]
    public float waktuBermain = 60f;
    public int targetUang = 60000;
    private int totalUang = 0;
    private bool gameAktif = false;
    public TextMeshProUGUI teksTombolSuara; // Tambahkan ini di deretan [Header("Referensi UI Baru")]

    void Awake() { instance = this; }

    void Start()
    {
        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (panelMenang != null) panelMenang.SetActive(false);
        if (panelPause != null) panelPause.SetActive(false);
        if (panelSetting != null) panelSetting.SetActive(false);
        if (tombolPauseKecil != null) tombolPauseKecil.SetActive(false);

        // MATIKAN Minimap saat instruksi muncul
        if (uiMinimap != null) uiMinimap.SetActive(false);

        if (panelInstruksi != null)
        {
            panelInstruksi.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Update()
    {
        if (gameAktif)
        {
            waktuBermain -= Time.deltaTime;
            teksTimer.text = "Waktu: " + Mathf.RoundToInt(waktuBermain).ToString() + "s";

            if (waktuBermain <= 0)
            {
                waktuBermain = 0;
                TriggerGameOver();
            }
        }
    }

    // --- FUNGSI INSTRUKSI ---
    public void TutupInstruksiDanMulai()
    {
        if (panelInstruksi != null) panelInstruksi.SetActive(false);
        if (tombolPauseKecil != null) tombolPauseKecil.SetActive(true);

        // NYALAKAN Minimap saat game mulai
        if (uiMinimap != null) uiMinimap.SetActive(true);

        Time.timeScale = 1f;
        gameAktif = true;
    }

    // --- FUNGSI PAUSE MENU ---
    public void BukaPauseMenu()
    {
        if (panelPause != null) panelPause.SetActive(true);

        // Sembunyikan tombol pause kecil biar layarnya bersih pas menu pause terbuka
        if (tombolPauseKecil != null) tombolPauseKecil.SetActive(false);

        Time.timeScale = 0f;
    }

    public void LanjutGame() // Resume
    {
        if (panelPause != null) panelPause.SetActive(false);

        // Munculkan lagi tombol pause kecilnya
        if (tombolPauseKecil != null) tombolPauseKecil.SetActive(true);

        Time.timeScale = 1f;
    }

    public void KeluarKeMainMenu() // Exit
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // --- FUNGSI SETTING ---
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
        bool suaraMenyala = PlayerPrefs.GetInt("SuaraMute", 0) == 0;
        suaraMenyala = !suaraMenyala;
        PlayerPrefs.SetInt("SuaraMute", suaraMenyala ? 0 : 1);

        AudioListener.volume = suaraMenyala ? 1f : 0f;

        // Ini baris yang bikin tulisannya update
        if (teksTombolSuara != null)
        {
            teksTombolSuara.text = suaraMenyala ? "SUARA: ON" : "SUARA: OFF";
        }
    }

    // --- FUNGSI BAWAAN LAMA ---
    void TriggerGameOver()
    {
        gameAktif = false;
        if (panelGameOver != null) panelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TambahUang(int jumlah)
    {
        totalUang += jumlah;
        teksUang.text = "Uang: Rp " + totalUang.ToString();

        // UBAH 60000 MENJADI targetUang
        if (totalUang >= targetUang && panelMenang != null && gameAktif)
        {
            gameAktif = false;
            panelMenang.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void FungsiTombolRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FungsiLanjutLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_2");
    }
}