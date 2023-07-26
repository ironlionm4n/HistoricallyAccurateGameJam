using Player;
using UnityEngine;

namespace Enemy.States
{ 
    public class ShootingState : IState
    {
        private Animator _animator;
        private GameObject _enemyGameObject;
        private GameObject _playerGameObject;
        private float _detectionDistance;
        private Transform _shootingPosition;
        private float _timeBetweenShots = 2f;
        private float _currentShotTime;
        private LayerMask _playerMask;
        private float _weaponDamage;
        
        public ShootingState(GameObject enemyGameObject, GameObject playerGameObject, float detectionDistance, Transform shootingTransform, LayerMask playerMask, float weaponDamage)
        {
            _enemyGameObject = enemyGameObject;
            _playerGameObject = playerGameObject;
            _detectionDistance = detectionDistance;
            _shootingPosition = shootingTransform;
            _playerMask = playerMask;
            _weaponDamage = weaponDamage;
        }
        
        public void Enter()
        {
            _currentShotTime = 0f;
        }

        public void Execute()
        {
            _currentShotTime -= Time.deltaTime;
            var playerPosition = _playerGameObject.transform.position;
            _enemyGameObject.transform.LookAt(playerPosition);
            Debug.DrawRay(_shootingPosition.position, _shootingPosition.forward, Color.red);
            if (_currentShotTime <= 0)
            {
                _currentShotTime = _timeBetweenShots;
                if (Physics.Raycast(_shootingPosition.position, _shootingPosition.forward,out var raycastHit, Mathf.Infinity, _playerMask))
                {
                    Debug.Log(raycastHit.transform.name);
                    var playerHealth = raycastHit.collider.GetComponent<PlayerHealth>();
                    if (playerHealth)
                    {
                        playerHealth.TakeDamage(_weaponDamage);
                    }
                }
            }
        }

        public void Exit()
        {
            
        }

        public bool HasLostPlayer()
        {
            return Vector3.Distance(_enemyGameObject.transform.position, _playerGameObject.transform.position) >
                   _detectionDistance;
        }
    }
}