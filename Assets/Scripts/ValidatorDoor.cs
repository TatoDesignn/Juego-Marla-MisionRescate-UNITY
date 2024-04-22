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

    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip activationSound; // Sonido a reproducir cuando se activan todos los validadores

    private bool alreadyActivated = false; // Variable para evitar la reproducción repetida del sonido

    void Update()
    {
        if (linea1.activeInHierarchy && linea2.activeInHierarchy && linea3.activeInHierarchy && linea4.activeInHierarchy && !alreadyActivated)
        {
            StartCoroutine(PlaySoundAndActivateDoor());
        }
    }

    IEnumerator PlaySoundAndActivateDoor()
    {
        // Reproducir el sonido de activación
        audioSource.PlayOneShot(activationSound);
        alreadyActivated = true; // Evitar la reproducción repetida del sonido

        // Esperar la duración del sonido
        yield return new WaitForSeconds(activationSound.length);

        // Desactivar la puerta y la interfaz
        puerta.transform.Translate(puerta.transform.position.x, puerta.transform.position.y, -10.52f);
        interfaz.SetActive(false);
    }
}
