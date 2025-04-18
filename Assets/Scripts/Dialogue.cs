using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3,10)]
    public string[] sentences;
    public float textSpeed = 0.02f;

}
