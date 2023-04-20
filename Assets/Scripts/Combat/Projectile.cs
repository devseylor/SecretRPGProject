using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float _speed = 10f;
        [SerializeField] bool _isHomeing;
        [SerializeField] ParticleSystem _hitEffect = null;
        [SerializeField] float maxLifeTime = 10;
        [SerializeField] float _lifeAfterDestroyImpact = 2;
        [SerializeField] GameObject[] _destroyOnHit = null;
        [SerializeField] UnityEvent onProjectileHit;

        Health _target = null;
        GameObject _instigator = null;
        float _damage = 0;
        
        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (_target == null) return;
            if (_isHomeing && !_target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this._target = target;
            this._damage = damage;
            this._instigator = instigator;

            Destroy(gameObject,maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = _target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null)
            {
                return _target.transform.position;
            }
            return _target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != _target || _target.IsDead()) return;
            _speed = 0;
            
            _target.TakeDamage(_instigator, _damage);

            onProjectileHit.Invoke();
            if (_hitEffect)
            {
                Instantiate(_hitEffect.gameObject, GetAimLocation(), Quaternion.identity);
            }
            foreach(GameObject toDestroy in _destroyOnHit)
            {
                Destroy(toDestroy);
            }
            Destroy(gameObject, _lifeAfterDestroyImpact);
        }
    }
}
