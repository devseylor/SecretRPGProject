using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.DamageText
{
    public class DamageSpawner : MonoBehaviour
    {
        [SerializeField] DamageText _damageTextPrefab = null;

        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(_damageTextPrefab,transform);

            instance.SetValue(damageAmount);
        }
    }
}

