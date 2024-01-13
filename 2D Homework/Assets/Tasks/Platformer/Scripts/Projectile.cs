using System.Collections;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _damage = 40f;

        [SerializeField] private float _speed = 5f;

        [SerializeField] private float _timeToDie = 5f;

        private Vector3 _direction = Vector3.right;
        private Coroutine _currentCoroutine;

        private void Start()
        {
            _currentCoroutine = StartCoroutine(Die());
        }

        private void Update()
        {
            Move();
        }

        private void OnDisable()
        {
            Stop();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }

        public void Init(Vector3 direction)
        {
            _direction = direction;
        }

        private void Stop()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }

        private IEnumerator Die()
        {
            WaitForSeconds timeToDie = new(_timeToDie);

            yield return timeToDie;

            gameObject.SetActive(false);
        }

        private void Move()
        {
            transform.Translate(_direction * (_speed * Time.deltaTime));
        }
    }
}