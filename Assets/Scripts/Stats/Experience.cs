using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float _experiencePoints = 0;

        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            _experiencePoints += experience;
            onExperienceGained();
        }

        public object CaptureState()
        {
            return _experiencePoints;
        }

        public void RestoreState(object state)
        {
            _experiencePoints = (float)state;
        }

        public float GetPoints()
        {
            return _experiencePoints;
        }
    }
}