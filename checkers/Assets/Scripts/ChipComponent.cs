using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Checkers
{
    public class ChipComponent : BaseClickComponent, IPointerClickHandler
    {
        public CellComponent Pair { get; set; }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            Pair.AddAdditionalMaterial(focusMaterial, 1);
            CallBackEvent(Pair, true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            CallBackEvent(Pair, false);
        }

        public delegate void ClickEventHandler(ChipComponent component);
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEventHandler?.Invoke(this);
        }
        public event ClickEventHandler OnClickEventHandler;
    }
}
