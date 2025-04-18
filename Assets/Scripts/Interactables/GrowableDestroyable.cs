using UnityEngine;

public class GrowableDestroyable : Interactable
{
    public Sprite[] growthStages;
    public Sprite destroyedSprite;

    public int currentStage = 0;
    public bool isDestroyed = false;

    public void Grow() {
        if (isDestroyed) {
            ResetInteraction();
        }
        else if (currentStage < growthStages.Length) {
            spriteRenderer.sprite = growthStages[currentStage];
            UpdateColliderShape();
            currentStage++;
        }
    }

    public void Destroy() {
        if (spriteRenderer != null && destroyedSprite != null && !isDestroyed) {
            spriteRenderer.sprite = destroyedSprite;
            UpdateColliderShape();
            isDestroyed = true;
        }
    }

    public override void ResetInteraction() {
        spriteRenderer.sprite = baseSprite;
        UpdateColliderShape();
        currentStage = 0;
        isDestroyed = false;
    }
}