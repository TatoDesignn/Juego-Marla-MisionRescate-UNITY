using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPipe : MonoBehaviour
{
    [SerializeField] Transform[] MyPositions = new Transform[4];
    private int Stage = 0;


    public void Rotate()
    {
        this.transform.DOMove(MyPositions[Stage].position, 0.2f);
        this.transform.DORotate(MyPositions[Stage].eulerAngles, 0.3f);
        Stage++;
        if (Stage == MyPositions.Length)
            Stage = 0;
    }
}
