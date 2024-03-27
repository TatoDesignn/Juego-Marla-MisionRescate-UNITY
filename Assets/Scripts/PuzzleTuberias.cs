using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTuberias : MonoBehaviour
{
    public GameObject[] pipes;
    public float maxRotation = 360f;
    public float rotationAmount = 90f;

    private int currentPipeIndex = 0;

    public Material selectedMaterial;
    private Material defaultMaterial;

    void Start()
    {
        defaultMaterial = pipes[currentPipeIndex].GetComponent<Renderer>().material;
        UpdatePipeMaterial();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RotateCurrentPipe();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentPipeIndex++;
            if (currentPipeIndex >= pipes.Length)
            {
                currentPipeIndex = 0;
            }
            UpdatePipeMaterial();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentPipeIndex--;
            if (currentPipeIndex < 0)
            {
                currentPipeIndex = pipes.Length - 1; 
            }
            UpdatePipeMaterial();
        }
    }

    void RotateCurrentPipe()
    {
        GameObject currentPipe = pipes[currentPipeIndex];
        float currentRotation = currentPipe.transform.localEulerAngles.z;

        float targetRotation = currentRotation + rotationAmount;
        targetRotation = Mathf.Clamp(targetRotation, 0f, maxRotation);

        currentPipe.transform.localEulerAngles = new Vector3(0f, 0f, targetRotation);
    }

    void UpdatePipeMaterial()
    {
        foreach (GameObject pipe in pipes)
        {
            pipe.GetComponent<Renderer>().material = defaultMaterial;
        }
        pipes[currentPipeIndex].GetComponent<Renderer>().material = selectedMaterial;
    }
}
