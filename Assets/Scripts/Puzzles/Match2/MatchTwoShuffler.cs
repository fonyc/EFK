using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTwoShuffler : MonoBehaviour
{
    [SerializeField] Transform board;

    private void Start()
    {
        ShuffleBoard();
    }

    public void ShuffleBoard()
    {
        List<int> availablePositionList = GetSwappablePointList(board);
        if (!IsGreaterThanOne(availablePositionList)) return;
        foreach (Transform tile in board)
        {
            //Dont add in the shuffle already solved tiles
            if (tile.GetComponent<InteractableTile>().IsSolved) continue;

            if (availablePositionList.Count == 0) return;
            int originIndex = tile.GetSiblingIndex();
            int destinationIndex = GetRandomPositionInList(availablePositionList);

            SwapTiles(board.GetChild(originIndex), board.GetChild(destinationIndex));

            availablePositionList.Remove(destinationIndex);
        }
    }

    public void SwapTiles(Transform originObject, Transform destinationObject)
    {
        int originIndex = originObject.GetSiblingIndex();
        int destinationIndex = destinationObject.GetSiblingIndex();

        originObject.SetSiblingIndex(destinationIndex);
        destinationObject.SetSiblingIndex(originIndex);
    }

    public int GetRandomPositionInList(List<int> list)
    {
        int random = Random.Range(0, list.Count);
        return list[random];
    }

    public List<int> GetSwappablePointList(Transform board)
    {
        List<int> availablePositionList = new List<int>();

        foreach(Transform tile in board)
        {
            InteractableTile tileScript = tile.GetComponent<InteractableTile>();
            if (tileScript.IsSolved || tileScript.IsRevealed || tileScript.IsBusy) continue;
            availablePositionList.Add(tile.GetSiblingIndex());
        }
        return availablePositionList;
    }

    private bool IsGreaterThanOne(List<int> list)
    {
        return list.Count > 0;
    }
}
