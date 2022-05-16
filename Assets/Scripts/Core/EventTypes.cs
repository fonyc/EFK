using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTypes : MonoBehaviour
{ 
    
}

public class Arguments
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

public class RepaintCurseMeter : Arguments
{
    public float value;

    public RepaintCurseMeter(float value)
    {
        this.value = value;
    }
}

