using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharge {
    public Interactable interactable;
    public Coroutine recallCoroutine;
}

public class PlayerBehavior : MonoBehaviour {
    private Interactable currentInteractable;
    private List<ActiveCharge> activeCharges = new List<ActiveCharge>(4);
    private float resetDuration = 8f;
    public int maxCharges = 1;
    private Color interactColor = new Color(1.2f, 1.2f, 1.2f);
    public PlayerMovement playerMovement;

    void Start() {
        playerMovement.anim.SetInteger("currCharges", maxCharges - activeCharges.Count);
        Debug.Log("currCharges = " + (maxCharges - activeCharges.Count));
    }


    void Update() {
        if (!playerMovement.canControl) return;

        if (Input.GetKeyDown(KeyCode.O)) { // grow

            if (currentInteractable is Growable growable && activeCharges.Count < maxCharges) {
                if (growable.currentStage < growable.growthStages.Length) SpendCharge(growable);
                growable.Grow();
                growable.StartResetTimer(resetDuration);
            }
            else if (currentInteractable is GrowableDestroyable growableDestroyable  && activeCharges.Count < maxCharges) {
                if (growableDestroyable.currentStage < growableDestroyable.growthStages.Length || growableDestroyable.isDestroyed) SpendCharge(growableDestroyable);
                growableDestroyable.Grow();
                growableDestroyable.StartResetTimer(resetDuration);
            }
        }
        else if (Input.GetKeyDown(KeyCode.P)) { // destroy
            if (currentInteractable is Destroyable destroyable && activeCharges.Count < maxCharges) {
                if (!destroyable.isDestroyed) SpendCharge(destroyable);
                destroyable.Destroy();
                destroyable.StartResetTimer(resetDuration);
            }
            else if (currentInteractable is GrowableDestroyable growableDestroyable && activeCharges.Count < maxCharges) {
                if (!growableDestroyable.isDestroyed) SpendCharge(growableDestroyable);
                growableDestroyable.Destroy();
                growableDestroyable.StartResetTimer(resetDuration);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)) { // recall
            RecallAllCharges();
        }
        else if (Input.GetKeyDown(KeyCode.E)) { // interact
            if (currentInteractable is Plainable plainable) {
                plainable.Interact();
                plainable.StartResetTimer(1f);
            }
            if (currentInteractable is Pool pool) {
                pool.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        
        if (!collider.CompareTag("interactable") && !collider.CompareTag("interactableDanger")) return;
        Interactable interactable = collider.GetComponentInParent<Interactable>();
        if (interactable != null) {
            currentInteractable = interactable;
            currentInteractable.spriteRenderer.color = interactColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (!collider.CompareTag("interactable") && !collider.CompareTag("interactableDanger")) return;

        if (collider.GetComponentInParent<Interactable>() == currentInteractable) {
            currentInteractable.spriteRenderer.color = Color.white;
            currentInteractable = null;
        }
    }

    public void SetCurrentInteractable(Interactable interactable) {
        currentInteractable = interactable;
    }

    void SpendCharge(Interactable target) {
        if (activeCharges.Count >= maxCharges) {
            Debug.Log("No charges available!");
            return;
        }

        Coroutine recallCoroutine = StartCoroutine(AutoRecall(target));
        activeCharges.Add(new ActiveCharge { interactable = target, recallCoroutine = recallCoroutine });
        playerMovement.anim.SetInteger("currCharges", maxCharges - activeCharges.Count);
        Debug.Log("currCharges:" + (maxCharges - activeCharges.Count));
    }
    IEnumerator AutoRecall(Interactable target) {
        yield return new WaitForSeconds(resetDuration);
        RecallInteractable(target);
    }

    void RecallInteractable(Interactable target) {
        
        foreach (var c in activeCharges) {
        }

        int index = activeCharges.FindIndex(c => c.interactable.GetInstanceID() == target.GetInstanceID());
        if (index >= 0) {
            StopCoroutine(activeCharges[index].recallCoroutine);
            activeCharges.RemoveAt(index);
        } else {
            Debug.LogWarning("Could not find matching interactable in activeCharges list");
        }

        target.CancelResetTimer();
        target.ResetInteraction();
        playerMovement.anim.SetInteger("currCharges", maxCharges - activeCharges.Count);
        Debug.Log("currCharges:" + (maxCharges - activeCharges.Count));
    }

    public void RecallAllCharges() {

        if (activeCharges.Count != 0) {
            foreach (var charge in activeCharges) {
                if (charge.interactable != null) {
                    charge.interactable.CancelResetTimer();
                    charge.interactable.ResetInteraction();
                    StopCoroutine(charge.recallCoroutine);
                }
            }
        }

        activeCharges.Clear();
        playerMovement.anim.SetInteger("currCharges", maxCharges - activeCharges.Count);
        Debug.Log("currCharges:" + (maxCharges - activeCharges.Count));
    }

    public void SetMaxCharges(int newMax) {
        maxCharges = newMax;
        Debug.Log("Max charges set to: " + maxCharges);
    }
}