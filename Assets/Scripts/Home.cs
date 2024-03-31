using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class Home : MonoBehaviourPunCallbacks
{
    [Header("Configuracion del Home")]
    public GameObject conectando;
    public GameObject conectado;
    public GameObject boton1;
    public GameObject boton2;
    public GameObject conectar;

    private void Start()
    {

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        conectando.SetActive(false);
        conectado.SetActive(true);
        boton1.SetActive(true);
        boton2.SetActive(true);
    }

    public void Boton1()
    {
        Comunicador.valor = 0;
        conectar.SetActive(true);
    }

    public void Boton2()
    {
        Comunicador.valor = 1;
        conectar.SetActive(true);
    }

    public void Conectar()
    {
        PhotonNetwork.JoinOrCreateRoom("Sala1", new Photon.Realtime.RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
