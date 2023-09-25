using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Script;
    void Start()
    {
        
    }
    public void OnClick()
    {

        Script.GetComponent<Sort>().StartSort();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
