using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using EFK.Control;
using System;
using UnityEngine.Events;

namespace RPG.SceneManagement
{
    public class Door_Teleporter : MonoBehaviour
    {
        GameObject player;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIdentifier portalIdentifier; //Links enter portal with exit portal

        [Range(0.0f, 4.0f)]
        [SerializeField] private float fadeOutTime = 2f;
        [Range(0.0f, 4.0f)]
        [SerializeField] private float fadeInTime = 2f;

        #region LISTENERS
        private UnityAction<object> changeSceneListener;
        #endregion

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
            player = GameObject.FindGameObjectWithTag("Player");
            changeSceneListener = new UnityAction<object>(ManageEvent);
        }

        private void Start()
        {
            Type type = typeof(SceneTransition);
            EventManager.StartListening(type, changeSceneListener);
        }

        private void ManageEvent(object argument)
        {
            switch (argument)
            {
                case SceneTransition vartype:
                    Debug.Log("Teleport here --> Going to scene " + vartype.sceneToLoad);
                    StartCoroutine(SceneTransition(vartype.sceneToLoad));
                    break;
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        StartCoroutine(SceneTransition());
        //    }
        //}

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

            //InputActions playerInput = player.GetComponent<InputActions>();
            //playerInput.Disable();

            yield return fader.FadeOut(fadeOutTime);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            player = GameObject.FindGameObjectWithTag("Player");
            //InputActions newInputActions = player.GetComponent<InputActions>();
            //playerInput.Disable();

            UpdatePlayersPositionInPortal(GetExitPortal());

            yield return new WaitForSeconds(0.5f);
            fader.FadeIn(fadeInTime);
            //playerInput.Disable();
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