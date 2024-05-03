using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLabel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string e = GameMaster.GetInstance().enemy.e_name;   
        GetComponent<TextMeshProUGUI>().text = e;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
