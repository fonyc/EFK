using System.Collections;
using UnityEngine;

namespace EFK.Puzzles
{
    public class KeyController : MonoBehaviour
    {
        [SerializeField] private GameObject[] keyList;

        [Header("--- GAME SETTINGS ---")]
        [Space(5)]
        [SerializeField] private int numberOfRounds = 6;
        [SerializeField] private int roundNumber;
        [SerializeField] private int allowedMistakes;
        [SerializeField] private int mistakesNumber;
        [Range(0, 1f)]
        [SerializeField]
        private float timeBetweenNotes = 0.3f; 

        [Header("--- VARIABLES ---")]
        [Space(5)]
        [SerializeField] private int[] alreadyPlayedKeys;
        [SerializeField] private SimonSaysStates simonSaysState;
        [SerializeField] private int currentPlayedNote = 0;

        private int numberOfKeys;
        [SerializeField] Transform blackKeys;
        [SerializeField] Transform whiteKeys;
        [SerializeField] Transform[] keys;

        public SimonSaysStates SimonSaysState { get => simonSaysState; set => simonSaysState = value; }

        private void Awake()
        {
            numberOfKeys = blackKeys.childCount + whiteKeys.childCount;
            keys = new Transform[numberOfKeys];
            alreadyPlayedKeys = new int[numberOfRounds];        
        }

        private void Start()
        {
            InitAlreadyPlayedNotes();
            InitKeys();
            StartCoroutine(PlaySimonNotes());
        }

        private void Update()
        {
            
        }

        private void SwitchGamePhase(SimonSaysStates newState)
        {
            switch (newState)
            {
                case SimonSaysStates.SimonPhase:
                    break;
                case SimonSaysStates.PlayerPhase:
                    break;
            }
            SimonSaysState = newState;
        }

        private IEnumerator PlaySimonNotes()
        {
            if (roundNumber != alreadyPlayedKeys.Length)
            {
                AddNoteToList();
            }

            //Read first
            Debug.Log("Key number " + alreadyPlayedKeys[0] + " is shining");
            yield return new WaitForSeconds(timeBetweenNotes);

            //Read the rest
            for (int x = 0; x < roundNumber; x++)
            {
                //Key shines
                Debug.Log("Key number " + alreadyPlayedKeys[x] + " is shining");

                yield return new WaitForSeconds(timeBetweenNotes);
            }

            SwitchGamePhase(SimonSaysStates.PlayerPhase);
        }

        private void AddNoteToList()
        {
            int random = Random.Range(0, numberOfKeys);
            alreadyPlayedKeys[roundNumber] = random;
            Debug.Log(random);
        }

        #region KEY CALLED METHODS

        public void OnKeyPressed(int keyNumber)
        {
            if (simonSaysState != SimonSaysStates.PlayerPhase) return;

            //VFX key
            Debug.Log("Key number " + keyNumber + " was pressed");

            //Key is correct
            if (CheckPlayedKey(keyNumber))
            {
                currentPlayedNote = currentPlayedNote == roundNumber? currentPlayedNote + 1 : 0;
            }
            //Key is not correct
            else
            {
                //Wrong note --> add 1 fail
                currentPlayedNote = 0;
            }

            //Players turn is ended
            if(currentPlayedNote == 0)
            {
                //Remove player control
                SwitchGamePhase(SimonSaysStates.SimonPhase);
            }
        }

        private bool CheckPlayedKey(int keyIdPressed)
        {
            return keyIdPressed == alreadyPlayedKeys[currentPlayedNote];
        }

        #endregion

        #region INIT METHODS
        private void InitAlreadyPlayedNotes()
        {
            for (int x = 0; x < alreadyPlayedKeys.Length; x++)
            {
                alreadyPlayedKeys[x] = -1;
            }
        }

        private void InitKeys()
        {
            int index = 0;
            for (int x = index; x < whiteKeys.childCount; x++)
            {
                keys[x] = whiteKeys.GetChild(x);
                index++;
            }

            for (int x = index; x < numberOfKeys; x++)
            {
                keys[x] = blackKeys.GetChild(x - whiteKeys.childCount);
                index++;
            }
        }
        #endregion
    }
}

public enum SimonSaysStates
{
    SimonPhase = 0,
    PlayerPhase = 1,
    FinishedGame = 2
}
