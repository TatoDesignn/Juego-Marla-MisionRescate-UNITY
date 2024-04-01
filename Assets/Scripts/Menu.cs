using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Menu : MonoBehaviourPunCallbacks
{
    [Space]
    [Header("Configuracion menu:")]
    [SerializeField] private GameObject[] interfaces;
    private bool marla = false;
    private bool jonno = false;

    private void Start()
    {
        interfaces[0].SetActive(true);
        interfaces[5].SetActive(true);
    }
    public void BotonIncio()
    {
        interfaces[0].SetActive(false);
        interfaces[1].SetActive(true);
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
    }

    public void Jonno()
    {
        Comunicador.valor = 1;
        interfaces[3].SetActive(true);
    }

    public void Conectar()
    {
        PhotonNetwork.JoinOrCreateRoom("Sala1", new Photon.Realtime.RoomOptions { MaxPlayers = 2 }, null);
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
