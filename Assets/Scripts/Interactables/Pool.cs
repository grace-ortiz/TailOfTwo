using UnityEngine;
using System.Collections.Generic;

public class Pool : Interactable {

    [SerializeField] private Transform mainRespawnPoint;

    private bool isInteracted = false;

    public void Start() {
        isInteracted = false;
    }
    public void Interact()
    {
        if (isInteracted) return;

        PlayerBehavior player = FindFirstObjectByType<PlayerBehavior>();
        if (player != null)
        {
            player.SetMaxCharges(player.maxCharges + 1);
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
    

    public override void ResetInteraction() {}

    public override void OnInteract() {}
    
}