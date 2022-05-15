using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardMarker : MonoBehaviour
{
    public GameObject score0, score1, score2;


    public void Result(int game, bool victory)
    {
        switch (game)
        {
            case (0):
                if(victory == true)
                {
                score0 = GameObject.Find("/Jan-Ken-Ball/Board/Scoreboard/Score_1_Win");
                score0.SetActive(true);
                }

                else
                {
                score0 = GameObject.Find("/Jan-Ken-Ball/Board/Scoreboard/Score_1_Lose");
                score0.SetActive(true);
                }
                break;

            case (1):
                if (victory == true)
                {
                    score1 = GameObject.Find("/Jan-Ken-Ball/Board/Scoreboard/Score_2_Win");
                    score1.SetActive(true);
                }

                else
                {
                    score1 = GameObject.Find("/Jan-Ken-Ball/Board/Scoreboard/Score_2_Lose");
                    score1.SetActive(true);
                }
                break;

            case (2):
                if (victory == true)
                {
                    score2 = GameObject.Find("/Jan-Ken-Ball/Board/Scoreboard/Score_3_Win");
                    score2.SetActive(true);
                }

                else
                {
                    score2 = GameObject.Find("/Jan-Ken-Ball/Board/Scoreboard/Score_3_Lose");
                    score2.SetActive(true);
                }
                break;
        }
            
    }



}
