using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Checkers
{
    public class CellPath 
    {
        private CellComponent[,] _cells;
        private CellComponent cell;
        public CellPath(CellComponent[,] cells)
        {
            _cells = cells;
            InitNeighborCell(NeighborType.TopLeft);
            InitNeighborCell(NeighborType.TopRight);
            InitNeighborCell(NeighborType.BottomLeft);
            InitNeighborCell(NeighborType.BottomRight);
        }


        public void InitNeighborCell(NeighborType neighborType)
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int a = 0; a < _cells.GetLength(1); a++)
                {
                    if (chip(neighborType, i, a)) continue;
                }
            }
        }

        private bool chip (NeighborType neighborType, int i, int a)
        {
            int currentInt = (int)neighborType;
            switch (neighborType)
            {
                case NeighborType.TopLeft:
                    if (i - 1 < 0 || a - 1 < 0)
                    {
                        return false;
                    }
                    _cells[i, a]._neighbors.Add(neighborType, _cells[i - 1, a - 1]);
                    break;
                case NeighborType.TopRight:
                    if (i - 1 < 0 || a + 1 >= GenerateGameField.lengthBoard)
                    {
                        return false;
                    }
                    _cells[i, a]._neighbors.Add(neighborType, _cells[i - 1, a + 1]);
                    break;
                case NeighborType.BottomLeft:
                    if (i + 1 > GenerateGameField.widthBoard - 1 || a - 1 < 0)
                    {
                        return false;
                    }
                    _cells[i, a]._neighbors.Add(neighborType, _cells[i + 1, a - 1]);
                    break;
                case NeighborType.BottomRight:
                    if (i + 1 > GenerateGameField.widthBoard - 1 || a + 1 > GenerateGameField.lengthBoard - 1)
                    {
                        return false;
                    }
                    _cells[i, a]._neighbors.Add(neighborType, _cells[i + 1, a + 1]);
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}