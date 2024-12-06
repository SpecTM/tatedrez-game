using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
   public static class AnimationHelper 
   {
      public static Sequence FadeSequence(Image image, float duration, 
         LoopType loopType = LoopType.Yoyo, Ease inEase = Ease.InBounce, Ease outEase = Ease.OutBounce)
      {
         return DOTween.Sequence()
            .Append(image.DOFade(0.7f, duration).SetEase(inEase))
            .Append(image.DOFade(1f, duration).SetEase(outEase))
            .SetLoops(-1, loopType);
      }

      public static Sequence ScaleSequence(Transform transform, float endValue, float duration)
      {
         return DOTween.Sequence()
            .Append(transform.DOScale(endValue, duration))
            .Append(transform.DOScale(1, duration));
      }

      public static void FadeImage(Image image, float endValue, float duration)
      {
         image.DOFade(endValue, duration);
      }

      public static void Scale(Transform transform, float endValue, float duration, Ease ease = Ease.Linear)
      {
         transform.DOScale(endValue, duration).SetEase(ease);
      }
   }
}
