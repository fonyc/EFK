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
    private int randomnumber;
    private int games;

    void Start()
    {
        currentState = JanKenStates.StartPhase;
        //Debug.Log("Estamos en la fase incial, " + currentState);
        games = -1;
    }

    void Update()
    {

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
                //Aparecen las cartas del conde, reveladas, y el botón de jugar, esperamos que el jugador pulse el botón para pasar a la siguiente fase
                //Debug.Log("Estamos en fase start, esperamos a que el jugador pulse el botón");
                cardAnimator.SetBool("Restart", false);
                cardAnimator.SetBool("RP", false);
                cardAnimator.SetBool("RR", false);
                cardAnimator.SetBool("RS", false);
                cardAnimator.SetBool("PP", false);
                cardAnimator.SetBool("PR", false);
                cardAnimator.SetBool("PS", false);
                cardAnimator.SetBool("SP", false);
                cardAnimator.SetBool("SR", false);
                cardAnimator.SetBool("SS", false);

                break;

            case JanKenStates.CountPhase:
                //Comenzamos la animación del conde y elegimos una para la computadora, destruimos las cartas sobrantes y activamos la elegida del conde.
                //Luego aparecen las cartas del jugador reveladas y esperamos a que pulse botón para ir a la siguiente fase.
                //Debug.Log("Estamos en fase count, esperamos las animaciones, y luego que el jugador pulse el botón");
                cardAnimator.SetBool("Flip", true);

                randomnumber = Random.Range(0, 4);
                cardAnimator.SetInteger("CountShuffleRnd", randomnumber);
                //Debug.Log("El número es " + randomnumber);

                //el conde elige carta aquí
                int randomcard = Random.Range(0, 3);

                //Debug.Log("El conde ha elegido la carta " + randomcard);

                switch (randomcard)
                {
                    case 0:
                        //se ha elegido "rock"
                        //Debug.Log("El conde elige piedra");
                        countCard = CardType.Rock;
                        cardAnimator.SetInteger("CountPick", randomcard);
                        break;

                    case 1:
                        //se ha elegido "paper"
                        //Debug.Log("El conde elige papel");
                        countCard = CardType.Paper;
                        cardAnimator.SetInteger("CountPick", randomcard);
                        break;

                    case 2:
                        //se ha elegido "scissors"
                        //Debug.Log("El conde elige tijeras");
                        countCard = CardType.Scissors;
                        cardAnimator.SetInteger("CountPick", randomcard);
                        break;
                }

                break;


            case JanKenStates.PlayerPhase:
                //Comenzamos la animación del player y esperamos a que elija carta con el double tap. Después, si llevamos 3 partidas acabamos, si no, reiniciamos loop.

                //Debug.Log("Estamos en fase player");
                cardAnimator.SetBool("Flip", false);
                randomnumber = Random.Range(0, 4);
                cardAnimator.SetInteger("PlayerShuffleRnd", randomnumber);

                //Comprobamos si es victoria o derrota

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
        //Debug.Log("Phase vale " + phase);
        phase = phase == 2 ? 0 : phase + 1;
        //Debug.Log("Phase vale " + phase);

        JanKenStates state = (JanKenStates)phase;
        //Debug.Log("El estado actual es " + state);

        SwitchGamePhase(state);

    }

    public void Win()
    {
        games = games++;
        scoreboardMarker.Result(games, true);
        if (games < 2)
        {
            cardAnimator.SetBool("Restart", true);
        }
        else
        {
            Debug.Log("¡HAS TERMINADO EL JUEGO!");
        }
    }

    public void Lose()
    {
        games = games++;
        scoreboardMarker.Result(games, false);
        if (games != 2)
        {
            cardAnimator.SetBool("Restart", true);
        }
        else
        {
            Debug.Log("¡HAS TERMINADO EL JUEGO!");
        }
    }
}
