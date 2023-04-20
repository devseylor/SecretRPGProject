using UnityEngine;
using TMPro;
using System;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter _enemy;
        private void Awake()
        {
            _enemy = GameObject.FindWithTag("Player").GetComponent<Fighter>();

        }

        private void Update()
        {
            if(_enemy.GetTarget() != null)
            {
                Health health = _enemy.GetTarget();

                GetComponent<TMP_Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoint(), health.GetMaxHealthPoint());
            }
            else
            {
                GetComponent<TMP_Text>().text = "N/A";
            }
        }
    }
}