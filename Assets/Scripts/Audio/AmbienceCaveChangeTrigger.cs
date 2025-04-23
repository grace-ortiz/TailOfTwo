using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceCaveChangeTrigger : MonoBehaviour
{
    [Header("Parameter Change")]
    [SerializeField] private string parameterName;
    [SerializeField] private float enterValue = 0.2f;
    [SerializeField] private float exitValue = 0f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            AudioManager.instance.SetAmbienceCaveParameter(parameterName, enterValue);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            AudioManager.instance.SetAmbienceCaveParameter(parameterName, exitValue);
        }
    }
}