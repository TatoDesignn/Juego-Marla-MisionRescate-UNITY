using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medidor : MonoBehaviourPun
{
    [SerializeField] GameObject perder;
    [SerializeField] GameObject luces;
    [SerializeField] GameObject camaras;


    public void PantallaPerder()
    {
        Cursor.lockState = CursorLockMode.Confined;
        luces.SetActive(false); 
        camaras.SetActive(false);
        Comunicador.canMove = false;
        perder.SetActive(true);
    }
}
