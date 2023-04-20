using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SoundEffects
{
    public class AudioRandomizer : MonoBehaviour
    {

        [SerializeField] AudioClip[] _audioClips;
        
        private AudioClip RandomClip()
        {
            int randomNumber = Random.Range(0, _audioClips.Length);
            return _audioClips[randomNumber];
        }

        public void ChangeAudioClip()
        {
            GetComponent<AudioSource>().clip = RandomClip();
            GetComponent<AudioSource>().Play();
        }
    }
}