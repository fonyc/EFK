using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractableSlidePiece : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int slidePieceId;
    [SerializeField] private bool isVoidPiece;
    [SerializeField] private bool isInteractable;
    private SlidePuzzleController slidePuzzleController;

    public int SlidePieceId { get => slidePieceId; set => slidePieceId = value; }
    public bool IsVoidPiece { get => isVoidPiece; set => isVoidPiece = value; }
    public bool IsInteractable { get => isInteractable; set => isInteractable = value; }

    private void Awake()
    {
        slidePuzzleController = GameObject.FindGameObjectWithTag("SlidePuzzleController").GetComponent<SlidePuzzleController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Piece with Id " + slidePieceId + " pressed.");

        if (!isInteractable) return;

        Debug.Log("Is interactable, lets move the piece somewhere (but not over the rainbow)");
    }

    public void MakePieceTransparent()
    {
        isVoidPiece = true;
        isInteractable = false;
        GetComponent<Image>().color = new Color(
            GetComponent<Image>().color.r,
            GetComponent<Image>().color.g,
            GetComponent<Image>().color.b,
            0);
    }

    public void LoadImage(Sprite _sprite)
    {
        GetComponent<Image>().sprite = _sprite;
    }
}
