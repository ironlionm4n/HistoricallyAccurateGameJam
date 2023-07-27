using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] private float healthValue = 50f;
        private AudioSource healthPickupAudioSource;
        
        public static event Action<float> HealthPickedUp;

        private void Start()
        {
            healthPickupAudioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                healthPickupAudioSource.Play();
                HealthPickedUp?.Invoke(healthValue);
                Destroy(gameObject, 1f);
            }
        }
    }
}