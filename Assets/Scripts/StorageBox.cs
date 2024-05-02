using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class StorageBox : PuzzlesFather
{

    [SerializeField] private bool Completed = false;
    private bool ActiveKey = false;
    [SerializeField] private Image D;
    [SerializeField] private Image A;
    private InputManager inputManager;
    [SerializeField] private int Steps = 0;
    [SerializeField] private int stepsNeeded;

    public override void Interact()
    {
        this.enabled = true;
        if (Completed)
        {
            Exit();
            return;
        }
        else
            this.PuzzleHolder.SetActive(true);
    }

    public override void Exit()
    {
        this.PuzzleHolder.SetActive(false);
        this.enabled = false;
    }

    private void FixedUpdate()
    {
        if (!ActiveKey)
        {
            A.color = Color.gray;
            D.color = Color.white;
        }
        else
        {
            D.color = Color.gray;
            A.color = Color.white;
        }
        float KeyPressed = Input.GetAxis("Horizontal");
        if (KeyPressed < 0 && !ActiveKey) 
        { 
            Steps++;
            ActiveKey = true;
        }
        else if(KeyPressed > 0 && ActiveKey)
        {
            Steps++;
            ActiveKey = false;
        }
        Check4Complete();
          
    }

    private void Check4Complete()
    {
        if( Steps == stepsNeeded) 
        {
            Completed = true;
            Exit();
            Debug.Log("Ya te vestiste, la verdad irrealmente rápido");
        }
        else
            return;
    }
}
