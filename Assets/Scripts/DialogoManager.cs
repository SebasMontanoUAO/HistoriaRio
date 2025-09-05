using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DialogoManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI lineText;
    [SerializeField] GameObject dialoguePanel;

    [Header("Control del Jugador")]
    public MovimientoJugador playerMovementScript; // 🔥 referencia al script de movimiento

    private bool dialogueIsActive = false;
    private Coroutine dialogueCoroutine;

    public UnityEvent onConversationEnd;

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (nameText != null) nameText.text = "";
        if (lineText != null) lineText.text = "";
    }

    public void StartConversation(ScriptableObject conversation, System.Action onEnd = null)
    {
        if (conversation == null || dialogueIsActive) return;

        dialogueIsActive = true;
        dialoguePanel.SetActive(true);

        // 🔒 Bloquea movimiento del jugador correctamente
        if (playerMovementScript != null)
            playerMovementScript.BloquearMovimiento();

        if (dialogueCoroutine != null) StopCoroutine(dialogueCoroutine);
        dialogueCoroutine = StartCoroutine(PlayConversation(conversation, onEnd));
    }

    IEnumerator PlayConversation(ScriptableObject conversation, System.Action onEnd)
    {
        var field = conversation.GetType().GetField("conversationLines");
        if (field != null)
        {
            var conversationLines = (System.Array)field.GetValue(conversation);

            for (int i = 0; i < conversationLines.Length; i++)
            {
                var currentLine = conversationLines.GetValue(i);

                var speakerField = currentLine.GetType().GetField("speakerName");
                var dialogueField = currentLine.GetType().GetField("dialogueLine");

                string speakerName = speakerField?.GetValue(currentLine) as string ?? "";
                string dialogueLine = dialogueField?.GetValue(currentLine) as string ?? "";

                nameText.text = speakerName;
                lineText.text = dialogueLine;

                float readTime = Mathf.Max(2f, dialogueLine.Length * 0.06f);
                yield return new WaitForSeconds(readTime);
            }
        }

        EndConversation(onEnd);
    }

    void EndConversation(System.Action onEnd)
    {
        dialogueIsActive = false;
        dialoguePanel.SetActive(false);
        lineText.text = "";
        nameText.text = "";

        // 🔓 Reactiva movimiento del jugador correctamente
        if (playerMovementScript != null)
            playerMovementScript.DesbloquearMovimiento();

        onConversationEnd?.Invoke();
        onEnd?.Invoke();
    }
}