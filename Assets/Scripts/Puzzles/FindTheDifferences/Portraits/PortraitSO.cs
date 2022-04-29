using UnityEngine;

[CreateAssetMenu(fileName = "New Portrait", menuName = "FindTheDifferences/Portrait")]
public class PortraitSO : ScriptableObject
{
    [SerializeField] private PortraitTypes portraitType;
    [SerializeField] private Difference[] differenceList;
    [SerializeField] private Sprite sprite;

    public Difference[] DifferenceList { get => differenceList; set => differenceList = value; }
    public PortraitTypes PortraitType { get => portraitType; set => portraitType = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }

    public int CheckQuadrant(int quadrantPressed)
    {
        int found = -1;
        foreach(Difference diff in differenceList)
        {
            if (diff.quadrantLocation == quadrantPressed) found = diff.quadrantLocation;
        }
        return found;
    }

    [System.Serializable]
    public class Difference
    {
        public int quadrantLocation;
        public bool isSolved;
    }


}