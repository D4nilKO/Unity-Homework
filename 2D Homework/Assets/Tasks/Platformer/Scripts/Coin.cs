using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _amount;
        public int Amount => _amount;
    }
}