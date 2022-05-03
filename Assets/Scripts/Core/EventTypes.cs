using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTypes : MonoBehaviour
{ 
    
}

public class Arguments
{

}

public class LockCamera : Arguments
{

}

public class AddCurseMeter : Arguments
{
    public float value;

    public AddCurseMeter(float value)
    {
        this.value = value;
    }
}

