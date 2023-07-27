using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public Light lightSource;
    public float minOnDuration = 0.5f;   // Minimum duration in seconds the light stays on
    public float maxOnDuration = 1.5f;   // Maximum duration in seconds the light stays on
    public float minOffDuration = 0.1f;  // Minimum duration in seconds the light stays off
    public float maxOffDuration = 0.5f;  // Maximum duration in seconds the light stays off

    private void Start()
    {
        // Start the random flickering
        StartCoroutine(RandomFlicker());
    }

    private System.Collections.IEnumerator RandomFlicker()
    {
        while (true)
        {
            // Switch the light on
            lightSource.enabled = true;

            // Wait for a random duration between minOnDuration and maxOnDuration
            float onDuration = Random.Range(minOnDuration, maxOnDuration);
            yield return new WaitForSeconds(onDuration);

            // Switch the light off
            lightSource.enabled = false;

            // Wait for a random duration between minOffDuration and maxOffDuration
            float offDuration = Random.Range(minOffDuration, maxOffDuration);
            yield return new WaitForSeconds(offDuration);
        }
    }
}
