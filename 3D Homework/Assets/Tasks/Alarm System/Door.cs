using UnityEngine;
using UnityEngine.Events;

namespace Tasks.Alarm_System
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private UnityEvent _crookEntered = new();
        [SerializeField] private UnityEvent _crookExit = new();

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Crook crook))
            {
                _crookEntered?.Invoke();
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.TryGetComponent(out Crook crook))
            {
                _crookExit?.Invoke();
            }
        }
    }
}