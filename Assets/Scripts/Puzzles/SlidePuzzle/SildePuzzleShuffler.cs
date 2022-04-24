using System.Collections.Generic;
using UnityEngine;

public class SildePuzzleShuffler : MonoBehaviour
{
    [SerializeField] private Transform parentPuzzle;
    [Header("--- SHUFFLE SETTINGS ---")]
    [Space(5)]

    [Range(0,9)]
    [SerializeField] int correctImages;

    [Range(0, 9)]
    [SerializeField] int wrongImages;

    [SerializeField]
    List<int> availableList;

    public void ShuffleImage()
    {
        FillAvailableList();
        RemoveCorrectImagesFromAvailableList();
        ShuffleImages(availableList);
    }

    private void FillAvailableList()
    {
        foreach(Transform puzzlePiece in parentPuzzle)
        {
            if (puzzlePiece.GetComponent<InteractableSlidePiece>().IsVoidPiece) continue;
            availableList.Add(puzzlePiece.GetSiblingIndex());
        }
    }

    private void RemoveCorrectImagesFromAvailableList()
    {
        for(int x = 0; x < correctImages; x++)
        {
            availableList.RemoveAt(Random.Range(0,availableList.Count));
        }
    }

    private void ShuffleImages(List<int> _availableList)
    {
        int index = 0;
        foreach(Transform puzzlePiece in parentPuzzle)
        {
            int random = Random.Range(0, _availableList.Count);

            Transform destinationPiece = parentPuzzle.GetChild(random);

            //Send piece A to B
            puzzlePiece.GetComponent<InteractableSlidePiece>().RelocateTransformImage(random);
            destinationPiece.SetSiblingIndex(index);

            index++;
        }
    }
}
