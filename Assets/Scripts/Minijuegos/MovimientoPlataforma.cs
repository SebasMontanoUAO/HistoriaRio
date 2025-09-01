using System;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    private Rigidbody2D rbPlataforma;

    [SerializeField]
    private float velocidad = 10f;

    private Vector2 direccion;
    private float posicionY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbPlataforma = GetComponent<Rigidbody2D>();
        posicionY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        ObtenerInput();
        transform.position = new Vector2(transform.position.x, posicionY);
    }

    private void ObtenerInput()
    {
        float horizontal = Input.GetAxis("Horizontal");

        direccion = new Vector2(horizontal, 0);
    }

    private void FixedUpdate()
    {
        Movimiento();
    }

    private void Movimiento()
    {
        rbPlataforma.linearVelocityX = direccion.x * velocidad;
    }
}
