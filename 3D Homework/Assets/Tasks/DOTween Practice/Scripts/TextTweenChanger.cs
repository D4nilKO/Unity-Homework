using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tasks.DOTween_Practice.Scripts
{
    public class TextTweenChanger : MonoBehaviour
    {
        [SerializeField] private Text _textComponent;
        [SerializeField] private string _firstNewText;
        [SerializeField] private string _additionalText;
        [SerializeField] private string _secondNewText;
        [SerializeField] private float _duration;
        [SerializeField] private Color _color;
        
        private void Start()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_textComponent.DOText(_firstNewText, _duration).SetEase(Ease.Linear));
            sequence.Append(_textComponent.DOText(_additionalText, _duration).SetRelative());
            sequence.Append(_textComponent.DOText(_secondNewText, _duration, true, ScrambleMode.Custom, "*").SetEase(Ease.Linear));
            sequence.Append(_textComponent.DOColor(_color, _duration).SetEase(Ease.Linear));
        }
    }
}