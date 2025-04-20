using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Growable : Interactable {
    public Sprite[] growthStages; // add as many growth stages as you want here, don't include the base sprite 
    
    public int currentStage = 0; // stage 0 is the base sprite itself 
    

    public void Grow() {
        if (spriteRenderer == null || growthStages == null || growthStages.Length == 0)
            return;


        if (currentStage < growthStages.Length) {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.growPlantSound, this.transform.position);
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
        AudioManager.instance.PlayOneShot(FMODEvents.instance.growPlantSound, this.transform.position);
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