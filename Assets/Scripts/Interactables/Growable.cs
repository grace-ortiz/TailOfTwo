using UnityEngine;
using FMODUnity;

public class Growable : Interactable {
    public Sprite[] growthStages; // add as many growth stages as you want here, don't include the base sprite 
    
    public int currentStage { get; private set; } = 0; // stage 0 is the base sprite itself 
    
    public void Grow() {
        if (spriteRenderer == null || growthStages == null || growthStages.Length == 0)
            return;


        if (currentStage < growthStages.Length) {

            if (!fmodEventPath.IsNull) {
                eventInstance = RuntimeManager.CreateInstance(fmodEventPath);
                RuntimeManager.PlayOneShot(fmodEventPath, this.transform.position);
            }
            else {
                Debug.Log("No FMOD event path!");
            }

            if (morphDuration == 0) {
                QuickGrow();
            }
            else {
                StartCoroutine(SmoothInteract(growthStages[currentStage]));
            }
        }
    }

    public override void OnInteract() {
        currentStage++;
        if (useAnimation) anim.SetInteger("currentStage", currentStage);
    }

    private void QuickGrow() {
        spriteRenderer.sprite = growthStages[currentStage];
        UpdateColliderShape();
        currentStage++;
        if (useAnimation) anim.SetInteger("currentStage", currentStage);
    }

    public override void ResetInteraction() {
        currentStage = 0;
        spriteRenderer.sprite = baseSprite;
        UpdateColliderShape();
        if (useAnimation) anim.SetInteger("currentStage", currentStage);
    }
    
}