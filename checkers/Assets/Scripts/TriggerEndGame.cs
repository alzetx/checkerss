using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Checkers
{
    public class TriggerEndGame : MonoBehaviour
    {
        [SerializeField] public ColorType ChipForWin;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ChipComponent>())
            {
                ChipComponent chip = other.GetComponent<ChipComponent>();
                if (chip.Color == ChipForWin)
                {
                    Debug.Log("You Win!");
                }
            }
        }
    }
}



