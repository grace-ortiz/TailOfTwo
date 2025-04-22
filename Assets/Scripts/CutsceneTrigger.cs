using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject cutscene; // drag in the Order Fish UI image
    public Dialogue cutsceneDialogue; // drag in the cutscene dialogue
    public DialogueManager dialogueManager; // drag in your DialogueManager
    public float delayBeforeDialogue = 1f;
    public float delayAfterDialogue = 1f;

    public float fadeDuration = 1f;

    private bool triggered = false;
    private bool waitingForInput = false;
    private Image cutsceneImage;

    void Start()
    {
        cutsceneImage = cutscene.GetComponent<Image>();
        cutsceneImage.canvasRenderer.SetAlpha(0f);
        cutsceneImage.color = Color.black;
    }

    void Update()
    {
        if (waitingForInput && Input.anyKeyDown)
        {
            waitingForInput = false;
            StartCoroutine(FadeOutAndLoadScene("CreditsScene"));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(PlayCutscene());
        }
    }

    IEnumerator PlayCutscene()
    {
        cutscene.SetActive(true);
        yield return StartCoroutine(FadeInToBlack());
        
        yield return new WaitForSeconds(delayBeforeDialogue);

        dialogueManager.StartDialogue(cutsceneDialogue);
        dialogueManager.onDialogueComplete += OnDialogueComplete;
    }

    private void OnDialogueComplete()
    {
        dialogueManager.onDialogueComplete -= OnDialogueComplete;
        StartCoroutine(WaitBeforeInput());
    }

    IEnumerator WaitBeforeInput()
    {
        yield return new WaitForSeconds(delayAfterDialogue);
        waitingForInput = true;
    }

    IEnumerator FadeInToBlack()
    {
        if (cutsceneImage == null)
        {
            yield break;
        }

        
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalizedTime = t / fadeDuration;
            
            cutsceneImage.canvasRenderer.SetAlpha(Mathf.Lerp(0f, 1f, normalizedTime)); 
            cutsceneImage.color = Color.black;

            yield return null;
        }

        cutsceneImage.canvasRenderer.SetAlpha(1f); 
        cutsceneImage.color = Color.black; 

        yield return StartCoroutine(FadeToWhite());
    }

    IEnumerator FadeToWhite()
    {
        float t = 0f;
        
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalizedTime = t / fadeDuration;

            cutsceneImage.color = Color.Lerp(Color.black, Color.white, normalizedTime);

            yield return null;
        }

        cutsceneImage.color = Color.white; 
    }


    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalizedTime = t / fadeDuration;
            cutsceneImage.color = Color.Lerp(Color.white, Color.black, normalizedTime);
            yield return null;
        }

        cutsceneImage.color = Color.black;
        SceneManager.LoadScene(sceneName);
    }
}
