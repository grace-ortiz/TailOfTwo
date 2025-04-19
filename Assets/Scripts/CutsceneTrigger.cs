using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject cutsceneImage; // drag in the Order Fish UI image
    public Dialogue cutsceneDialogue; // drag in the cutscene dialogue
    public DialogueManager dialogueManager; // drag in your DialogueManager

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("player"))
        {
            triggered = true;
            cutsceneImage.SetActive(true);
            dialogueManager.StartDialogue(cutsceneDialogue);
            dialogueManager.onDialogueComplete += EndCutscene;
        }
    }

    private void EndCutscene()
    {
        dialogueManager.onDialogueComplete -= EndCutscene;
        SceneManager.LoadScene("CreditsScene"); // replace with your scene name
    }
}
