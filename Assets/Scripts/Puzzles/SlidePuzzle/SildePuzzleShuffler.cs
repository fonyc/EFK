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
            puzzlePiece.GetComponent<InteractableSlidePiece>().RelocateTransformImage(selectedShuffle.ShuffleList[index]);

            //if()
            index++;
        }
    }
}
