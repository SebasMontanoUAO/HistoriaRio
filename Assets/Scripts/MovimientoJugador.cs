using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField]
    private float velocidad = 10f;
    private Interactuable interactuableActual;
    private Vector2 direccion;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Variable para controlar la dirección del sprite
    private bool mirandoDerecha = true;

    // NUEVO: flag para bloquear movimiento (cuando hay diálogo)
    private bool puedeMoverse = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!puedeMoverse)
        {
            // Cancelar input
            direccion = Vector2.zero;
            return;
        }

        ObtenerInput();
        if (Input.GetKeyDown(KeyCode.E) && interactuableActual != null)
        {
            interactuableActual.Interactuar(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Movimiento();
        ActualizarAnimacion();
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
        if (puedeMoverse)
        {
            rb.linearVelocity = new Vector2(direccion.x * velocidad, direccion.y * velocidad);
        }
        else
        {
            // Importante: detener el Rigidbody para que no se deslice
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void ActualizarAnimacion()
    {
        if (direccion.magnitude < 0.01f || !puedeMoverse)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetFloat("Vertical", 0f);
            return;
        }

        if (Mathf.Abs(direccion.x) > Mathf.Abs(direccion.y))
        {
            animator.SetFloat("Speed", Mathf.Abs(direccion.x));
            animator.SetFloat("Vertical", 0f);

            if (direccion.x > 0 && mirandoDerecha)
            {
                Voltear();
            }
            else if (direccion.x < 0 && !mirandoDerecha)
            {
                Voltear();
            }
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            animator.SetFloat("Vertical", direccion.y);
        }
    }

    private void Voltear()
    {
        mirandoDerecha = !mirandoDerecha;
        spriteRenderer.flipX = !mirandoDerecha;
    }

    internal void SetInteractuable(Interactuable nuevoInteractuable)
    {
        interactuableActual = nuevoInteractuable;
    }

    // 🔑 NUEVOS MÉTODOS
    public void BloquearMovimiento()
    {
        puedeMoverse = false;
        rb.linearVelocity = Vector2.zero; // detener por completo
    }

    public void DesbloquearMovimiento()
    {
        puedeMoverse = true;
    }
}