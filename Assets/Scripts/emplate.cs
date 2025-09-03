using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogos/Conversation Template")]
public class ConversationTemplate : ScriptableObject
{
    public TextLine[] conversationLines;
}

[System.Serializable]
public class TextLine
{
    public string speakerName;
    [TextArea(3, 5)]
    public string dialogueLine;
}