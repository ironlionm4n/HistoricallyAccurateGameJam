using UnityEngine;

namespace Enemy.States
{
    public class TravelingState : IState
    {
        private GameObject _enemyGameObject;
        private Transform _targetWaypoint;
        private float _moveSpeed;
        
        public TravelingState(GameObject enemy, Transform startingWaypoint, float moveSpeed)
        {
            _enemyGameObject = enemy;
            _targetWaypoint = startingWaypoint;
            _moveSpeed = moveSpeed;
        }
        
        public void Enter()
        {
            _enemyGameObject.transform.LookAt(_targetWaypoint);
        }

        public void Execute()
        {
            var direction = (_targetWaypoint.position - _enemyGameObject.transform.position).normalized;
            _enemyGameObject.transform.position += direction * (_moveSpeed * Time.deltaTime);
        }

        public void Exit()
        {
            
        }

        public bool HasReachedDestination()
        {
            var dist = Vector3.Distance(_enemyGameObject.transform.position, _targetWaypoint.position);
            Debug.Log(dist);
            return dist < 1f;
        }
    }
}