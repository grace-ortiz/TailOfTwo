using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    // Cat SFX
    [field: Header("Jump SFX")]
    [field: SerializeField] public FMODUnity.EventReference jump { get; private set; }
    
    [field: Header("Land SFX")]
    [field: SerializeField] public FMODUnity.EventReference land { get; private set; }

    [field: Header("Walk SFX")]
    [field: SerializeField] public FMODUnity.EventReference walk { get; private set; }
    
    [field: Header("Splat SFX")]
    [field: SerializeField] public FMODUnity.EventReference splat { get; private set;}

    [field: Header("Death SFX")]
    [field: SerializeField] public FMODUnity.EventReference death { get; private set;}
    
    
    //Abilities 
    [field: Header("Grow SFX")]
    [field: SerializeField] public FMODUnity.EventReference growPlantSound { get; private set;}

    [field: Header("Grow SFX")]
    [field: SerializeField] public FMODUnity.EventReference growMushroomSound { get; private set;}

    [field: Header("Destroy SFX")]
    [field: SerializeField] public FMODUnity.EventReference destroySound { get; private set;}


    // level 1 SFX
    [field: Header("Ambience Level 1")]
    [field: SerializeField] public FMODUnity.EventReference ambienceLevelOne { get; private set;}
    
    [field: Header("Music Level 1")]
    [field: SerializeField] public FMODUnity.EventReference musicLevelOne { get; private set;}
    
    
    // UI SFX
    [field: Header("Pause SFX")]
    [field: SerializeField] public FMODUnity.EventReference pauseSound {get; private set;}



    
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
