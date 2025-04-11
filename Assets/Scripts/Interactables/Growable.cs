using UnityEngine;
public class Growable : Interactable {
    public SpriteRenderer spriteRenderer;
    public Sprite[] growthStages; // 0 = base, 1 = half-grown (opt), 2 = fully-grown
    protected int currentStage = 0;

    public void Grow() {
        if (spriteRenderer == null || growthStages == null || growthStages.Length == 0)
            return;

        if (currentStage < growthStages.Length) {
            spriteRenderer.sprite = growthStages[currentStage];
            currentStage++;
        }
    }

    public override void ResetInteraction() {
        currentStage = 0;
        spriteRenderer.sprite = baseSprite;
    }
}