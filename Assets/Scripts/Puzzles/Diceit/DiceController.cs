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
    #endregion

    #region Properties
    public bool DiceBeingDragged { get => diceBeingDragged; set => diceBeingDragged = value; }
    public GameObject SelectedDice { get => selectedDice; set => selectedDice = value; }
    public StickySquare OriginStickySquare { get => originStickySquare; set => originStickySquare = value; }
    public StickySquare DestinationStickySquare { get => destinationStickySquare; set => destinationStickySquare = value; }
    public GameObject LastTouchedDice { get => lastTouchedDice; set => lastTouchedDice = value; }
    #endregion

    public void SwapDices()
    {
        //Dice destination is outside any sticky place or its the same destination 
        if(destinationStickySquare == null || destinationStickySquare == originStickySquare)
        {
            //selectedDice.transform.SetParent(originStickySquare.transform);
            Parenting(originStickySquare.transform, selectedDice);    
        }
        //Destination not equal to origin --> Swap places
        else
        {
            Parenting(destinationStickySquare.transform, selectedDice);
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
}
