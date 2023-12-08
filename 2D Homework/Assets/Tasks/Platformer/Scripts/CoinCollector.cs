using Tasks.Platformer.Scripts;
using UnityEngine;

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
            Destroy(coin.gameObject);
        }
    }
}