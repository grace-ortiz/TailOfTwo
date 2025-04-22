using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;



public abstract class Interactable : MonoBehaviour {
    public Sprite baseSprite; 
    public SpriteRenderer spriteRenderer;
    private Coroutine resetCoroutine;
    public PolygonCollider2D polygonCollider;
    public BoxCollider2D boxCollider;
    public float triggerBufferX = 1.0f;
    public float triggerBufferY = 1.0f;
    public bool useAnimation = true;
    protected Animator anim;
    public float morphDuration = 0.5f;

    [field: SerializeField] public EventReference fmodEventPath {get; private set;}
    protected EventInstance eventInstance;
    

    public abstract void ResetInteraction();
    public abstract void OnInteract();

    void Start()
    {
        UpdateColliderShape();
        anim = GetComponent<Animator>();
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

        UpdateBoxCollider(triggerBufferX, triggerBufferY);
    }

    public void UpdateBoxCollider(float triggerBufferX, float triggerBufferY)
    {
        if (boxCollider == null || spriteRenderer == null || spriteRenderer.sprite == null)
            return;

        Bounds bounds = spriteRenderer.sprite.bounds;

        float colliderWidth = bounds.size.x + triggerBufferX;
        float colliderHeight = bounds.size.y + triggerBufferY;

        boxCollider.size = new Vector2(colliderWidth, colliderHeight);
        boxCollider.offset = spriteRenderer.sprite.bounds.center;
    }

    public IEnumerator SmoothInteract(Sprite newSprite) {
        Sprite oldSprite = spriteRenderer.sprite;

        // old paths
        List<Vector2[]> oldPaths = new List<Vector2[]>();
        for (int i = 0; i < oldSprite.GetPhysicsShapeCount(); i++) {
            var path = new List<Vector2>();
            oldSprite.GetPhysicsShape(i, path);
            oldPaths.Add(path.ToArray());
        }

        // read shape of new sprie
        spriteRenderer.sprite = newSprite;
        List<Vector2[]> newPaths = new List<Vector2[]>();
        for (int i = 0; i < newSprite.GetPhysicsShapeCount(); i++) {
            var path = new List<Vector2>();
            newSprite.GetPhysicsShape(i, path);
            newPaths.Add(path.ToArray());
        }

        // set back to old after quickly reading
        spriteRenderer.sprite = oldSprite;

        //  only interpolate if the path counts match
        if (oldPaths.Count != newPaths.Count) {
            spriteRenderer.sprite = newSprite;
            UpdateColliderShape();
            OnInteract();
            
            yield break;
        }

        float timer = 0f;
        while (timer < morphDuration) {
            float t = timer / morphDuration;
            polygonCollider.pathCount = oldPaths.Count;

            // get the highest Y points of both paths for smooth transition
            float fromTopY = GetHighestY(oldPaths);
            float toTopY = GetHighestY(newPaths);
            float flatYThisFrame = Mathf.Lerp(fromTopY, toTopY, t);

            for (int i = 0; i < oldPaths.Count; i++) {
                Vector2[] from = oldPaths[i];
                Vector2[] to = newPaths[i];
                Vector2[] interpolated = new Vector2[from.Length];

                for (int j = 0; j < from.Length; j++) {
                    if (j < to.Length) {
                        bool isTopPoint = Mathf.Approximately(from[j].y, fromTopY) && Mathf.Approximately(to[j].y, toTopY);

                        if (isTopPoint) {
                            interpolated[j] = new Vector2(
                                Mathf.Lerp(from[j].x, to[j].x, t),
                                flatYThisFrame
                            );
                        } else {
                            interpolated[j] = Vector2.Lerp(from[j], to[j], t);
                        }
                    }
                    else {
                        interpolated[j] = from[j]; // fallback
                    }
                }

                polygonCollider.SetPath(i, interpolated);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.sprite = newSprite;
        UpdateColliderShape();
        OnInteract();
        
    }

    private float GetHighestY(List<Vector2[]> paths) {
        float highest = float.MinValue;
        foreach (var path in paths) {
            foreach (var point in path) {
                if (point.y > highest)
                    highest = point.y;
            }
        }
        return highest;
    }
}
