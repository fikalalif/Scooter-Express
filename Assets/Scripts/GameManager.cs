using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // WAJIB dimasukkan untuk fungsi Restart/Pindah Scene

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Referensi UI")]
    public TextMeshProUGUI teksTimer;
    public TextMeshProUGUI teksUang;
    public GameObject panelGameOver; // Tempat naruh Panel_GameOver nanti

    [Header("Pengaturan Game")]
    public float waktuBermain = 60f;
    private int totalUang = 0;
    private bool gameAktif = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Memastikan panel game over mati di awal permainan
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
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

    void TriggerGameOver()
    {
        gameAktif = false;

        // Memunculkan panel kalah ke layar pemain
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }

        // Menghentikan pergerakan motor & fisika dunia game saat kalah
        Time.timeScale = 0f;
    }

    public void TambahUang(int jumlah)
    {
        totalUang += jumlah;
        teksUang.text = "Uang: Rp " + totalUang.ToString();
    }

    // Fungsi khusus untuk dipasang di Tombol Restart
    public void FungsiTombolRestart()
    {
        Time.timeScale = 1f; // Mengembalikan waktu dunia game jadi normal lagi sebelum reload

        // Mengulang Scene aktif (Level 1) dari awal lagi
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}