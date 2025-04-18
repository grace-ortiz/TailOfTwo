using UnityEngine;

public class PoolBarrierPuzzle : MonoBehaviour {
    public Destroyable egg;
    public GameObject barrier; 

    void Start() {
        barrier.SetActive(true);
    }

    void Update() {
        if (egg.isDestroyed) {
            barrier.SetActive(false);
        }
    }
}