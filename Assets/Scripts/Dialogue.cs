using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea(2, 5)]
    public string sentence;
}

[System.Serializable]
public class Dialogue
{
    public DialogueLine[] lines;
    public float textSpeed = 0.02f;
}