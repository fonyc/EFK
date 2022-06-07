using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DragonMobileUI.Scripts
{
    public abstract class BaseJoystickController : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField]
        public OnInteractEvent onInteract;
        
        protected Vector2 MaxPossibleJoystickBorders;
        protected RectTransform Transform;
        protected RectTransform ParentTransform;
        protected Vector2 StartPosition;

        public virtual void OnDrag(PointerEventData eventData)
        {
            var dragPosition = eventData.position;
            var difference = dragPosition - StartPosition;
            var moveVector = Vector2.Min(new Vector2(Mathf.Abs(difference.x), Mathf.Abs(difference.y)),
                              MaxPossibleJoystickBorders) * difference.normalized;
            Transform.position = StartPosition + moveVector;
            onInteract?.Invoke(moveVector / MaxPossibleJoystickBorders);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            Transform.anchoredPosition = Vector2.zero;
            onInteract?.Invoke(Vector2.zero);
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            MaxPossibleJoystickBorders = (ParentTransform.rect.size - Transform.rect.size) / 2;
            var position = Transform.position;
            StartPosition = new Vector2(position.x, position.y);
        }
    }

    [System.Serializable]
    public class OnInteractEvent : UnityEvent<Vector2>
    {
    }
}
