using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Beats : MonoBehaviour
{
    [SerializeField] GameObject Ring;
    [SerializeField] Image MyImgKeys;
    RectTransform CanvasPos;
    [SerializeField] private static float interval = 2.5f;
    public KeyCode MyKey;
    Osu instance;

    private void Awake()
    {
        instance = Osu.instance;
        CanvasPos = GetComponent<RectTransform>();
        CanvasPos.anchoredPosition = new Vector2(Random.Range(-300,300), Random.Range(-65,145));
        this.MyKey = instance.AssignKey(this.MyKey);
        this.MyImgKeys.sprite = instance.AssingImage(MyImgKeys.sprite);
    }

    private void Update()
    {
        this.Ring.transform.localScale = Vector3.Lerp(this.Ring.transform.localScale, new Vector3(0.8f,0.8f,0.8f), interval*Time.deltaTime);
        if (Input.GetKeyDown(this.MyKey)) 
        {
            if (this.Ring.transform.localScale.x < 1.3f && this.Ring.transform.localScale.x > 0.9f)
            {
                this.instance.HitOrMiss(1);
                Destroy(this.gameObject);

            }
            else 
            {
                this.instance.HitOrMiss(-1);
                Destroy(this.gameObject);
            }
        }

        if(this.Ring.transform.localScale.x < 0.95f)
        {
            this.instance.HitOrMiss(-1);
            Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (interval < 3.6f)
            interval += 0.3f;
    }
}

