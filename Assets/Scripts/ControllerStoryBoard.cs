using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControllerStoryBoard : MonoBehaviour
{
    public static ControllerStoryBoard Instance;
    Menu menu;

    [Header("Configuracion de los storyboard:")]
    Image imageUI;
    [SerializeField] private Sprite[] storyJ;
    [SerializeField] private Sprite[] storyM;
    [SerializeField] private GameObject transcion;

    private int posicion;
    public bool marla = false;
    public bool jonno = false;

    private void Awake()
    {
        if (ControllerStoryBoard.Instance == null)
        {
            ControllerStoryBoard.Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        imageUI = GetComponent<Image>();
        menu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Menu>();
    }

    public void StoryBoard()
    {
        transcion.SetActive(false);

        if (jonno)
        {
            imageUI.sprite = storyJ[posicion];   
        }
        else if(marla)
        {
            imageUI.sprite = storyM[posicion];
        }

        Invoke("ActivarTransicion", 3f);
    }

    private void ActivarTransicion()
    {
        posicion += 1;
        transcion.SetActive(true);
        if(posicion < 6)
        {
            Invoke("StoryBoard", 1f);
        }
        else if(posicion == 6)
        {
            menu.Conectar();
        }
    }
}
