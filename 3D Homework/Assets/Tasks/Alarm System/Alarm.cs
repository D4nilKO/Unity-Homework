using System.Collections;
using UnityEngine;

namespace Tasks.Alarm_System
{
    [RequireComponent(typeof(AudioSource))]
    public class Alarm : MonoBehaviour
    {
        [SerializeField] [Range(0.1f, 1f)] private float _speed;

        private AudioSource _audioSource;
        private Coroutine _currentCoroutine;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private IEnumerator SmoothChangeVolume(float targetVolume)
        {
            while (_audioSource.volume != targetVolume)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speed * Time.deltaTime);
                yield return null;
            }
        }

        public void TurnOnSiren()
        {
            _audioSource.volume = 0;

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(SmoothChangeVolume(1));
        }

        public void TurnOffSiren()
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(SmoothChangeVolume(0));
        }
    }
}