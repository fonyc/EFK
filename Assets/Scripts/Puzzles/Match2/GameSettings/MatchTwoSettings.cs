using UnityEngine;

[CreateAssetMenu(fileName = "New MatchTwo Settings", menuName = "PuzzleSettings/MatchTwo")]
public class MatchTwoSettings : ScriptableObject
{
    [Header("--- GAME SETTINGS ---")]
    [Space(5)]
    [SerializeField] private int curseMeterPenaltyPerExtraMovement;

    [Space(5)]
    [SerializeField] private int maxMovements;

    [Space(5)]
    [SerializeField] private int numberOfJokers;

    [Space(5)]
    [SerializeField] private int jokerRoundSpawn;

    public int JokerRoundSpawn { get => jokerRoundSpawn; set => jokerRoundSpawn = value; }
    public int NumberOfJokers { get => numberOfJokers; set => numberOfJokers = value; }
    public int MaxMovements { get => maxMovements; set => maxMovements = value; }
    public int CurseMeterPenaltyPerExtraMovement { get => curseMeterPenaltyPerExtraMovement; set => curseMeterPenaltyPerExtraMovement = value; }
}
