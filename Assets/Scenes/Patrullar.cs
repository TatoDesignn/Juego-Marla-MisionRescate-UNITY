using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private float tiempoQuieto; 
    [SerializeField] private GameObject icono;
    [SerializeField] private GameObject Detector;


    private bool Encountered = false;
    private Animator animator;
    private int siguientePaso;
    new Renderer renderer;
    private bool estaQuieto = true;
    private float tiempoInicioQuieto;

    private void Start()
    {
        siguientePaso = Random.Range(0, puntosMovimiento.Length);
        renderer = GetComponent<Renderer>();
        siguientePaso = 0;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player Marla = other.GetComponent<Player>();
        if (Marla != null)
        {
            Encountered = true;
            animator.SetBool("Aiming", true);
            Marla.medidorAnimator.SetTrigger("Cargar");
            Marla.medidorAnimator.SetFloat("Multiplier", 0.8f);
            transform.LookAt(Marla.gameObject.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player Marla = other.GetComponent<Player>();
        if (Marla != null)
        {
            Encountered = false;
            animator.SetBool("Aiming", false);
            Marla.medidorAnimator.SetTrigger("Reanudar");
            Marla.medidorAnimator.SetFloat("Multiplier", 1f);
        }
    }

    private void Update()
    {
        if (!Encountered)
        {
            if (estaQuieto)
            {
                animator.SetBool("Walking", false);
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
            else
                animator.SetBool("Walking", true);

            transform.LookAt(puntosMovimiento[siguientePaso].position);
            transform.position = Vector3.MoveTowards(transform.position, puntosMovimiento[siguientePaso].position, velocidadMovimiento * Time.deltaTime);
        
        
            if(Vector3.Distance(this.transform.position, puntosMovimiento[siguientePaso].position) <= distanciaMinima)
            {
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
}
