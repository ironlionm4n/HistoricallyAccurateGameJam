using UnityEngine;

namespace Enemy.States
{
    public class TravelingState : IState
    {
        public string name = "Traveling";
        private GameObject _enemyGameObject;
        private GameObject _playerGameObject;
        private Transform _targetWaypoint;
        private float _moveSpeed;
        private float _detectionDistance;
        private Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public TravelingState(GameObject enemy, Transform startingWaypoint, float moveSpeed, GameObject playerGameObject, float detectionDistance, Animator animator)
        {
            _enemyGameObject = enemy;
            _playerGameObject = playerGameObject;
            _targetWaypoint = startingWaypoint;
            _moveSpeed = moveSpeed;
            _detectionDistance = detectionDistance;
            _animator = animator;
        }
        
        public void Enter()
        {
            _enemyGameObject.transform.LookAt(_targetWaypoint);
            _animator.SetBool(IsWalking, true);
        }

        public void Execute()
        {
            // look for player

            Debug.Log(name);
            
            var direction = (_targetWaypoint.position - _enemyGameObject.transform.position).normalized;
            _enemyGameObject.transform.position += direction * (_moveSpeed * Time.deltaTime);
        }

        public void Exit()
        {
            // stop moving to aim/shoot
        }

        public bool HasReachedDestination()
        {
            var dist = Vector3.Distance(_enemyGameObject.transform.position, _targetWaypoint.position);
            return dist < 1.5f;
        }

        public bool HasFoundPlayer()
        {
            return Vector3.Distance(_enemyGameObject.transform.position, _playerGameObject.transform.position) <= _detectionDistance;
        }
    }
}