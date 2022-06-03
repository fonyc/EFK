using UnityEngine;

public class GameProgress_Data : MonoBehaviour
{
    [SerializeField] private float curseMeter;
    [SerializeField] private bool[] collectibleList;
    [SerializeField] private bool choice;

    public float CurseMeter { get => curseMeter; set => curseMeter = value; }
    public bool[] CollectibleList { get => collectibleList; set => collectibleList = value; }
    public bool Choice { get => choice; set => choice = value; }

    public GameProgress_Data()
    {
        curseMeter = 0;
        collectibleList = new bool[10];
        choice = false;
    }

}
