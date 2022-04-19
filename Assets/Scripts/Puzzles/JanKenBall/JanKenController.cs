using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanKenController : MonoBehaviour
{

    [SerializeField] private CardType selectedCard;
    [SerializeField] private CardType chosenCard;

    void Start()
    {
        
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
        }

    }

}
