using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tasks.DOTween_Practice.Scripts
{
    public class TextTweenChanger : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private string _newText;
        [SerializeField] private string _additionalText;
        [SerializeField] private string _newNewText;
        [SerializeField] private float _duration;
        [SerializeField] private Color _color;
        
        private void Start()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_text.DOText(_newText, _duration).SetEase(Ease.Linear));
            sequence.Append(_text.DOText(_additionalText, _duration).SetRelative());
            sequence.Append(_text.DOText(_newNewText, _duration, true, ScrambleMode.Custom, "*").SetEase(Ease.Linear));
            sequence.Append(_text.DOColor(_color, _duration).SetEase(Ease.Linear));
        }
    }
}