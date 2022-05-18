using UnityEngine;

[CreateAssetMenu(fileName = "New SlidePuzzle Settings", menuName = "PuzzleSettings/SlidePuzzle")]
public class SlidePuzzleSettings : ScriptableObject
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

    public int ExtraTimers { get => extraTimers; set => extraTimers = value; }
    public float ExtraTimerValue { get => extraTimerValue; set => extraTimerValue = value; }
    public float BaseTime { get => baseTime; set => baseTime = value; }
    public float CurseMeterPerExtraTime { get => curseMeterPerExtraTime; set => curseMeterPerExtraTime = value; }
}
