using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] [Min(0.1f)] private float _damage = 0.1f;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player player))
            {
                Attack(player);
            }
        }

        private void Attack(Component player)
        {
            if (player.gameObject.TryGetComponent(out Health health))
            {
                health.ApplyDamage(_damage);
            }
        }
    }
}