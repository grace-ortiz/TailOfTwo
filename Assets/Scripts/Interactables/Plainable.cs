using UnityEngine;
using System.Collections.Generic;

public class Plainable : Interactable {
    public Sprite interactedSprite;
    public bool isInteracted;
    public MusicPuzzle musicPuzzle;

    public void Interact() {
        // play sound
        print("SOUND HAPPENING");

        if (!isInteracted && spriteRenderer != null && interactedSprite != null) {
            spriteRenderer.sprite = interactedSprite;
            UpdateColliderShape();
            isInteracted = true;
        }

        if (musicPuzzle != null) {
            musicPuzzle.OnItemInteracted(this);
        }
    }
    

    public override void ResetInteraction() {
        spriteRenderer.sprite = baseSprite;
        UpdateColliderShape();
        isInteracted = false;
    }

    public override void OnInteract() {
        
    }
    
}