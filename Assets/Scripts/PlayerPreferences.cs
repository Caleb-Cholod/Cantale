using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("PlayerHP", 42);
        PlayerPrefs.SetInt("CurrentPlayerHP", 42);
        PlayerPrefs.SetInt("Gold", 300);

        PlayerPrefs.SetInt("CurrXP", 0);
        PlayerPrefs.SetInt("XpLevel", 1);


        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
