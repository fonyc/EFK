using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField]
    private DiceData[] diceList = null;

    [SerializeField]
    private GameObject selectedDice = null;

    [SerializeField]
    private StickySquare originStickySquare = null;

    [SerializeField]
    private StickySquare destinationStickySquare = null;

    [SerializeField]
    private bool diceBeingDragged = false;

    [SerializeField]
    private bool isNextSquare = false;

    #region Properties
    public bool DiceBeingDragged { get => diceBeingDragged; set => diceBeingDragged = value; }
    public GameObject SelectedDice { get => selectedDice; set => selectedDice = value; }
    public DiceData[] DiceList { get => diceList; set => diceList = value; }
    public StickySquare OriginStickySquare { get => originStickySquare; set => originStickySquare = value; }
    public StickySquare DestinationStickySquare { get => destinationStickySquare; set => destinationStickySquare = value; }
    public bool IsNextSquare { get => isNextSquare; set => isNextSquare = value; }
    #endregion

    private void SwapDices()
    {
        GameObject auxDice = originStickySquare.GetComponent<StickySquare>().DiceInside;
    }

    private void CheckFacedDices()
    {

    }

    private void GetFacedDice()
    {

    }
}
