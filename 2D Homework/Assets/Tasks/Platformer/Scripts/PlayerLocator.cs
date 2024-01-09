using System;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class PlayerLocator : MonoBehaviour
    {
        [SerializeField] private float _visionRange = 10f;

        private bool _isPlayerFounded;

        public event Action<Player> PlayerFounded;
        public event Action PlayerLost;

        private void Update()
        {
            Locate();
        }

        private void Locate()
        {
            Collider2D player =
                Physics2D.OverlapCircle(transform.position, _visionRange, LayerMask.GetMask(nameof(Player)));

            if (player && _isPlayerFounded == false)
            {
                PlayerFounded?.Invoke(player.GetComponent<Player>());
                _isPlayerFounded = true;
            }
            else if (player == null && _isPlayerFounded)
            {
                PlayerLost?.Invoke();
                _isPlayerFounded = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _visionRange);
        }
    }
}