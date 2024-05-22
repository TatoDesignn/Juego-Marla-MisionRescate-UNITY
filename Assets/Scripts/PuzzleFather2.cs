using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PuzzleFather2 : MonoBehaviourPun
{
    [SerializeField] Material[] Materials;
    [SerializeField] protected GameObject PuzzleHolder;
    [SerializeField] protected GameObject myCamera;
    private Material myMaterial;
    private Vector3 previousLocation;

    private void Awake()
    {
        myMaterial = GetComponent<Material>();
    }
    public void ChangeCamera(bool entering)
    {
        if (entering)
        {
            previousLocation = Camera.main.transform.position;
            Camera.main.transform.DOMove(myCamera.transform.position, 1f, false);
            Camera.main.transform.DORotateQuaternion(myCamera.transform.rotation, 2f);

            Interact();
        }
        else
        {
            Camera.main.transform.DOMove(previousLocation, 0.5f, false);
            Camera.main.transform.DORotate(new Vector3(0f, 0f, 0f), 1f);

            Exit();
        }
    }

    /*public void InteractabeSig(bool hit)
    {
        if (hit)
        {
            myMaterial = Materials[0];          
        }

    }*/

    public abstract void Interact();
    public abstract void Exit();
}
