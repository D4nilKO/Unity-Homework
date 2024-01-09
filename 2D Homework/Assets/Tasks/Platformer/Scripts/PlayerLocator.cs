using System;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class PlayerLocator : MonoBehaviour
    {
        [SerializeField] private float _visionRange = 10f;

        private bool _playerFounded;

        public event Action<Player> OnPlayerLocated;
        public event Action OnPlayerMissing;

        private void Update()
        {
            Locate();
        }

        private void Locate()
        {
            Collider2D player =
                Physics2D.OverlapCircle(transform.position, _visionRange, LayerMask.GetMask(nameof(Player)));

            if (player && _playerFounded == false)
            {
                OnPlayerLocated?.Invoke(player.GetComponent<Player>());
                Debug.Log("Player found");
                _playerFounded = true;
            }
            else if (player == null && _playerFounded)
            {
                OnPlayerMissing?.Invoke();
                Debug.Log("Player missing");
                _playerFounded = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _visionRange);
        }
    }
}