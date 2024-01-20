using System;
using System.Collections;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(ObjectLocator))]
    [RequireComponent(typeof(Health))]
    public class VampiсAttack : MonoBehaviour
    {
        [SerializeField] [Min(0.1f)] private float _tickDamage = 0.1f;
        [SerializeField] [Min(0.1f)] private float _timeToTick = 0.1f;
        [SerializeField] [Min(0.1f)] private float _duration = 6f;
        
        [SerializeField] private KeyCode _keyCode = KeyCode.E;

        [SerializeField] private LayerMask _layerMask;
        
        // private bool _isTargetFounded;

        private Coroutine _currentCoroutine;

        private ObjectLocator _objectLocator;
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _objectLocator = GetComponent<ObjectLocator>();
        }

        private void OnEnable()
        {
            _objectLocator.TargetFounded += StartDamaging;
            _objectLocator.TargetLost += StopDamaging;
        }

        private void OnDisable()
        {
            StopDamaging();
            
            _objectLocator.TargetFounded -= StartDamaging;
            _objectLocator.TargetLost -= StopDamaging;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                StopDamaging();
                
                _objectLocator.Locate();
            }
            else if (_objectLocator.IsTargetAlreadyFounded)
            {
                _objectLocator.Locate();
            }
        }

        private void StartDamaging(GameObject target)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            StartCoroutine(StealingHealth(target));
        }

        private void StopDamaging()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }

        private IEnumerator StealingHealth(GameObject target)
        {
            WaitForSeconds tick = new(_timeToTick);
            
            // _isTargetFounded = true;

            int countOfTicks = Mathf.CeilToInt(_duration / _timeToTick);

            for (int i = 0; i < countOfTicks; i++)
            {
                StealHealth(target, _tickDamage);

                yield return tick;
            }
            
            _objectLocator.ClearFlag();
        }

        private void StealHealth(GameObject target, float health)
        {
            target.GetComponent<Health>().ApplyDamage(health);
            _health.Heal(health);
        }
    }
}