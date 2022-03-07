using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle", menuName = "Puzzle")]
public class PuzzleSO : ScriptableObject
{
    [SerializeField] private int puzzleId;
    [SerializeField] private GameObject puzzleCanvas;

    public GameObject PUZZLECANVAS
    {
        get { return puzzleCanvas; }
    }

    public int PUZZLEID
    {
        get { return puzzleId; }
    }
}
