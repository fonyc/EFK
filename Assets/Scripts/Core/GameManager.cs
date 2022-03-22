using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("--- GAME STATE ---")]
    [SerializeField] private GameStates _gameState = GameStates.MainMenu;

    public void SetGameState(GameStates gameState)
    {
        switch (gameState)
        {
            case (GameStates.MainMenu):
            {
                break;
            }
            case (GameStates.Explore):
            {
                break;
            }
            case (GameStates.Puzzle):
            {
                break;
            }
            case (GameStates.Ecounter):
            {
                break;
            }
            case (GameStates.GameOver):
            {
                break;
            }
        }

        _gameState = gameState;
    }
}
