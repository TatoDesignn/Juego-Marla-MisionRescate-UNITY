using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lights : MonoBehaviour
{
    [Header("Configuracion de las luces")]
    public GameObject luces;
    private bool prender = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.E) && prender)
            {
                luces.SetActive(!luces.activeInHierarchy);
                prender = false;
                Invoke("TurnOff", 0.2f);
            }
        }
    }

    private void TurnOff()
    {
        prender = true;
        
    }
}
