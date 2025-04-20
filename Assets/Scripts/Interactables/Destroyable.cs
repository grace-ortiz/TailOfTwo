using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class Destroyable : Interactable {
    public Sprite destroyedSprite;
    public bool isDestroyed = false;

    // [field: SerializeField] public FMODUnity.EventReference fmodEventPath2 {get; private set;}
    // private EventInstance eventInstance2;
    // void Start()
    // {
    //     // Optional: Create instance ahead of time if you're reusing it
    //     eventInstance2 = RuntimeManager.CreateInstance(fmodEventPath2);
    // }
    public void Destroy() {
        if (!isDestroyed && spriteRenderer != null && destroyedSprite != null) {
            if (morphDuration == 0) {
                QuickDestroy();
            }
            else {
                StartCoroutine(SmoothInteract(destroyedSprite));
            }
        }
    }

    public void QuickDestroy() {
        spriteRenderer.sprite = destroyedSprite;
        UpdateColliderShape();
        isDestroyed = true;
        if (useAnimation) anim.SetBool("isDestroyed", true);
    }

    public override void OnInteract() {
        isDestroyed = true;
        if (useAnimation) anim.SetBool("isDestroyed", true);
    }

    public override void ResetInteraction() {
        spriteRenderer.sprite = baseSprite;
        UpdateColliderShape();
        isDestroyed = false;
        if (useAnimation) anim.SetBool("isDestroyed", false);
    }
}