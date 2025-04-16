using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos;

    public GameObject camera;

    public float parallaxRatio; // background movement relative to camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
    }

    // tutorial said to use fixed update to stop jittering
    void fixedUpdate()
    {
        float distance = camera.transform.position.x * parallaxRatio; // 0 = move w cam, 1 = won't move, 0.5 = half speed

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
