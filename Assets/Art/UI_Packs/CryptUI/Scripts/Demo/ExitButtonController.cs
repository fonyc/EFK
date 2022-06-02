using UnityEngine;

namespace CryptUI.Scripts.Demo
{
    public class ExitButtonController : MonoBehaviour
    {
        public void Exit()
        {
            Application.Quit(0);
        }
    }
}
