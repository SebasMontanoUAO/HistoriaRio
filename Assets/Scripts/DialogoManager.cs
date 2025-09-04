using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DialogoManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI lineText;
    [SerializeField] GameObject dialoguePanel;

    bool dialogueIsActive = false;
    Coroutine dialogueCoroutine;

    public UnityEvent onConversationEnd;
    void Start()
    {
        // Canvas y textos inician desactivados SIEMPRE
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        // Limpiar textos al inicio
        if (nameText != null)
        {
            nameText.text = "";
        }
        if (lineText != null)
        {
            lineText.text = "";
        }
    }

    public void StartConversation(ScriptableObject conversation)
    {
        if (conversation == null || dialogueIsActive) return;

        dialogueIsActive = true;
        dialoguePanel.SetActive(true);

        if (dialogueCoroutine != null) StopCoroutine(dialogueCoroutine);
        dialogueCoroutine = StartCoroutine(PlayConversation(conversation));
    }

    IEnumerator PlayConversation(ScriptableObject conversation)
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

        EndConversation();
    }

    void EndConversation()
    {
        dialogueIsActive = false;
        dialoguePanel.SetActive(false);
        lineText.text = "";
        nameText.text = "";

        onConversationEnd?.Invoke();
    }
}