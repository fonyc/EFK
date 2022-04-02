using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickySquare : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;

    [SerializeField]
    private DiceController diceController;

    [SerializeField] 
    private int arrayPositionInList;

    [SerializeField]
    private GameObject diceInside;

    public GameObject DiceInside { get => diceInside; set => diceInside = value; }
    public int ArrayPositionInList { get => arrayPositionInList; set => arrayPositionInList = value; }

    private void Awake()
    {
        rectTransform = transform as RectTransform;
        diceController = GameObject.FindGameObjectWithTag("DiceController").GetComponent<DiceController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!diceController.DiceBeingDragged) return;
        if (diceController.IsNextSquare) diceController.DestinationStickySquare = gameObject.GetComponent<StickySquare>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        diceController.IsNextSquare = diceController.DiceBeingDragged ? true : false;
        if(diceController.DestinationStickySquare == null)
        {
            diceController.OriginStickySquare = GetComponent<StickySquare>();
        }
    }
}
