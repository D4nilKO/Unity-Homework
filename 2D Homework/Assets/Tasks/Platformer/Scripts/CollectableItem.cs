using UnityEngine;
using UnityEngine.Events;

namespace Tasks.Platformer.Scripts
{
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> _collected;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
            {
                _collected?.Invoke(player.gameObject);
                
                gameObject.SetActive(false);
            }
        }
    }
}