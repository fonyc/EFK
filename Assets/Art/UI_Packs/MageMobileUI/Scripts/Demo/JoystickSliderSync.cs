using UnityEngine;
using UnityEngine.UI;

namespace DragonMobileUI.Scripts.Demo
{
    public class JoystickSliderSync : MonoBehaviour
    {
        public Slider horizontalAxes;
        public Slider verticalAxes;
        // Start is called before the first frame update
        public void UpdateSliderValues(Vector2 axes)
        {
            horizontalAxes.value = axes.x;
            verticalAxes.value = axes.y;
        }
    }
}
