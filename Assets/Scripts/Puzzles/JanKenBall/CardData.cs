using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardData : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private CardType cardID;
    [SerializeField] private bool isSelected;
    [SerializeField] private JanKenController janKenController;

    private void Awake()
    {
        janKenController = GameObject.FindGameObjectWithTag("JanKenBallController").GetComponent<JanKenController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        janKenController.PickaCard(cardID);
    }

}
