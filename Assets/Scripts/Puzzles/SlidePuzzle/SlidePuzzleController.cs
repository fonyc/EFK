using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzleController : MonoBehaviour
{
    [SerializeField] private SildePuzzleShuffler shuffler;
    [SerializeField] private Transform parentPuzzle;
    [SerializeField] private SlidePuzzleImagesSO[] imagesList;
    private Transform[,] matrix;
    private const int matrixSize = 3;
    private int selectedImage;
    struct PieceData
    {
        public int row;
        public int col;
        public PieceData(int r, int c)
        {
            row = r;
            col = c;
        }
    }

    void Start()
    {
        selectedImage = SelectRandomImage();
        LoadSpritesToCanvas(selectedImage);
        MakeVoidPieceTransparent(imagesList[selectedImage].VoidPiece);
        

        shuffler.ShuffleImage();
        InitMatrix();
        RefreshInteractablePieces();

    }

    private int SelectRandomImage()
    {
        return Random.Range(0, imagesList.Length);
    }

    private void LoadSpritesToCanvas(int selectedImage)
    {
        int index = 0;
        foreach (Transform piece in parentPuzzle)
        {
            piece.GetComponent<InteractableSlidePiece>().LoadImage(imagesList[selectedImage].ImageList[index]);
            index++;
        }
    }

    private void MakeVoidPieceTransparent(int piece)
    {
        parentPuzzle.GetChild(piece).GetComponent<InteractableSlidePiece>().MakePieceTransparent();
    }

    private void InitMatrix()
    {
        matrix = new Transform[3, 3];

        for (int r = 0; r < matrixSize; r++)
        {
            for (int c = 0; c < matrixSize; c++)
            {
                matrix[r, c] = parentPuzzle.GetChild(c + r * matrixSize);
            }
        }
    }

    private void ReadMatrix()
    {
        for (int r = 0; r < matrixSize; r++)
        {
            for (int c = 0; c < matrixSize; c++)
            {
                Debug.Log(matrix[r, c].name);
            }
        }
    }

    private PieceData FindVoidPiece()
    {
        PieceData result = new PieceData(-1,-1);

        for (int r = 0; r < matrixSize; r++)
        {
            for (int c = 0; c < matrixSize; c++)
            {
                if (matrix[r, c].GetComponent<InteractableSlidePiece>().IsVoidPiece)
                {
                    result.row = r;
                    result.col = c;
                    return result;
                }
            }
        }
        return result;

    }

    private void RefreshInteractablePieces()
    {
        TurnToNonInteractable();
        PieceData voidPieceLocation = FindVoidPiece();
        if (voidPieceLocation.col == -1) return;

        Transform voidPiece = matrix[voidPieceLocation.row, voidPieceLocation.col];

        //In case we are in the top row
        if (voidPieceLocation.row != 0)
        {
            matrix[voidPieceLocation.row - 1, voidPieceLocation.col].GetComponent<InteractableSlidePiece>().IsInteractable = true;
        }

        if (voidPieceLocation.row != matrixSize - 1)
        {
            matrix[voidPieceLocation.row + 1, voidPieceLocation.col].GetComponent<InteractableSlidePiece>().IsInteractable = true;
        }

        if (voidPieceLocation.col != 0)
        {
            matrix[voidPieceLocation.row, voidPieceLocation.col -1].GetComponent<InteractableSlidePiece>().IsInteractable = true;
        }

        if (voidPieceLocation.col != matrixSize -1)
        {
            matrix[voidPieceLocation.row, voidPieceLocation.col + 1].GetComponent<InteractableSlidePiece>().IsInteractable = true;
        }
    }

    private void TurnToNonInteractable()
    {
        foreach(Transform piece in parentPuzzle)
        {
            piece.GetComponent<InteractableSlidePiece>().IsInteractable = false;
        }
    }

}
