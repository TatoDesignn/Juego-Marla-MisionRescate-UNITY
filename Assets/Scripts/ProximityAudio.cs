using System.Collections;
using UnityEngine;

public class ProximityAudio : MonoBehaviour
{
    public AudioClip audioClip; // El clip de audio que deseas reproducir
    public float triggerDistance = 5f; // Distancia a la que el jugador debe estar para escuchar el audio
    public GameObject player; // El objeto jugador

    private AudioSource audioSource;
    private bool audioStarted = false;

    void Start()
    {
        // Añade un AudioSource al objeto si no tiene uno
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.spatialBlend = 1.0f; // 1.0f significa que es un sonido 3D

        // Inicia la corutina que esperará 30 segundos antes de permitir que el audio se reproduzca
        StartCoroutine(StartAudioAfterDelay(60f));
    }

    void Update()
    {
        if (audioStarted && player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= triggerDistance && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else if (distance > triggerDistance && audioSource.isPlaying)
            {
                audioSource.Stop(); // Detiene completamente el audio si el jugador está fuera de la distancia
            }
        }
    }

    IEnumerator StartAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioStarted = true;
    }
}
