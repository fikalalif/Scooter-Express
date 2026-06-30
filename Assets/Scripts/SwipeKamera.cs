using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeKamera : MonoBehaviour
{
    public float kecepatanPutar = 2f;

    // Pakai LateUpdate agar kamera muter setelah motor selesai bergerak
    void LateUpdate()
    {
        float geserX = 0f;

        // 1. BACA INPUT DARI HP (TOUCH)
        if (Input.touchCount > 0)
        {
            Touch sentuhan = Input.GetTouch(0);

            // Cek kalau jari di area kanan layar & gak kena tombol UI
            if (sentuhan.position.x > Screen.width / 2 && !EventSystem.current.IsPointerOverGameObject(sentuhan.fingerId))
            {
                if (sentuhan.phase == TouchPhase.Moved)
                {
                    geserX = sentuhan.deltaPosition.x * kecepatanPutar * 0.1f;
                }
            }
        }
        // 2. BACA INPUT DARI MOUSE PC (BUAT TESTING)
        else if (Input.GetMouseButton(0))
        {
            // Cek kalau kursor di area kanan layar & gak ngeklik tombol UI
            if (Input.mousePosition.x > Screen.width / 2 && !EventSystem.current.IsPointerOverGameObject())
            {
                geserX = Input.GetAxis("Mouse X") * kecepatanPutar;
            }
        }

        // Kalau ada geseran, putar objek ini ke kiri/kanan
        if (geserX != 0)
        {
            transform.Rotate(Vector3.up, geserX, Space.World);
        }
    }
}