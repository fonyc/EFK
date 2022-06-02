using UnityEngine;

namespace CryptUI.Scripts.Demo
{
    public class DemoFlowController : MonoBehaviour
    {
        public GameObject mainMenuPanel;
        public GameObject loadingPanel;
        public GameObject fakeGamePanel;
        public GameObject ampulesPanel;
        public GameObject prefabsPanel;

        private void Start()
        {
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            mainMenuPanel.SetActive(true);
            loadingPanel.SetActive(false);
            fakeGamePanel.SetActive(false);
            ampulesPanel.SetActive(false);
            prefabsPanel.SetActive(false);
        }

        public void StartLoading()
        {
            mainMenuPanel.SetActive(false);
            loadingPanel.SetActive(true);
            fakeGamePanel.SetActive(false);
            ampulesPanel.SetActive(false);
            prefabsPanel.SetActive(false);
        }

        public void OpenGame()
        {
            mainMenuPanel.SetActive(false);
            loadingPanel.SetActive(false);
            fakeGamePanel.SetActive(true);
            ampulesPanel.SetActive(false);
            prefabsPanel.SetActive(false);
        }

        public void ShowAmpules()
        {
            mainMenuPanel.SetActive(false);
            loadingPanel.SetActive(false);
            fakeGamePanel.SetActive(false);
            ampulesPanel.SetActive(true);
            prefabsPanel.SetActive(false);
        }

        public void ShowPrefabs()
        {
            mainMenuPanel.SetActive(false);
            loadingPanel.SetActive(false);
            fakeGamePanel.SetActive(false);
            ampulesPanel.SetActive(false);
            prefabsPanel.SetActive(true);
        }
    }
}
