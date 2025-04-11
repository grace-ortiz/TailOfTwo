using UnityEngine;

public class GrowableDestroyable : Interactable
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] growthStages; // 0 = base, 1 = halfway (opt), 2 = fully-grown
    public Sprite destroyedSprite;

    private int currentStage = 0;
    private bool isDestroyed = false;

    public void Grow() {
        if (isDestroyed) {
            ResetInteraction();
        }
        else if (currentStage < growthStages.Length) {
            spriteRenderer.sprite = growthStages[currentStage];
            currentStage++;
        }
    }

    public void Destroy() {
        if (spriteRenderer != null && destroyedSprite != null && !isDestroyed) {
            spriteRenderer.sprite = destroyedSprite;
            currentStage = 0;
            isDestroyed = true;
        }
    }

    public override void ResetInteraction() {
        spriteRenderer.sprite = baseSprite;
        currentStage = 0;
        isDestroyed = false;
    }
}