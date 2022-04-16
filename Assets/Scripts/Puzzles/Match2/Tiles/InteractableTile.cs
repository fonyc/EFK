using UnityEngine.EventSystems;
using UnityEngine;

public class InteractableTile : MonoBehaviour, IPointerClickHandler
{
    [Header(" --- TILE SETTINGS ---")]
    [Space(5)]
    [SerializeField] private int tileId;
    [SerializeField] private Sprite frontTile;
    [SerializeField] private Sprite backTile;

    [Header(" --- VARIABLES ---")]
    [Space(5)]
    [SerializeField] private MatchTwoController matchTwoController;
    [SerializeField] private bool isRevealed;
    [SerializeField] private bool isSolved;

    #region PROPERTIES
    public int TileId { get => tileId; set => tileId = value; }
    public Sprite FrontTile { get => frontTile; set => frontTile = value; }
    public Sprite BackTile { get => backTile; set => backTile = value; }
    public bool IsRevealed { get => isRevealed; set => isRevealed = value; }
    public bool IsSolved { get => isSolved; set => isSolved = value; }
    #endregion

    private void Awake()
    {
        matchTwoController = GameObject.FindGameObjectWithTag("Match2Controller").GetComponent<MatchTwoController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Transform tile = eventData.pointerClick.transform;
        if (tile == null) return;   

        matchTwoController.OnTilePressed(tile.GetComponent<InteractableTile>());
    }
}
