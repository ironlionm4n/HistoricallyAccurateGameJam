using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player.AudioManagers
{
    public class PlayerFootStepsAudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] footStepAudioClips;

        private AudioSource _audioSource;


        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayFootStepOneShot()
        {
            _audioSource.PlayOneShot(GetFootStepAudioClip());
        }
        
        public AudioClip GetFootStepAudioClip()
        {
            return footStepAudioClips[Random.Range(0, footStepAudioClips.Length)];
        }
    }
}