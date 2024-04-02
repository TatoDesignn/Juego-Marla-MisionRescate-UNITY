using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerDoor : MonoBehaviour
{
    [Header("Feedbacks de puzzle")]
    public GameObject indicadorON;
    public GameObject indicadorOFF;
    public GameObject candadoOFF;
    public GameObject candadoON;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PointDoor"))
        {
            indicadorON.SetActive(true);
            indicadorOFF.SetActive(false);
            candadoON.SetActive(true);
            candadoOFF.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PointDoor"))
        {
            indicadorON.SetActive(false);
            indicadorOFF.SetActive(true);
            candadoON.SetActive(false);
            candadoOFF.SetActive(true);
        }
    }
}
