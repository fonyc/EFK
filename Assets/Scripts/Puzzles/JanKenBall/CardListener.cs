using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardListener : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardType cardID;
    [SerializeField] private JanKenController janKenController;
    private void Awake()
    {
        janKenController = GameObject.FindGameObjectWithTag("JanKenBallController").GetComponent<JanKenController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (janKenController.currentState != JanKenStates.PlayerPhase) return;
        janKenController.PickaCard(cardID);
    }
}
