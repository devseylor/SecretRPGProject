using RPG.Attributes;
using RPG.Control;
using System.Collections;
using UnityEngine;

namespace RPG.Combat 
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig _weapon = null;
        [SerializeField] float _healthToRestore = 0;
        [SerializeField] float _respownTime = 3;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                Pickup(other.gameObject);
            }
        }

        private void Pickup(GameObject subject)
        {
            if(_weapon != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(_weapon);
            }
            if(_healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(_healthToRestore);
            }
            StartCoroutine(HideForSeconds(_respownTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            HidePickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void HidePickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandelRaycast(PlayerController callingContoller)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingContoller.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}

