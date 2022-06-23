using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractableSlidePiece : MonoBehaviour, IPointerClickHandler
{
    [Header("--- PIECE SETTINGS ---")]
    [Space(5)]
    [SerializeField] private int slidePieceId;
    [SerializeField] private bool isVoidPiece;
    [SerializeField] private bool isInteractable;
    [SerializeField] private Vector3 interactionDirection;

    private SlidePuzzleController slidePuzzleController;

    public int SlidePieceId { get => slidePieceId; set => slidePieceId = value; }
    public bool IsVoidPiece { get => isVoidPiece; set => isVoidPiece = value; }
    public bool IsInteractable { get => isInteractable; set => isInteractable = value; }
    public Vector3 InteractionDirection { get => interactionDirection; set => interactionDirection = value; }

    private void Awake()
    {
        slidePuzzleController = GameObject.FindGameObjectWithTag("SlidePuzzleController").GetComponent<SlidePuzzleController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable) return;
        slidePuzzleController.OnPieceClicked(this);
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
