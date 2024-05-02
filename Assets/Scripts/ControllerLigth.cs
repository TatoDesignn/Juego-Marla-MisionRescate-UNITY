using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLigth : MonoBehaviour
{
    [Space]
    [Header("Controlador de luz")]
    [SerializeField] private GameObject luces;
    [SerializeField] private RectTransform puntoT;
    [SerializeField] private RectTransform movimiento;
    [SerializeField] private GameObject primera;
    [SerializeField] private GameObject segunda;
    [SerializeField] private GameObject rectanguloLargo;
    [SerializeField] private GameObject palanca;
    [SerializeField] private float velocidad;
    [SerializeField] private AudioSource audioSource; // Referencia al componente AudioSource
    [SerializeField] private AudioClip apagarSound; // Sonido que se reproducirá al apagar el ControllerLigth
    [SerializeField] private AudioClip aumentoVelocidadSound; // Sonido que se reproducirá al aumentar la velocidad

    [Space]
    [Header("Variables locales:")]
    private float factorMovimiento;
    private int contador;
    private Vector2 punto1;
    private Vector2 punto2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource del mismo GameObject
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Agregar AudioSource si no existe
        }
        audioSource.clip = apagarSound; // Asignar el sonido al AudioSource

        Ubicacion();
        punto1 = new Vector2(48.4f, 0);
        punto2 = new Vector2(-48.4f, 0);
        contador = 0;
    }

    void Update()
    {
        factorMovimiento = Mathf.PingPong(Time.time * velocidad, 1);
        movimiento.anchoredPosition = Vector2.Lerp(punto1, punto2, factorMovimiento);
    }

    private void Ubicacion()
    {
        float randomX = Random.Range(-40, 40);
        puntoT.anchoredPosition = new Vector2(randomX, puntoT.anchoredPosition.y);
    }

    private IEnumerator ApagarConSonido()
    {
        // Desactivar o hacer invisible cada componente individualmente
        luces.SetActive(false);
        puntoT.gameObject.SetActive(false);
        movimiento.gameObject.SetActive(false);
        primera.SetActive(false);
        segunda.SetActive(false);
        rectanguloLargo.SetActive(false);
        palanca.transform.eulerAngles = new Vector3(-160f, 0, -90);

        // Reproducir el sonido al apagar el ControllerLigth
        if (audioSource != null && apagarSound != null) // Verificar si hay AudioSource y AudioClip asignados
        {
            audioSource.PlayOneShot(apagarSound); // Reproducir el sonido al apagar el ControllerLigth
            yield return new WaitForSeconds(apagarSound.length); // Esperar la duración del sonido
        }

        // Desactivar el objeto después de que el sonido haya terminado
        gameObject.SetActive(false);
    }

    public void Correctas()
    {
        contador += 1;

        if(contador == 1)
        {
            primera.SetActive(true);
            velocidad += 0.5f;
            if (aumentoVelocidadSound != null)
            {
                audioSource.PlayOneShot(aumentoVelocidadSound); // Reproducir el sonido de aumento de velocidad
            }
            Ubicacion();
        }
        if (contador == 2)
        {
            segunda.SetActive(true);
            velocidad += 0.5f;
            if (aumentoVelocidadSound != null)
            {
                audioSource.PlayOneShot(aumentoVelocidadSound); // Reproducir el sonido de aumento de velocidad
            }
            Ubicacion();
        }
        if (contador == 3)
        {
            StartCoroutine(ApagarConSonido()); // Iniciar la corrutina para apagar con sonido
        }
    }
}
