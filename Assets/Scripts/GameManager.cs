using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Referensi UI")]
    public TextMeshProUGUI teksTimer;
    public TextMeshProUGUI teksUang;
    public GameObject panelGameOver;
    public GameObject panelMenang; // TAMBAHAN: Kolom untuk panel baru

    [Header("Pengaturan Game")]
    public float waktuBermain = 60f;
    private int totalUang = 0;
    private bool gameAktif = true;

    void Awake() { instance = this; }

    void Start()
    {
        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (panelMenang != null) panelMenang.SetActive(false); // Pastikan panel menang mati di awal
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

    void TriggerGameOver()
    {
        gameAktif = false;
        if (panelGameOver != null) panelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    // FUNGSI DI-UPGRADE
    public void TambahUang(int jumlah)
    {
        totalUang += jumlah;
        teksUang.text = "Uang: Rp " + totalUang.ToString();

        // Cek jika uang sudah mencapai 60.000
        if (totalUang >= 60000 && panelMenang != null && gameAktif)
        {
            gameAktif = false;
            panelMenang.SetActive(true);
            Time.timeScale = 0f; // Berhentikan waktu
        }
    }

    public void FungsiTombolRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // FUNGSI BARU UNTUK TOMBOL LEVEL 2
    public void FungsiLanjutLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_2"); // Pastikan lu bikin Scene bernama Level_2 nanti
    }
}