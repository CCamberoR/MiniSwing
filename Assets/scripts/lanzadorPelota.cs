using UnityEngine;

public class lanzadorPelota : MonoBehaviour
{
    public float fuerzaMaxima = 20f; // La fuerza máxima del lanzamiento
    public float factorFuerza = 10f; // Un factor para ajustar la fuerza del lanzamiento
    public Rigidbody pelotaRigidbody; // El Rigidbody de la pelota que será lanzada

    private Vector3 posicionInicial; // La posición inicial del arrastre

    // Método que se llama cuando se mantiene presionado el clic (o toque) y se mueve el mouse
    private void OnMouseDrag()
    {
        // Obtener la posición del ratón en el mundo
        Vector3 posicionMouse = Input.mousePosition;
        posicionMouse.z = 10; // Ajustar la distancia desde la cámara

        // Convertir la posición del ratón a coordenadas del mundo
        Vector3 posicionObjetivo = Camera.main.ScreenToWorldPoint(posicionMouse);

        // Mover la pelota hacia la posición del ratón
        pelotaRigidbody.MovePosition(posicionObjetivo);
    }

    // Método que se llama cuando se levanta el clic (o toque)
    private void OnMouseUpAsButton()
    {
        // Calcular la dirección del lanzamiento
        Vector3 direccionLanzamiento = (transform.position - posicionInicial).normalized;

        // Calcular la distancia arrastrada y ajustar la fuerza de acuerdo con el factor de fuerza
        float distanciaArrastrada = Vector3.Distance(transform.position, posicionInicial);
        float fuerzaLanzamiento = Mathf.Clamp(distanciaArrastrada * factorFuerza, 0f, fuerzaMaxima);

        // Aplicar la fuerza al Rigidbody de la pelota
        pelotaRigidbody.AddForce(direccionLanzamiento * fuerzaLanzamiento, ForceMode.Impulse);
    }

    // Método que se llama cuando se presiona el clic (o toque)
    private void OnMouseDown()
    {
        // Registrar la posición inicial del arrastre
        posicionInicial = transform.position;
    }
}
