using UnityEngine;

[CreateAssetMenu(fileName = "New Dice", menuName = "Diceit/Dice")]
public class DiceSO : ScriptableObject
{
    [SerializeField] private int diceId;
    [SerializeField] private Sprite image;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool canBeDragged;

    public bool CanBeDragged { get => canBeDragged; set => canBeDragged = value; }
    public bool IsLocked { get => isLocked; set => isLocked = value; }
    public Sprite Image { get => image; set => image = value; }
    public int DiceId { get => diceId; set => diceId = value; }
}
