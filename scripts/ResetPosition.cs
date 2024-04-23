using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 lastZeroVelocityPosition;
    private Rigidbody rb;
    public AudioSource audioSource;
    public AudioClip badShotSound;

    void Start(){
        // Guarda la posición inicial de la pelota
        initialPosition = transform.position;
        lastZeroVelocityPosition = initialPosition;

        // Obtiene el componente Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        // Verifica si la velocidad es menor que 0.15f
        if (rb.velocity.magnitude < 0.15f){
            // Si la velocidad es 0, guarda la posición actual
            lastZeroVelocityPosition = transform.position;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        // Verifica si la posición Y de la pelota es menor que -5
        if (transform.position.y < -5f){
            // Si la posición es menor que -5, restablece la posición a la última donde la velocidad era 0
            transform.position = new Vector3(lastZeroVelocityPosition.x, lastZeroVelocityPosition.y + 1, lastZeroVelocityPosition.z);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            audioSource.PlayOneShot(badShotSound);
        }
    }
}
