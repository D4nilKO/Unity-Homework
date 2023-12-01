using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    
    public class GroundCheck : MonoBehaviour
    {
        private const int GroundLayerNumber = 6;

        private PlayerMovement _playerMovement;

        private void Start()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == GroundLayerNumber)
            {
                Debug.Log("on ground!!!");
                _playerMovement.OnGround();
            }
        }
    }
}