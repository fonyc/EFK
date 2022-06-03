using UnityEngine;

namespace CryptUI.Scripts.Demo
{
    public class LoadingBarEventHandler : MonoBehaviour
    {
        public DemoFlowController controller;

        public void StartGame()
        {
            controller.OpenGame();
        }
    }
}
