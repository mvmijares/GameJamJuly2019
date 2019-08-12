using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour
{

    public bool mute;
    [Range(0f, 1f)]
    public float volume;

    [SerializeField] private float originVolume; // volume before mute;

    [SerializeField] private GameObject backgroundAudio;
    private GameObject audioToggle;
    private static AudioManager _instance;

    public static AudioManager instance { get { return _instance; } }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if(audioToggle)
        {
            audioToggle.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Game":
                {
                    backgroundAudio = GameObject.FindGameObjectWithTag("Background Music");
                    if (backgroundAudio)
                        backgroundAudio.GetComponent<AudioSource>().volume = volume;

                    break;
                }
            case "Main Menu":
                {
                    audioToggle = GameObject.FindGameObjectWithTag("Mute");
                    if (audioToggle)
                    {
                        Toggle toggle = audioToggle.GetComponent<Toggle>();
                        toggle.onValueChanged.AddListener(delegate { OnToggleValueChanged(toggle); });
                    }
                    break;
                }
        }
    }
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
   
    private void OnToggleValueChanged(Toggle change)
    {
        if (!change.isOn)
        {
            volume = originVolume;
        }
        else
        {
            originVolume = volume;
            volume = 0;
        }
    }
}
