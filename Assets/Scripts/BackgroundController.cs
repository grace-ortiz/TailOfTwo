using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPosX, startPosY;
    // private float length;

    public GameObject camera;

    public float parallaxRatioX, parallaxRatioY; // background movement relative to camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        // length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // tutorial said to use fixed update to stop jittering
    void Update()
    {
        float distanceX = camera.transform.position.x * parallaxRatioX; // 0 = move w cam, 1 = won't move, 0.5 = half speed
        float distanceY = camera.transform.position.y * parallaxRatioY; // 0 = move w cam, 1 = won't move, 0.5 = half speed
        // float movement = camera.transform.position.x * (1 - parallaxRatio);
        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);
    
        // if(movement > startPos + length)
        // {
        //     startPos += length;
        // }
        // else if (movement < startPos - length)
        // {
        //     startPos -= length;
        // }
    }
}
