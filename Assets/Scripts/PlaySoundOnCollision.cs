using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public List<AudioClip> collisionSounds;  
    public List<string> collisionTags;       
    public List<AudioClip> triggerSounds;    
    public List<string> triggerTags;         

    private AudioSource audioSource;  

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
       
        int index = collisionTags.IndexOf(collision.gameObject.tag);
        if (index != -1)
        {
           
            audioSource.PlayOneShot(collisionSounds[index]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Buscar si el tag del objeto colisionado está en la lista de tags de trigger
        int index = triggerTags.IndexOf(other.gameObject.tag);
        if (index != -1)
        {
            // Reproducir el sonido correspondiente al tag
            audioSource.PlayOneShot(triggerSounds[index]);

            // Esperar la duración del sonido antes de destruir el objeto
            float soundDuration = triggerSounds[index].length;
            StartCoroutine(DestroyAfterDelay(other.gameObject, soundDuration));
        }
    }

    // Método para destruir el objeto después de un retraso
    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
