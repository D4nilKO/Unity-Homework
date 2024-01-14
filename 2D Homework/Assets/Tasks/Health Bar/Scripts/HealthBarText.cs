using Tasks.Platformer.Scripts;
using TMPro;
using UnityEngine;

namespace Tasks.Health_Bar.Scripts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HealthBarText : MonoBehaviour
    {
        [SerializeField] protected Health _health;
        
        private TextMeshProUGUI _barText;
        
        private void Awake()
        {
            _barText = gameObject.GetComponent<TextMeshProUGUI>();
        }
        
        private void OnEnable()
        {
            _health.HealthChanged += UpdateBar;
        }

        private void OnDisable()
        {
            _health.HealthChanged -= UpdateBar;
        }

        private void UpdateBar(float health)
        {
            _barText.text = $"{health} / {_health.MaxHealth}";
        }
    }
}