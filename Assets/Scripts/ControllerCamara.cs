using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamara : MonoBehaviour
{
    [Space]
    [Header("Configuración de las cámaras:")]
    [SerializeField] private GameObject[] camaras; 
    [SerializeField] private float velocidad;

    [Space]
    [Header("Variables locales:")]
    private int camaraActiva = -1; 

    void Update()
    {
        if (camaraActiva != -1) 
        {
            if (Input.GetKey(KeyCode.A))
            {
                camaras[camaraActiva].transform.Rotate(Vector3.up, -velocidad * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                camaras[camaraActiva].transform.Rotate(Vector3.up, velocidad * Time.deltaTime);
            }
        }
    }

    public void ActivarCamara(int indice)
    {
        if (indice >= 0 && indice < camaras.Length)
        {
            camaraActiva = indice;
        }
    }

    public void CamaraUno() { ActivarCamara(0); }
    public void CamaraDos() { ActivarCamara(1); }
    public void CamaraTres() { ActivarCamara(2); }
    public void CamaraCuatro() { ActivarCamara(3); }

}
