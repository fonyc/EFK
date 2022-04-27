using UnityEngine;

[CreateAssetMenu(fileName = "New SlidePuzzle Shuffle", menuName = "SlidePuzzle/Solution")]
public class SlidePuzzleShuffleSO : ScriptableObject
{
    [SerializeField] private int[] shuffleList;

    public int[] ShuffleList { get => shuffleList; set => shuffleList = value; }
}
