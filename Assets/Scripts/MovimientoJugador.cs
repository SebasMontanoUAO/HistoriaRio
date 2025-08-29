using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField]
    private float velocidad = 10f;

    private Interactuable interactuableActual;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        if (Input.GetKeyDown(KeyCode.E) && interactuableActual != null)
        {
            interactuableActual.Interactuar(gameObject);
        } 
    }

    private void Movimiento()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 direccion = new Vector2(horizontal, vertical);
        direccion = direccion.normalized;

        transform.Translate(direccion * Time.deltaTime * velocidad);
    }

    internal void SetInteractuable(Interactuable nuevoInteractuable)
    {
        interactuableActual = nuevoInteractuable; 
    }
}
