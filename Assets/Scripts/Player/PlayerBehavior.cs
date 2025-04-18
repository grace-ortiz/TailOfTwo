using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    private Interactable currentInteractable;
    private float resetDuration = 5f;
    private Color interactColor = new Color(1.15f, 1.15f, 1.15f);

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
                print("current interactable: " + currentInteractable);
                currentInteractable.CancelResetTimer();
                currentInteractable.ResetInteraction();
                
        }
        else if (Input.GetKeyDown(KeyCode.E)) { // interact
            if (currentInteractable is Plainable plainable) {
                plainable.Interact();
                plainable.StartResetTimer(1f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        print("entered collider");
        if (!collider.CompareTag("interactable") && !collider.CompareTag("interactableDanger")) return;

        Interactable interactable = collider.GetComponent<Interactable>();
        if (interactable != null) {
            currentInteractable = interactable;
            currentInteractable.spriteRenderer.color = interactColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        print("exited collider");
        if (!collider.CompareTag("interactable") && !collider.CompareTag("interactableDanger")) return;

        if (collider.GetComponent<Interactable>() == currentInteractable) {
            currentInteractable.spriteRenderer.color = Color.white;
            currentInteractable = null;
        }
    }

    public void SetCurrentInteractable(Interactable interactable) {
        currentInteractable = interactable;
    }
}