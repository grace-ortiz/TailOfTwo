using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AmbienceCavernStart : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.SetAmbienceParameter("Cavern Intensity",0.2f);
    }
}
