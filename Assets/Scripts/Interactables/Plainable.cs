using UnityEngine;
using FMODUnity;

public class Plainable : Interactable {
    public Sprite interactedSprite;
    public bool isInteracted { get; private set; }
    public MusicPuzzle musicPuzzle;

    public void Interact() {
        // play sound
        if (!fmodEventPath.IsNull) {
            eventInstance = RuntimeManager.CreateInstance(fmodEventPath);
            RuntimeManager.PlayOneShot(fmodEventPath, this.transform.position);
        }
        else {
            Debug.Log("No FMOD event path!");
        }

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