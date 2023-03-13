using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{

    [SerializeField] private GameObject confettiManager;
    [SerializeField] private GroundedCharacterController player1;
    [SerializeField] private GroundedCharacterController player2;
    [SerializeField] private Button rematchButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Image UIBlur;
    
    private List<ParticleSystem> _listConfetti;

    void Awake()
    {
        _listConfetti = new List<ParticleSystem>();
        foreach (var confetti in GetComponentsInChildren<ParticleSystem>())
        {
            _listConfetti.Add(confetti);
        }
        
    }
    
    public void Rematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
    }
    
    public void Menu()
    {
        SceneManager.LoadScene("MainMenuScene",LoadSceneMode.Single);
    }

    void Start()
    {
        float player1Hp = player1.currentHP;
        float player2Hp = player2.currentHP;
    }
    
    public void victory()
    {
        
        
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        
        confettiManager.SetActive(true);
        rematchButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        UIBlur.gameObject.SetActive(true);
        
        
        foreach (var confetti in _listConfetti)
        {
            confetti.Play();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
