using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        private void OnEnable()
        {
            HealthPickup.HealthPickedUp += OnHealthPickedUp;
        }

        private void OnDisable()
        {
            HealthPickup.HealthPickedUp -= OnHealthPickedUp;
        }

        private void OnHealthPickedUp(float value)
        {
            healthSlider.value += value;
        }

        public void TakeDamage(float damage)
        {
            healthSlider.value -= damage;
            Debug.Log(healthSlider.value);
            if (healthSlider.value <= 0)
            {
                Debug.Log("Game Over");
            }
        }
    }
}