using UnityEngine;

public class lanzadorPelota : MonoBehaviour
{
    public float fuerzaMaxima = 20f; // La fuerza máxima del lanzamiento
    public float factorFuerza = 10f; // Un factor para ajustar la fuerza del lanzamiento
    public Rigidbody pelotaRigidbody; // El Rigidbody de la pelota que será lanzada

    private Vector3 posicionInicialArrastre; // La posición inicial del arrastre

    void OnMouseDown()
    {
        posicionInicialArrastre = Input.mousePosition;
        posicionInicialArrastre.z = 10; // Ajustar la distancia desde la cámara
        posicionInicialArrastre = Camera.main.ScreenToWorldPoint(posicionInicialArrastre);
    }

    void OnMouseUp()
    {
        Vector3 direccionLanzamiento = (transform.position - posicionInicialArrastre).normalized;

        float distanciaArrastrada = Vector3.Distance(transform.position, posicionInicialArrastre);
        float fuerzaLanzamiento = Mathf.Clamp(distanciaArrastrada * factorFuerza, 0f, fuerzaMaxima);

        pelotaRigidbody.AddForce(direccionLanzamiento * fuerzaLanzamiento, ForceMode.Impulse);
    }
}
