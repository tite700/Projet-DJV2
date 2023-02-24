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
    
    private List<string> listStage;
    private DropDown dropscript;
    
    [SerializeField] private GameObject dropDownMenu;
    
    private void Awake()
    {
        dropscript = dropDownMenu.GetComponent<DropDown>();        
        listStage = dropscript.listStage;
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
        SceneManager.LoadScene(settingsScene, LoadSceneMode.Additive);
    }

    public void Fight()
    {
        if (!dropscript.StageChoisi.IsUnityNull())
        {
            SceneManager.LoadScene(dropscript.StageChoisi, LoadSceneMode.Single);
        }
        
    }
}
