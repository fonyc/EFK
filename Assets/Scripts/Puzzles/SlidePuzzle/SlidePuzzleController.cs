using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzleController : MonoBehaviour
{
    [SerializeField] private SildePuzzleShuffler shuffler;
    [SerializeField] private Transform parentPuzzle;
    [SerializeField] private SlidePuzzleImagesSO[] imagesList;
    [SerializeField] private Transform voidPiece;

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

    public void OnPieceClicked(InteractableSlidePiece piece, Vector3 interactionDirection)
    {
        //This method takes for granted that the piece is interactable
        int voidPieceIndex = voidPiece.transform.GetSiblingIndex();
        int pieceIndex = piece.transform.GetSiblingIndex();

        piece.RelocateTransformImage(voidPieceIndex);
        voidPiece.GetComponent<InteractableSlidePiece>().RelocateTransformImage(pieceIndex);

        UpdateMatrix();

        RefreshInteractablePieces();
    }

    private void UpdateMatrix()
    {
        for (int r = 0; r < matrixSize; r++)
        {
            for (int c = 0; c < matrixSize; c++)
            {
                matrix[r, c] = parentPuzzle.GetChild(c + r * matrixSize);
            }
        }
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

    #region NEIGHBOURS METHODS

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
                    voidPiece = matrix[r, c];
                    return result;
                }
            }
        }
        voidPiece = null;
        return result;
    }

    private void RefreshInteractablePieces()
    {
        TurnToNonInteractable();
        PieceData voidPieceLocation = FindVoidPiece();
        if (voidPieceLocation.col == -1) return;


        //In case we are in the top row
        if (voidPieceLocation.row != 0)
        {
            InteractableSlidePiece piece = matrix[voidPieceLocation.row - 1, voidPieceLocation.col].GetComponent<InteractableSlidePiece>();
            
            piece.IsInteractable = true;
            piece.InteractionDirection = Vector3.down;
        }

        if (voidPieceLocation.row != matrixSize - 1)
        {
            InteractableSlidePiece piece = matrix[voidPieceLocation.row + 1, voidPieceLocation.col].GetComponent<InteractableSlidePiece>();
            piece.IsInteractable = true;
            piece.InteractionDirection = Vector3.up;
        }

        if (voidPieceLocation.col != 0)
        {
            InteractableSlidePiece piece = matrix[voidPieceLocation.row, voidPieceLocation.col - 1].GetComponent<InteractableSlidePiece>();
            piece.IsInteractable = true;
            piece.InteractionDirection = Vector3.right;
        }

        if (voidPieceLocation.col != matrixSize -1)
        {
            InteractableSlidePiece piece = matrix[voidPieceLocation.row, voidPieceLocation.col + 1].GetComponent<InteractableSlidePiece>();
            piece.IsInteractable = true;
            piece.InteractionDirection = Vector3.left;
        }
    }

    private void TurnToNonInteractable()
    {
        foreach(Transform piece in parentPuzzle)
        {
            piece.GetComponent<InteractableSlidePiece>().IsInteractable = false;
        }
    }

    #endregion


}
