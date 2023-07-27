using System;
using UnityEngine;
using UnityEngine.UI;
using Player.AudioManagers;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private PlayerHurtAudioManager playerHurtAudioManager;
        [SerializeField] private AudioSource hurtAudioSource;

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
            hurtAudioSource.PlayOneShot(playerHurtAudioManager.GetRandomAudioClip());
            healthSlider.value -= damage;
            Debug.Log(healthSlider.value);
            if (healthSlider.value <= 0)
            {
                Debug.Log("Game Over");
            }
        }
    }
}