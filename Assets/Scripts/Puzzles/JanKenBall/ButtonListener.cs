using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonListener : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private JanKenController janKenController;
    [SerializeField] private JanKenStates currentState;


    private void Awake()
    {
        janKenController = GameObject.FindGameObjectWithTag("JanKenBallController").GetComponent<JanKenController>();
        currentState = JanKenStates.StartPhase;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Pulsaste el botón");
        janKenController.AdvanceGame();
    }
}
