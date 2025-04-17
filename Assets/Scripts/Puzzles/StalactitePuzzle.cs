using UnityEngine;

public class StalactitePuzzle : MonoBehaviour {
    public GrowableDestroyable stalac1;
    public GrowableDestroyable stalac2;
    public GameObject platform; 

    void Start() {
        platform.SetActive(false);
    }

    void Update() {
        if (stalac1.isDestroyed && stalac2.currentStage == 1) {
            platform.SetActive(true);
        }
    }
}