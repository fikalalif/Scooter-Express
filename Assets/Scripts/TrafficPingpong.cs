using UnityEngine;

public class TrafficPingPong : MonoBehaviour
{
    [Header("Pengaturan Jarak & Kecepatan")]
    public float jarakMajuMundur = 80f;  // Nah ini spasinya udah dihapus bro
    public float kecepatanMobil = 4f;

    private Vector3 posisiAwal;

    void Start()
    {
        posisiAwal = transform.position;
    }

    void Update()
    {
        float hitungJarak = Mathf.PingPong(Time.time * kecepatanMobil, jarakMajuMundur);

        transform.position = posisiAwal + (transform.forward * hitungJarak);
    }
}