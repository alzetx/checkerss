using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Checkers
{
    public class Handler : GenerateGameField
    {
        [Tooltip("выбери цвет шашек, за который будешь играть  :)"), SerializeField]
        public static ColorType YourColorChekers { get; private set; }
        [SerializeField] protected Material _enterMaterial;
        [SerializeField, Range (1,3)] protected float _speedMoveChip;
        [SerializeField] protected Material _choiceMove;
        [SerializeField] protected CameraRotate _mainCamera;
       [SerializeField] private GameObject eventsys; //todo
        [SerializeField] private CameraRotate _cameraRotate;
        private CellComponent[,] _cells;
        private List<ChipComponent> _chips;
        private BaseClickComponent _currentCell;
        private ChipComponent _chip;
        private ChipComponent _yetChip;
        private CellComponent _cellVariant1;
        private CellComponent _cellVariant2;
        private CellPath _cellPath;

        private void Start()
        {
            SwitchSide();
        }
        public void InitCell(CellComponent[,] cells )
        {
            _cells = cells;
            _cellPath = new CellPath(_cells);
        }

        public void InitChip(List<ChipComponent> chips)
        {
              foreach (var chip in chips)
             {
                 chip.OnClickEventHandler += LightChip;
             }  
        }

        private void RemoveLightNeighbore()
        {
            if (_cellVariant1 != null)
            {
                if (_cellVariant2 != null)
                {
                    _cellVariant2.RemoveAdditionalMaterial(1);
                    _cellVariant2.OnClickEventHandlerCell -= MoveChip;
                }
                _cellVariant1.RemoveAdditionalMaterial(1);
                _cellVariant1.OnClickEventHandlerCell -= MoveChip;
            }
        }

        private void LightNeighbore(ChipComponent chip, NeighborType left, NeighborType right)
        {
            RemoveLightNeighbore();

                if (chip.Pair.GetNeighbors(right) != null && chip.Pair.GetNeighbors(right).Pair == null)
            {
                _cellVariant2 = chip.Pair.GetNeighbors(right);
                _cellVariant2.AddAdditionalMaterial(_choiceMove, 1);
                _cellVariant2.OnClickEventHandlerCell += MoveChip;
            }
                if (chip.Pair.GetNeighbors(left) != null && chip.Pair.GetNeighbors(left).Pair == null)
            {
                _cellVariant1 = chip.Pair.GetNeighbors(left);
                _cellVariant1.AddAdditionalMaterial(_choiceMove, 1);
                _cellVariant1.OnClickEventHandlerCell += MoveChip;
            }
            
        }
        



        public void LightChip(ChipComponent chip)
        {
            _chip = chip;

            if (YourColorChekers == _chip.Color)
            {
                if (_yetChip != null)
                {
                 _yetChip.RemoveAdditionalMaterial(1);
                }

                _chip.AddAdditionalMaterial(_enterMaterial, 1);
                if (YourColorChekers == ColorType.Black)
                {
                    LightNeighbore(_chip, NeighborType.TopLeft, NeighborType.TopRight);
                }
                else
                {
                    LightNeighbore(_chip, NeighborType.BottomLeft, NeighborType.BottomRight);
                }
            }
            else
            {
                Debug.Log("Мне кажется вы играете за другую сторону");
            }
            _yetChip = _chip;
        }


        protected void SwitchSide()
        {
            if (YourColorChekers == ColorType.White)
            {
                StartCoroutine(_mainCamera.Rotate());
            }
            else
            {
                StartCoroutine(_mainCamera.Rotate());
            }
        }

        private void SwitchYourColorSide()
        {
            if (YourColorChekers == ColorType.White)
            {
                YourColorChekers = ColorType.Black;
            }
            else
            {
                YourColorChekers = ColorType.White;
            }
        }

        private void OnDisable()
        {
            foreach (var chip in _chips)
            {
                chip.OnClickEventHandler -= LightChip;
            }
        }

      

        private void MoveChip(CellComponent cellVariant)
        {
            {
                StartCoroutine(CorutineMoveChip(cellVariant));
                RemoveLightNeighbore();
                _chip.Pair.Pair = null;
                _chip.Pair = cellVariant;
                cellVariant.Pair = _chip;
                SwitchYourColorSide();
                StartCoroutine(_mainCamera.Rotate());
            }
        }
        public IEnumerator CorutineMoveChip(CellComponent cellVariant)
        {
            eventsys.SetActive(false);
            while (Vector3.Distance(_chip.transform.position, cellVariant.transform.position) >= 0.01)
            {
                _chip.transform.position = Vector3.Lerp(_chip.transform.position, cellVariant.transform.position, Time.deltaTime * _speedMoveChip);
                yield return null;
            }
            eventsys.SetActive(true);
        }
    }

   
}