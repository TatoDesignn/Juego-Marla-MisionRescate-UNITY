using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Diagnostics;

public class Menu : MonoBehaviourPunCallbacks
{
    [Space]
    [Header("Configuracion menu:")]
    [SerializeField] private GameObject[] interfaces;
    [SerializeField] private GameObject canvasCompleto;
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject camara;
    private bool marla = false;
    private bool jonno = false;
    private bool inicio = true;
    private bool seleccion = false;

    private bool unio = false;
    private bool crear = false;

    private void Start()
    {
        interfaces[0].SetActive(true);
        interfaces[5].SetActive(true);
    }

    public void BotonIncio()
    {
        interfaces[0].SetActive(false);
        interfaces[1].SetActive(true);
        inicio = false;
        seleccion = true;
        IniciarPhoton();
    }

    private void IniciarPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        interfaces[1].SetActive(false);
        interfaces[8].SetActive(true);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Crear()
    {
        PhotonNetwork.JoinOrCreateRoom("misionrescate", new Photon.Realtime.RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = 3 }, default);
        crear = true;
        unio = false;
    }

    public void Unirse()
    {
        PhotonNetwork.JoinRoom("misionrescate");
        unio = true;
        crear = false;
    }

    public override void OnJoinedRoom()
    {
        interfaces[8].SetActive(false);

        if (crear)
        {
            interfaces[9].SetActive(true);
        }
        else if (unio)
        {
            interfaces[10].SetActive(true);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        interfaces[8].SetActive(false);
        interfaces[10].SetActive(false);
        interfaces[11].SetActive(true);
    }

    public void Continuar()
    {
        interfaces[9].SetActive(false);
        interfaces[10].SetActive(false);
        interfaces[2].SetActive(true);
    }

    public void ContinuarFallo()
    {
        interfaces[11].SetActive(false);
        interfaces[8].SetActive(true);
    }

    public void Marla()
    {
        Comunicador.valor = 0;
        interfaces[3].SetActive(true);
        marla = true;
        jonno = false;
    }

    public void Jonno()
    {
        Comunicador.valor = 1;
        interfaces[3].SetActive(true);
        jonno = true;
        marla = false;
    }

    public void Buscar()
    {
        interfaces[7].SetActive(true);

        if (marla)
        {
            Invoke("ActivarStoryM", 0.1f);
        }
        else if (jonno)
        {
            Invoke("ActivarStoryJ", 0.1f);
        }
    }

    private void ActivarStoryJ()
    {
        ControllerStoryBoard.Instance.jonno = true;
        ControllerStoryBoard.Instance.StoryBoard();
    }

    private void ActivarStoryM()
    {
        ControllerStoryBoard.Instance.marla = true;
        ControllerStoryBoard.Instance.StoryBoard();
    }
    public void Conectar()
    {
        if(marla)
        {
            PhotonNetwork.Instantiate("marla", new Vector3(-22.62f, -4.15f, 8.84f), Quaternion.identity, 0);
        }
        else if(jonno)
        {
            PhotonNetwork.Instantiate("jonno", new Vector3(-18.98f, 7.16f, 37.44f), Quaternion.identity, 0);
        }

        hudCanvas.SetActive(true);
        camara.SetActive(false);
        canvasCompleto.SetActive(false);
    }

    public void Creditos()
    {
        if(inicio)
        {
            interfaces[0].SetActive(false);
            interfaces[6].SetActive(true);
        }
        else if(seleccion)
        {
            interfaces[2].SetActive(false);
            interfaces[6].SetActive(true);
        }
    }

    public void Atras()
    {
        if (inicio)
        {
            interfaces[0].SetActive(true);
            interfaces[6].SetActive(false);
        }
        else if (seleccion)
        {
            interfaces[2].SetActive(true);
            interfaces[6].SetActive(false);
        }
    }

    public void Salir()
    {
        Application.Quit();
    }
}
