using UnityEngine;
using UnityEngine.EventSystems;

namespace DragonMobileUI.Scripts
{
    public class OneDirectionSwipeController : MonoBehaviour, IEndDragHandler, IDragHandler
    {
        public bool vertical;

        public delegate void OnSwipeCallback(int direction);

        public OnSwipeCallback callback;
    
        public void OnEndDrag(PointerEventData eventData)
        {
            var dragVector = eventData.position - eventData.pressPosition;
            int direction;
            if (vertical)
            {
                direction = (int)new Vector2(0, dragVector.y).normalized.y;
            }
            else
            {
                direction = (int)new Vector2(dragVector.y, 0).normalized.x;
            }

            if (direction == 0) return;
            Debug.Log("moving: " + direction);
            callback?.Invoke(direction);

        }

        public void OnDrag(PointerEventData eventData)
        {
        }
    }
}
