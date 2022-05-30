using UnityEngine;
using UnityEngine.UI;

namespace CryptUI.Scripts.Demo
{
    public class TickTimer : MonoBehaviour
    {
        public Text text;

        public void SetText(string value)
        {
            text.text = value;
        }
    }
}
