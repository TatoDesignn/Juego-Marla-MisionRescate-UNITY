using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Timer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject perderCinematicas;
    private float elapsedTime;
    private bool isTimerRunning = false;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            elapsedTime = 600f; 

        }
    }

    void Update()
    {
        if (!isTimerRunning)
        {
            if (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;
            }
            else if (elapsedTime < 0)
            {
                elapsedTime = 0;
                timerText.color = Color.red;
                isTimerRunning = false;
                perderCinematicas.SetActive(true);
                Invoke("Perder", 12f);
            }

            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void Perder()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(elapsedTime);
        }
        else
        {
            elapsedTime = (float)stream.ReceiveNext();
        }
    }
}
