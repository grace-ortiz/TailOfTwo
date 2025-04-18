using UnityEngine;

public class Destroyable : Interactable {
    public Sprite destroyedSprite;
    public bool isDestroyed = false;


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