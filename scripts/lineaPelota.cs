using UnityEngine;

public class lineaPelota : MonoBehaviour
{
    public Transform objetivo; // La pelota a la que seguirá la cámara
    private Vector3 offset; // Offset entre la posición de la cámara y la pelota

    void Start()
    {
        // Calcular el offset entre la linea y la pelota
        offset = new Vector3(0f, 0f, 0f);

        // Posicionar la cámara inicialmente
        transform.position = objetivo.position - offset;
        transform.LookAt(objetivo);

    }

    void FixedUpdate()
    {
        // Calcular el offset entre la linea y la pelota
        offset = new Vector3(0f, 0f, 0f);

        // Posicionar la cámara inicialmente
        transform.position = objetivo.position - offset;
        transform.LookAt(objetivo);

        
    }
}