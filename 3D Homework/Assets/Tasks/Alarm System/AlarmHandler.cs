using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks.Alarm_System
{
    [RequireComponent(typeof(AudioSource))]
    public class AlarmHandler : MonoBehaviour
    {
        [SerializeField] [Range(0.1f, 1f)] private float _speed;
        private AudioSource _audioSource;
        private Coroutine _currentCoroutine;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Crook crook))
            {
                TurnOnSiren();
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.TryGetComponent(out Crook crook))
            {
                TurnOffSiren();
            }
        }

        private IEnumerator SmoothChangeVolume(float targetVolume)
        {
            while (_audioSource.volume != targetVolume)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speed * Time.deltaTime);
                yield return null;
            }
        }

        private void TurnOnSiren()
        {
            _audioSource.volume = 0;

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(SmoothChangeVolume(1));
        }

        private void TurnOffSiren()
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(SmoothChangeVolume(0));
        }
    }
}