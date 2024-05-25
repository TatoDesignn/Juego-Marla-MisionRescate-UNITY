using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class StorageBox : PuzzlesFather
{
    Hud hud;
    [SerializeField] private bool Completed = false;
    private bool ActiveKey = false;
    [SerializeField] private Image D;
    [SerializeField] private Image A;
    [SerializeField] private GameObject DBase;
    [SerializeField] private GameObject ABase;
    private InputManager inputManager;
    [SerializeField] private int Steps = 0;
    [SerializeField] private int stepsNeeded;
    [SerializeField] private Slider Slider;
    [SerializeField] GameObject _Door;
    [SerializeField] private Image PresstoExit;
    [SerializeField] ParticleSystem Signifier;

    public override void Interact()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
        hud.activado = true;
        this.enabled = true;
        if (Completed)
        {
            Exit();
            return;
        }
        else
            this.PuzzleHolder.SetActive(true);
        Slider.maxValue = stepsNeeded;
        PresstoExit.enabled = false;
    }

    public override void Exit()
    {
        hud.activado = false;
        this.PuzzleHolder.SetActive(false);
        Signifier.gameObject.SetActive(false);
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
            _Door.SetActive(false);
            Completed = true;
            DBase.SetActive(false);
            A.enabled = false;
            ABase.SetActive(false);
            D.enabled = false;
            PresstoExit.enabled = true;
        }
        else
            return;
    }
}
