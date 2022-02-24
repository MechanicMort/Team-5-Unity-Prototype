using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    private static BackgroundMusic music = null;
    public static BackgroundMusic Music
    {
        get { return music; }
    }

    void Awake()
    {
        if (music != null && music != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else             
        {
            music = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
