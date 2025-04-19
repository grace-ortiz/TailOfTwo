using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool playOnce = false; // 💡 new toggle in Inspector
    private bool hasPlayed = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            //Debug.Log("Trigger entered by: " + other.name);
            if (playOnce && hasPlayed)
                return;

            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

            if (playOnce)
                hasPlayed = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
}
