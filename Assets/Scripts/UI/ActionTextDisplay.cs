using DG.Tweening;
using TMPro;
using UnityEngine;
using Utils;

public class ActionTextDisplay : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI actionText;

   private void Awake()
   {
      EventManager.OnActionTextChange += ChangeActionText;
   }

   private void OnDisable()
   {
      EventManager.OnActionTextChange -= ChangeActionText;
   }

   private void ChangeActionText(string text)
   {
      
      Sequence sequence = AnimationHelper.ScaleSequence(actionText.gameObject.transform, 0.85f, 0.15f);
      actionText.text = text;
   }
}
