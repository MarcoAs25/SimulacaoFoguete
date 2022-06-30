using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource ambient, engine1, engine2;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    public void PlayEngineStage1()
    {
        engine1.mute = false;
    }
    public void PlayEngineStage2()
    {
        engine2.mute = false;
    }
    public void StopEngineStage1()
    {
        engine1.mute = true;
    }
    public void StopEngineStage2()
    {
        engine2.mute = true;
    }
    private void Start()
    {
        SetAudiosource();
    }
    public void SetAudiosource()
    {
        ambient.Play();
        engine1.mute = true;
        engine2.mute = true;
        engine1.Play();
        engine2.Play();
    }
}
