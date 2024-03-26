using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidatorDoor : MonoBehaviour
{
    [Space]
    [Header("Objetos validadores de la puerta: ")]
    public GameObject linea1;
    public GameObject linea2;
    public GameObject linea3;
    public GameObject linea4;

    [Space]
    [Header("Configuracion de la Puerta")]
    public GameObject interfaz;
    public GameObject puerta;

    void Update()
    {
        if(linea1.activeInHierarchy && linea2.activeInHierarchy && linea3.activeInHierarchy && linea4.activeInHierarchy)
        {
            puerta.transform.rotation = Quaternion.Euler(0, 0, 0);
            interfaz.SetActive(false);
        }
    }
}
