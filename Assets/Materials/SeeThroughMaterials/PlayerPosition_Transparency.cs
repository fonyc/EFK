using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition_Transparency : MonoBehaviour
{
    [SerializeField] Material wallMaterial;
    [SerializeField] Transform playerPosition;

    [SerializeField] private static int sizeId = Shader.PropertyToID("_CircleSize");


    void Update()
    {
        if((transform.position.z > playerPosition.transform.localPosition.z))
        {
            Debug.Log(gameObject.name + "/" + transform.position.z + "/" + playerPosition.transform.localPosition.z);
            wallMaterial.SetFloat(sizeId, 0);
        }
        else
        {
            wallMaterial.SetFloat(sizeId, 0.5f);
        }
    }
}
