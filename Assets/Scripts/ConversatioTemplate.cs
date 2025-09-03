using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [Header("NPC Dialogue Settings")]
    [SerializeField] ScriptableObject myConversation; // Cambiar a ScriptableObject genérico
    [SerializeField] string playerTag = "Player";
    [SerializeField] DialogoManager dialogoManager; // Referencia al DialogoManager

    void Start()
    {
        // Si no se asignó manualmente, buscar el DialogoManager en la escena
        if (dialogoManager == null)
        {
            dialogoManager = FindObjectOfType<DialogoManager>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si es el jugador y si existe el DialogoManager
        if (other.CompareTag(playerTag) && dialogoManager != null)
        {
            dialogoManager.StartConversation(myConversation);
        }
    }
}