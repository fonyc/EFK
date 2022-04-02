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
        Color color = gameObject.GetComponent<Image>().color;
        color = new Color(color.r, color.g, color.b, color.a - 0.4f);

        diceRectTransform.localScale = Vector2.one * 0.8f;
        diceController.DiceBeingDragged = true;
        diceController.SelectedDice = gameObject;
        gameObject.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(diceRectTransform, 
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
        Color color = gameObject.GetComponent<Image>().color;
        color = new Color(color.r, color.g, color.b, color.a + 0.4f);

        diceRectTransform.localScale = Vector2.one;
        diceController.DiceBeingDragged = false;

        gameObject.GetComponent<Image>().raycastTarget = true;

        //Check if the dice is from another sticky place/ the same sticky / void

        //No destination. Means player picked and item and dropped in the same place 
        if(diceController.DestinationStickySquare == null)
        {
            //Return dice to its original position
        }
        else
        {
            //Dice goes to the same place as before
            if (diceController.DestinationStickySquare.ArrayPositionInList == diceController.OriginStickySquare.ArrayPositionInList)
            {
                //Return dice to its original position
                Debug.Log("DEJA VU! I have been in this place before...");
            }
            //Sticky objectives are not the same, so its a new objective
            else
            {
                Debug.Log("SWITCH!");
                //Swap dice to new location
            }
        }

        
    }
    #endregion
}
