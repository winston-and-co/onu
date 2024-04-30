using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapPanelText : MonoBehaviour
{
    // Start is called before the first frame update
    
    void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = 
            PlayerData.GetInstance().Player.hp + "/" + 
            PlayerData.GetInstance().Player.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
