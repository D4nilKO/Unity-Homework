using DG.Tweening;
using UnityEngine;

namespace Tasks.DOTween_Practice.Scripts
{
    public class Move : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _distance;

        private void Start()
        {
            transform.DOMove(transform.position + Vector3.right * _distance, _duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }
    }
}