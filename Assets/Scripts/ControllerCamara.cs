using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamara : MonoBehaviour
{
    [Space]
    [Header("Configuracion de las camaras:")]
    [SerializeField] private GameObject vigilancia1;
    [SerializeField] private GameObject vigilancia2;
    [SerializeField] private GameObject vigilancia3;
    [SerializeField] private GameObject vigilancia4;
    [SerializeField] private float velocidad;

    [Space]
    [Header("Variables locales:")]
    private bool camara1;
    private bool camara2;
    private bool camara3;
    private bool camara4;


    void Start()
    {
        camara1 = false;
        camara2 = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if(camara1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                vigilancia1.transform.Rotate(Vector3.up, -velocidad * Time.deltaTime);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                vigilancia1.transform.Rotate(Vector3.up, velocidad * Time.deltaTime);
            }
        }
        else if(camara2)
        {
            if (Input.GetKey(KeyCode.A))
            {
                vigilancia2.transform.Rotate(Vector3.up, -velocidad * Time.deltaTime);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                vigilancia2.transform.Rotate(Vector3.up, velocidad * Time.deltaTime);
            }
        }
        else if (camara3)
        {
            if (Input.GetKey(KeyCode.A))
            {
                vigilancia3.transform.Rotate(Vector3.up, -velocidad * Time.deltaTime);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                vigilancia3.transform.Rotate(Vector3.up, velocidad * Time.deltaTime);
            }
        }
        else if (camara4)
        {
            if (Input.GetKey(KeyCode.A))
            {
                vigilancia4.transform.Rotate(Vector3.up, -velocidad * Time.deltaTime);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                vigilancia4.transform.Rotate(Vector3.up, velocidad * Time.deltaTime);
            }
        }
    }

    public void CamaraUno()
    {
        camara1 = true;
        camara2 = false; 
        camara3 = false;
        camara4 = false;
    }

    public void CamaraDos()
    {
        camara1 = false;
        camara2 = true;
        camara3 = false;
        camara4 = false;
    }

    public void CamaraTres()
    {
        camara1 = false;
        camara2 = false;
        camara3 = true;
        camara4 = false;
    }

    public void CamaraCuatro()
    {
        camara1 = false;
        camara2 = false;
        camara3 = false;
        camara4 = true;
    }
}
