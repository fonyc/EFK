using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardMarker : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite winSprite;
    public Sprite loseSprite;
    [SerializeField] private Transform[] scoreList;
    [SerializeField] private Sprite win;
    [SerializeField] private Sprite lose;



    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        int f = scoreList.Length;
        for (int i =0;  i == f+1; i++)
        {
            scoreList[i] = transform.GetChild(i);
        }
    }

    public void Result(int game, bool victory)
    {
        switch (game)
        {
            case (0):
                if(victory) {ChangeSpritetoWin();}
                else{ChangeSpritetoLose();}
                break;
            case (1):
                if (victory){ChangeSpritetoWin();}
                else{ChangeSpritetoLose();}
                break;
            case (2):
                if (victory){ChangeSpritetoWin();}
                else
                {ChangeSpritetoLose();}
                break;
        }
    }
    private void ChangeSpritetoWin()
    {
        spriteRenderer.sprite = winSprite;
    }
    private void ChangeSpritetoLose()
    {
        spriteRenderer.sprite = loseSprite;
    }
}
