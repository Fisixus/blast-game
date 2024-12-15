using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIExtensions
{
    public class StartButton : Button
    {
        [SerializeField] private Image _leftImage;
        [SerializeField] private Image _rightImage;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            transform.parent.DOScale(Vector3.one * 0.85f, 0.15f).SetEase(Ease.Linear);
            _leftImage.DOColor(Color.gray, 0.15f);
            _rightImage.DOColor(Color.gray, 0.15f);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            transform.parent.DOScale(Vector3.one, 0.15f).SetEase(Ease.Linear);
            _leftImage.DOColor(Color.white, 0.15f);
            _rightImage.DOColor(Color.white, 0.15f);
        }
    }
}