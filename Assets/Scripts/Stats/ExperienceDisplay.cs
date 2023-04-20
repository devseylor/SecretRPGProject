using UnityEngine;
using TMPro;
using System;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience _experience;

        private void Awake()
        {
            _experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            GetComponent<TMP_Text>().text = String.Format("{0}", _experience.GetPoints());
        }
    }
}