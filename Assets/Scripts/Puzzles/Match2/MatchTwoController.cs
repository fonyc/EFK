using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchTwoController : MonoBehaviour
{
    #region VARIABLES
    [Header(" --- GENERAL ---")]
    [Space(5)]
    [SerializeField] MatchTwoSettings matchTwoSettings;
    [SerializeField] List<InteractableTile> tileCouple;
    [SerializeField] int currentSolvedCouples;
    [SerializeField] private Timer timer;
    private TimerVariables timerVariables;
    private int currentBusyTiles;

    [Header(" --- SPIN ---")]
    [Space(5)]
    [Range(0f, 1f)]
    [SerializeField] float showAnimationTime = 0.25f;
    [SerializeField] float hideAnimationTime = 0.1f;
    [SerializeField][Range(0, 1f)] float waveStrength = 0.7f;
    private bool boardIsAnimated;

    [Header(" --- SHUFFLER ---")]
    [Space(5)]
    private MatchTwoShuffler shuffler;
    [SerializeField] Transform board;

    [Header(" --- JOKER ---")]
    [Space(5)]
    [SerializeField] Sprite jokerSprite;
    private Sprite previousSprite;
    private InteractableTile jokerTile;
    private bool isJokerSpawned;
    private bool isJokerSelected;
    #endregion

    #region PROPERTIES
    public List<InteractableTile> TileCouple { get => tileCouple; set => tileCouple = value; }
    public Transform Board { get => board; set => board = value; }
    public MatchTwoSettings MatchTwoSettings { get => matchTwoSettings; set => matchTwoSettings = value; }
    #endregion

    void Awake()
    {
        TileCouple = new List<InteractableTile>();
        shuffler = GetComponent<MatchTwoShuffler>();
        timer = GetComponent<Timer>();

        CreateTimerSettings();
    }

    private void Start()
    {
        ShowInteraction showInteraction = new ShowInteraction(null);
        EventManager.TriggerEvent(showInteraction);

        timer.InitTimerVariables(timerVariables);
    }

    public void OnTilePressed(InteractableTile tile)
    {
        if (boardIsAnimated) return;
        if (CheckVictory()) return;
        if (tile.IsBusy || tile.IsRevealed || tile.IsSolved) return;

        //Reveal tile and add it to the current revealed tiles
        tile.IsRevealed = true;
        currentBusyTiles++;
        if (tile.IsJoker) isJokerSelected = true;

        //check if the joker is the last tile
        if (tile.IsJoker && currentBusyTiles % 2 == 0)
        {
            boardIsAnimated = true;
        }

        StartCoroutine(FlipTile(tile.transform, tile.FrontTile, true));
    }

    private void Update()
    {
        if (TileCouple.Count == 2) CheckTileCouple();
    }

    private IEnumerator FlipTile(Transform tile, Sprite targetSprite, bool isShowing)
    {
        tile.GetComponent<InteractableTile>().IsBusy = true;
        float elapsedTime = 0;
        Quaternion currentRotation = tile.transform.rotation;
        Quaternion targetRotation = currentRotation * Quaternion.Euler(0, 90, 0);

        while (elapsedTime < showAnimationTime)
        {
            tile.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, (elapsedTime / showAnimationTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        tile.GetComponent<Image>().sprite = targetSprite;
        tile.transform.rotation = targetRotation;

        currentRotation = tile.transform.rotation;
        targetRotation = currentRotation * Quaternion.Euler(0, -90, 0);
        elapsedTime = 0;

        while (elapsedTime < hideAnimationTime)
        {
            tile.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, (elapsedTime / hideAnimationTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        tile.transform.rotation = targetRotation;

        tile.GetComponent<InteractableTile>().IsBusy = false;

        //Add the tile to tile couple if the tile is being shown
        if (isShowing) TileCouple.Add(tile.GetComponent<InteractableTile>());
        else tile.GetComponent<InteractableTile>().IsRevealed = false;


    }

    private void CheckTileCouple()
    {
        if (CheckTiles())
        {
            //Correct couple stuff
            currentSolvedCouples++;
            MarkCoupleAsCorrect();

            //Add Joker To Board in the correct round
            if (currentSolvedCouples == MatchTwoSettings.JokerRoundSpawn && !isJokerSpawned)
            {
                isJokerSpawned = true;
                AddJokerToBoard();
            }

            if (CheckVictory())
            {
                SolvePuzzle solvedPuzzleTrigger = new SolvePuzzle();
                EventManager.TriggerEvent(solvedPuzzleTrigger);
                timer.IsStopped = true;
            }

        }
        //Tile couple is not correct
        else
        {
            if (TileCouple[0].IsJoker || TileCouple[1].IsJoker) StartCoroutine(HideIncorrectTiles_Delay());
            else HideIncorrectTiles();

            MarkCoupleAsNotRevealed();
        }

        //Clear Couple tiles
        tileCouple.Clear();
        currentBusyTiles = currentBusyTiles - 2;
    }

    private void HideIncorrectTiles()
    {
        StartCoroutine(FlipTile(TileCouple[0].transform, TileCouple[0].BackTile, false));
        StartCoroutine(FlipTile(TileCouple[1].transform, TileCouple[1].BackTile, false));
    }

    private IEnumerator HideIncorrectTiles_Delay()
    {
        StartCoroutine(FlipTile(TileCouple[0].transform, TileCouple[0].BackTile, false));
        StartCoroutine(FlipTile(TileCouple[1].transform, TileCouple[1].BackTile, false));

        boardIsAnimated = true;
        yield return new WaitForSeconds(2f);
        boardIsAnimated = false;
        RemoveJokerTile();
        shuffler.ShuffleBoard();
    }

    #region JOKER METHODS

    private void AddJokerToBoard()
    {
        int jokerPosition = shuffler.GetRandomPositionInList(shuffler.GetSwappablePointList(board));
        InteractableTile tile = board.GetChild(jokerPosition).GetComponent<InteractableTile>();
        jokerTile = tile;
        jokerTile.IsJoker = true;
        previousSprite = jokerTile.FrontTile;
        jokerTile.FrontTile = jokerSprite;
        Debug.Log("Joker is in position " + tile.name);
    }

    private void RemoveJokerTile()
    {
        jokerTile.IsJoker = false;
        jokerTile.FrontTile = previousSprite;
        isJokerSelected = false;
    }

    #endregion

    #region TIMER METHODS
    private void CreateTimerSettings()
    {
        timerVariables = new TimerVariables(
            matchTwoSettings.ExtraTimers,
            matchTwoSettings.ExtraTimerValue,
            matchTwoSettings.BaseTime,
            matchTwoSettings.CurseMeterPerExtraTime);
    }

    #endregion

    #region UTILITY TILE METHODS
    public bool CheckTiles()
    {
        bool hasSameId = TileCouple[0].TileId == TileCouple[1].TileId;
        bool isJoker = TileCouple[0].IsJoker || TileCouple[1].IsJoker;

        return hasSameId && !isJoker;
    }

    private bool CheckVictory()
    {
        return currentSolvedCouples == board.childCount / 2;
    }

    public void MarkCoupleAsNotRevealed()
    {
        foreach (InteractableTile tile in TileCouple)
        {
            tile.IsRevealed = false;
        }
    }

    public void MarkCoupleAsCorrect()
    {
        foreach (InteractableTile tile in TileCouple)
        {
            tile.IsSolved = true;
        }
    }
    #endregion
}
