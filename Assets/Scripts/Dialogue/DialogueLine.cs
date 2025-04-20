using UnityEngine;

[System.Serializable]
public class DialogueLine : MonoBehaviour
{
    public string speakerName;
    public string prompt;
    [TextArea(2, 5)]
    public string sentence;
}