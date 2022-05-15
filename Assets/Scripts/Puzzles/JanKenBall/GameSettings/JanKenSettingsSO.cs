using UnityEngine;

[CreateAssetMenu(fileName = "New JanKen Settings", menuName = "PuzzleSettings/JanKen")]
public class JanKenSettingsSO : ScriptableObject
{
    [Header("--- GAME SETTINGS ---")]
    [Space(5)]
    [SerializeField] private int curseMeterPenaltyPerLoss;

    [Space(5)]
    [SerializeField] private int curseMeterPenaltyPerTie;

    [Space(5)]
    [SerializeField] private int speed;

    public int Speed { get => speed; set => speed = value; }
    public int CurseMeterPenaltyPerTie { get => curseMeterPenaltyPerTie; set => curseMeterPenaltyPerTie = value; }
    public int CurseMeterPenaltyPerLoss { get => curseMeterPenaltyPerLoss; set => curseMeterPenaltyPerLoss = value; }

}
