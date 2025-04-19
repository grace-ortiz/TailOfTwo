using UnityEngine;
using System.Collections.Generic;

public class Growable : Interactable {
    public Sprite[] growthStages; // add as many growth stages as you want here, don't include the base sprite 
    
    public int currentStage = 0; // stage 0 is the base sprite itself 

    public void Grow() {
        if (spriteRenderer == null || growthStages == null || growthStages.Length == 0)
            return;

        if (currentStage < growthStages.Length) {
            spriteRenderer.sprite = growthStages[currentStage];
            UpdateColliderShape();
            currentStage++;
            anim.SetInteger("currentStage", currentStage);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.growPlantSound, this.transform.position);
            
        }
    }

    public override void ResetInteraction() {
        currentStage = 0;
        spriteRenderer.sprite = baseSprite;
        UpdateColliderShape();
        anim.SetInteger("currentStage", currentStage);
    }
    
}