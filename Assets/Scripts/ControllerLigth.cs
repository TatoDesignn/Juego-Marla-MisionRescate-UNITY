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
    [SerializeField] private float velocidad;

    [Space]
    [Header("Variables locales:")]
    private float factorMovimiento;
    private Vector2 punto1;
    private Vector2 punto2;

    void Start()
    {
        Ubicacion();
        punto1 = new Vector2(48.4f, 0);
        punto2 = new Vector2(-48.4f, 0);
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

    public void Encender()
    {
        luces.SetActive(true);
        gameObject.SetActive(false);
    }
}
