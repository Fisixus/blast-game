using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.GridElements.GridPawns.Effect
{
    public class BaseGridObjectEffect : MonoBehaviour
    {
        private Sequence _shakeSeq;
        private Tween _shiftTween;
        private Tween _shiftTweenAsync;

        public void Shake(float duration = 0.05f)
        {
            _shakeSeq?.Kill();
            _shakeSeq = DOTween.Sequence()
                .Append(transform.DORotate(new Vector3(0f, 0f, -45f), duration).SetEase(Ease.OutQuad))
                .Append(transform.DORotate(new Vector3(0f, 0f, 0f), duration).SetEase(Ease.InQuad))
                .Append(transform.DORotate(new Vector3(0f, 0f, 45f), duration).SetEase(Ease.InQuad))
                .Append(transform.DORotate(new Vector3(0f, 0f, 0f), duration).SetEase(Ease.OutQuad));
        }

        public Tween Shift(Vector3 targetPosition, float duration, Ease easeType, float? overshoot = null)
        {
            _shiftTween?.Kill(); // Ensure only one tween sequence is active

            _shiftTween = overshoot.HasValue
                ? transform.DOMove(targetPosition, duration).SetEase(easeType, overshoot.Value)
                : transform.DOMove(targetPosition, duration).SetEase(easeType);

            return _shiftTween;
        }
        
        public async UniTask ShiftAsync(Vector3 targetPosition, float duration, Ease easeType, float? overshoot = null)
        {
            _shiftTweenAsync?.Kill(); // Ensure only one tween sequence is active

            var completionSource = new UniTaskCompletionSource();

            _shiftTweenAsync = overshoot.HasValue
                ? transform.DOMove(targetPosition, duration)
                    .SetEase(easeType, overshoot.Value)
                    .OnComplete(() => completionSource.TrySetResult())
                : transform.DOMove(targetPosition, duration)
                    .SetEase(easeType)
                    .OnComplete(() => completionSource.TrySetResult());

            await completionSource.Task;
        }
    }
}
