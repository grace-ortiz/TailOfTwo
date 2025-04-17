using UnityEngine;

public class Destroyable : Interactable {
    public Sprite destroyedSprite;
    public bool isDestroyed = false;


    public void Destroy() {
        if (!isDestroyed && spriteRenderer != null && destroyedSprite != null) {
            spriteRenderer.sprite = destroyedSprite;
            UpdateColliderShape();
            isDestroyed = true;
            anim.SetBool("isDestroyed", true);
        }
    }

    public override void ResetInteraction() {
        spriteRenderer.sprite = baseSprite;
        UpdateColliderShape();
        isDestroyed = false;
        anim.SetBool("isDestroyed", false);
    }
}