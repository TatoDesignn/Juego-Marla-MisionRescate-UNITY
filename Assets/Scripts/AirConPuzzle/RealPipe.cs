using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPipe : MonoBehaviour
{
    [SerializeField] Vector3[] MyPositions = new Vector3[4];
    [SerializeField] Vector3[] MyRotations = new Vector3[4];
    private int Stage = 0;


    public void Rotate()
    {
        this.transform.DOMove(MyPositions[Stage], 0.2f);
        this.transform.DORotate(MyRotations[Stage], 0.3f);
        Stage++;
        if (Stage == MyRotations.Length)
            Stage = 0;
    }
}
