using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using EFK.Control;

namespace RPG.SceneManagement
{
    public class Door_Teleporter : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;

        //Links enter portal with exit portal
        [SerializeField] private DestinationIdentifier portalIdentifier; 

        [Range(0.0f, 4.0f)]
        [SerializeField] private float fadeOutTime = 2f;
        [Range(0.0f, 4.0f)]
        [SerializeField] private float fadeInTime = 2f;

        Coroutine sceneTranstition_coro;

        enum DestinationIdentifier
        {
            A = 0,
            B = 1,
            C = 2,
            D = 3,
            E = 4,
            F = 5
        }

        private void Awake()
        {
            spawnPoint = transform.GetChild(0);
        }

        public void SceneTransition_Coro(int level)
        {
            if(sceneTranstition_coro == null)
            {
                sceneTranstition_coro = StartCoroutine(SceneTransition(level));
            }
        }

        private IEnumerator SceneTransition(int sceneToLoad)
        {
            if (sceneToLoad < 0)
            {
                //Ensures game doesnt break if we forget to set the portal build index correctly
                Debug.LogError("Portal's sceneToLoad is less than zero, therefore it cannot load the scene");
                yield break;
            }

            //Unparent and save the object to next scene to compare to the next portal
            //so we can put the player in the correct place
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            PlayerController ia = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            ia.InputActions.Disable();

            yield return fader.FadeOut(fadeOutTime);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            UpdatePlayersPositionInPortal(GetExitPortal());

            yield return new WaitForSeconds(0.5f);
            fader.FadeIn(fadeInTime);
            ia.InputActions.Enable();
            Destroy(gameObject);
        }

        private void UpdatePlayersPositionInPortal(Door_Teleporter portal)
        {
            if (portal != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                //Disable player control with iunput actions
                player.transform.position = portal.spawnPoint.position;
                player.transform.rotation = portal.spawnPoint.rotation;
                //Give back player control 
            }
        }

        private Door_Teleporter GetExitPortal()
        {
            foreach (Door_Teleporter portal in FindObjectsOfType<Door_Teleporter>())
            {
                //we check portal == this cause on the scene load, we brought the old portal, so we can check it without realize it
                if (portal == this || portal.portalIdentifier != portalIdentifier) continue;
                return portal;
            }
            return null;
        }
    }

}
