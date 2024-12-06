using DG.Tweening;
using UnityEngine;

namespace Core.GridElements.GridPawns.Effect
{
    public class BaseGridObjectEffect : MonoBehaviour
    {
        private Sequence _shakeSeq;
        private Tween _shiftTween;

        public void Shake()
        {
            _shakeSeq?.Kill();
            _shakeSeq = DOTween.Sequence()
                .Append(transform.DORotate(new Vector3(0f, 0f, -45f), 0.05f).SetEase(Ease.OutQuad))
                .Append(transform.DORotate(new Vector3(0f, 0f, 0f), 0.05f).SetEase(Ease.InQuad))
                .Append(transform.DORotate(new Vector3(0f, 0f, 45f), 0.05f).SetEase(Ease.InQuad))
                .Append(transform.DORotate(new Vector3(0f, 0f, 0f), 0.05f).SetEase(Ease.OutQuad));
        }

        public Tween Shift(Vector3 targetPosition, float duration, Ease easeType, float? overshoot = null)
        {
            _shiftTween?.Kill(); // Ensure only one tween sequence is active

            _shiftTween = overshoot.HasValue
                ? transform.DOMove(targetPosition, duration).SetEase(easeType, overshoot.Value)
                : transform.DOMove(targetPosition, duration).SetEase(easeType);

            return _shiftTween;
        }
    }
}
