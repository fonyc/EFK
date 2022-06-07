using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanKenController : MonoBehaviour
{

    [SerializeField] private CardType selectedCard;
    [SerializeField] private CardType chosenCard;
    [SerializeField] private CardType countCard;
    [SerializeField] private JanKenSettingsSO difficultySettings;
    [SerializeField] public JanKenStates currentState;
    [SerializeField] private ScoreboardMarker scoreboardMarker;
    public Animator cardAnimator;
    private int countRandom, playerRandom;
    public int games;

    void Start()
    {
        currentState = JanKenStates.StartPhase;
        games = 1;
        chosenCard = CardType.None;
    }
    public void PickaCard(CardType card)
    {
        if (selectedCard != card)
        {
            Debug.Log("He pre-seleccionado la carta " + card);
            selectedCard = card;
        }
        else
        {
            Debug.Log("He elegido la carta " + card);
            chosenCard = card;
            cardAnimator.SetBool("PlayerChose", true);
            AdvanceGame();
        }
    }
    public void GamePhase(JanKenStates newState)
    {
        switch (newState)
        {
            case JanKenStates.StartPhase:
                //Esta fase es para que el jugador se coloque, simplemente esperamos pulsar botón.
                break;
            case JanKenStates.CountPhase:
                //En esta fase juega y elige el conde. Tras elegir su carta, salen las nuestras y esperamos botón de nuevo.
                cardAnimator.SetBool("Flip", true);
                RestartAnimationValues();
                CountPick();
                break;
            case JanKenStates.PlayerPhase:
                //Comenzamos la animación del player y esperamos a que elija carta con el double tap.
                cardAnimator.SetBool("Flip", false);
                playerRandom = Random.Range(0, 4);
                cardAnimator.SetInteger("PlayerShuffleRnd", playerRandom);
                Debug.Log("La carta del conde es " + countCard + " y la nuestra es " + chosenCard);
                break;
            case JanKenStates.RevealingPhase:
                if (chosenCard == CardType.None) return;
                RevealWinner();
                break;
        }
        currentState = newState;
    }
    public void AdvanceGame()
    {
        int phase = (int)currentState;
        phase = phase == 3 ? 0 : phase + 1;
        JanKenStates state = (JanKenStates)phase;
        GamePhase(state);
    }
    public void Win()
    {
        Debug.Log("Ganaste");
        games = games + 1;
        scoreboardMarker.Result(games, true);
        if (games < 4)
        {
            cardAnimator.SetBool("Restart", true);
        }
        else
        {
            cardAnimator.SetBool("Restart", false);
            Debug.Log("¡HAS TERMINADO EL JUEGO!");
        }
        Debug.Log("Avanzamos a la partida " + games);
    }
    public void Lose()
    {
        Debug.Log("Perdiste");
        games = games + 1;
        scoreboardMarker.Result(games, false);
        //Trigger Curse Meter Event
        AddCurseMeter addCurseMeter = new AddCurseMeter(difficultySettings.CurseMeterPenaltyPerLoss);
        EventManager.TriggerEvent(addCurseMeter);
        if (games != 4)
        {
            cardAnimator.SetBool("Restart", true);
        }
        else
        {
            cardAnimator.SetBool("Restart", false);
            Debug.Log("¡HAS TERMINADO EL JUEGO!");
        }
        Debug.Log("Avanzamos a la partida " + games);
    }
    public void RestartAnimationValues()
    {
        cardAnimator.SetBool("RP", false);
        cardAnimator.SetBool("RR", false);
        cardAnimator.SetBool("RS", false);
        cardAnimator.SetBool("PP", false);
        cardAnimator.SetBool("PR", false);
        cardAnimator.SetBool("PS", false);
        cardAnimator.SetBool("SP", false);
        cardAnimator.SetBool("SR", false);
        cardAnimator.SetBool("SS", false);
        cardAnimator.SetBool("PlayerChose", false);
        chosenCard = CardType.None;
    }
    private void CountPick()
    {
        countRandom = Random.Range(0, 4);
        cardAnimator.SetInteger("CountShuffleRnd", countRandom);
        int randomcard = Random.Range(0, 3);
        switch (randomcard)
        {
            case 0:
                countCard = CardType.Rock;
                cardAnimator.SetInteger("CountPick", randomcard);
                break;
            case 1:
                countCard = CardType.Paper;
                cardAnimator.SetInteger("CountPick", randomcard);
                break;
            case 2:
                countCard = CardType.Scissors;
                cardAnimator.SetInteger("CountPick", randomcard);
                break;
        }
    }
    private void RevealWinner()
    {
        switch (countCard)
        {
            case CardType.Rock:
                switch (chosenCard)
                {
                    case CardType.Paper:
                        cardAnimator.SetBool("RP", true);
                        Win();
                        break;
                    case CardType.Rock:
                        cardAnimator.SetBool("RR", true);
                        Lose();
                        break;
                    case CardType.Scissors:
                        cardAnimator.SetBool("RS", true);
                        Lose();
                        break;
                }
                break;

            case CardType.Paper:
                switch (chosenCard)
                {
                    case CardType.Paper:
                        cardAnimator.SetBool("PP", true);
                        Lose();
                        break;
                    case CardType.Rock:
                        cardAnimator.SetBool("PR", true);
                        Lose();
                        break;
                    case CardType.Scissors:
                        cardAnimator.SetBool("PS", true);
                        Win();
                        break;
                }
                break;

            case CardType.Scissors:
                switch (chosenCard)
                {
                    case CardType.Paper:
                        cardAnimator.SetBool("SP", true);
                        Lose();
                        break;
                    case CardType.Rock:
                        cardAnimator.SetBool("SR", true);
                        Win();
                        break;
                    case CardType.Scissors:
                        cardAnimator.SetBool("SS", true);
                        Lose();
                        break;
                }
                break;
        }
    }
}
