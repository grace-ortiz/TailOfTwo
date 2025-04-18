using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox; 
    public Animator animator;
    public float textSpeed;
    public PlayerMovement playerMovement;
    public Rigidbody2D playerRb;
    public Animator playerAnimator;




    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false); // Start hidden
    }

    public void StartDialogue(Dialogue dialogue)
    {
        /* 
         * For some reason this hides the dialogue box when it is included
        if (playerMovement != null)
        {
            playerMovement.enabled = false; //Stops input on dialogue
        }

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero; //Stops all motion
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isWalking", false); // Stops walking animation
        }
        */

        this.textSpeed = dialogue.textSpeed;
        animator.SetBool("IsOpen", true);

        //Debug.Log("Starting conversation with " + dialogue.name);
        dialogueBox.SetActive(true);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed); // typing speed
        }
    }

    void EndDialogue()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = true; //resumes movement
        }

        animator.SetBool("IsOpen", false);
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }
}
