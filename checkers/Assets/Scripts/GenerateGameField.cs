using System.Collections.Generic;
using UnityEngine;

namespace Checkers
{
    public class GenerateGameField : MonoBehaviour
    {

        [SerializeField] public static int lengthBoard = 8;
        [SerializeField] public static int widthBoard = 8;
        [SerializeField] private int _rowCell = 3;


        private CellComponent[,] _cells;
        private CellComponent _cellPrefab;
        private CellComponent _cell;
        private Material _blackCellMaterial;
        private TriggerEndGame _triggerEndBlack;
        private TriggerEndGame _triggerEndWhite;
        public CellComponent[,] _cellsBlack;

        private List<ChipComponent> _chipsWhite = new();
        private List<ChipComponent> _chipsBlack = new();
        private ChipComponent _chipPrefab;
        private ChipComponent _chip;
        private Material _blackChipMaterial;

        private Handler _handler;

        private void Start()
        {
            InitializationResources();
            GenerateCells();
            GenerateChipsWhiteSide();
            GenerateChipsBlacktSide();

            _handler = GetComponent<Handler>();
            _handler.InitCell(_cells);
            _handler.InitChip(_chipsWhite);
            _handler.InitChip(_chipsBlack);
        }
        private void InitializationResources()
        {
            _cellPrefab = Resources.Load<CellComponent>("Cell");
            _chipPrefab = Resources.Load<ChipComponent>("Chip");

            _triggerEndBlack = Resources.Load<TriggerEndGame>("TriggerEndBlack");
            _triggerEndWhite = Resources.Load<TriggerEndGame>("TriggerEndWhite");
            _blackCellMaterial = Resources.Load<Material>("Material/CellBlack");
            _blackChipMaterial = Resources.Load<Material>("Material/ChipBlack");

        }


        private void GenerateCells()
        {
            Instantiate(_triggerEndBlack);
            Instantiate(_triggerEndWhite);
            int count = 0;
            _cells = new CellComponent[lengthBoard, widthBoard];
            _cellsBlack = new CellComponent[lengthBoard, widthBoard/2];
            float zPosition = 0f;
            int colorIndex = 1;
            for (int i = 0; i < lengthBoard; i++)
            {
                colorIndex = colorIndex == 0 ? 1 : 0;
                for (int a = 0; a < widthBoard; a++)
                {
                    _cell = Instantiate(_cellPrefab, new Vector3(a, 0, zPosition), Quaternion.identity, transform);
                    colorIndex = colorIndex == 0 ?  1 : 0;
                    _cell.Color = (ColorType)colorIndex;
                    _cells[i, a] = _cell;
                    if (_cell.Color == ColorType.Black)
                    {
                        _cellsBlack[i, count] = _cell;
                        count++;
                        _cell.AddAdditionalMaterial(_blackCellMaterial, 0);
                        if (count == 4)
                        {
                            count = 0;
                        }
                        
                    }

                }
                zPosition++;
            }
        }
        private void GenerateChipsWhiteSide()
        {
            for (int i = 0; i < _rowCell; i++)
            {
                for (int a = 0; a <= lengthBoard-1; a++) 
                {
                    if (_cells[i, a].Color is ColorType.Black)
                    {
                        _chipsBlack.Add(GenerateChip(_cells[i, a]));
                    }
                }
            }

            
        }
        private void GenerateChipsBlacktSide()
        {
          

            for (int i = widthBoard - _rowCell; i <= _cells.GetUpperBound(0); i++)
            {
                for (int a = 0; a <= lengthBoard - 1; a++)
                {
                    if (_cells[i, a].Color is ColorType.Black)
                    {
                       _chipsWhite.Add(GenerateChip(_cells[i, a], true));
                    }
                }
            }
        }
        private ChipComponent GenerateChip(CellComponent cell, bool blackMaterial = false)
        {
            _chip = Instantiate(_chipPrefab, cell.transform.position, Quaternion.identity, transform);
            if (blackMaterial)
            {
                _chip.AddAdditionalMaterial(_blackChipMaterial, 0);
                _chip.Color = ColorType.Black;
            }
            cell.Pair = _chip;
            _chip.Pair = cell;
            return _chip;
        }

       
    }
}
