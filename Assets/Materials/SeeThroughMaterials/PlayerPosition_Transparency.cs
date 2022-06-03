using UnityEngine;

public class PlayerPosition_Transparency : MonoBehaviour
{
    [SerializeField] Transform playerPosition;

    [SerializeField] private static int sizeId = Shader.PropertyToID("_CircleSize");

    

    void Update()
    {
        if(playerPosition == null)
        {
            Debug.LogWarning("No player position in shader transparency");
            return;
        }

        if((transform.position.z > playerPosition.transform.position.z))
        {
            ChangeCircleSize(0f);
            
        }
        else
        {
            ChangeCircleSize(0.5f);
            Debug.Log(gameObject.name);
        }
    }

    private void ChangeCircleSize(float qty)
    {
        GetComponent<Renderer>().material.SetFloat("_CircleSize", qty);
    }
}
