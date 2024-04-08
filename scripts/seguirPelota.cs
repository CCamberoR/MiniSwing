using UnityEngine;

public class seguirPelota : MonoBehaviour
{
    public Transform objetivo; // La pelota a la que seguirá la cámara
    public float distanciaVertical = 5.0f; // Distancia vertical entre la cámara y la pelota
    public float distanciaHorizontal = 5.0f; // Distancia horizontal entre la cámara y la pelota
    public float alturaInicial = 3.0f; // Altura inicial de la cámara
    public float velocidadRotacion = 50.0f; // Velocidad de rotación de la cámara
    private Vector3 offset; // Offset entre la posición de la cámara y la pelota
    private float distanciaActualVertical; // Distancia actual entre la cámara y la pelota
    private float anguloVertical = 0.0f; // Ángulo vertical de la cámara

    void Start()
    {
        // Calcular el offset entre la cámara y la pelota
        offset = new Vector3(0f, alturaInicial - distanciaVertical, -distanciaHorizontal);

        // Posicionar la cámara inicialmente
        transform.position = objetivo.position - offset;
        transform.LookAt(objetivo);

        // Calcular la distancia inicial vertical
        distanciaActualVertical = Vector3.Distance(transform.position, objetivo.position);

    }

    void FixedUpdate()
    {
        // Calcular la nueva dirección de la cámara
        Quaternion rotation = Quaternion.Euler(anguloVertical, transform.rotation.eulerAngles.y, 0f);
        Vector3 direccion = rotation * Vector3.forward;

        // Movimiento de la cámara con las teclas de flecha
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Actualizar el ángulo vertical de la cámara
        anguloVertical += movimientoVertical * velocidadRotacion * Time.fixedDeltaTime;
        anguloVertical = Mathf.Clamp(anguloVertical, -89.0f, 89.0f);

        // Calcular la nueva posición de la cámara alrededor de la pelota
        Vector3 movimiento = Quaternion.AngleAxis(movimientoHorizontal * velocidadRotacion * Time.fixedDeltaTime, Vector3.up) * direccion;

        // Aplicar la nueva posición de la cámara
        Vector3 nuevaPosicion = objetivo.position - movimiento.normalized * offset.magnitude;

        // Mantener la misma distancia vertical
        Vector3 direccionVertical = (transform.position - objetivo.position).normalized;
        nuevaPosicion += direccionVertical * (distanciaActualVertical - Vector3.Distance(nuevaPosicion, objetivo.position));

        transform.position = nuevaPosicion;

        // Asegurar que la cámara mire hacia la pelota
        transform.LookAt(objetivo);

        
    }
}