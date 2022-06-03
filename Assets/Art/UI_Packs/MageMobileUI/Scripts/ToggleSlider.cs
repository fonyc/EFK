using UnityEngine;

namespace DragonMobileUI.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class ToggleSlider : MonoBehaviour
    {
        public void SetValue(bool value) {
            var animator = GetComponent<Animator>();
            animator.StopPlayback();
            animator.Play(value ? "Enable" : "Disable");
        }
    }
}
