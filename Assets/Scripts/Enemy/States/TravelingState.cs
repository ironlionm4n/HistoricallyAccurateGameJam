using UnityEngine;

namespace Enemy.States
{
    public class TravelingState : IState
    {
        private GameObject _enemyGameObject;
        private GameObject _playerGameObject;
        private Transform _targetWaypoint;
        private float _moveSpeed;
        private float _detectionDistance;
        
        public TravelingState(GameObject enemy, Transform startingWaypoint, float moveSpeed, GameObject playerGameObject, float detectionDistance)
        {
            _enemyGameObject = enemy;
            _playerGameObject = playerGameObject;
            _targetWaypoint = startingWaypoint;
            _moveSpeed = moveSpeed;
            _detectionDistance = detectionDistance;
        }
        
        public void Enter()
        {
            _enemyGameObject.transform.LookAt(_targetWaypoint);
        }

        public void Execute()
        {
            // look for player

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
            Debug.Log(dist);
            return dist < 1f;
        }

        public bool HasFoundPlayer()
        {
            return Vector3.Distance(_enemyGameObject.transform.position, _playerGameObject.transform.position) <= _detectionDistance;
        }
    }
}