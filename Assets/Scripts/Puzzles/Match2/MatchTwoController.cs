using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchTwoController : MonoBehaviour
{
    [SerializeField] List<InteractableTile> tileCouple;
    [SerializeField] int currentRevealedTiles;
    [SerializeField] int spinningTiles;
    [Range(0f,1f)]
    [SerializeField] float animationTime = 0.25f;

    [SerializeField] Transform board;

    #region PROPERTIES
    public int CurrentRevealedTiles { get => currentRevealedTiles; set => currentRevealedTiles = value; }
    public List<InteractableTile> TileCouple { get => tileCouple; set => tileCouple = value; }
    public Transform Board { get => board; set => board = value; }
    #endregion

    void Awake()
    {
        TileCouple = new List<InteractableTile>();
    }

    public void OnTilePressed(InteractableTile tile)
    {
        if (spinningTiles == 2) return;
        if (CurrentRevealedTiles == 2) return;
        if (tile.IsRevealed || tile.IsSolved) return;

        //Reveal tile and add it to the current revealed tiles
        tile.IsRevealed = true;
        TileCouple.Add(tile);

        StartCoroutine(RevealAndCheckTile(tile));
    }

    private IEnumerator RevealAndCheckTile(InteractableTile tile)
    {
        spinningTiles++;
        yield return StartCoroutine(FlipTile(tile.transform, tile.FrontTile));
        CurrentRevealedTiles++;

        if (CurrentRevealedTiles == 2)
        {
            if (CheckTiles())
            {
                MarkCoupleAsCorrect();
            }
            else
            {
                yield return HideIncorrectTiles();
                MarkCoupleAsNotRevealed();
            }
            ResetCouple();
        }
    }

    private IEnumerator FlipTile(Transform tile, Sprite targetSprite)
    {
        float elapsedTime = 0;
        Quaternion currentRotation = tile.transform.rotation;
        Quaternion targetRotation = currentRotation * Quaternion.Euler(0, 90, 0);

        while (elapsedTime < animationTime)
        {
            tile.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, (elapsedTime / animationTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        tile.GetComponent<Image>().sprite = targetSprite;

        currentRotation = tile.transform.rotation;
        targetRotation = currentRotation * Quaternion.Euler(0, -90, 0);
        elapsedTime = 0;

        while (elapsedTime < animationTime)
        {
            tile.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, (elapsedTime / animationTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    
    private IEnumerator HideIncorrectTiles()
    {
        //First coroutine happens witout waiting. Second one need to be waited to end at the same time so the reset happens visually and internally At the Same time
        StartCoroutine(FlipTile(TileCouple[0].transform, TileCouple[0].BackTile));
        yield return StartCoroutine(FlipTile(TileCouple[1].transform, TileCouple[1].BackTile));
    }

    #region UTILITY METHODS
    public bool CheckTiles()
    {
        if (TileCouple.Count == 2)
        {
            return TileCouple[0].TileId == TileCouple[1].TileId;
        }
        else return false;
    }

    public void MarkCoupleAsNotRevealed()
    {
        foreach(InteractableTile tile in TileCouple)
        {
            tile.IsRevealed = false;
        }
    }

    public void ResetCouple()
    {
        tileCouple.Clear();
        currentRevealedTiles = 0;
        spinningTiles = 0;
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
