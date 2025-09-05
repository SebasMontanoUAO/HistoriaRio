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

    private bool alreadyTalked = false; // 🔥 Evita que hable más de una vez

    void Start()
    {
        if (dialogoManager == null)
        {
            dialogoManager = FindObjectOfType<DialogoManager>();
        }
    }

    public void StartDialogue()
    {
        if (alreadyTalked) return; // 🔥 si ya habló, no hace nada

        if (dialogoManager != null)
        {
            dialogoManager.StartConversation(myConversation, () =>
            {
                // 🔥 marcar como hablado
                alreadyTalked = true;

                // si debe cargar escena
                if (cargaEscena && !string.IsNullOrEmpty(nombreEscenaACargar))
                {
                    SceneManager.LoadScene(nombreEscenaACargar);
                }
            });
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            StartDialogue();
        }
    }
}