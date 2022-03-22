using UnityEngine;

namespace EFK.Puzzles
{
    public class SimonSays : MonoBehaviour
    {
        [SerializeField] private GameObject[] keyList;

        //[Header("--- GAME SETTINGS ---")]
        //[Space(5)]
        //[SerializeField] private int roundNumber = 6;
        //[SerializeField] private int[]

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }


        #region GAME LOGIC 

        public void OnKeyPressed(int keyNumber)
        {
            Debug.Log("Key number " + keyNumber + " was pressed");
        }

        #endregion
    }
}
