using UnityEngine;
using UnityEngine.EventSystems;
namespace EFK.Puzzles
{
    public class StickySquare : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private DiceController diceController;

        private void Awake()
        {
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
}