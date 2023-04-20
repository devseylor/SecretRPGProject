using UnityEngine;
using TMPro;
using System;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats _level;

        private void Awake()
        {
            _level = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            GetComponent<TMP_Text>().text =  _level.GetLevel().ToString();
        }
    }
}