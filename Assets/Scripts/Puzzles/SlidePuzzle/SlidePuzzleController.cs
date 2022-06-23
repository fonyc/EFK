using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SlidePuzzleController : MonoBehaviour
{
    [Header("--- GAME SETTINGS ---")]
    [SerializeField] private SlidePuzzleSettings slidePuzzleSettings;
    [SerializeField] private SildePuzzleShuffler shuffler;
    [SerializeField] private Transform parentPuzzle;
    [SerializeField] private SlidePuzzleImagesSO[] imagesList;
    [SerializeField] private Transform voidPiece;
    [SerializeField] private float pieceAnimationTime = 0.2f;
    private float imageDistance;
    private bool boardIsBusy = false;

    //Timer settings
    private Timer timer;
    private TimerVariables timerVariables;

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

    private void Awake()
    {
        timer = GetComponent<Timer>();

        timerVariables = new TimerVariables(
            slidePuzzleSettings.ExtraTimers,
            slidePuzzleSettings.ExtraTimerValue,
            slidePuzzleSettings.BaseTime,
            slidePuzzleSettings.CurseMeterPerExtraTime);
    }

    void Start()
    {
        selectedImage = SelectRandomImage();
        LoadSpritesToCanvas(selectedImage);
        MakeVoidPieceTransparent(imagesList[selectedImage].VoidPiece);

        shuffler.ShuffleImage();
        InitMatrix();
        RefreshInteractablePieces();

        timer.InitTimerVariables(timerVariables);

        ShowInteraction showInteraction = new ShowInteraction(null);
        EventManager.TriggerEvent(showInteraction);
    }

    public async void OnPieceClicked(InteractableSlidePiece piece)
    {
        if(boardIsBusy) return;

        //REMOVE THIS FROM HERE --> IMAGE DISTANCE CANT BE IN START CAUSE CABVAS DIDNT INIT THE IMAGES MUST BE LATER
        imageDistance = parentPuzzle.GetChild(1).transform.position.x - parentPuzzle.GetChild(0).transform.position.x;
        Debug.Log("Distance is: " + imageDistance);

        //This method takes for granted that the piece is interactable
        int voidPieceIndex = voidPiece.transform.GetSiblingIndex();
        int pieceIndex = piece.transform.GetSiblingIndex();

        await AnimateImage(piece);

        //Change indexes inside the parent
        piece.transform.SetSiblingIndex(voidPieceIndex);
        voidPiece.transform.SetSiblingIndex(pieceIndex);

        if (UpdateMatrixAndCheckVictory())
        {
            Debug.Log("Player wins!");
            SolvePuzzle solvedPuzzleTrigger = new SolvePuzzle();
            EventManager.TriggerEvent(solvedPuzzleTrigger);
            timer.IsStopped = true;
        }

        RefreshInteractablePieces();
    }

    public async Task AnimateImage(InteractableSlidePiece piece)
    {
        boardIsBusy = true;
        Vector3 targetPosition = piece.transform.position + piece.InteractionDirection * 94.82f;
        float duration = pieceAnimationTime;

        float time = 0;
        Vector3 startPosition = piece.transform.position;

        while (time < duration)
        {
            piece.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            await Task.Yield();
        }
        piece.transform.position = targetPosition;
        boardIsBusy = false;
    }

    private bool UpdateMatrixAndCheckVictory()
    {
        int correctPieces = matrixSize * matrixSize;

        for (int r = 0; r < matrixSize; r++)
        {
            for (int c = 0; c < matrixSize; c++)
            {
                if (matrix[r, c].GetComponent<InteractableSlidePiece>().SlidePieceId == matrix[r, c].transform.GetSiblingIndex())
                {
                    correctPieces--;
                }

                matrix[r, c] = parentPuzzle.GetChild(c + r * matrixSize);
            }
        }
        return correctPieces == 0;
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
        PieceData result = new PieceData(-1, -1);

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

        if (voidPieceLocation.col != matrixSize - 1)
        {
            InteractableSlidePiece piece = matrix[voidPieceLocation.row, voidPieceLocation.col + 1].GetComponent<InteractableSlidePiece>();
            piece.IsInteractable = true;
            piece.InteractionDirection = Vector3.left;
        }
    }

    private void TurnToNonInteractable()
    {
        foreach (Transform piece in parentPuzzle)
        {
            piece.GetComponent<InteractableSlidePiece>().IsInteractable = false;
        }
    }

    #endregion

}
