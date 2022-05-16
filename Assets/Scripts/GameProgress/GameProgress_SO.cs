using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Game Progress", menuName = "Game Progress")]
public class GameProgress_SO : ScriptableObject
{
    [SerializeField] private float curseMeter;
    [SerializeField] private bool[] collectibleList;
    [SerializeField] private bool choice;

    public float CurseMeter { get => curseMeter; set => curseMeter = value; }
    public bool[] CollectibleList { get => collectibleList; set => collectibleList = value; }
    public bool Choice { get => choice; set => choice = value; }
}
