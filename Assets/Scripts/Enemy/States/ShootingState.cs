using Player;
using UnityEngine;

namespace Enemy.States
{
    public class ShootingState : IState
    {
        public string name = "Shooting";
        private Animator _animator;
        private GameObject _enemyGameObject;
        private GameObject _playerGameObject;
        private float _detectionDistance;
        private Transform _shootingPosition;
        private float _timeBetweenShots = 2f;
        private float _currentShotTime;
        private LayerMask _playerMask;
        private float _weaponDamage;
        private AudioSource _gunShotAudioSource;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsFiring = Animator.StringToHash("IsFiring");

        public ShootingState(GameObject enemyGameObject, GameObject playerGameObject, float detectionDistance,
            Transform shootingTransform, LayerMask playerMask, float weaponDamage, AudioSource gunShotAudioSource, Animator animator)
        {
            _enemyGameObject = enemyGameObject;
            _playerGameObject = playerGameObject;
            _detectionDistance = detectionDistance;
            _shootingPosition = shootingTransform;
            _playerMask = playerMask;
            _weaponDamage = weaponDamage;
            _gunShotAudioSource = gunShotAudioSource;
            _animator = animator;
        }

        public void Enter()
        {
            _currentShotTime = 0f;
            _animator.SetBool(IsWalking, false);
            _animator.SetBool(IsFiring, true);
        }

        public void Execute()
        {
            Debug.Log(name);
            _currentShotTime -= Time.deltaTime;
            var playerPosition = _playerGameObject.transform.position;
            _enemyGameObject.transform.LookAt(playerPosition);
            Debug.DrawRay(_shootingPosition.position, _shootingPosition.forward * 10f, Color.red);
            if (_currentShotTime <= 0)
            {
                _currentShotTime = _timeBetweenShots;
                _gunShotAudioSource.PlayOneShot(_gunShotAudioSource.clip);
                if (Physics.Raycast(_shootingPosition.position, _shootingPosition.forward, out var raycastHit,
                        Mathf.Infinity, _playerMask))
                {
                    Debug.Log(raycastHit.transform.name);
                    var playerHealth = raycastHit.collider.GetComponent<PlayerHealth>();
                    if (playerHealth) playerHealth.TakeDamage(_weaponDamage);
                }
            }
        }

        public void Exit()
        {
            _animator.SetBool(IsFiring, false);
        }

        public bool HasLostPlayer()
        {
            return Vector3.Distance(_enemyGameObject.transform.position, _playerGameObject.transform.position) >
                   _detectionDistance;
        }
    }
}