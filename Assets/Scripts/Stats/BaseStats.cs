using GameDevTV.Utils;
using System;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int _startingLevel = 1;
        [SerializeField] CharacterClass _characterClass;
        [SerializeField] Progression _progression = null;
        [SerializeField] GameObject _levelUpParticalEffect;
        [SerializeField] bool _shouldUseModifiers = false;

        public event Action onLevelUp;

        LazyValue<int> _currentLevel;

        Experience experience;

        private void Awake()
        {
            experience = GetComponent<Experience>();
            _currentLevel = new LazyValue<int>(CalculateLevel);
        }

        private void Start()
        {
            _currentLevel.ForceInit();
        }

        private void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > _currentLevel.value)
            {
                _currentLevel.value = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {   
            if (_levelUpParticalEffect)
            {
                Instantiate(_levelUpParticalEffect, transform.position, Quaternion.identity);
            }
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat) / 100);
        }

        private float GetBaseStat(Stat stat)
        {
            return _progression.GetStat(stat, _characterClass, GetLevel());
        }

        public int GetLevel()
        {
            return _currentLevel.value;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!_shouldUseModifiers) return 0;

            float total = 0;
            foreach (IModifierProvider percentage in GetComponents<IModifierProvider>())
            {
                foreach (float modifiers in percentage.GetPercentageModifier(stat))
                {
                    total += modifiers;
                }
            }
            return total;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if (!_shouldUseModifiers) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifiers in provider.GetAdditiveModifiers(stat))
                {
                    total += modifiers;
                }
            }
            return total;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return _startingLevel;

            float currentXP = experience.GetPoints();
            int penultimateLevel = _progression.GetLevels(Stat.ExperienceToLevelup, _characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPtoLevelup = _progression.GetStat(Stat.ExperienceToLevelup, _characterClass, level);
                if (XPtoLevelup > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}