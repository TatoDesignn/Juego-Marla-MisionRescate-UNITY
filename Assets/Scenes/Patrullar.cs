using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private float tiempoQuieto; 
    [SerializeField] private GameObject icono;
    private int siguientePaso;
    new Renderer renderer;
    private bool estaQuieto = true;
    private float tiempoInicioQuieto;

    private void Start()
    {
        siguientePaso = Random.Range(0, puntosMovimiento.Length);
        renderer = GetComponent<Renderer>();
        siguientePaso = 0;
    }

    private void Update()
    {
        if (estaQuieto)
        {
            tiempoInicioQuieto -= Time.deltaTime;
            if (tiempoInicioQuieto <=0)
            {
                estaQuieto = false;
                tiempoInicioQuieto = 0f;
                icono.SetActive(false);
            }
            else
            {
             
                return;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, puntosMovimiento[siguientePaso].position, velocidadMovimiento * Time.deltaTime);
        
        
        if(Vector3.Distance(this.transform.position, puntosMovimiento[siguientePaso].position) <= distanciaMinima)
        {
            transform.DOLookAt(puntosMovimiento[siguientePaso].position, tiempoQuieto/1.5f);
            //transform.LookAt(puntosMovimiento[siguientePaso].position);
            estaQuieto = true;
            siguientePaso++;
            if(siguientePaso >= puntosMovimiento.Length)
            {
                siguientePaso = 0;
            }
            tiempoInicioQuieto = tiempoQuieto;
        }
    }
}
