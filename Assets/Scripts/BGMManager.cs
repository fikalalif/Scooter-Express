using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Bikin variabel static buat ngecek apakah lagu udah nyala
    private static BGMManager instance;

    void Awake()
    {
        // Kalau belum ada lagu yang nyala, jadikan ini sebagai yang utama
        if (instance == null)
        {
            instance = this;
            // Ini mantra utamanya biar objeknya kebal dari pergantian Scene!
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Kalau ternyata udah ada lagu yang nyala (misal lu balik dari Level 1 ke Main Menu),
            // hancurkan objek yang baru ini biar suaranya nggak tabrakan/dobel.
            Destroy(gameObject);
        }
    }
}