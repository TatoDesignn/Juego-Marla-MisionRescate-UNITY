using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamara : MonoBehaviour
{
    [Space]
    [Header("Configuraci�n de las c�maras:")]
    [SerializeField] private GameObject[] camaras; 
    [SerializeField] private float velocidad;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoMovimiento;


    [Space]
    [Header("Variables locales:")]
    private int camaraActiva = -1;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        if (camaraActiva != -1) 
        {
            if (Input.GetKey(KeyCode.A))
            {
                camaras[camaraActiva].transform.Rotate(Vector3.up, -velocidad * Time.fixedDeltaTime);
                ReproducirSonidoMovimiento();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                camaras[camaraActiva].transform.Rotate(Vector3.up, velocidad * Time.fixedDeltaTime);
                ReproducirSonidoMovimiento();
            }
        }
    }

    public void ActivarCamara(int indice)
    {
        if (indice >= 0 && indice < camaras.Length)
        {
            camaraActiva = indice;
        }
    }

    private void ReproducirSonidoMovimiento()
{
    if (sonidoMovimiento != null && audioSource != null)
    {
        audioSource.PlayOneShot(sonidoMovimiento);
    }
}

    public void CamaraUno() { ActivarCamara(0); }
    public void CamaraDos() { ActivarCamara(1); }
    public void CamaraTres() { ActivarCamara(2); }
    public void CamaraCuatro() { ActivarCamara(3); }

}
