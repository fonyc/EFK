using UnityEngine;

[CreateAssetMenu(fileName = "New SlidePuzzle Image", menuName = "SlidePuzzle/Image")]
public class SlidePuzzleImagesSO : ScriptableObject
{
    //Refers to which image is the one that will turn transparent cause its the less important in the draw
    [SerializeField] private int voidPiece;

    [SerializeField] private Sprite[] imageList;

    public Sprite[] ImageList { get => imageList; set => imageList = value; }
    public int VoidPiece { get => voidPiece; set => voidPiece = value; }
}
