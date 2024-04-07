using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        // Guarda la posición inicial de la pelota
        initialPosition = transform.position;
    }

    void Update()
    {
        // Verifica si la posición Y de la pelota es menor que -5
        if (transform.position.y < -5f)
        {
            // Si la posición es menor que -5, restablece la posición a la inicial
            transform.position = initialPosition;
        }
    }
}
