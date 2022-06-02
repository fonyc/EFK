using UnityEngine;
using UnityEngine.EventSystems;

namespace DragonMobileUI.Scripts
{
    public class StaticJoystickController : BaseJoystickController
    {

        private void Start()
        {
            Transform = gameObject.GetComponent<RectTransform>();
            ParentTransform = (RectTransform) Transform.parent;
        }

    }
}
