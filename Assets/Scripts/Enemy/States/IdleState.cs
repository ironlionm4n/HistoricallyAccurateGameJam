using System;
using System.Collections;
using UnityEngine;

namespace Enemy.States
{
    
    public class IdleState : IState
    {
        private readonly float _idleTime;
        private float _elapsedTime;

        public IdleState(float idleTime)
        {
            _idleTime = idleTime;
        }
        
        public void Enter()
        {
            _elapsedTime = 0f;
        }

        public void Execute()
        {
            _elapsedTime += Time.deltaTime;
        }

        public void Exit()
        {
            
        }

        public bool IsDoneIdling()
        {
            return _elapsedTime >= _idleTime;
        }
    }
}