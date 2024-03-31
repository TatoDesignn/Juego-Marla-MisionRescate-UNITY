using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerDoor : MonoBehaviour
{
    public GameObject indicadorON;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PointDoor"))
        {
            indicadorON.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PointDoor"))
        {
            indicadorON.SetActive(false);
        }
    }
}
