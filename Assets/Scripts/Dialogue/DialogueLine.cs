using UnityEngine;

[System.Serializable]
public class DialogueLine : MonoBehaviour
{
    public string speakerName;
    [TextArea(2, 5)]
    public string sentence;
}