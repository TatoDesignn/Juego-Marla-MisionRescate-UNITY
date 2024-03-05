using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzlesFather : MonoBehaviour
{   
    [SerializeField] Camera myCamera;
    private Vector3 previousLocation;

    public void ChangeCamera(bool entering)
    {
        if (entering)
        {
            previousLocation = Camera.main.transform.position;
            Camera.main.transform.position = myCamera.transform.position;
            Camera.main.transform.rotation = myCamera.transform.rotation;


            Interact();
        }
        else
        {
            Camera.main.transform.position = previousLocation;
        }
    }
    public abstract void Interact();

   
}
