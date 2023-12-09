using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] [Min(0)] private int _money;

        public void Add(int amount)
        {
            if (amount < 0)
                return;

            _money += amount;
        }
    }
}