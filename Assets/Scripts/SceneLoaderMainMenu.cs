using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class SceneLoaderMainMenu : MonoBehaviour
{
    public string settingsScene;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TestArena()
    {
        SceneManager.LoadScene(settingsScene, LoadSceneMode.Additive);
    }
}
