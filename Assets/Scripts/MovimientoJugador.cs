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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
        rb.linearVelocity = new Vector2(direccion.x * velocidad, direccion.y * velocidad);
    }

    private void ActualizarAnimacion()
    {
        // Si no hay movimiento, resetear ambos parámetros
        if (direccion.magnitude < 0.01f)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetFloat("Vertical", 0f);
            return;
        }

        // Determinar la dirección predominante para las animaciones
        if (Mathf.Abs(direccion.x) > Mathf.Abs(direccion.y))
        {
            // Movimiento horizontal predominante
            animator.SetFloat("Speed", Mathf.Abs(direccion.x));
            animator.SetFloat("Vertical", 0f);

            // Manejar el volteo del sprite solo en movimiento horizontal
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
            // Movimiento vertical predominante
            animator.SetFloat("Speed", 0f);
            animator.SetFloat("Vertical", direccion.y);
        }
    }

    private void Voltear()
    {
        // Cambiar la dirección
        mirandoDerecha = !mirandoDerecha;

        // Voltear el sprite usando flipX
        spriteRenderer.flipX = !mirandoDerecha;

        // Alternativa: Voltear usando la escala (descomenta si prefieres este método)
        // Vector3 escala = transform.localScale;
        // escala.x *= -1;
        // transform.localScale = escala;
    }

    internal void SetInteractuable(Interactuable nuevoInteractuable)
    {
        interactuableActual = nuevoInteractuable;
    }
}