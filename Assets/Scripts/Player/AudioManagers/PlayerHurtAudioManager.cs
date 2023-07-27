using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.AudioManagers
{
    public class PlayerHurtAudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] hurtAudioClips;

        public AudioClip GetRandomAudioClip()
        {
            return hurtAudioClips[Random.Range(0, hurtAudioClips.Length)];
        }
    }
}