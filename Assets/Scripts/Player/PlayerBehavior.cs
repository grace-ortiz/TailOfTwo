using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    private Interactable currentInteractable;
    private float resetDuration = 5f;

    void Update() {
        if (Input.GetKeyDown(KeyCode.O)) { // grow
            if (currentInteractable is Growable growable) {
                growable.Grow();
                growable.StartResetTimer(resetDuration);
            }
            else if (currentInteractable is GrowableDestroyable growableDestroyable) {
                growableDestroyable.Grow();
                growableDestroyable.StartResetTimer(resetDuration);
            }
        }
        else if (Input.GetKeyDown(KeyCode.P)) { // destroy
            if (currentInteractable is Destroyable destroyable) {
                destroyable.Destroy();
                destroyable.StartResetTimer(resetDuration);
            }
            else if (currentInteractable is GrowableDestroyable growableDestroyable) {
                growableDestroyable.Destroy();
                growableDestroyable.StartResetTimer(resetDuration);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)) { // recall
            if (currentInteractable != null)
                currentInteractable.CancelResetTimer();
                currentInteractable.ResetInteraction();
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        print("entered collider");
        if (!collider.CompareTag("interactable")) return;

        Interactable interactable = collider.GetComponent<Interactable>();
        if (interactable != null) {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        print("exited collider");
        if (!collider.CompareTag("interactable")) return;

        if (collider.GetComponent<Interactable>() == currentInteractable) {
            currentInteractable = null;
        }
    }

    public void SetCurrentInteractable(Interactable interactable) {
        currentInteractable = interactable;
    }
}