using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyItem : MonoBehaviour
{
    public TMP_Text GoldText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SubtractGold(int cost){
        if(PlayerPrefs.GetInt("Gold") - cost >= 0){
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - cost);
            Debug.Log("New Gold - " + PlayerPrefs.GetInt("Gold"));
            PlayerPrefs.Save();
            GoldText.text = "Gold: " + PlayerPrefs.GetInt("Gold");
        }
    }
}
