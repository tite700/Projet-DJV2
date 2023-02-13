using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFade : MonoBehaviour
{
    
    [SerializeField] private RectTransform healthBarBackground;
    [SerializeField] private RectTransform healthBarFill;

    private Image fillImage;
    private Image backgroundImage;
    private bool isDead = false;
    private float shrinkSpeed = 2f;
    private float _maxHp = 100f;
    private float _currentHp = 100f;

    private void Awake()
    {
        fillImage = healthBarFill.GetComponent<Image>();
        backgroundImage = healthBarBackground.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PerteHp());
    }

    IEnumerator PerteHp()
    {
        Debug.Log("fill amount " + fillImage.fillAmount);
        Debug.Log("back amount " + backgroundImage.fillAmount);

        while (!isDead)
        {
            if (_currentHp <= 0)
            {
                _currentHp = 0;
                isDead = true;
            }
        
            _currentHp -= 10f ;
            fillImage.fillAmount = _currentHp / _maxHp;
            Debug.Log("fill amount " + fillImage.fillAmount);
            
            while (backgroundImage.fillAmount > fillImage.fillAmount)
            {
                yield return new WaitForSeconds(0.1f);
                if (Time.deltaTime * shrinkSpeed < 0.07f)
                {
                    backgroundImage.fillAmount -= Time.deltaTime * shrinkSpeed;
                }
                else
                {
                    backgroundImage.fillAmount -= 0.07f;
                }
                Debug.Log("back amount " + backgroundImage.fillAmount);
            }

        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
