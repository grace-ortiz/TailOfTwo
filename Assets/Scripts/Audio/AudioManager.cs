using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Dynamic;
using System;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private EventInstance musicEventInstance;
    private EventInstance ambienceEventInstance;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        instance = this;
        eventInstances = new List<EventInstance>();
    }

    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.ambienceLevelOne);
        AudioManager.instance.SetAmbienceParameter("Ambience Intensity",0.2f);
        InitializeMusic(FMODEvents.instance.musicLevelOne);
        InitializeAmbience(FMODEvents.instance.ambienceLevelTwo);
        AudioManager.instance.SetAmbienceParameter("Cavern Intensity",0.0f);
    }

    public void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = RuntimeManager.CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    public void SetAmbienceParameter(String parameterName, float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }
    public void SetMusicArea(MusicArea area)
    {
        musicEventInstance.setParameterByName("area", (float) area); 
    }    
    public void PlayOneShot(EventReference sounds, Vector3 worldpos)
    {
        RuntimeManager.PlayOneShot(sounds, worldpos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance; 
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = RuntimeManager.CreateInstance(musicEventReference);
        musicEventInstance.start();
    }
    private void CleanUp()
    {
        //stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
