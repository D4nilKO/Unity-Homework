using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceMaterial _resourceMaterial;
        [SerializeField][Min(1)] private int _amount;
        [SerializeField] private bool _collected;

        public void SetCollected()
        {
            _collected = true;
        }
    }
}