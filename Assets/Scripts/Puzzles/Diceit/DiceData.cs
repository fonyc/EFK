using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EFK.Puzzles
{
    public class DiceData : MonoBehaviour
    {
        [SerializeField] private int diceId;
        [SerializeField] private bool isLocked; //from the upper part --> cannot be dragged
        [SerializeField] private bool isSolved;
        [SerializeField] private bool isSelected; //Was the last moved dice

        public bool IsLocked { get => isLocked; set => isLocked = value; }
        public int DiceId { get => diceId; set => diceId = value; }
        public bool IsSolved { get => isSolved; set => isSolved = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
    }
}