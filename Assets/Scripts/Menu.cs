using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class Menu : MonoBehaviourPunCallbacks
{
    [Space]
    [Header("Configuracion menu:")]
    [SerializeField] private GameObject[] interfaces;
    private bool marla = false;
    private bool jonno = false;
    private bool inicio = true;
    private bool seleccion = false;


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
        interfaces[2].SetActive(true);
    }

    public void Marla()
    {
        Comunicador.valor = 0;
        interfaces[3].SetActive(true);
        marla = true;
    }

    public void Jonno()
    {
        Comunicador.valor = 1;
        interfaces[3].SetActive(true);
        jonno = true;
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
        PhotonNetwork.JoinOrCreateRoom("Sala1", new Photon.Realtime.RoomOptions { MaxPlayers = 2 }, null);
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

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
