using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardData : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private CardType cardID;
    [SerializeField] private bool isSelected;
    [SerializeField] private JanKenController janKenController;

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
