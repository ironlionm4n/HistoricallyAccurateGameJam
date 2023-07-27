using System;
using System.Collections;
using System.Collections.Generic;
using Guns;
using UnityEngine;
using UnityEngine.Rendering;

namespace Player
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private ParticleSystem shootParticles;
        [SerializeField] private Animator gunAnimator;
        [SerializeField] private GunScriptableObjects gunScriptableObject;
        [SerializeField] private AudioSource gunShotAudioSource;
        public GunScriptableObjects CurrentGunScriptableObject => gunScriptableObject;

        private float _nextTimeToFire = 0f;
        private static readonly int ShootHash = Animator.StringToHash("Shoot");
        private int _currentRounds;
        private static readonly int Reload = Animator.StringToHash("Reload");
        private static readonly int IsAiming = Animator.StringToHash("IsAiming");
        public static event Action GunWasFired;

        private void Start()
        {
            _currentRounds = gunScriptableObject.MagazineSize;
        }

        private void Update()
        {
            if (Input.GetMouseButton(1)) gunAnimator.SetBool(IsAiming, true);
            if(Input.GetMouseButtonUp(1)) gunAnimator.SetBool(IsAiming, false);
            if (Input.GetKeyDown(KeyCode.R)) ReloadGun();
            if (Input.GetMouseButton(0) && Time.time >= _nextTimeToFire && _currentRounds > 0) Shoot();
        }

        private void ReloadGun()
        {
            if (_currentRounds == gunScriptableObject.MagazineSize) return;

            gunAnimator.SetTrigger(Reload);
        }

        public void SetCurrentRoundsToMagazineSize()
        {
            _currentRounds = gunScriptableObject.MagazineSize;
        }

        private void Shoot()
        {
            _currentRounds--;
            GunWasFired?.Invoke();
            gunShotAudioSource.PlayOneShot(gunShotAudioSource.clip);
            _nextTimeToFire = Time.time + 1f / gunScriptableObject.FireRate;
            shootParticles.Play();
            if (!ShootingHelper._inAimState)
                gunAnimator.SetTrigger(ShootHash);
            var mainCameraTransform = mainCamera.transform;
            if (!Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out var hit,
                    enemyLayer)) return;

            var target = hit.transform.GetComponent<Target>();
            if (target != null) target.TakeDamage(gunScriptableObject.DamagePerBullet);

            if (_currentRounds <= 0) ReloadGun();
        }
    }
}