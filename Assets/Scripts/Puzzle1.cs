using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : PuzzlesFather
{
    public override void Interact()
    {
        Debug.Log("Puzzle Activado, pero a qué costo?");
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

}
