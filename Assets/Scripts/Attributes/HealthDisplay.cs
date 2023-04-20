using UnityEngine;
using TMPro;
using System;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health _health;

        private void Awake()
        {
            _health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            GetComponent<TMP_Text>().text = String.Format("{0:0}/{1:0}", _health.GetHealthPoint(), _health.GetMaxHealthPoint());
        }
    }
}