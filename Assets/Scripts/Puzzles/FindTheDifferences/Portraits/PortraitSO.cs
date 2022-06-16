using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Portrait", menuName = "FindTheDifferences/Portrait")]
public class PortraitSO : ScriptableObject
{
    [SerializeField] private PortraitTypes portraitType;
    [SerializeField] private List<int> differenceList;
    [SerializeField] private Sprite sprite;

    public PortraitTypes PortraitType { get => portraitType; set => portraitType = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public List<int> DifferenceList { get => differenceList; set => differenceList = value; }

    /// <summary>
    /// Method that given a quadrant, returns the index of where is located that value in the difference list
    /// </summary>
    /// <param name="quadrantPressed"></param>
    /// <returns>The index of the quadrant value. Returns -1 if not found</returns>
    public int CheckQuadrant(int quadrantPressed)
    {
        if (differenceList.Contains(quadrantPressed))
        {
            return differenceList.IndexOf(quadrantPressed);
        }
        else
        {
            return -1;
        }
    }
}