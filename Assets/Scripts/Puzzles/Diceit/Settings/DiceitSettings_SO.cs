using UnityEngine;

[CreateAssetMenu(fileName = "New Diceit Settings", menuName = "PuzzleSettings/Diceit")]
public class DiceitSettings_SO : ScriptableObject
{
    [SerializeField] private int maxMistakes;
    [SerializeField] private float curseMeterPerMistake;

    public int MaxMistakes { get => maxMistakes; set => maxMistakes = value; }
    public float CurseMeterPerMistake { get => curseMeterPerMistake; set => curseMeterPerMistake = value; }
}
