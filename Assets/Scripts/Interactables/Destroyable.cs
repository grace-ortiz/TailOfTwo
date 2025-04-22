using UnityEngine;
using FMODUnity;

public class Destroyable : Interactable {
    public Sprite destroyedSprite;
    public bool isDestroyed { get; private set; } = false;
    public bool disableColliderOnDestroy = false;


    public void Destroy() {
        if (!isDestroyed && spriteRenderer != null && destroyedSprite != null) {

            if (!fmodEventPath.IsNull) {
                eventInstance = RuntimeManager.CreateInstance(fmodEventPath);
                RuntimeManager.PlayOneShot(fmodEventPath, this.transform.position);
            }
            else {
                Debug.Log("No FMOD event path!");
            }
            
            if (disableColliderOnDestroy) {
                polygonCollider.enabled = false;
            }

            if (morphDuration == 0) {
                QuickDestroy();
            }
            else {
                StartCoroutine(SmoothInteract(destroyedSprite));
            }
        }
    }

    public void QuickDestroy() {
        spriteRenderer.sprite = destroyedSprite;
        UpdateColliderShape();
        isDestroyed = true;
        if (useAnimation) anim.SetBool("isDestroyed", true);
    }

    public override void OnInteract() {
        isDestroyed = true;
        if (useAnimation) anim.SetBool("isDestroyed", true);
    }

    public override void ResetInteraction() {
        spriteRenderer.sprite = baseSprite;
        UpdateColliderShape();
        if (disableColliderOnDestroy) {
            polygonCollider.enabled = true;
        }
        isDestroyed = false;
        if (useAnimation) anim.SetBool("isDestroyed", false);
    }
}