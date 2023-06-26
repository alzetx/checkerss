using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorType
{
    public ColorType Color { get; set; }
    public enum ColorType
    {
        White,
        Black
    }
}
