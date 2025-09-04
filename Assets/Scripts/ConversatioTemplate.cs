using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCDialogueTrigger : MonoBehaviour
{
    [Header("NPC Dialogue Settings")]
    [SerializeField] ScriptableObject myConversation; // Conversación de este NPC
    [SerializeField] string playerTag = "Player";

    [Header("Asignar en el Inspector")]
    [SerializeField] DialogoManager dialogoManager;   // Referencia al DialogoManager global
    [SerializeField] bool cargaEscena = false;        // ¿Este NPC carga una escena?
    [SerializeField] string nombreEscenaACargar;      // Nombre de la escena a cargar

    void Start()
    {
        if (dialogoManager == null)
        {
            dialogoManager = FindObjectOfType<DialogoManager>();
        }
    }

    public void StartDialogue()
    {
        if (dialogoManager != null)
        {
            dialogoManager.StartConversation(myConversation);

            if (cargaEscena && !string.IsNullOrEmpty(nombreEscenaACargar))
            {
                dialogoManager.onConversationEnd.AddListener(CargarEscena);
            }
        }
    }

    void CargarEscena()
    {
        SceneManager.LoadScene(nombreEscenaACargar);
        dialogoManager.onConversationEnd.RemoveListener(CargarEscena);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            StartDialogue(); 
        }
    }
}