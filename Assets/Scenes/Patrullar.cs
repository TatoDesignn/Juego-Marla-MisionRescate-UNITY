using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private float tiempoQuieto = 5f; 
    [SerializeField] private GameObject icono;
    private int siguientePaso = 0;
    private Renderer renderer;
    private bool estaQuieto = false;
    private float tiempoInicioQuieto;

    private void Start()
    {
        siguientePaso = Random.Range(0, puntosMovimiento.Length);
        renderer = GetComponent<Renderer>();
        Girar();
    }

    private void Update()
    {
        if (estaQuieto)
        {
            if (Time.time - tiempoInicioQuieto >= tiempoQuieto)
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
        
        if(Vector3.Distance(transform.position, puntosMovimiento[siguientePaso].position) < distanciaMinima)
        {
            siguientePaso++;
            if(siguientePaso >= puntosMovimiento.Length)
            {
                siguientePaso = 0;
            }
            Girar();
        }

        // Realizar el raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            
            if (hit.collider.CompareTag("ObjetoEspecifico"))
            {
                icono.SetActive(true);
                estaQuieto = true;
                tiempoInicioQuieto = Time.time;
            }
            else
            {
               
                icono.SetActive(false);
            }

            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);
        }
        else
        {
     
            Debug.DrawRay(transform.position, transform.forward * 100, Color.green);
        }
    }

   private void Girar()
{
    if(transform.position.x < puntosMovimiento[siguientePaso].position.x)
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    else
    {
        transform.rotation = Quaternion.identity;
    }
}

}
