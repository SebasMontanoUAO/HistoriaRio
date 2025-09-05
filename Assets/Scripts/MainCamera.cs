using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configuraci�n de Seguimiento")]
    public Transform target; // El personaje a seguir
    public float suavizado = 0.125f; // Qu� tan suave es el movimiento
    public Vector3 offset = new Vector3(0, 0, -10); // Distancia de la c�mara

    [Header("L�mites de C�mara con GameObjects")]
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

        // Guardar valores de l�mites a partir de los GameObjects
        if (limiteIzquierdaObj != null) limiteIzquierda = limiteIzquierdaObj.transform.position.x;
        if (limiteDerechaObj != null) limiteDerecha = limiteDerechaObj.transform.position.x;
        if (limiteInferiorObj != null) limiteInferior = limiteInferiorObj.transform.position.y;
        if (limiteSuperiorObj != null) limiteSuperior = limiteSuperiorObj.transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calcular la posici�n deseada
        Vector3 posicionDeseada = target.position + offset;

        // Aplicar l�mites
        posicionDeseada.x = Mathf.Clamp(posicionDeseada.x, limiteIzquierda, limiteDerecha);
        posicionDeseada.y = Mathf.Clamp(posicionDeseada.y, limiteInferior, limiteSuperior);

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