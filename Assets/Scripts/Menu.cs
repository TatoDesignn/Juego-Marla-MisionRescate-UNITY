using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private Button botonMarla;
    [SerializeField] private Button botonJonno;
    [SerializeField] private GameObject[] cinematicas;

    [Space]
    [Header("Personaje Seleccionado:")]
    private bool marla = false;
    private bool actualizarMarla = false;
    private bool jonno = false;
    private bool actualizarJonno = false;

    [Space]
    [Header("Ventana/ubicacion:")]
    private bool inicio = true;
    private bool sala = false;
    private bool seleccion = false;

    [Space]
    [Header("Configurar sala:")]
    private bool unio = false;
    private bool crear = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Primera();
    }

    private void Primera()
    {
        interfaces[18].SetActive(true);
        Invoke("Segunda", 11f);
    }

    private void Segunda()
    {
        interfaces[18].SetActive(false);
        interfaces[19].SetActive(true);
        interfaces[0].SetActive(true);
        interfaces[5].SetActive(true);
    }

    private void Update()
    {
        if (actualizarMarla && seleccion)
        {
            interfaces[13].SetActive(true);
        }
        else if (actualizarJonno && seleccion)
        {
            interfaces[14].SetActive(true);
        }
    }

    public void BotonIncio()
    {
        interfaces[0].SetActive(false);
        interfaces[1].SetActive(true);
        inicio = false;
        sala = true;
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
        interfaces[16].SetActive(false);
    }

    public void Unirse()
    {
        PhotonNetwork.JoinRoom("misionrescate");
        unio = true;
        crear = false;
        interfaces[16].SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        interfaces[8].SetActive(false);
        sala = false;

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
        interfaces[16].SetActive(true);
        seleccion = true;
    }

    public void ContinuarFallo()
    {
        interfaces[11].SetActive(false);
        interfaces[8].SetActive(true);
    }

    public void Marla()
    {
        if (!actualizarMarla)
        {
            marla = true;
            botonJonno.interactable = false;
            interfaces[3].SetActive(true);
            interfaces[15].SetActive(true);
            Comunicador.valor = 0;
            photonView.RPC("RPC_Marla", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void RPC_Marla()
    {
        actualizarMarla = true;

        if(seleccion)
        {
            interfaces[13].SetActive(true);
        }
    }

    public void Jonno()
    {
        if (!actualizarJonno)
        {
            jonno = true;
            botonMarla.interactable = false;
            interfaces[3].SetActive(true);
            interfaces[15].SetActive(true);
            Comunicador.valor = 1;
            photonView.RPC("RPC_Jonno", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void RPC_Jonno()
    {
        actualizarJonno = true;

        if (seleccion)
        {
            interfaces[14].SetActive(true);
        }
    }

    public void Restablecer()
    {
        photonView.RPC("RPC_Quitar", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_Quitar()
    {
        jonno = false;
        marla = false;
        actualizarMarla = false;
        actualizarJonno = false;
        botonMarla.interactable = false;
        botonJonno.interactable = false;
        botonMarla.interactable = true;
        botonJonno.interactable = true;
        interfaces[3].SetActive(false);
        interfaces[15].SetActive(false);
        interfaces[13].SetActive(false);
        interfaces[14].SetActive(false);
    }

    public void Buscar()
    {
        interfaces[17].SetActive(true);

        if (marla)
        {
            Cinematicas(0);
        }
        else if (jonno)
        {
            Cinematicas(1);
        }
    }

    private void Cinematicas(int numero)
    {
        if(numero == 0)
        {
            cinematicas[numero].SetActive(true);
            Invoke("Conectar", 14.1f);
        }
        else if(numero == 1) 
        {
            cinematicas[numero].SetActive(true);
            Invoke("Conectar", 16.1f);
        }
    }

    public void Saltar()
    {
        Conectar();
    }

    /*private void ActivarStoryJ()
    {
        ControllerStoryBoard.Instance.jonno = true;
        ControllerStoryBoard.Instance.StoryBoard();
    }

    private void ActivarStoryM()
    {
        ControllerStoryBoard.Instance.marla = true;
        ControllerStoryBoard.Instance.StoryBoard();
    }*/
    public void Conectar()
    {
        hudCanvas.SetActive(true);
        camara.SetActive(false);

        if (marla)
        {
            PhotonNetwork.Instantiate("MarlaCompleta", new Vector3(-20f, -3f, 9.45f), Quaternion.identity, 0);
            marla = false;
        }
        else if(jonno)
        {
            PhotonNetwork.Instantiate("JonnoCompleta", new Vector3(-21f, 8.85f, 44.2f), Quaternion.identity, 0);
            jonno = false;
        }


        canvasCompleto.SetActive(false);
    }

    public void Creditos()
    {
        if(inicio)
        {
            interfaces[0].SetActive(false);
            interfaces[6].SetActive(true);
        }
        else if (sala)
        {
            interfaces[8].SetActive(false);
            interfaces[6].SetActive(true);
            interfaces[11].SetActive(false);
        }
        else if(seleccion)
        {
            jonno = false;
            marla = false;
            actualizarMarla = false;
            actualizarJonno = false;
            interfaces[2].SetActive(false);
            interfaces[6].SetActive(true);
            interfaces[9].SetActive(false);
            interfaces[10].SetActive(false);
            interfaces[13].SetActive(false);
            interfaces[14].SetActive(false);
        }
    }

    public void Atras()
    {
        if (inicio)
        {
            interfaces[0].SetActive(true);
            interfaces[6].SetActive(false);
        }
        else if (sala)
        {
            interfaces[8].SetActive(true);
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
        if (inicio)
        {
            interfaces[0].SetActive(false);
            interfaces[12].SetActive(true);
        }
        else if (sala)
        {
            interfaces[8].SetActive(false);
            interfaces[12].SetActive(true);
            interfaces[11].SetActive(false);
        }
        else if (seleccion)
        {
            jonno = false;
            marla = false;
            actualizarMarla = false;
            actualizarJonno = false;
            interfaces[2].SetActive(false);
            interfaces[12].SetActive(true);
            interfaces[9].SetActive(false);
            interfaces[10].SetActive(false);
            interfaces[13].SetActive(false);
            interfaces[14].SetActive(false);
        }
    }

    public void No()
    {
        if (inicio)
        {
            interfaces[0].SetActive(true);
            interfaces[12].SetActive(false);
        }
        else if (sala)
        {
            interfaces[8].SetActive(true);
            interfaces[12].SetActive(false);
        }
        else if (seleccion)
        {
            interfaces[2].SetActive(true);
            interfaces[12].SetActive(false);
        }
    }

    public void Si()
    {
        Application.Quit();
    }
}
