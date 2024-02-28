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
            PhotonNetwork.Instantiate("marla", new Vector3(0f, 1.42f, 0f), Quaternion.identity, 0);
        }
        else if (Comunicador.valor == 1)
        {
            PhotonNetwork.Instantiate("jonno", new Vector3(1.812f, 1.42f, 0f), Quaternion.identity, 0);
        }
    }
}
