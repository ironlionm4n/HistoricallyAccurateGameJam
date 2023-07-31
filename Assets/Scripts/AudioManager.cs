using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioClip _bulletsWhizzing_Clip;
    [SerializeField] private AudioClip _gunshot_Clip;
    [SerializeField] private AudioClip _breath_Clip;
    [SerializeField] private AudioClip _enemyDeath_Clip;
    [SerializeField] private AudioClip _enemyHurt_Clip;

    public void PlayBulletsWhizzing()
    {
        _audiosource.PlayOneShot(_bulletsWhizzing_Clip);
    }
    public void PlayGunshot()
    {
        _audiosource.PlayOneShot(_gunshot_Clip);
    }
    public void PlayBreath()
    {
        _audiosource.PlayOneShot(_breath_Clip);
    }
    public void PlayEnemyDeath()
    {
        _audiosource.PlayOneShot(_enemyDeath_Clip);
    }
    public void PlayEnemyHurt()
    {
        _audiosource.PlayOneShot(_enemyHurt_Clip);
    }
}
