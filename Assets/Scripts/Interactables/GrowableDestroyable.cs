using UnityEngine;

public class GrowableDestroyable : Interactable
{
    public Sprite[] growthStages;
    public Sprite destroyedSprite;

    public int currentStage = 0;
    public bool isDestroyed = false;

    public GDInteractType interactionType = GDInteractType.Grow;

    public enum GDInteractType {
        Grow,
        Destroy
    }

    public void Grow() {
        interactionType = GDInteractType.Grow;

        if (isDestroyed) {
            ResetInteraction();
        }
        else if (currentStage < growthStages.Length) {
            if (morphDuration == 0) {
                QuickGrow();
            }
            else {
                StartCoroutine(SmoothInteract(growthStages[currentStage]));
            }
        }
    }

    public void Destroy() {
        interactionType = GDInteractType.Destroy;

        if (spriteRenderer != null && destroyedSprite != null && !isDestroyed) {
            if (morphDuration == 0) {
                QuickDestroy();
            }
            else {
                StartCoroutine(SmoothInteract(destroyedSprite));
            }
        }
    }

    public void QuickGrow() {
        spriteRenderer.sprite = growthStages[currentStage];
        UpdateColliderShape();
        currentStage++;
        if (useAnimation) anim.SetInteger("currentStage", currentStage);
    }

    public void QuickDestroy() {
        spriteRenderer.sprite = destroyedSprite;
        UpdateColliderShape();
        isDestroyed = true;
        if (useAnimation) anim.SetBool("isDestroyed", true);
    }

    public override void OnInteract() {
        switch (interactionType) {
            case GDInteractType.Grow:
                Grow();
                break;
            case GDInteractType.Destroy:
                Destroy();
                break;
        }
    }

    public override void ResetInteraction() {
        spriteRenderer.sprite = baseSprite;
        UpdateColliderShape();
        currentStage = 0;
        isDestroyed = false;
    }
}