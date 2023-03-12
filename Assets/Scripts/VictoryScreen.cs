using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{

    [SerializeField] private GameObject confettiManager;
    [SerializeField] private GroundedCharacterController player1;
    [SerializeField] private GroundedCharacterController player2;
    private List<ParticleSystem> _listConfetti;

    void Awake()
    {
        _listConfetti = new List<ParticleSystem>();
        _listConfetti.Add(GetComponent<ParticleSystem>());
        foreach (var confetti in GetComponentsInChildren<ParticleSystem>())
        {
            _listConfetti.Add(confetti);
        }
        
        confettiManager.SetActive(false);
    }

    void Start()
    {
        float player1Hp = player1.currentHP;
        float player2Hp = player2.currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        // if one of the player is dead start the confetti
        if (player1.currentHP <= 0 || player2.currentHP <= 0)
        {
            player1.LockMovement(true);
            player2.LockMovement(true);
            confettiManager.SetActive(true);
            
            
            foreach (var confetti in _listConfetti)
            {
                confetti.Play();
            }
        }
    }
}
