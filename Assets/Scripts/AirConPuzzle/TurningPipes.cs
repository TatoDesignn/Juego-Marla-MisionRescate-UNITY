using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurningPipes : PuzzlesFather
{
    [SerializeField] Image[] RotatingPipes;
    Pipes[] pipes;
    [SerializeField] Color[] Colors;
    private int index = 0;

    /*private void Awake()
    {
        int i = 0;
        foreach (var pipe in RotatingPipes)
        {
            pipes[i] = pipe.GetComponent<Pipes>();
            i++;
        }
    }*/
    public override void Interact()
    {
        this.PuzzleHolder.SetActive(true);
        ChangePipe(0);
    }

    public override void Exit()
    {
        this.PuzzleHolder.SetActive(false);
    }

    private void ChangePipe(int dir)
    {
        RotatingPipes[index].color = Color.white;
        if(dir < 0)
        {
            if(index > 0)
            {
                index--;
            }
        }
        else
        {
            if (index < RotatingPipes.Length) 
            {
                index++;
            }
        }
        RotatingPipes[index].color = Color.gray;
    }

    private void RotatePipe()
    {
        RotatingPipes[index].transform.DORotate((RotatingPipes[index].transform.eulerAngles + new Vector3(0, 0, 90)), 1.5f);
        if (RotatingPipes[index].transform.eulerAngles.z > 359)
            RotatingPipes[index].transform.eulerAngles = new Vector3(0,0,0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            ChangePipe(1);
        }
        else if(Input.GetKeyDown(KeyCode.A)) 
        {
            ChangePipe(-1);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            RotatePipe();
        }
    }

}
