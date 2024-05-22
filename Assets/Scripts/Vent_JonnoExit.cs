using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Vent_JonnoExit : MonoBehaviourPun
{
    private Camera Camera;
    [SerializeField] private GameObject cinematica;
    [SerializeField] private GameObject interfaz;
    [SerializeField] private GameObject interfaz2;

    public void Interact(Camera camera, GameObject jonno)
    {
        Camera = camera;
        StartCoroutine("CloseView", Camera);
    }

    IEnumerator CloseView(Camera camera)
    {
        camera.DOFieldOfView(0, 1f);

        photonView.RPC("RCP_Cinematic", RpcTarget.All);

        /*
         * AQUÍ VA EL CÓDIGO PARA 
         * CUANDO EMPIEZA LA CINEMÁTICA
         * FINAL DE JONNO
         * */

       yield return null;
    }

    [PunRPC]
    void RCP_Cinematic()
    {
        interfaz2.SetActive(false);
        cinematica.SetActive(true);
        Invoke("ActiveInterface", 15f);
    }

    private void ActiveInterface()
    {
        Cursor.lockState = CursorLockMode.Confined;
        interfaz.SetActive(true);
    }
}
