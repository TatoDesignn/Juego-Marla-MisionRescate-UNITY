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
    [SerializeField] private Slider Slider;

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
        Slider.maxValue = stepsNeeded;
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
            A.enabled = true;
            D.enabled = false;
        }
        else
        {
            D.enabled = true;
            A.enabled = false;
        }
        float KeyPressed = Input.GetAxis("Horizontal");
        if (KeyPressed < 0 && !ActiveKey)
        {
            Steps++;
            ActiveKey = true;
        }
        else if (KeyPressed > 0 && ActiveKey)
        {
            Steps++;
            ActiveKey = false;
        }
        Check4Complete();
          
    }

    private void LateUpdate()
    {
        Slider.value = Steps;
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
