using System;
using System.Collections;
using UnityEngine;

namespace Tasks.Transformations
{
    public class ScalingSize : MonoBehaviour
    {
        [SerializeField] private float _timeToFullScale = 2f;

        [SerializeField] private float _maxScale = 2f;
        private float _startScale;

        [SerializeField] private bool _looping = true;

        private void Start()
        {
            _startScale = GetCurrentScale();

            StartCoroutine(Scale(_startScale, _maxScale));
        }

        private IEnumerator Scale(float startScale, float targetScale)
        {
            float currentTime = 0;

            while (currentTime < _timeToFullScale)
            {
                currentTime += Time.deltaTime;
                transform.localScale =
                    Vector3.one * Mathf.Lerp(startScale, targetScale, currentTime / _timeToFullScale);

                yield return null;
            }

            if (_looping == false) yield break;
            
            float currentTargetScale = Math.Abs(GetCurrentScale() - targetScale) < 0.01f ? startScale : targetScale;
            StartCoroutine(Scale(GetCurrentScale() , currentTargetScale));
        }

        private float GetCurrentScale()
        {
            Vector3 localScale = transform.localScale;
            return Mathf.Max(localScale.x, localScale.y, localScale.z);
        }
    }
}