using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EFK.Interactables
{
    public class Collectible : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.tag = "Interactable";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
