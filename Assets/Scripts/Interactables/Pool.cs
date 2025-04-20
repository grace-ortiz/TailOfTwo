using UnityEngine;
using System.Collections.Generic;

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
        if (isInteracted) return;

        if (player != null)
        {
            player.SetMaxCharges(player.maxCharges + 1);
            player.RecallAllCharges();
            isInteracted = true;

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