using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class ForegroundFade : MonoBehaviour
{
    public float fadeDuration = 1f;

    private TilemapRenderer tilemapRenderer;
    private Color originalColor;
    private Coroutine currentFade;

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        originalColor = tilemapRenderer.material.color;
    }

    void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (currentFade != null) StopCoroutine(currentFade);
                currentFade = StartCoroutine(FadeToAlpha(0f));
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (currentFade != null) StopCoroutine(currentFade);
                currentFade = StartCoroutine(FadeToAlpha(originalColor.a));
            }
        }

        IEnumerator FadeToAlpha(float targetAlpha)
        {
            float elapsed = 0f;
            Color startColor = tilemapRenderer.material.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                tilemapRenderer.material.color = Color.Lerp(startColor, endColor, elapsed / fadeDuration);
                yield return null;
            }

            tilemapRenderer.material.color = endColor;
        }
}   
