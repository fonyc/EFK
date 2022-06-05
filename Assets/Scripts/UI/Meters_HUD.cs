using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EFK.UI
{
    public class Meters_HUD : MonoBehaviour
    {
        [SerializeField] RectTransform curseMeterBar;

        #region LISTENERS
        private UnityAction<object> curseMeterRepainterListener;
        #endregion

        private void Awake()
        {
            curseMeterRepainterListener = new UnityAction<object>(ManageEvent);
        }

        private void Start()
        {
            Type addCurseMeterType = typeof(AddCurseMeter);
            EventManager.StartListening(addCurseMeterType, curseMeterRepainterListener);

            UpdateCurseMeterBetweenScenes();
        }

        private void ManageEvent(object argument)
        {
            switch (argument)
            {
                case RepaintCurseMeter vartype:
                    Debug.Log("Actualizando del HUD para mostrar " + vartype.value);
                    ModifyCurseMeterBar(vartype.value);
                    break;
            }
        }

        private void UpdateCurseMeterBetweenScenes()
        {
            float curseMeterValue = GameObject.FindGameObjectWithTag("GameProgressData")
                .GetComponent<GameProgress_Data>()
                .CurseMeter;

            ModifyCurseMeterBar(curseMeterValue);
        }

        private void ModifyCurseMeterBar(float value)
        {
            //Given that the bar ranges from 0 to 1, transform the value to adjust to that margins
            float scaledValue = value / 100;
            curseMeterBar.localScale = new Vector3(
                curseMeterBar.localScale.x,
                scaledValue,
                curseMeterBar.localScale.z);
        }

        private void OnDisable()
        {
            Type addCurseMeterType = typeof(AddCurseMeter);
            EventManager.StopListening(addCurseMeterType, curseMeterRepainterListener);
        }
    }

}

