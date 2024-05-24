using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public List<AudioClip> collisionSounds;  // Lista de sonidos para colisiones
    public List<string> collisionTags;       // Lista de tags para colisiones
    public List<AudioClip> triggerSounds;    // Lista de sonidos para triggers
    public List<string> triggerTags;         // Lista de tags para triggers

    private AudioSource audioSource;  // Referencia al componente AudioSource del jugador

    void Start()
    {
        // Obtener el componente AudioSource adjunto al jugador
        audioSource = GetComponent<AudioSource>();
    }

    // Método llamado cuando el jugador entra en colisión con otro collider
    private void OnCollisionEnter(Collision collision)
    {
        // Buscar si el tag del objeto colisionado está en la lista de tags de colisión
        int index = collisionTags.IndexOf(collision.gameObject.tag);
        if (index != -1)
        {
            // Reproducir el sonido correspondiente al tag
            audioSource.PlayOneShot(collisionSounds[index]);
        }
    }

    // Método llamado cuando el jugador entra en el trigger de otro collider
    private void OnTriggerEnter(Collider other)
    {
        // Buscar si el tag del objeto colisionado está en la lista de tags de trigger
        int index = triggerTags.IndexOf(other.gameObject.tag);
        if (index != -1)
        {
            // Reproducir el sonido correspondiente al tag
            audioSource.PlayOneShot(triggerSounds[index]);
        }
    }
}
