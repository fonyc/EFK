using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    #region VARIABLES
    [SerializeField]
    private GameObject selectedDice = null;

    [SerializeField]
    private GameObject lastTouchedDice = null;

    [SerializeField]
    private StickySquare originStickySquare = null;

    [SerializeField]
    private StickySquare destinationStickySquare = null;

    [SerializeField]
    private bool diceBeingDragged = false;

    [SerializeField]
    private int solutions;

    [SerializeField]
    Transform upperParent;

    Shuffler shuffler;

    #endregion

    #region Properties
    public bool DiceBeingDragged { get => diceBeingDragged; set => diceBeingDragged = value; }
    public GameObject SelectedDice { get => selectedDice; set => selectedDice = value; }
    public StickySquare OriginStickySquare { get => originStickySquare; set => originStickySquare = value; }
    public StickySquare DestinationStickySquare { get => destinationStickySquare; set => destinationStickySquare = value; }
    public GameObject LastTouchedDice { get => lastTouchedDice; set => lastTouchedDice = value; }
    public int Solutions { get => solutions; set => solutions = value; }
    #endregion

    private void Awake()
    {
        shuffler = GetComponent<Shuffler>();
        Solutions = upperParent.childCount;
    }

    public void SwapDices()
    {
        //Dice destination is outside any sticky place or its the same destination 
        if(destinationStickySquare == null || destinationStickySquare == originStickySquare)
        {
            Parenting(originStickySquare.transform, selectedDice);    
        }
        //Destination not equal to origin --> Swap places
        else
        {
            Parenting(destinationStickySquare.transform, selectedDice);
            //after the swap, call the solver to solve the movement
            SolveSolution(lastTouchedDice);
            if(solutions != 1)shuffler.PartialShuffle();
        }
    }

    public void Parenting(Transform _parent, GameObject dice)
    {
        if (destinationStickySquare != null && destinationStickySquare != originStickySquare)
        {
            //Take destination dice to origin
            GameObject destinationDice = destinationStickySquare.transform.GetChild(0).gameObject;
            destinationDice.transform.SetParent(originStickySquare.transform);

            destinationDice.GetComponent<RectTransform>().anchorMin = Vector2.one * 0.5f;
            destinationDice.GetComponent<RectTransform>().anchorMax = Vector2.one * 0.5f;
            destinationDice.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        //Bring origin dice to destination
        selectedDice.transform.SetParent(_parent);

        dice.GetComponent<RectTransform>().anchorMin = Vector2.one * 0.5f;
        dice.GetComponent<RectTransform>().anchorMax = Vector2.one * 0.5f;
        dice.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public void SolveSolution(GameObject dice)
    {
        int diceId = dice.GetComponent<DiceData>().DiceId;

        int diceBottomPosition = dice.transform.parent.GetSiblingIndex();

        DiceData oposingDice = upperParent.GetChild(diceBottomPosition).GetChild(0).GetComponent<DiceData>();

        if (diceId == oposingDice.DiceId)
        {
            dice.GetComponent<DiceData>().IsSolved = true;
            dice.GetComponent<DiceData>().IsLocked = true;
            oposingDice.IsSolved = true;
            Solutions--;
        }
    }
}
