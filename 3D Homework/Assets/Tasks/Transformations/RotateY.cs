using UnityEngine;

namespace Tasks.Transformations
{
    public class RotateY : MonoBehaviour
    {
        [SerializeField] [Range(0, 40)] private int _rotationSpeed = 10;

        private void Update()
        {
            Spin();
        }

        private void Spin()
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
        }
    }
}