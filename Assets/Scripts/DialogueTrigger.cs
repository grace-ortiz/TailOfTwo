using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool playOnce = false; 
    private bool hasPlayed = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Trigger entered by: " + other.name);
            if (playOnce && hasPlayed)
                return;

            FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);

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
