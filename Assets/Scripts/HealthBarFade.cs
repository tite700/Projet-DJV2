using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFade : MonoBehaviour
{
    [SerializeField] private RectTransform healthBarBackground;
    [SerializeField] private RectTransform healthBarFill;

    [SerializeField] private GameObject character;

    private Image fillImage;
    private Image backgroundImage;
    private float shrinkSpeed = 2f;

    private void Awake()
    {
        fillImage = healthBarFill.GetComponent<Image>();
        backgroundImage = healthBarBackground.GetComponent<Image>();
    }


    public IEnumerator PerteHp(float currentHp, float maxHp)
    {
        yield return new WaitForSeconds(0.1f);


        fillImage.fillAmount = currentHp / maxHp;

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

        }
    }
    
    public void Update()
    {
        if (character.GetComponent<GroundedCharacterController>().currentHP <= 0)
        {
            fillImage.fillAmount = 0;
            backgroundImage.fillAmount = 0;
        }
    }
}