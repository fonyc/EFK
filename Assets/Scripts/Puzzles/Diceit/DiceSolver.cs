using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSolver : MonoBehaviour
{
    private bool CheckSolution(DiceData lowerDice, DiceData upperDice)
    {
        return lowerDice.DiceId == upperDice.DiceId;
    }
}
