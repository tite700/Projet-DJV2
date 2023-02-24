using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    
    public List<string> listStage ;
    private TMP_Dropdown _dropdown;

    public string StageChoisi;
    
    // Start is called before the first frame update
    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        
        for (int i = 0; i < _dropdown.options.Count; i++)
        {
            listStage.Add(_dropdown.options[i].text);
        }
    }
    
    public void OnValueChanged(int value)
    {
        StageChoisi = _dropdown.options[value].text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
