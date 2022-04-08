using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffler : MonoBehaviour
{
    [SerializeField] private Transform upperParentList;
    [SerializeField] private Transform bottomParentList;
    [SerializeField] private List<int> availablePositionList;

    private void Awake()
    {
        availablePositionList = new List<int>();
    }

    private void Start()
    {
        PartialShuffle();
    }

    /// <summary>
    /// Shuffles the dices. Must have dices inside the sticky squares. 
    /// Sticky squares must have DiceData as a child.
    /// </summary>
    /// <param name="parentList">Parent we want to shuffle</param> 
    /// <param name="avoidAutoSolutions">True when the upper list is already shuffled and the bottom needs to readjust to not get in front the solution</param> 
    private void Shuffle(Transform parentList, bool avoidAutoSolutions)
    {
        // Fisher-Yates shuffle algorithm
        availablePositionList = GetSwapPointList(parentList);
        
        foreach (Transform stickySquare in parentList)
        {
            //Filter solved or selected dices
            DiceData dice = stickySquare.GetChild(0).gameObject.GetComponent<DiceData>();
            if (dice.IsSolved || dice.IsSelected) continue;

            //Remove possible faced results in available list
            int diceId = stickySquare.GetChild(0).gameObject.GetComponent<DiceData>().DiceId;
            bool wasInList = false;
            int numberToAvoid = avoidAutoSolutions ? GetIndexPartnerinUpperParent(upperParentList, diceId) : -1;

            if (availablePositionList.Contains(numberToAvoid) && availablePositionList.Count > 1 && numberToAvoid != -1)
            {
                availablePositionList.Remove(numberToAvoid);
                wasInList = true;
            }

            //Take a random dice from list. Remove the used position afterwards
            int randomIndexInList = Random.Range(0, availablePositionList.Count);
            int randomPosition = availablePositionList[randomIndexInList];
            availablePositionList.Remove(randomPosition);

            //Return the number we wanted to avoid
            if (wasInList) availablePositionList.Add(numberToAvoid);


            //Swap current sticky Square with the random index collected in randomPosition
            SwapDiceToPosition(randomPosition, stickySquare);
        }
        //ReShuffleMirrorDices(CheckShuffle());
    }

    /// <summary>
    /// Returns a list with the positions that are not selected or solved
    /// </summary>
    /// <param name="parentList"></param>
    /// <returns></returns>
    private List<int> GetSwapPointList(Transform parentList)
    {
        //Returns an int list made of the indexes that can be swapped
        List<int> list = new List<int>();

        foreach (Transform stickySquare in parentList)
        {
            DiceData dice = stickySquare.GetChild(0).gameObject.GetComponent<DiceData>();
            if (dice.IsSolved || dice.IsSelected) continue;

            list.Add(stickySquare.GetSiblingIndex());
        }

        return list;
    }

    /// <summary>
    /// Given an index and a dice, swaps the dice in the target objective and viceversa
    /// </summary>
    /// <param name="newIndexPositionInParent"></param>
    /// <param name="stickySquare"></param>
    private void SwapDiceToPosition(int newIndexPositionInParent, Transform stickySquare)
    {
        //Transform diceOne = stickySquare.GetChild(0);
        //Transform diceTwo = stickySquare.parent.GetChild(newIndexPositionInParent);

        ////Parent the dices in the new positions
        //diceOne.SetParent(stickySquare.parent.GetChild(newIndexPositionInParent));
        //diceTwo.SetParent(stickySquare.parent.GetChild(newIndexPositionInParent));

        ////Move the dices to the new parents
        //RelocateDice(diceOne);
        //RelocateDice(diceTwo.GetChild(0));

        Transform diceOne = stickySquare.GetChild(0);
        Transform diceTwo = stickySquare.parent.GetChild(newIndexPositionInParent).GetChild(0);

        diceOne.SetParent(stickySquare.parent.GetChild(newIndexPositionInParent));
        diceTwo.SetParent(stickySquare.parent.GetChild(stickySquare.GetSiblingIndex()));

        RelocateDice(diceOne);
        RelocateDice(diceTwo);
    }

    private void RelocateDice(Transform dice)
    {
        dice.GetComponent<RectTransform>().anchorMin = Vector2.one * 0.5f;
        dice.GetComponent<RectTransform>().anchorMax = Vector2.one * 0.5f;
        dice.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// Given an oposing parent and a diceID, determines where in the oposing parent 
    /// is the partner dice
    /// </summary>
    /// <param name="parent">Oposing parent</param>
    /// <param name="idToAvoid">Id of the dices</param>
    /// <returns></returns>
    private int GetIndexPartnerinUpperParent(Transform parent, int idToAvoid)
    {
        foreach(Transform stickySquare in parent)
        {
            if (stickySquare.GetChild(0).gameObject.GetComponent<DiceData>().DiceId == idToAvoid)
            {
                return stickySquare.GetSiblingIndex();
            }
        }
        return -1;
    }

    public void FullShuffle()
    {
        Shuffle(upperParentList, false);
        Shuffle(bottomParentList, true);
    }

    public void PartialShuffle()
    {
        Shuffle(bottomParentList, true);
    }

    #region Mirror cases Methods

    private Transform CheckShuffle()
    {
        int index = 0;
        foreach(Transform stickySquare in bottomParentList)
        {
            int bottomId = stickySquare.GetChild(0).GetComponent<DiceData>().DiceId;
            int upperId = upperParentList.GetChild(index).GetChild(0).GetComponent<DiceData>().DiceId;
            if (upperId == bottomId)
            {
                Debug.Log(stickySquare.GetChild(0).name + "with ID: " + upperId + " is equal");
                return stickySquare;
            }
            index++;
        }
        return null;
    }

    private void ReShuffleMirrorDices(Transform stickySquare)
    {
        if (stickySquare == null) return;

        List<int> availablePositionList = new List<int>();

        availablePositionList = GetSwapPointList(bottomParentList);

        //Remove problematic point
        availablePositionList.Remove(stickySquare.GetSiblingIndex());
        ReadList(availablePositionList);

        int firstAvailablePosition = availablePositionList[0];
        Debug.Log("Swapping " + stickySquare + "To position " + firstAvailablePosition);
        SwapDiceToPosition(firstAvailablePosition, stickySquare);
    }

    private void ReadList(List<int> list)
    {
        foreach(int item in list)
        {
            Debug.Log(item);
        }
    }

    #endregion

}
