using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager instance { get { return _instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
