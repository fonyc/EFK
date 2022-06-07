using UnityEngine;

namespace CryptUI.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class ToggleSlider : MonoBehaviour
    {
        public GameObject onLabel;
        public GameObject offLabel;
        
        public void SetValue(bool value) {
            var animator = GetComponent<Animator>();
            animator.StopPlayback();
            animator.Play(value ? "Enable" : "Disable");
            onLabel.SetActive(value);
            offLabel.SetActive(!value);
        }
    }
}
