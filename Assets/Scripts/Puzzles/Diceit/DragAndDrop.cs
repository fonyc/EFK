using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform diceRectTransform;
    private Vector3 velocity = Vector3.zero;

    private DiceController diceController;

    [Range(0f,.05f)]
    [SerializeField] private float dampingSpeed = .025f;

    private void Awake()
    {
        diceRectTransform = transform as RectTransform;
        diceController = GameObject.FindGameObjectWithTag("DiceController").GetComponent<DiceController>();
    }

    #region DRAG/DROP
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject.GetComponent<DiceData>().IsLocked) 
        {
            Debug.Log("Dice is locked");
            return;
        }

        //Set Origin and Destination to this stick square
        diceController.OriginStickySquare = eventData.pointerDrag.transform.parent.gameObject.GetComponent<StickySquare>();
        diceController.DestinationStickySquare = diceController.OriginStickySquare;

        diceController.DiceBeingDragged = true;
        diceController.SelectedDice = gameObject;

        //Set Canvas as dice parent (so other dices dont block sight)
        eventData.pointerDrag.transform.SetParent(diceController.transform);

        //Make color and size selection
        Color color = gameObject.GetComponent<Image>().color;
        color = new Color(color.r, color.g, color.b, color.a - 0.4f);
        gameObject.GetComponent<Image>().color = color;

        diceRectTransform.localScale = Vector2.one * 0.8f;
        
        //Remove blocking raycast to reach sticky squares behind
        gameObject.GetComponent<Image>().raycastTarget = false;
        Debug.Log("But seem i dont care");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.gameObject.GetComponent<DiceData>().IsLocked)
        {
            Debug.Log("Dice is locked");
            return;
        }

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
        if (eventData.pointerDrag.gameObject.GetComponent<DiceData>().IsLocked)
        {
            Debug.Log("Dice is locked");
            return;
        }

        //Return alpha to its original value
        Color color = gameObject.GetComponent<Image>().color;
        color = new Color(color.r, color.g, color.b, color.a + 0.4f);
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
}
