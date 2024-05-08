using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_JonnoEntrance : MonoBehaviour
{
    private Camera Camera;
    private GameObject Jonno;
    [SerializeField] private GameObject PipesStartPoint;
    public void Interact(Camera camera, GameObject jonno)
    {
        Camera = camera;
        Jonno = jonno;
        StartCoroutine("CloseView", Camera);
    }

    IEnumerator CloseView(Camera camera)
    {
        float OrigFOV = 60f;
        camera.DOFieldOfView(0, 1f);
        Animator animator = Jonno.GetComponent<Animator>();
        animator.SetBool("Pipes", true);
        animator.SetBool("isCrouching", true);
        yield return new WaitForSeconds(1.5f);
        Jonno.transform.rotation = Quaternion.identity;
        Jonno.transform.position = PipesStartPoint.transform.position;
        camera.DOFieldOfView(OrigFOV, 1f);

    }
}
