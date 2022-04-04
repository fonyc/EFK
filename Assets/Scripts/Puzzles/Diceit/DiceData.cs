using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceData : MonoBehaviour
{
    [SerializeField] private int diceId;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool canBeDragged;

    public bool CanBeDragged { get => canBeDragged; set => canBeDragged = value; }
    public bool IsLocked { get => isLocked; set => isLocked = value; }
    public int DiceId { get => diceId; set => diceId = value; }
}
