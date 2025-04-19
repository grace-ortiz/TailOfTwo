using UnityEngine;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;

public class Plainable : Interactable {
    public Sprite interactedSprite;
    public bool isInteracted;
    public MusicPuzzle musicPuzzle;

    // public string fmodEventPath;
    [field: SerializeField] public FMODUnity.EventReference fmodEventPath {get; private set;}
    private EventInstance eventInstance;
    void Start()
    {
        // Optional: Create instance ahead of time if you're reusing it
        eventInstance = RuntimeManager.CreateInstance(fmodEventPath);
    }
    public void Interact() {
        // play sound
        print("SOUND HAPPENING");
        RuntimeManager.PlayOneShot(fmodEventPath, this.transform.position);

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