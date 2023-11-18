using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletCreator : MonoBehaviour
{
    [SerializeField] public float _bulletSpeed;

    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private float _timeToShoot;
    
    public Transform _gun;

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        WaitForSeconds waitShooting = new (_timeToShoot);
        
        while (true)
        {
            Vector3 direction = (_gun.position - transform.position).normalized;
            Vector3 newBulletPosition = transform.position + direction;
            
            Rigidbody newBullet = Instantiate(_bulletPrefab, newBulletPosition, Quaternion.identity);
            
            newBullet.transform.up = direction;
            newBullet.velocity = direction * _bulletSpeed;

            yield return waitShooting;
        }
    }
}