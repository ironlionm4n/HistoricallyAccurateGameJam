using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] private float healthValue = 50f;
        
        public static event Action<float> HealthPickedUp;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                HealthPickedUp?.Invoke(healthValue);
                Destroy(gameObject);
            }
        }
    }
}