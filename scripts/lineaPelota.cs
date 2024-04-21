using UnityEngine;

public class lineaPelota : MonoBehaviour
{
    public Transform objetivo; // La pelota a la que seguirá la cámara
    public LineRenderer lineRenderer; // El LineRenderer que comienza desde la pelota
    private Vector3 offset; // Offset entre la posición de la cámara y la pelota

    void Start()
    {
        // Si el LineRenderer no está asignado, intenta obtenerlo del mismo objeto
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        // Configura el LineRenderer para usar dos posiciones
        lineRenderer.positionCount = 2;

        // Calcular el offset entre la linea y la pelota
        offset = new Vector3(0f, 0f, 0f);

        // Posicionar la cámara inicialmente
        transform.position = objetivo.position - offset;
        transform.LookAt(objetivo);
    }

    void FixedUpdate()
    {
        // Posicionar la cámara inicialmente
        transform.position = objetivo.position - offset;
        transform.LookAt(objetivo);

        // Establece la primera posición del LineRenderer en la posición de la pelota
        lineRenderer.SetPosition(0, objetivo.position);

        // Establece la segunda posición del LineRenderer en la dirección en la que quieres lanzar la pelota
        // Aquí, simplemente lo establecemos a 10 unidades en la dirección en la que la cámara está mirando
        lineRenderer.SetPosition(1, transform.position + transform.forward * 10);
    }
}