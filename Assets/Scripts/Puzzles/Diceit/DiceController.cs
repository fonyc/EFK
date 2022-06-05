using System;
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

        [Header("--- GAME VFX ---")]
        [Space(5)]
        [SerializeField] Transform upperParent;
        [SerializeField] Transform bottomParent;

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
                        //Auto-Solve the last dice
                        ActivateGreenTick(GetUnsolvedBottomDice(),GetUnsolvedUpperDice());
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
                ActivateGreenTick(dice, oposingDice.gameObject);
                return true;
            }
            return false;
        }

        private void ActivateGreenTick(GameObject upperDice, GameObject bottomDice)
        {
            upperDice.transform.GetChild(1).gameObject.SetActive(true);
            bottomDice.transform.GetChild(1).gameObject.SetActive(true);
        }

        private GameObject GetUnsolvedUpperDice()
        {
            int index = 0;
            while (index < upperParent.childCount)
            {
                if (!upperParent.GetChild(index).GetChild(0).GetComponent<DiceData>().IsSolved) return upperParent.GetChild(index).GetChild(0).gameObject;
                index++;
            }
            return null;
        }

        private GameObject GetUnsolvedBottomDice()
        {
            int index = 0;
            while (index < bottomParent.childCount)
            {
                if (!bottomParent.GetChild(index).GetChild(0).GetComponent<DiceData>().IsSolved) return bottomParent.GetChild(index).GetChild(0).gameObject;
                index++;
            }
            return null;
        }
    }
}