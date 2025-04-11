using UnityEngine;
using System.Collections;


public abstract class Interactable : MonoBehaviour {
    public Sprite baseSprite; 
    private Coroutine resetCoroutine;

    public abstract void ResetInteraction();

    public void StartResetTimer(float delay)
    {
        if (resetCoroutine != null)
            StopCoroutine(resetCoroutine);

        resetCoroutine = StartCoroutine(ResetAfterDelay(delay));
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetInteraction();
    }

    public void CancelResetTimer()
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }
    }
}
