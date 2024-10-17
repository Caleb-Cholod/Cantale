using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using Random=UnityEngine.Random;

public class giveItem : MonoBehaviour
{
    private GameObject dataHolder;
    private GameObject itemHolder;

    // Start is called before the first frame update
    void Start()
    {
        //Generate a random number
        //randomNumber = Random.Range(0, 2);
    }

    // Update is called once per frame
    public void Give(int ItemID){
        dataHolder = GameObject.FindWithTag("DataHolder");
        itemHolder = GameObject.FindWithTag("ItemHolder");

        //Generate item to give to player off random

        switch(ItemID){
            case 0:
                //Item - Complete Breakfast
                PlayerPrefs.SetInt("PlayerHP", PlayerPrefs.GetInt("PlayerHP") + 3);
                PlayerPrefs.Save();

                break;

            case 1:
                //Item - Unwashed Mouthpiece
                dataHolder.GetComponent<Preferences>().baseAddPoison += 2;

                break;

            case 2:
                //Item - Glass Cymbal
                dataHolder.GetComponent<Preferences>().baseArmor += 1;
                break;

        }
    }
    public void GiveEquip(int ItemID){
        dataHolder.GetComponent<Preferences>().EquipmentID = ItemID;
    }

}
