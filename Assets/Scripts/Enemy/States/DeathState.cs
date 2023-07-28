using UnityEngine;

namespace Enemy.States
{
    public class DeathState : IState
    {
        public string name = "Death";
        private float _deathTimer = 1f;
        private readonly EnemyBehavior _enemyBehavior;
        private AudioSource _deathAudioSource;

        public DeathState(EnemyBehavior enemyBehavior, AudioSource deathAudioSource)
        {
            _enemyBehavior = enemyBehavior;
            _deathAudioSource = deathAudioSource;
        }
        
        public void Enter()
        {
            _deathAudioSource.Play();
        }

        public void Execute()
        {
            Debug.Log(name);
            _deathTimer -= Time.deltaTime;
            if (_deathTimer <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _enemyBehavior.Die();
        }

        public void Exit()
        {
            
        }
    }
}