using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamara : MonoBehaviourPun
{
    [Space]
    [Header("Configuraci�n de las c�maras:")]
    [SerializeField] private GameObject[] camaras; 
    [SerializeField] private float velocidad;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoMovimiento;

    public bool activo = false;

    [Space]
    [Header("Variables locales:")]
    private int camaraActiva = -1;

    void Update()
    {
        if (activo)
        {
            if (Input.GetKey(KeyCode.A))
            {
                photonView.RPC("RotateCameraRPC", RpcTarget.All, -velocidad * Time.fixedDeltaTime, camaraActiva);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                photonView.RPC("RotateCameraRPC", RpcTarget.All, velocidad * Time.fixedDeltaTime, camaraActiva);
            }
        }
    }

    [PunRPC]
    void RotateCameraRPC(float amount, int activeCameraIndex)
    {
        if (activeCameraIndex >= 0 && activeCameraIndex < camaras.Length)
        {
            camaras[activeCameraIndex].transform.Rotate(Vector3.up, amount);
            ReproducirSonidoMovimiento();
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
