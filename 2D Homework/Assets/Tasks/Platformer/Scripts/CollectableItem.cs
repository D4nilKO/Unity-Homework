using UnityEngine;
using UnityEngine.Events;

namespace Tasks.Platformer.Scripts
{
    public class CollectableItem : MonoBehaviour
    {
        public UnityEvent<GameObject> _onCollect;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
            {
                _onCollect?.Invoke(player.gameObject);
                
                gameObject.SetActive(false);
            }
        }
    }
}