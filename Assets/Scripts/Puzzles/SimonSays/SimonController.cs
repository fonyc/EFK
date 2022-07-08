using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EFK.Puzzles
{
    public class SimonController : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField] private GameObject[] keyList;

        [Header("--- GAME SETTINGS ---")]
        [Space(5)]
        [SerializeField] SimonSaysGameSettingsSO gameSettings;

        [Header("--- GAME COUNTERS ---")]
        [Space(5)]
        [SerializeField] private int currentRound;
        [Space(5)]
        [SerializeField] private int currentMistakes;
        [Space(5)]
        [SerializeField] private int currentPlayedNote = 0;
        [Space(5)]
        [SerializeField] private int[] alreadyPlayedKeys;
        [SerializeField] private float timeBetweenRounds = 1f;


        [Header("--- VARIABLES ---")]
        [SerializeField] private SimonSaysStates simonSaysState;

        private int numberOfKeys;
        [SerializeField] Transform blackKeys;
        [SerializeField] Transform whiteKeys;
        [SerializeField] Transform[] keys;
        #endregion

        #region PROPERTIES
        public SimonSaysStates SimonSaysState { get => simonSaysState; set => simonSaysState = value; }
        public int CurrentPlayedNote { get => currentPlayedNote; set => currentPlayedNote = value; }
        public SimonSaysGameSettingsSO GameSettings { get => gameSettings; set => gameSettings = value; }
        public int CurrentRound { get => currentRound; set => currentRound = value; }
        public int CurrentMistakes { get => currentMistakes; set => currentMistakes = value; }
        #endregion

        private void Awake()
        {
            alreadyPlayedKeys = new int[GameSettings.NumberOfRounds];
            numberOfKeys = blackKeys.childCount + whiteKeys.childCount;
            keys = new Transform[numberOfKeys];
        }

        private void Start()
        {
            ShowInteraction showInteraction = new ShowInteraction(null);
            EventManager.TriggerEvent(showInteraction);

            InitAlreadyPlayedNotes();
            InitKeys();
            SwitchGamePhase(SimonSaysStates.SimonPhase);
        }

        public void SwitchGamePhase(SimonSaysStates newState)
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
                    SolvePuzzle winTrigger = new SolvePuzzle();
                    EventManager.TriggerEvent(winTrigger);
                    break;
                case SimonSaysStates.GameFailed:
                    Debug.Log("--- YOU LOSE ---");
                    SolvePuzzle gameOverTrigger = new SolvePuzzle();
                    EventManager.TriggerEvent(gameOverTrigger);
                    break;
            }
            SimonSaysState = newState;
        }

        public void OnKeyPressed(int keyNumber)
        {
            if (simonSaysState != SimonSaysStates.PlayerPhase) return;

            //VFX key
            StartCoroutine(LightKey(keyNumber, false));

            //Key is correct
            if (CheckPlayedKey(keyNumber))
            {
                //Players turn is ended.
                if (CurrentPlayedNote == CurrentRound)
                {
                    //No more rounds, game is finished
                    if (CurrentRound == gameSettings.NumberOfRounds - 1)
                    {
                        SwitchGamePhase(SimonSaysStates.GameFinished);
                    }
                    //Round is over --> Simon Phase
                    else
                    {
                        CurrentRound++;
                        CurrentPlayedNote = 0;
                        SwitchGamePhase(SimonSaysStates.SimonPhase);
                    }
                }
                //Player still has notes to play
                else
                {
                    CurrentPlayedNote++;
                    Debug.Log("one more note...");
                }
            }
            //Key is not correct
            else
            {
                //Too much mistakes --> Game Over
                if (CurrentMistakes >= gameSettings.AllowedMistakes)
                {
                    CurrentPlayedNote = 0;
                    CurrentMistakes = 0;
                    SwitchGamePhase(SimonSaysStates.GameFailed);
                }
                //One mistake --> Rewatch sequence
                else
                {
                    CurrentPlayedNote = 0;
                    CurrentMistakes++;

                    //Trigger curse meter event
                    AddCurseMeter curseMeterTrigger = new AddCurseMeter(gameSettings.CurseMeterPerMistake);
                    EventManager.TriggerEvent(curseMeterTrigger);

                    //Add error to UI
                    PaintError addErrorTrigger = new PaintError();
                    EventManager.TriggerEvent(addErrorTrigger);

                    Debug.Log("Mistake! Current Mistakes: " + CurrentMistakes);

                    SwitchGamePhase(SimonSaysStates.SimonPhase);
                }
            }
        }

        private IEnumerator PlaySimonNotes()
        {
            yield return new WaitForSeconds(timeBetweenRounds);

            if (CheckEmptyKeyInCurrentRound())
            {
                alreadyPlayedKeys[currentRound] = PlayRandomKey();
            }

            //Read the notes
            for (int x = 0; x < currentRound + 1; x++)
            {
                yield return StartCoroutine(LightKey(alreadyPlayedKeys[x], false));
            }

            SwitchGamePhase(SimonSaysStates.PlayerPhase);
        }

        private IEnumerator LightKey(int key, bool isPhantom)
        {
            PianoKey pianoKeyScript = keys[key].GetComponent<PianoKey>();
            pianoKeyScript.TurnOnKey();
            yield return new WaitForSeconds(GameSettings.TimeBetweenNotes);
            pianoKeyScript.TurnOffKey();
            yield return new WaitForSeconds(GameSettings.TimeBetweenNotes);
        }

        #region UTILITY METHODS

        private int PlayRandomKey()
        {
            return Random.Range(0, numberOfKeys);
        }

        private bool CheckEmptyKeyInCurrentRound()
        {
            return alreadyPlayedKeys[CurrentRound] == -1;
        }

        private bool CheckPlayedKey(int keyIdPressed)
        {
            return keyIdPressed == alreadyPlayedKeys[CurrentPlayedNote];
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