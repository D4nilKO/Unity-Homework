using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(Wallet))]
    public class CoinCollector : MonoBehaviour
    {
        private Wallet _wallet;

        private void Start()
        {
            _wallet = GetComponent<Wallet>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Coin coin))
            {
                _wallet.Add(coin.Amount);
                coin.gameObject.SetActive(false);
            }
        }
    }
}