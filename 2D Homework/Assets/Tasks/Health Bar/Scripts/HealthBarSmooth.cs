using System.Collections;
using UnityEngine;

namespace Tasks.Health_Bar.Scripts
{
    public class HealthBarSmooth : HealthBar
    {
        [SerializeField] [Range(0.001f, 0.1f)] private float _step = 0.001f;

        private Coroutine _currentCoroutine;

        protected override void OnDisableAdditional()
        {
            if (_currentCoroutine == null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }

        protected override void UpdateBar(float health)
        {
            StartFillBar(health);
        }

        private void StartFillBar(float targetHealth)
        {
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(SmoothFillBar(targetHealth));
            }
            else
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(SmoothFillBar(targetHealth));
            }
        }

        private IEnumerator SmoothFillBar(float targetValue)
        {
            targetValue /= _health.MaxHealth;

            while (Bar.value != targetValue)
            {
                Bar.value = Mathf.MoveTowards(Bar.value, targetValue, _step);;

                yield return null;
            }
        }
    }
}