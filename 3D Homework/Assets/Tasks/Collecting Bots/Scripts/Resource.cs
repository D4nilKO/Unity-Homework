using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceMaterial _resourceMaterial;
        [SerializeField] [Min(1)] private int _amount = 1;
        
        public bool HasBot { get; private set; }

        public void SetBusy()
        {
            HasBot = true;
        }
        
        public int GetAmount()
        {
            return _amount;
        }

        public ResourceMaterial GetResourceType()
        {
            return _resourceMaterial;
        }
    }
}