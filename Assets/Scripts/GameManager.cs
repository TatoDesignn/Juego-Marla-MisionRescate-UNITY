using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Personajes();
    }

    public void Personajes()
    {
        if (Comunicador.valor == 0)
        {
            PhotonNetwork.Instantiate("marla", new Vector3(-22.62f, 0.93f, 8.84f), Quaternion.identity, 0);
        }
        else if (Comunicador.valor == 1)
        {
            PhotonNetwork.Instantiate("jonno", new Vector3(-18.98f, 7.16f, 37.44f), Quaternion.identity, 0);
        }
    }
}
