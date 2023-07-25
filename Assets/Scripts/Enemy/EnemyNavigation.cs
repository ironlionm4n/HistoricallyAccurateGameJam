using System.Collections;
using System.Collections.Generic;
using Enemy.States;
using UnityEngine;

namespace Enemy
{
    public class EnemyNavigation : MonoBehaviour
    {
        [SerializeField] private float idleTime;
        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private float moveSpeed;
        private IState _currentState;
        private int _currentWaypointIndex;

        // Start is called before the first frame update
        private void Start()
        {
            ChangeState(new IdleState(idleTime));
        }

        // Update is called once per frame
        private void Update()
        {
            _currentState.Execute();
            StateTransitionChecking();
        }

        private void StateTransitionChecking()
        {
            if (_currentState is TravelingState)
            {
                var travelingState = (TravelingState)_currentState;
                if (travelingState.HasReachedDestination()) ChangeState(new IdleState(idleTime));
            }

            if (_currentState is IdleState)
            {
                var idleState = (IdleState)_currentState;
                if (idleState.IsDoneIdling())
                    if (waypoints.Count > 0)
                        ChangeState(new TravelingState(gameObject, GetTargetWaypoint(), moveSpeed));
            }
        }

        private Transform GetTargetWaypoint()
        {
            var targetWaypoint = waypoints[_currentWaypointIndex];
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= waypoints.Count) _currentWaypointIndex = 0;

            return targetWaypoint;
        }

        private void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}