using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configuraci�n de Seguimiento")]
    [SerializeField] private Transform target; // El personaje a seguir
    [SerializeField] private float suavizado = 0.125f; // Qu� tan suave es el movimiento
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10); // Distancia de la c�mara

    [Header("L�mites de C�mara (Opcional)")]
    [SerializeField] private bool usarLimites = false;
    [SerializeField] private float limiteIzquierda = -10f;
    [SerializeField] private float limiteDerecha = 10f;
    [SerializeField] private float limiteInferior = -5f;
    [SerializeField] private float limiteSuperior = 5f;

    void Start()
    {
        // Si no se asign� target manualmente, buscar al jugador
        if (target == null)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null)
            {
                target = jugador.transform;
            }
            else
            {
                Debug.LogWarning("No se encontr� un GameObject con tag 'Player'. Asigna manualmente el target.");
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calcular la posici�n deseada
        Vector3 posicionDeseada = target.position + offset;

        // Aplicar l�mites si est�n habilitados
        if (usarLimites)
        {
            posicionDeseada.x = Mathf.Clamp(posicionDeseada.x, limiteIzquierda, limiteDerecha);
            posicionDeseada.y = Mathf.Clamp(posicionDeseada.y, limiteInferior, limiteSuperior);
        }

        // Movimiento suavizado hacia la posici�n deseada
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, suavizado);
        transform.position = posicionSuavizada;
    }

    // M�todo para cambiar el objetivo en tiempo de ejecuci�n
    public void CambiarObjetivo(Transform nuevoObjetivo)
    {
        target = nuevoObjetivo;
    }

    // M�todo para establecer offset din�micamente
    public void EstablecerOffset(Vector3 nuevoOffset)
    {
        offset = nuevoOffset;
    }
}