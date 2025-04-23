using UnityEngine;
using FMODUnity;

public class Pool : Interactable {

    [SerializeField] private Transform mainRespawnPoint;

    private bool isInteracted = false;
    private PlayerBehavior player;

    public void Start() {
        isInteracted = false;
        player = FindFirstObjectByType<PlayerBehavior>();
        
    }
    public void Interact()
    {

        if (player != null)
        {
            if (!fmodEventPath.IsNull) {
                eventInstance = RuntimeManager.CreateInstance(fmodEventPath);
                RuntimeManager.PlayOneShot(fmodEventPath, this.transform.position);
            }
            else {
                Debug.Log("No FMOD event path!");
            }

            if (!isInteracted) {
                player.SetMaxCharges(player.maxCharges + 1);
                isInteracted = true;
            }

            player.RecallAllCharges();
            
            if (mainRespawnPoint != null)
            {
                player.transform.position = mainRespawnPoint.position;
                Debug.Log("Player respawned and gained a charge!");
            }
            else
            {
                Debug.LogWarning("Main respawn point is not assigned.");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        player.RecallAllCharges();
    }
    

    public override void ResetInteraction() {}

    public override void OnInteract() {}
    
}