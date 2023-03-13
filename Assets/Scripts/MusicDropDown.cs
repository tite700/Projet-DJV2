using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicDropDown : MonoBehaviour
{
    
    public List<string> listMusic ;
    private TMP_Dropdown _dropdown;

    public string musiqueChoisie;
    
    // Start is called before the first frame update
    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        
        for (int i = 0; i < _dropdown.options.Count; i++)
        {
            listMusic.Add(_dropdown.options[i].text);
        }
    }
    
    public void OnValueChanged(int value)
    {
        musiqueChoisie = _dropdown.options[value].text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}