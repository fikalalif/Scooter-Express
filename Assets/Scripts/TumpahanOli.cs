using UnityEngine;

public class TumpahanOli : MonoBehaviour
{
    public float durasiSlip = 2f;

    void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah yang nabrak oli adalah motor player
        if (other.CompareTag("Player"))
        {
            ScooterController motor = other.GetComponent<ScooterController>();

            if (motor != null)
            {
                motor.TerkenaOli(durasiSlip);
                Debug.Log("Kena Oli! Oleng kapten!");
            }
        }
    }
}