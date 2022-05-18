using UnityEngine;

[CreateAssetMenu(fileName = "New SimonSays Settings", menuName = "PuzzleSettings/SimonSays")]
public class SimonSaysGameSettingsSO : ScriptableObject
{
    [Header("--- GAME SETTINGS ---")]
    [Space(5)]
    [SerializeField] private int numberOfRounds;

    [Space(5)]
    [SerializeField] private int allowedMistakes;

    [Space(5)]
    [SerializeField] private int maxPhantomInterventions;

    [Space(5)]
    [SerializeField] private int[] phantomProbabilities;

    [Space(5)]
    [Range(0, 1.5f)]
    [SerializeField]
    private float timeBetweenNotes;

    [Space(5)]
    [SerializeField] private float curseMeterPerMistake;

    public int NumberOfRounds { get => numberOfRounds; set => numberOfRounds = value; }
    public int AllowedMistakes { get => allowedMistakes; set => allowedMistakes = value; }
    public int MaxPhantomInterventions { get => maxPhantomInterventions; set => maxPhantomInterventions = value; }
    public float TimeBetweenNotes { get => timeBetweenNotes; set => timeBetweenNotes = value; }
    public int[] PhantomProbabilities { get => phantomProbabilities; set => phantomProbabilities = value; }
    public float CurseMeterPerMistake { get => curseMeterPerMistake; set => curseMeterPerMistake = value; }
}
