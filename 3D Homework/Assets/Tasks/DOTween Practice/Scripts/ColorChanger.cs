using DG.Tweening;
using UnityEngine;

namespace Tasks.DOTween_Practice.Scripts
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] private Color _targetColor;
        [SerializeField] private float _duration;

        private Material _material;

        private void Start()
        {
            _material = GetComponent<Renderer>().material;
            
            _material.DOColor(_targetColor, _duration)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}