using System.Collections.Generic;
using UnityEngine;

public class SildePuzzleShuffler : MonoBehaviour
{
    [SerializeField] private Transform parentPuzzle;
    [Header("--- SHUFFLE SETTINGS ---")]
    [Space(5)]

    [SerializeField] private SlidePuzzleShuffleSO[] shuffleList;

    public void ShuffleImage()
    {
        ShuffleImages();
    }

    private void ShuffleImages()
    {
        int random = Random.Range(0, shuffleList.Length);
        Debug.Log(random);
        SlidePuzzleShuffleSO selectedShuffle = shuffleList[random];
        int index = 0;
        foreach (Transform puzzlePiece in parentPuzzle)
        {

            if(puzzlePiece.GetComponent<InteractableSlidePiece>().SlidePieceId == puzzlePiece.GetSiblingIndex())
            {
                puzzlePiece.SetSiblingIndex(selectedShuffle.ShuffleList[index]);
            }
            else
            {
                int positionA = puzzlePiece.transform.GetSiblingIndex();
                int positionB = parentPuzzle.GetChild(selectedShuffle.ShuffleList[index]).GetSiblingIndex();
                puzzlePiece.SetSiblingIndex(selectedShuffle.ShuffleList[index]);
            }

            index++;
        }
    }
}
