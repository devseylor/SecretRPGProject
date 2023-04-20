using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] float _weaponRange = 2f;
        [SerializeField] float _weaponDamage = 5f;
        [SerializeField] float _percentageDamageBonus = 0;
        [SerializeField] AnimatorOverrideController _animatorOverride = null;
        [SerializeField] Weapon _equippedPrefab = null;
        [SerializeField] bool _isRightHanded = true;
        [SerializeField] Projectile _projectile = null;

        const string _weaponName = "Weapon";

        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand,leftHand);

            Weapon weapon = null;

            if (_equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                weapon = Instantiate(_equippedPrefab, handTransform);
                weapon.gameObject.name = _weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
           
            if (_animatorOverride != null)
            {
                animator.runtimeAnimatorController = _animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

            return weapon;
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(_weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(_weaponName);
            }
            if(oldWeapon == null) return;
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (_isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }
        
        public bool HasProjectile()
        {
            return _projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(_projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

        public float GetRange()
        {
            return _weaponRange;
        }

        public float GetPercentageBonus()
        {
            return _percentageDamageBonus;
        }

        public float GetDamage()
        {
            return _weaponDamage;
        }
    }
}