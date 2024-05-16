using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_JonnoExit : MonoBehaviour
{
    private Camera Camera;
    public void Interact(Camera camera, GameObject jonno)
    {
        Camera = camera;
        StartCoroutine("CloseView", Camera);
    }

    IEnumerator CloseView(Camera camera)
    {
        camera.DOFieldOfView(0, 1f);
        
        /*
         * AQUÍ VA EL CÓDIGO PARA 
         * CUANDO EMPIEZA LA CINEMÁTICA
         * FINAL DE JONNO
         * */

        yield return null;
    }
}
