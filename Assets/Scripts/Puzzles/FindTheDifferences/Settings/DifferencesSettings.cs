using UnityEngine;

[CreateAssetMenu(fileName = "New Differences Settings", menuName = "PuzzleSettings/FindDifferences")]
public class DifferencesSettings : ScriptableObject
{
    [SerializeField] private int differencesToDiscover;
    [SerializeField] private int maxMistakes;
    [SerializeField] private float curseMeterPerMistake;

    public int DifferencesToDiscover { get => differencesToDiscover; set => differencesToDiscover = value; }
    public int MaxMistakes { get => maxMistakes; set => maxMistakes = value; }
    public float CurseMeterPerMistake { get => curseMeterPerMistake; set => curseMeterPerMistake = value; }
}
