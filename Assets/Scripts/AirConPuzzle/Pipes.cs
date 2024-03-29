using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipes : MonoBehaviour
{
    [SerializeField] GameObject[] MyPositions = new GameObject[4];
    private int Pos = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    public void Rotate()
    {
        this.transform.position = MyPositions[Pos].transform.position;
        this.transform.DORotate(MyPositions[Pos].transform.eulerAngles,0.3f);
        Pos++;
        if (Pos == MyPositions.Length)
            Pos = 0;
    }
    
}
