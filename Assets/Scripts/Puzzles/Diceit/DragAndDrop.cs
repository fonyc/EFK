using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        DiceData draggedDiceData = eventData.pointerDrag.gameObject.GetComponent<DiceData>();
        if (draggedDiceData.IsLocked) return;

        //Set Origin and Destination to this stick square
        diceController.OriginStickySquare = eventData.pointerDrag.transform.parent.gameObject.GetComponent<StickySquare>();
        diceController.DestinationStickySquare = diceController.OriginStickySquare;

        diceController.DiceBeingDragged = true;
        diceController.SelectedDice = gameObject;

        ChangeDiceSelection(draggedDiceData);

        //Set Canvas as dice parent (so other dices dont block sight)
        eventData.pointerDrag.transform.SetParent(diceController.transform);

        //Make color and size selection
        Color color = gameObject.GetComponent<Image>().color;
        color = new Color(color.r, color.g, color.b, color.a - alphaSelection);
        gameObject.GetComponent<Image>().color = color;

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
        if (eventData.pointerDrag.gameObject.GetComponent<DiceData>().IsLocked) return;
        
        //Return alpha to its original value
        Color color = gameObject.GetComponent<Image>().color;
        color = new Color(color.r, color.g, color.b, color.a + alphaSelection);
        gameObject.GetComponent<Image>().color = color;

        //Return size to its original value
        diceRectTransform.localScale = Vector2.one;

        diceController.DiceBeingDragged = false;
        gameObject.GetComponent<Image>().raycastTarget = true;

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
}
