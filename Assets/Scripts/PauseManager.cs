using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Image UIBlur;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void ResumeButton()
    {
        UIBlur.gameObject.SetActive(false);
        pauseScreen.SetActive(false);
    }
    
    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UIBlur.gameObject.SetActive(!UIBlur.gameObject.activeSelf);
            pauseScreen.SetActive(!pauseScreen.activeSelf);
        }
    }
}
