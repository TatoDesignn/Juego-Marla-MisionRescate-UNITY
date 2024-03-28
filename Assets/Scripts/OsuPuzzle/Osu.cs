using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Osu : PuzzlesFather
{
    [SerializeField] GameObject beatPrefab;
    [SerializeField] public float intervalRestart;
    [SerializeField] private float interval;
    public static Osu instance;
    private static KeyCode[] keys = new KeyCode[4];
    private static int i = 0;

    private int attempt = 0;
    public int Hits;
    private int HitsNeeded = 5;
    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        Hits = 0;
        keys[0] = KeyCode.W;
        keys[1] = KeyCode.S;
        keys[2] = KeyCode.A;
        keys[3] = KeyCode.D;
    }
    public override void Exit()
    {
        Hits = 0;
        interval = 5;
        this.PuzzleHolder.SetActive(false);
        this.enabled = false;
    }

    public override void Interact()
    {
        this.PuzzleHolder.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(interval <= 0)
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
        i++;
        if (i == 4) i = 0;
        return beat;
    }

    public void HitOrMiss(int value)
    {
        Hits += value;
        if(Hits == -5)
        {
            intervalRestart = 2.5f;
            Exit();
            base.ChangeCamera(false);
            Debug.Log("Fallaste, vuelve a intentarlo");
        }
        else if(Hits == HitsNeeded)
        {
            attempt++;
            SetUpNext(attempt);
            Exit();
            base.ChangeCamera(false);
            Debug.Log("Reclusa liberada");
        }
    }

    private void SetUpNext(int attemp)
    {
        switch (attemp)
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
