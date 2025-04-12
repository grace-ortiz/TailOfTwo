using UnityEngine;

public class Destroyable : Interactable {
    public Sprite destroyedSprite;
    protected bool isDestroyed = false;


    public void Destroy() {
        if (!isDestroyed && spriteRenderer != null && destroyedSprite != null) {
            spriteRenderer.sprite = destroyedSprite;
            UpdateColliderShape();
            isDestroyed = true;
        }
    }

    public override void ResetInteraction() {
        spriteRenderer.sprite = baseSprite;
        UpdateColliderShape();
        isDestroyed = false;
    }
}