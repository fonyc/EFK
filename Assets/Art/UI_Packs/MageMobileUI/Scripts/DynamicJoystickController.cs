using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DragonMobileUI.Scripts
{
    public class DynamicJoystickController : BaseJoystickController
    {
        public RectTransform joystick;
        public RectTransform joystickPinButton;

        private void Start()
        {
            Transform = joystickPinButton;
            ParentTransform = joystick;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            joystick.gameObject.SetActive(false);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            joystick.gameObject.SetActive(true);
            joystick.position = eventData.position;
            base.OnBeginDrag(eventData);
        }

        private void OnDrawGizmos()
        {
            var rect = GetComponent<RectTransform>().rect;
            rect.position = GetComponent<RectTransform>().position;
            Gizmos.DrawWireCube(rect.position, rect.size);
            Gizmos.DrawLine(rect.position - rect.size / 2, rect.position + rect.size / 2);
            Gizmos.DrawLine(rect.position - rect.size / 2 * (Vector2.left + Vector2.up), rect.position + rect.size / 2 * (Vector2.left + Vector2.up));
            
        }
    }
}
