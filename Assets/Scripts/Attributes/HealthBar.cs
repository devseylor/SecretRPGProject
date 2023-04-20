using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes 
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health _healthComponent = null;

        [SerializeField] RectTransform _foreground = null;

        [SerializeField] Canvas _canvas = null;

        private void Start()
        {
            _canvas.enabled = false;
        }

        private void Update()
        {
            if (Mathf.Approximately(_healthComponent.GetFraction(), 0) || Mathf.Approximately(_healthComponent.GetFraction(),1))
            {
                _canvas.enabled = false;
                return;
            }
            _canvas.enabled = true;
            _foreground.localScale = new Vector3(_healthComponent.GetFraction(), 1, 1);
        }
    }
}

