﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Checkers
{
    public class CellComponent : BaseClickComponent, IPointerClickHandler
    {

        public  Dictionary<NeighborType, CellComponent> _neighbors = new Dictionary<NeighborType, CellComponent>();


        /// <summary>
        /// Возвращает соседа клетки по указанному направлению
        /// </summary>
        /// <param name="type">Перечисление направления</param>
        /// <returns>Клетка-сосед или null</returns>
        public CellComponent GetNeighbors(NeighborType type)
        {
            CellComponent cell;
            return _neighbors.TryGetValue(type, out cell) ? cell : null;
        }
        
        public ChipComponent Pair { get; set; }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            AddAdditionalMaterial(focusMaterial,1); 
            CallBackEvent(this, true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            RemoveAdditionalMaterial(1);
            CallBackEvent(this, false);
        }

        public event ClickEventHandlerCell OnClickEventHandlerCell;
       

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEventHandlerCell?.Invoke(this);
        }

        public delegate void ClickEventHandlerCell(CellComponent component);

        /// <summary>
        /// Конфигурирование связей клеток
        /// </summary>


    }

    /// <summary>
    /// Тип соседа клетки
    /// </summary>
    public enum NeighborType : byte
    {
        /// <summary>
        /// Клетка сверху и слева от данной
        /// </summary>
        TopLeft,
        /// <summary>
        /// Клетка сверху и справа от данной
        /// </summary>
        TopRight,
        /// <summary>
        /// Клетка снизу и слева от данной
        /// </summary>
        BottomLeft,
        /// <summary>
        /// Клетка снизу и справа от данной
        /// </summary>
        BottomRight
    }
}