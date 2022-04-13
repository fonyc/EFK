using System.Collections;
using UnityEngine;

namespace EFK.Puzzles
{
    public class KeyController : MonoBehaviour
    {
        [SerializeField] private GameObject[] keyList;

        [Header("--- GAME SETTINGS ---")]
        [Space(5)]
        [SerializeField] private int numberOfRounds;
        [SerializeField] private int currentRound;
        [SerializeField] private int allowedMistakes;
        [SerializeField] private int currentMistakes;
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
            numberOfRounds = 6;
            alreadyPlayedKeys = new int[numberOfRounds];
            numberOfKeys = blackKeys.childCount + whiteKeys.childCount;
            keys = new Transform[numberOfKeys];
        }

        private void Start()
        {
            allowedMistakes = 3;
            InitAlreadyPlayedNotes();
            InitKeys();
            SwitchGamePhase(SimonSaysStates.SimonPhase);
        }

        private void SwitchGamePhase(SimonSaysStates newState)
        {
            switch (newState)
            {
                case SimonSaysStates.SimonPhase:
                    Debug.Log("--- SIMON ---");
                    StartCoroutine(PlaySimonNotes());
                    break;
                case SimonSaysStates.PlayerPhase:
                    Debug.Log("--- PLAYER ---");
                    break;
                case SimonSaysStates.GameFinished:
                    Debug.Log("--- YOU WIN ---");
                    break;
                case SimonSaysStates.GameFailed:
                    Debug.Log("--- YOU LOSE ---");
                    break;
            }
            SimonSaysState = newState;
        }

        private IEnumerator PlaySimonNotes()
        {
            if (CheckEmptyKeyInCurrentRound())
            {
                alreadyPlayedKeys[currentRound] = PlayRandomKey();
            }

            //Read the notes
            for (int x = 0; x < currentRound + 1; x++)
            {
                //Key shines
                Debug.Log("Key number " + alreadyPlayedKeys[x] + " is shining");

                yield return new WaitForSeconds(timeBetweenNotes);
            }

            SwitchGamePhase(SimonSaysStates.PlayerPhase);
        }

        private int PlayRandomKey()
        {
            return Random.Range(0, numberOfKeys);
        }

        private bool CheckEmptyKeyInCurrentRound()
        {
            return alreadyPlayedKeys[currentRound] == -1;
        }

        #region KEY CALLED METHODS

        public void OnKeyPressed(int keyNumber)
        {
            if (simonSaysState != SimonSaysStates.PlayerPhase) return;

            //VFX key
            Debug.Log("Key with name " + keys[keyNumber].name + " was pressed");

            //Key is correct
            if (CheckPlayedKey(keyNumber))
            {
                //Players turn is ended.
                if (currentPlayedNote == currentRound)
                {
                    Debug.Log("Player turn ended: CPlyNote,CRound" + currentPlayedNote + currentRound);
                    //No more rounds, game is finished
                    if (currentRound == numberOfRounds - 1)
                    {
                        SwitchGamePhase(SimonSaysStates.GameFinished);
                    }
                    //Round is over --> Simon Phase
                    else
                    {
                        currentRound++;
                        currentPlayedNote = 0;
                        SwitchGamePhase(SimonSaysStates.SimonPhase);
                    }
                }
                //Player still has notes to play
                else
                {
                    currentPlayedNote++;
                    Debug.Log("one more note...");
                }
            }
            //Key is not correct
            else
            {
                //Too much mistakes --> Game Over
                if (currentMistakes >= allowedMistakes)
                {
                    currentPlayedNote = 0;
                    currentMistakes = 0;
                    SwitchGamePhase(SimonSaysStates.GameFailed);
                }
                //One mistake --> Rewatch sequence
                else
                {
                    currentPlayedNote = 0;
                    currentMistakes++;

                    Debug.Log("Mistake! Current Mistakes: " + currentMistakes);

                    SwitchGamePhase(SimonSaysStates.SimonPhase);
                }
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
    GameFinished = 2,
    GameFailed = 3
}
