using UnityEngine;

[CreateAssetMenu(fileName = "New MatchTwo Settings", menuName = "PuzzleSettings/MatchTwo")]
public class MatchTwoSettings : ScriptableObject
{
    [Header("--- GAME SETTINGS ---")]
    [Space(5)]
    [SerializeField] private float extraTimerValue;

    [Space(5)]
    [SerializeField] private int extraTimers;

    [Space(5)]
    [SerializeField] private float baseTime;

    [Space(5)]
    [SerializeField] private float curseMeterPerExtraTime;

    [Space(5)]
    [SerializeField] private int numberOfJokers;

    [Space(5)]
    [SerializeField] private int jokerRoundSpawn;

    public int JokerRoundSpawn { get => jokerRoundSpawn; set => jokerRoundSpawn = value; }
    public int NumberOfJokers { get => numberOfJokers; set => numberOfJokers = value; }
    public int ExtraTimers { get => extraTimers; set => extraTimers = value; }
    public float ExtraTimerValue { get => extraTimerValue; set => extraTimerValue = value; }
    public float BaseTime { get => baseTime; set => baseTime = value; }
    public float CurseMeterPerExtraTime { get => curseMeterPerExtraTime; set => curseMeterPerExtraTime = value; }
}
