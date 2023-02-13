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

public class TitleScreen : MonoBehaviour
{
   [SerializeField] private GameObject audioManager;
   [SerializeField] private string menuScene;
   
   private TextMeshProUGUI[] _tabText;
   private bool _spacePressed = false;
   
   

   public void Start()
   {
      _tabText = GetComponentsInChildren<TextMeshProUGUI>();

      StartCoroutine(Clignotement());
   }

   
   public void StartGame()
   {
      DontDestroyOnLoad(audioManager);
      SceneManager.LoadScene(menuScene,LoadSceneMode.Single);
      
   }
   
   public void QuitGame()
   {
      Debug.Log("Quit");
      Application.Quit();
   }


   private IEnumerator Clignotement()
   {
      while (!_spacePressed)
      {
         foreach (var text in _tabText)
         {
            text.GameObject().SetActive(!text.GameObject().activeSelf);
         }
         yield return new WaitForSeconds(0.75f);
      }
   }
   public void Update()
   {

      if (Input.GetKeyDown("space"))
      {
         _spacePressed = true;
      
         StartGame();
      }
   }
}
