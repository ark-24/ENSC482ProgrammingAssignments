using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Dropdown SelectDropdown;

    public GameObject SFU;
    public void OnSelect()
    {
        SFU.GetComponent<SFU>().ColourChange(SelectDropdown.value);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
