using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Drag and drop Script Requires an Image component to work
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(DiceData))]
public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region VARIABLES
    private DiceController diceController;
    
    private RectTransform diceRectTransform;
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    [Range(0f, 1f)]
    private float alphaSelection = 0.4f;


    [Range(0f,.05f)]
    [SerializeField] private float dampingSpeed = .025f;
    #endregion

    private void Awake()
    {
        diceRectTransform = transform as RectTransform;
        diceController = GameObject.FindGameObjectWithTag("DiceController").GetComponent<DiceController>();
    }

    #region DRAG/DROP
    public void OnBeginDrag(PointerEventData eventData)
    {
        DiceData diceData = eventData.pointerDrag.gameObject.GetComponent<DiceData>();
        if (diceData.IsLocked || diceData.IsSolved) return;

        //Set Origin and Destination to this stick square
        diceController.OriginStickySquare = eventData.pointerDrag.transform.parent.gameObject.GetComponent<StickySquare>();
        diceController.DestinationStickySquare = diceController.OriginStickySquare;

        diceController.DiceBeingDragged = true;
        diceController.SelectedDice = gameObject;

        //Changes the selected dice from old-->new
        ChangeDiceSelection(diceData);

        //Set Canvas as dice parent (so other dices dont block sight)
        eventData.pointerDrag.transform.SetParent(diceController.transform);

        //Make color and size selection
        TurnGameObjectTransparent(gameObject, alphaSelection, true);

        //Make selection smaller
        diceRectTransform.localScale = Vector2.one * 0.8f;
        
        //Remove blocking raycast to reach sticky squares behind
        gameObject.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject.GetComponent<DiceData>().IsLocked) return;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(diceRectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out var globalMousePosition))
        {
            diceRectTransform.position = Vector3.SmoothDamp(diceRectTransform.position, 
                globalMousePosition, 
                ref velocity, 
                dampingSpeed);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DiceData diceData = eventData.pointerDrag.gameObject.GetComponent<DiceData>();
        if (diceData.IsLocked || diceData.IsSolved) return;

        //Avoid people trying to drop something on an already solved/locked dice
        if(diceController.DestinationStickySquare != null && diceController.DestinationStickySquare != diceController.OriginStickySquare)
        {
            DiceData selectedDiceData = diceController.DestinationStickySquare.transform.GetChild(0).GetComponent<DiceData>();
            if (selectedDiceData.IsLocked || selectedDiceData.IsSolved)
            {
                diceController.DestinationStickySquare = null;
            }
        }
        //Case the player puts dice in the same place 
        else if(diceController.DestinationStickySquare == diceController.OriginStickySquare)
        {
            diceController.DestinationStickySquare = null;
        }

        //Return alpha to its original value
        TurnGameObjectTransparent(gameObject, alphaSelection, false);

        //Return size to its original value
        diceRectTransform.localScale = Vector2.one;

        gameObject.GetComponent<Image>().raycastTarget = true;

        diceController.DiceBeingDragged = false;

        diceController.SwapDices();

        diceController.SelectedDice = null;
        diceController.OriginStickySquare = null;
        diceController.DestinationStickySquare = null;
    }
    #endregion

    private void ChangeDiceSelection(DiceData newDice)
    {
        if (diceController.LastTouchedDice != null)
        {
            diceController.LastTouchedDice.GetComponent<DiceData>().IsSelected = false;
        }

        newDice.IsSelected = true;
        diceController.LastTouchedDice = gameObject;
    }

    private void TurnGameObjectTransparent(GameObject go, float alphaAmount, bool value)
    {
        int sign = value ? -1 : 1;
        Color color = go.GetComponent<Image>().color;
        color = new Color(color.r, color.g, color.b, color.a + sign * alphaAmount);
        go.GetComponent<Image>().color = color;
    }
}
