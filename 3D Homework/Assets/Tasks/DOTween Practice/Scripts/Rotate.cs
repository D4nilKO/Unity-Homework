using DG.Tweening;
using UnityEngine;

namespace Tasks.DOTween_Practice.Scripts
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _angle;

        private void Start()
        {
            transform.DORotate(transform.eulerAngles + Vector3.forward * _angle, _duration)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }
    }
}