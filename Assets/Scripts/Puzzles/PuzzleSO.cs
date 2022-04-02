using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle", menuName = "Puzzle")]
public class PuzzleSO : ScriptableObject
{
    [SerializeField] private int puzzleId;
    [SerializeField] private GameObject puzzleCanvas;

    public int PuzzleId { get => puzzleId; set => puzzleId = value; }
    public GameObject PuzzleCanvas { get => puzzleCanvas; set => puzzleCanvas = value; }
}
