using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool _isAlreadyTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if(!_isAlreadyTriggered && other.gameObject.tag == "Player")
            {
                _isAlreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }

        public object CaptureState()
        {
            return _isAlreadyTriggered;
        }

        public void RestoreState(object state)
        {
            _isAlreadyTriggered = (bool)state;
        }
    }
}
