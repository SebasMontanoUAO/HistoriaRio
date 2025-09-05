using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configuración de Seguimiento")]
    public Transform target; // El personaje a seguir
    public float suavizado = 0.125f; // Qué tan suave es el movimiento
    public Vector3 offset = new Vector3(0, 0, -10); // Distancia de la cámara

    [Header("Límites de Cámara con GameObjects")]
    public GameObject limiteIzquierdaObj;
    public GameObject limiteDerechaObj;
    public GameObject limiteInferiorObj;
    public GameObject limiteSuperiorObj;

    private float limiteIzquierda;
    private float limiteDerecha;
    private float limiteInferior;
    private float limiteSuperior;

    void Start()
    {
        // Si no se asignó target manualmente, buscar al jugador
        if (target == null)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null)
            {
                target = jugador.transform;
            }
            else
            {
                Debug.LogWarning("No se encontró un GameObject con tag 'Player'. Asigna manualmente el target.");
            }
        }

        // Guardar valores de límites a partir de los GameObjects
        if (limiteIzquierdaObj != null) limiteIzquierda = limiteIzquierdaObj.transform.position.x;
        if (limiteDerechaObj != null) limiteDerecha = limiteDerechaObj.transform.position.x;
        if (limiteInferiorObj != null) limiteInferior = limiteInferiorObj.transform.position.y;
        if (limiteSuperiorObj != null) limiteSuperior = limiteSuperiorObj.transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calcular la posición deseada
        Vector3 posicionDeseada = target.position + offset;

        // Aplicar límites
        posicionDeseada.x = Mathf.Clamp(posicionDeseada.x, limiteIzquierda, limiteDerecha);
        posicionDeseada.y = Mathf.Clamp(posicionDeseada.y, limiteInferior, limiteSuperior);

        // Movimiento suavizado hacia la posición deseada
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, suavizado);
        transform.position = posicionSuavizada;
    }

    // Método para cambiar el objetivo en tiempo de ejecución
    public void CambiarObjetivo(Transform nuevoObjetivo)
    {
        target = nuevoObjetivo;
    }

    // Método para establecer offset dinámicamente
    public void EstablecerOffset(Vector3 nuevoOffset)
    {
        offset = nuevoOffset;
    }
}