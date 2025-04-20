using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

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
    private Queue<DialogueLine> lines;
    private bool isTyping = false;
    private string currentSentence = "";

    [Header("Speaker Portraits")]
    public Sprite fishPortrait;
    public Sprite catPortrait;

    public Image portraitImage;

    private Dictionary<string, Sprite> speakerPortraitMap;

    public System.Action onDialogueComplete;



    void Start()
    {
        lines = new Queue<DialogueLine>();


        // Initialize portrait map
        speakerPortraitMap = new Dictionary<string, Sprite>()
    {
        { "Fish", fishPortrait },
        { "Cat", catPortrait }
    };

        dialogueBox.SetActive(false); // Start hidden
    }

    public void StartDialogue(Dialogue dialogue)
    {
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


        this.textSpeed = dialogue.textSpeed;
        animator.SetBool("IsOpen", true);
        //Debug.Log("Starting conversation with " + dialogue.name);
        dialogueBox.SetActive(true);
        dialogueBox.SetActive(true);

        lines.Clear();
        foreach (DialogueLine line in dialogue.lines)
        {
            lines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = lines.Dequeue();


        if (portraitImage != null)
        {
            if (speakerPortraitMap.ContainsKey(line.speakerName))
            {
                portraitImage.sprite = speakerPortraitMap[line.speakerName];
                portraitImage.enabled = true;
            }
            else
            {
                portraitImage.enabled = false; // hide if no match
            }
        }


        StopAllCoroutines();
        nameText.text = line.speakerName;
        StartCoroutine(TypeSentence(line.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        currentSentence = sentence;

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed); // typing speed
        }
        isTyping = false;

    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        dialogueBox.SetActive(false);

        if (playerMovement != null)
        {
            playerMovement.enabled = true; //resumes movement
        }

        if (onDialogueComplete != null){
            onDialogueComplete.Invoke();
        }

    }

    void Update()
    {
        if (dialogueBox.activeSelf && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = currentSentence;
                isTyping = false;
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }
}
