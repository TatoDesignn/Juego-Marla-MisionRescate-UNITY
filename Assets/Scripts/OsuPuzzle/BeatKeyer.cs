using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatKeyer : MonoBehaviour
{
    protected static BeatKeyer instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
}
