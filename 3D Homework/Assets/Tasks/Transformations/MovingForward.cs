using UnityEngine;

namespace Tasks.Transformations
{
    public class MovingForward : MonoBehaviour
    {
        [SerializeField] [Range(0f, 40f)] private float _speed = 10f;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }
    }
}