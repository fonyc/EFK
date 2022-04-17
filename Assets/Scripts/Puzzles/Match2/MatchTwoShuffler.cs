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

        foreach (Transform tile in board)
        {
            if (tile.GetComponent<InteractableTile>().IsSolved) continue;

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
            if (tile.GetComponent<InteractableTile>().IsSolved) continue;
            availablePositionList.Add(tile.GetSiblingIndex());
        }
        return availablePositionList;
    }
}
