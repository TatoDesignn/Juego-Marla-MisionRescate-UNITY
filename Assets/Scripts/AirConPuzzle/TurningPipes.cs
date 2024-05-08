using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurningPipes : PuzzlesFather
{
    [SerializeField] GameObject[] Owners;
    Image[] RotatingPipes;
    Pipes[] pipes;
    [SerializeField] Color[] Colors;
    private int index = 0;

    private void Awake()
    {
        RotatingPipes = new Image[Owners.Length];
        pipes = new Pipes[Owners.Length];
        int j = 0;
        foreach(GameObject SoCalled in Owners)
        {
            RotatingPipes[j] = SoCalled.transform.GetComponent<Image>();
            j++;
        }
        int i = 0;

        foreach (GameObject pipe in Owners)
        {
            pipes[i] = pipe.GetComponent<Pipes>();
            i++;
        }
    }
    public override void Interact()
    {
        this.PuzzleHolder.SetActive(true);
        RotatingPipes[index].color = Color.gray;
    }

    public override void Exit()
    {
        this.PuzzleHolder.SetActive(false);
    }

    private void ChangePipe(int dir)
    {
        if(this.PuzzleHolder == isActiveAndEnabled)
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
                if (index < RotatingPipes.Length-1) 
                {
                    index++;
                }
            }
            RotatingPipes[index].color = Color.gray;
        }
        else
            return;
    }

    private void RotatePipe()
    {
        pipes[index].Rotate();
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
