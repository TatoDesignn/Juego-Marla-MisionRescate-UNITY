using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Image colidingpipes = collision.GetComponent<Image>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    
}
