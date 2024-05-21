using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Osu : PuzzlesFather
{
    Hud hud;
    [SerializeField] GameObject beatPrefab;
    [SerializeField] public float intervalRestart;
    [SerializeField] private float interval;
    [SerializeField] private Material[] Bulbs;
    [SerializeField] GameObject _Door;
    public static Osu instance;
    [SerializeField] private Sprite[] ImgKeys;
    private static KeyCode[] keys = new KeyCode[4];
    private static int i = 0;

    private int attempt = 0;
    public int Hits;
    private int HitsNeeded = 5;

    [SerializeField] private AudioSource failSound;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        Hits = 0;
        keys[0] = KeyCode.W;
        keys[1] = KeyCode.A;
        keys[2] = KeyCode.S;  
        keys[3] = KeyCode.D;

        foreach (Material mat in Bulbs)
            mat.color = Color.red;

        this.enabled = false;

    }

    public override void Exit()
    {
        hud.activado = false;
        Hits = 0;
        interval = 5;
        this.PuzzleHolder.SetActive(false);
        this.enabled = false;
    }

    public override void Interact()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
        hud.activado = true;
        this.PuzzleHolder.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (interval <= 0)
        {
            GenerateBeat();
            interval = intervalRestart;
        }
        interval -= Time.deltaTime;

    }

    private void GenerateBeat()
    {
        Instantiate(beatPrefab, this.PuzzleHolder.transform);
        if (intervalRestart > 0.5f)
            intervalRestart -= 0.1f;
    }

    public KeyCode AssignKey(KeyCode beat)
    {
        beat = keys[i];
        return beat;
    }

    public Sprite AssingImage(Sprite beat)
    {
        beat = ImgKeys[i];
        i++;
        if (i == keys.Length) i = 0;
        return beat;
    }

    public void HitOrMiss(int value)
    {
        Hits += value;
        if (Hits == -5)
        {
            intervalRestart = 2.5f;
            Exit();
            base.ChangeCamera(false);
            Debug.Log("Fallaste, vuelve a intentarlo");
            // Reproducir sonido de fallo
            if (failSound != null)
                failSound.Play();
        }
        else if (Hits == HitsNeeded)
        {
            Bulbs[attempt].color = Color.green;
            attempt++;
            SetUpNext(attempt);
            if(attempt >= 3)
                _Door.SetActive(false);
            Exit();
            base.ChangeCamera(false);
        }
    }

    private void SetUpNext(int attempt)
    {
        switch (attempt)
        {
            case 0:
                HitsNeeded = 5; break;
            case 1:
                HitsNeeded = 8; break;
            case 2:
                HitsNeeded = 12; break;
            case 3:
                HitsNeeded = 16; break;
            case 4:
                HitsNeeded = 20; break;
            default:
                HitsNeeded = 5; break;

        }
    }
}