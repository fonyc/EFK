using UnityEngine;
using UnityEngine.UI;

namespace DragonMobileUI.Scripts
{
    public class ResourceBarTextController : MonoBehaviour
    {
        public Text barText;

        public void UpdateText(float value)
        {
            barText.text = $"{value:P0}";
        }
    }
}
