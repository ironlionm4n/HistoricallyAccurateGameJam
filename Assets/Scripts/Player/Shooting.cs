using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float damage;
        [SerializeField] private ParticleSystem shootParticles;
        [SerializeField] private float fireRate;
        [SerializeField] private Animator gunAnimator;

        private float _nextTimeToFire = 0f;
        private static readonly int ShootHash = Animator.StringToHash("Shoot");

        void Update()
        {
            if (Input.GetMouseButton(0) && Time.time >= _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        private void Shoot()
        {
            shootParticles.Play();
            gunAnimator.SetTrigger(ShootHash);
            var mainCameraTransform = mainCamera.transform;
            if (Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out var hit, enemyLayer))
            {
                Debug.Log(hit.collider.name);    
                var target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }
    }
}