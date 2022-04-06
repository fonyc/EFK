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
        Shuffle(upperParentList);
        Shuffle(bottomParentList);
    }

    private void Shuffle(Transform parentList)
    {
        // Fisher-Yates shuffle algorithm
        availablePositionList = GetSwapPointList(parentList);
        
        foreach (Transform stickySquare in parentList)
        {
            //Filter solved or selected dices
            DiceData dice = stickySquare.GetChild(0).gameObject.GetComponent<DiceData>();
            if (dice.IsSelected || dice.IsSolved) continue;

            //Take a random dice from list. Remove the used position afterwards
            int randomIndexInList = Random.Range(0, availablePositionList.Count);
            int randomPosition = availablePositionList[randomIndexInList];
            availablePositionList.RemoveAt(randomIndexInList);

            //Swap current sticky Square with the random index collected in randomPosition
            SwapDicesWithoutTarget(randomPosition, stickySquare);
        }
    }

    private List<int> GetSwapPointList(Transform parentList)
    {
        //Returns an int list made of the indexes that can be swapped
        List<int> list = new List<int>();

        foreach (Transform stickySquare in parentList)
        {
            DiceData dice = stickySquare.GetChild(0).gameObject.GetComponent<DiceData>();
            if (dice.IsSelected || dice.IsSolved) continue;
            

            list.Add(stickySquare.GetSiblingIndex());
        }

        return list;
    }

    private void SwapDicesWithoutTarget(int newIndexPositionInParent, Transform stickySquare)
    {
        int oldPosition = stickySquare.GetSiblingIndex();

        Transform diceOne = stickySquare.GetChild(0);
        Transform diceTwo = stickySquare.parent.GetChild(newIndexPositionInParent);

        //Parent the dices in the new positions
        diceOne.SetParent(stickySquare.parent.GetChild(newIndexPositionInParent));
        diceTwo.SetParent(stickySquare.parent.GetChild(newIndexPositionInParent));

        //Move the dices to the new parents
        RelocateDice(diceOne);
        RelocateDice(diceTwo.GetChild(0));
    }

    private void RelocateDice(Transform dice)
    {
        dice.GetComponent<RectTransform>().anchorMin = Vector2.one * 0.5f;
        dice.GetComponent<RectTransform>().anchorMax = Vector2.one * 0.5f;
        dice.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
