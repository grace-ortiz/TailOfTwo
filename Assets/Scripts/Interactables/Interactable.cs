using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public abstract class Interactable : MonoBehaviour {
    public Sprite baseSprite; 
    public SpriteRenderer spriteRenderer;
    private Coroutine resetCoroutine;
    public PolygonCollider2D polygonCollider;
    public BoxCollider2D boxCollider;
    public float triggerBuffer = 1.0f;
    

    public abstract void ResetInteraction();

    private void Awake() {
        if (polygonCollider == null)
            polygonCollider = GetComponent<PolygonCollider2D>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Debug.Log("Initial Position: " + transform.position);
        UpdateColliderShape();
    }

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

    public void UpdateColliderShape() {
        if (spriteRenderer.sprite == null || polygonCollider == null || boxCollider == null) return;

        polygonCollider.pathCount = spriteRenderer.sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < polygonCollider.pathCount; i++) {
            path.Clear();
            spriteRenderer.sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path);
        }

        UpdateBoxCollider(triggerBuffer);
    }

    public void UpdateBoxCollider(float triggerBuffer)
    {
        if (boxCollider == null || spriteRenderer == null || spriteRenderer.sprite == null)
            return;

        Bounds bounds = spriteRenderer.sprite.bounds;

        float colliderWidth = bounds.size.x + triggerBuffer;
        float colliderHeight = bounds.size.y + triggerBuffer;

        boxCollider.size = new Vector2(colliderWidth, colliderHeight);
        boxCollider.offset = spriteRenderer.sprite.bounds.center;

        print(boxCollider.size);
    }
}
