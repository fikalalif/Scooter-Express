using UnityEngine;

public class ScooterController : MonoBehaviour
{
    public float maxSpeed = 15f;       // Kecepatan maksimal motor
    public float turnSpeed = 60f;      // Kecepatan belok
    public float acceleration = 3f;    // Seberapa halus tarikan gasnya (makin kecil makin berat/lemot)

    private float currentSpeed = 0f;
    private float horizontalInput;
    private float forwardInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Bikin tarikan gas jadi lebih halus (ada momentum)
        float targetSpeed = forwardInput * maxSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);

        // Pakai Vector3.up karena ngikutin rotasi Vario lu yang kemarin
        transform.Translate(Vector3.up * Time.deltaTime * currentSpeed);

        // Motor cuma bisa belok kalau lagi ada kecepatan berjalan
        if (Mathf.Abs(currentSpeed) > 0.5f)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput, Space.World);
        }
    }
}