using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class SceneLoaderMainMenu : MonoBehaviour
{
    public string settingsScene;
    public string menuScene;
    
    private List<string> _listStage;
    private DropDownStage dropscript;
    private MusicDropDown musicScript;
    private GameObject _audioManager;
    
    [SerializeField] private GameObject dropDownMenu;
    [SerializeField] private GameObject musicDropDown;
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;
    
    private void Awake()
    {
        dropscript = dropDownMenu.GetComponent<DropDownStage>();
        musicScript = musicDropDown.GetComponent<MusicDropDown>();
        
        //find the Audio Manager in the OnDestroyOnLoad
        _audioManager = GameObject.Find("AudioManager");
        
        //_listStage = dropscript.listStage;
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SettingsScene()
    {
        SceneManager.LoadScene(settingsScene, LoadSceneMode.Single);
    }

    public void TitleScreenScene()
    {
        SceneManager.LoadScene("WelcomeScene", LoadSceneMode.Single);
    }
    
    public void Fight()
    {
        if (!dropscript.StageChoisi.IsUnityNull())
        {
            SceneManager.LoadScene(dropscript.StageChoisi, LoadSceneMode.Single);
        }
        
        if (!musicScript.musiqueChoisie.IsUnityNull())
        {
            if (musicScript.musiqueChoisie == "Drowning")
            {
                _audioManager.GetComponent<AudioSource>().clip = music1;
                music1.LoadAudioData();
                _audioManager.GetComponent<AudioSource>().Play();
                
            }
            else if (musicScript.musiqueChoisie == "Burnout")
            {
                _audioManager.GetComponent<AudioSource>().clip = music2; 
                music2.LoadAudioData();
                _audioManager.GetComponent<AudioSource>().Play();
            }
        }
        
    }
}
