using UnityEngine;

public class Destroyable : Interactable {
    public SpriteRenderer spriteRenderer;
    public Sprite destroyedSprite;
    protected bool isDestroyed = false;


    public void Destroy() {
        if (!isDestroyed && spriteRenderer != null && destroyedSprite != null) {
            spriteRenderer.sprite = destroyedSprite;
            isDestroyed = true;
        }
    }

    public override void ResetInteraction() {
        spriteRenderer.sprite = baseSprite;
        isDestroyed = false;
    }
}