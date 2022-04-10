using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffler : MonoBehaviour
{
    [SerializeField] private Transform upperParentList;
    [SerializeField] private Transform bottomParentList;

    private void Start()
    {
        ApplyShuffledList(GetShuffledList(bottomParentList, true));
    }

    private GameObject[] GetShuffledList(Transform parentList, bool avoidAutoSolutions)
    {
        List<int> availablePositionList = GetSwapPointList(parentList);
        List<int> copyOfAvailablePositions = GetSwapPointList(parentList);
        GameObject[] shuffledList = new GameObject[bottomParentList.childCount];

        foreach(Transform stickySquare in parentList)
        {
            DiceData dice = stickySquare.GetChild(0).gameObject.GetComponent<DiceData>();
            if (dice.IsSolved || dice.IsSelected)
            {
                shuffledList[stickySquare.GetSiblingIndex()] = null;
            }
            else
            {
                int diceId = dice.DiceId;
                bool returnPositionToList = false;
                int numberToAvoid = avoidAutoSolutions ? GetIndexPartnerinUpperParent(upperParentList, diceId) : -1;

                //Mirror position is inside available list
                if (availablePositionList.Contains(numberToAvoid))
                {
                    if (availablePositionList.Count > 1 && numberToAvoid != -1)
                    {
                        availablePositionList.Remove(numberToAvoid);
                        returnPositionToList = true;
                    }
                    //Edge Case: Last number is going to be located in a prohibited spot
                    else if (availablePositionList.Count == 1)
                    {
                        Debug.Log("edge case!");
                        copyOfAvailablePositions.Remove(numberToAvoid);
                        int newPosition = PickRandomPosition(copyOfAvailablePositions);
                        //Swap positions 
                        shuffledList[numberToAvoid] = shuffledList[newPosition];
                        shuffledList[newPosition] = stickySquare.GetChild(0).gameObject;
                        continue;
                    }
                }

                //Take a random dice from list. Remove the used position afterwards
                int randomPosition = PickRandomPosition(availablePositionList);
                availablePositionList.Remove(randomPosition);

                //Return the number we wanted to avoid
                if (returnPositionToList) availablePositionList.Add(numberToAvoid);

                //Add the dice inside the shuffled list
                shuffledList[randomPosition] = stickySquare.GetChild(0).gameObject;
            }
        }
        return shuffledList;
    }

    private void ApplyShuffledList(GameObject[] shuffledList)
    {
        for(int x = 0; x < shuffledList.Length; x++)
        {
            if (shuffledList[x] == null) continue;
            shuffledList[x].transform.SetParent(bottomParentList.GetChild(x));
            RelocateDice(shuffledList[x]);
        }
    }

    private int PickRandomPosition(List<int> availablePositionList)
    {
        int randomIndexInList = Random.Range(0, availablePositionList.Count);
        int randomPosition = availablePositionList[randomIndexInList];
        return randomPosition;
    }

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

    private void RelocateDice(GameObject dice)
    {
        dice.GetComponent<RectTransform>().anchorMin = Vector2.one * 0.5f;
        dice.GetComponent<RectTransform>().anchorMax = Vector2.one * 0.5f;
        dice.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

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
        //Check if works for upper parent too
        //ApplyShuffledList(GetShuffledList(upperParentList, false));
        ApplyShuffledList(GetShuffledList(bottomParentList, true));
    }

    public void PartialShuffle()
    {
        ApplyShuffledList(GetShuffledList(bottomParentList, true));
    }

}
