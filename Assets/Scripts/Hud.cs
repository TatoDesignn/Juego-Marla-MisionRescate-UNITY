using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private Image imagen;
    [SerializeField] private Sprite[] jugadores;
    [SerializeField] private GameObject ajustes;
    [SerializeField] private GameObject controles;
    [SerializeField] private GameObject controlesInicio;
    [SerializeField] private GameObject creditos;
    [SerializeField] private GameObject salir;

    public bool activado;

    void Start()
    {
        controlesInicio.SetActive(true);
        activado = true;
        Cursor.lockState = CursorLockMode.Confined;
        Comunicador.canMove = false;

        if (Comunicador.valor == 0)
        {
            imagen.sprite = jugadores[0];
        }
        else if(Comunicador.valor == 1)
        {
            imagen.sprite = jugadores[1];
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !activado)
        {
            ajustes.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Comunicador.canMove = false;
            activado = true;
        }
    }

    public void Controles()
    {
        ajustes.SetActive(false);
        controles.SetActive(true);
    }

    public void Creditos()
    {
        ajustes.SetActive(false);
        creditos.SetActive(true);
    }

    public void Cerrar()
    {
        ajustes.SetActive(false);
        controles.SetActive(false);
        creditos.SetActive(false);
        salir.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Comunicador.canMove = true;
        activado = false;
    }

    public void Continuar()
    {
        controlesInicio.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Comunicador.canMove = true;
        activado = false;
    }

    public void Salir()
    {
        ajustes.SetActive(false);
        salir.SetActive(true);
    }

    public void Si()
    {
        Application.Quit();
    }
}
