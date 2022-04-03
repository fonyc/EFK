using UnityEngine;
using UnityEngine.EventSystems;

public class StickySquare : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;

    [SerializeField]
    private DiceController diceController;

    [SerializeField] 
    private int arrayPositionInList;

    public int ArrayPositionInList { get => arrayPositionInList; set => arrayPositionInList = value; }

    private void Awake()
    {
        rectTransform = transform as RectTransform;
        diceController = GameObject.FindGameObjectWithTag("DiceController").GetComponent<DiceController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!diceController.DiceBeingDragged) return;
        diceController.DestinationStickySquare = gameObject.GetComponent<StickySquare>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!diceController.DiceBeingDragged) return;
        diceController.DestinationStickySquare = null;
    }
}
