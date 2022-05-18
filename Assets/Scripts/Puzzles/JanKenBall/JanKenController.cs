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
    private int countrnnumber, playerrnnumber;
    public int games;

    void Start()
    {
        currentState = JanKenStates.StartPhase;
        games = -1;
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
            cardAnimator.SetTrigger("PlayerChose");
        }
    }
    public void SwitchGamePhase(JanKenStates newState)
    {
        switch (newState)
        {
            case JanKenStates.StartPhase:
                break;

            case JanKenStates.CountPhase:
                //Comenzamos la animación del conde y elegimos una para la computadora, destruimos las cartas sobrantes y activamos la elegida del conde.
                //Luego aparecen las cartas del jugador reveladas y esperamos a que pulse botón para ir a la siguiente fase.
                cardAnimator.SetBool("Flip", true);
                RestartAnimationValues();
                countrnnumber = Random.Range(0, 4);
                cardAnimator.SetInteger("CountShuffleRnd", countrnnumber);
                //el conde elige carta aquí
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

                break;
            case JanKenStates.PlayerPhase:
                //Comenzamos la animación del player y esperamos a que elija carta con el double tap. Después, si llevamos 3 partidas acabamos, si no, reiniciamos loop.
                cardAnimator.SetBool("Flip", false);
                playerrnnumber = Random.Range(0, 4);
                cardAnimator.SetInteger("PlayerShuffleRnd", playerrnnumber);
                if (chosenCard == CardType.None) return;
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
                break;
        }
        currentState = newState;
    }
    public void AdvanceGame()
    {
        int phase = (int)currentState;
        phase = phase == 2 ? 0 : phase + 1;
        JanKenStates state = (JanKenStates)phase;
        SwitchGamePhase(state);
    }
    public void Win()
    {
        Debug.Log("Ganaste");
        games = games + 1;
        scoreboardMarker.Result(games, true);
        if (games < 2)
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
        if (games != 2)
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
        chosenCard = CardType.None;
    }
}
