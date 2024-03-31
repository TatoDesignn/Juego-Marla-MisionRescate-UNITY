using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraDetectar : MonoBehaviour
{
    public GameObject aviso;

    private void Start()
    {
        aviso.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CamaraSensor"))
        {
            aviso.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CamaraSensor"))
        {
            aviso.SetActive(false);
        }
    }
}
