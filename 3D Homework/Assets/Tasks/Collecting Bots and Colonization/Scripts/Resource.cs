using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceMaterial _resourceMaterial;
        [SerializeField] [Min(1)] private int _amount = 1;

        public ResourceMaterial ResourceType => _resourceMaterial;
        public int Amount => _amount;
    }
}