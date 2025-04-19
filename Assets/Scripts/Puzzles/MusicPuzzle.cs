using UnityEngine;

public class MusicPuzzle : MonoBehaviour {
    public Plainable crystal1;
    public Plainable crystal2;
    public Plainable crystal3;
    public Plainable mushroom1;
    public Plainable mushroom2;
    public Plainable mushroom3;

    public GameObject barrier;

    private int nextItemHit = 0;
    private bool musicComplete = false;

    private Plainable[] correctOrder;

    void Start() {
        barrier.SetActive(true);
        nextItemHit = 0;

        correctOrder = new Plainable[] { crystal3, mushroom1, crystal1, mushroom2, crystal2, mushroom3 };

        foreach (var item in correctOrder) {
            item.musicPuzzle = this;
        }
    }

    void Awake()
    {
        ResetPuzzle();
    }

    public void OnItemInteracted(Plainable item) {
        if (musicComplete) return;

        if (item == correctOrder[nextItemHit]) {
            nextItemHit++;

            if (nextItemHit >= correctOrder.Length) {
                CompletePuzzle();
            }
        } else {
            ResetPuzzle();
        }
    }

    private void CompletePuzzle() {
        musicComplete = true;
        barrier.SetActive(false);
    }

    private void ResetPuzzle()
    {
        nextItemHit = 0;
        musicComplete = false;
    }
}