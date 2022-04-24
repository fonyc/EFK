using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzleController : MonoBehaviour
{
    [SerializeField] private SildePuzzleShuffler shuffler;
    [SerializeField] private Transform parentPuzzle;
    [SerializeField] private SlidePuzzleImagesSO[] imagesList;
    private int selectedImage;

    void Start()
    {
        selectedImage = SelectRandomImage();
        LoadSpritesToCanvas(selectedImage);
        MakeVoidPieceTransparent(imagesList[selectedImage].VoidPiece);

        shuffler.ShuffleImage();
    }

    private int SelectRandomImage()
    {
        return Random.Range(0, imagesList.Length);
    }

    private void LoadSpritesToCanvas(int selectedImage)
    {
        int index = 0;
        foreach(Transform piece in parentPuzzle)
        {
            piece.GetComponent<InteractableSlidePiece>().LoadImage(imagesList[selectedImage].ImageList[index]);
            index++;
        }
    }

    private void MakeVoidPieceTransparent(int piece)
    {
        parentPuzzle.GetChild(piece).GetComponent<InteractableSlidePiece>().MakePieceTransparent();
    }
}
