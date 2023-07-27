using System.Collections;
using System.Collections.Generic;
using Enemy;
using Enemy.States;
using UnityEngine;
using UnityEngine.Serialization;

public class Target : MonoBehaviour
{
    [SerializeField] private float health = 150f;
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private AudioClip[] hurtAudioClips;
    [SerializeField] private AudioSource hurtAudioSource;
    [SerializeField] private EnemyBehavior enemyBehavior;
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            hurtAudioSource.clip = hurtAudioClips[Random.Range(0, hurtAudioClips.Length)];
            hurtAudioSource.Play();
        }
        else
        {
            enemyBehavior.ChangeState(new DeathState(enemyBehavior, deathAudioSource));
        }
    }
}
