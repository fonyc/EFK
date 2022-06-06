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

public class SolvePuzzle : Arguments
{
    public SolvePuzzle() { }
}

public class ShowInteraction : Arguments
{
    public Sprite sprite;

    public ShowInteraction(Sprite sprite) 
    {
        this.sprite = sprite;
    }
}

public class PaintError : Arguments
{
    public PaintError() {}
}

