using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField]
    private float velocidad = 10f;

    private Interactuable interactuableActual;
    private Vector2 direccion;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ObtenerInput();

        if (Input.GetKeyDown(KeyCode.E) && interactuableActual != null)
        {
            interactuableActual.Interactuar(gameObject);
        } 
    }

    private void FixedUpdate()
    {
        Movimiento();
    }

    private void ObtenerInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direccion = new Vector2(horizontal, vertical);
        direccion = direccion.normalized;
    }

    private void Movimiento()
    {
        rb.linearVelocity = new Vector2(direccion.x * velocidad, direccion.y * velocidad);
    }

    internal void SetInteractuable(Interactuable nuevoInteractuable)
    {
        interactuableActual = nuevoInteractuable; 
    }
}
