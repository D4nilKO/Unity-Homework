using System.Collections;
using UnityEngine;

namespace Tasks.Health_Bar
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
            targetValue = Map(targetValue, 0, _health.MaxHealth, 0, 1);

            while (Bar.value != targetValue)
            {
                float value = Mathf.MoveTowards(Bar.value, targetValue, _step);
                Debug.Log(value);
                Bar.value = value;

                yield return null;
            }

            Debug.Log("End");
        }

        private float Map(float value, float inMin, float inMax, float outMin, float outMax)
        {
            return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }
    }
}