using EFK.Control;
using RPG.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EFK.Puzzles
{
    public class DiceController : MonoBehaviour
    {
        #region VARIABLES
        [Header("--- FADER SETTINGS --- ")]
        [Space(5)]
        [Range(0, 5)]
        [SerializeField] private float fadeOutTime;
        [Range(0, 5)]
        [SerializeField] private float fadeInTime;

        //Inner game variables
        private GameObject selectedDice = null;
        private GameObject lastTouchedDice = null;
        private StickySquare originStickySquare = null;
        private StickySquare destinationStickySquare = null;
        private bool diceBeingDragged = false;

        [Header("--- GAME SETTINGS ---")]
        [Space(5)]
        [SerializeField] DiceitSettings_SO gameSettings;
        private int currentMistakes;
        private int solutions;

        [SerializeField]
        Transform upperParent;

        Shuffler shuffler;

        #endregion

        #region Properties
        public bool DiceBeingDragged { get => diceBeingDragged; set => diceBeingDragged = value; }
        public GameObject SelectedDice { get => selectedDice; set => selectedDice = value; }
        public StickySquare OriginStickySquare { get => originStickySquare; set => originStickySquare = value; }
        public StickySquare DestinationStickySquare { get => destinationStickySquare; set => destinationStickySquare = value; }
        public GameObject LastTouchedDice { get => lastTouchedDice; set => lastTouchedDice = value; }
        public int Solutions { get => solutions; set => solutions = value; }
        #endregion

        private void Awake()
        {
            shuffler = GetComponent<Shuffler>();
            Solutions = upperParent.childCount;
        }

        private void Start()
        {
            Input.multiTouchEnabled = false;
        }

        public void SwapDices()
        {
            //Dice destination is outside any sticky place or its the same destination 
            if (destinationStickySquare == null || destinationStickySquare == originStickySquare)
            {
                Parenting(originStickySquare.transform, selectedDice);
            }
            //Destination not equal to origin --> Swap places
            else
            {
                Parenting(destinationStickySquare.transform, selectedDice);
                //after the swap, call the solver to solve the movement
                if (SolveSolution(lastTouchedDice))
                {
                    Debug.Log("Solucion correcta");
                    Solutions--;
                    if (CheckVictory())
                    {
                        //Trigger puzzle end
                        SolvePuzzle solvePuzzleTrigger = new SolvePuzzle();
                        EventManager.TriggerEvent(solvePuzzleTrigger);
                    }
                }
                else
                {
                    Debug.Log("Mistakes were made");
                    currentMistakes++;
                    //Trigger curse meter
                    AddCurseMeter addCurseMeterTrigger = new AddCurseMeter(gameSettings.CurseMeterPerMistake);
                    EventManager.TriggerEvent(addCurseMeterTrigger);

                    if (CheckDefeat())
                    {
                        //Trigger puzzle end
                        SolvePuzzle solvePuzzleTrigger = new SolvePuzzle();
                        EventManager.TriggerEvent(solvePuzzleTrigger);
                    }
                }

                //Shuffle after swapping correct or not
                if (solutions != 1) shuffler.PartialShuffle();
            }
        }

        private bool CheckDefeat()
        {
            return currentMistakes >= gameSettings.MaxMistakes;
        }

        private bool CheckVictory()
        {
            return solutions == 1;
        }

        public void Parenting(Transform _parent, GameObject dice)
        {
            if (destinationStickySquare != null && destinationStickySquare != originStickySquare)
            {
                //Take destination dice to origin
                GameObject destinationDice = destinationStickySquare.transform.GetChild(0).gameObject;
                destinationDice.transform.SetParent(originStickySquare.transform);

                destinationDice.GetComponent<RectTransform>().anchorMin = Vector2.one * 0.5f;
                destinationDice.GetComponent<RectTransform>().anchorMax = Vector2.one * 0.5f;
                destinationDice.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }

            //Bring origin dice to destination
            selectedDice.transform.SetParent(_parent);

            dice.GetComponent<RectTransform>().anchorMin = Vector2.one * 0.5f;
            dice.GetComponent<RectTransform>().anchorMax = Vector2.one * 0.5f;
            dice.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public bool SolveSolution(GameObject dice)
        {
            int diceId = dice.GetComponent<DiceData>().DiceId;

            int diceBottomPosition = dice.transform.parent.GetSiblingIndex();

            DiceData oposingDice = upperParent.GetChild(diceBottomPosition).GetChild(0).GetComponent<DiceData>();

            if (diceId == oposingDice.DiceId)
            {
                dice.GetComponent<DiceData>().IsSolved = true;
                dice.GetComponent<DiceData>().IsLocked = true;
                oposingDice.IsSolved = true;
                return true;
            }
            return false;
        }
    }
}