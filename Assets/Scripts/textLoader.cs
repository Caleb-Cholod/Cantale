using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textLoader : MonoBehaviour
{
    public TMP_Text GoldText;
    public TMP_Text levelText;
    public TMP_Text XPText;
    public TMP_Text HPText;

    private GameObject dataHolder;
    // Start is called before the first frame update
    void Start()
    {
        dataHolder = GameObject.FindWithTag("DataHolder");

        HPText.text = PlayerPrefs.GetInt("CurrentPlayerHP") + "/" + PlayerPrefs.GetInt("PlayerHP");
        GoldText.text = "Gold: " + PlayerPrefs.GetInt("Gold");
        levelText.text = "Lvl " + PlayerPrefs.GetInt("XpLevel");
        //XPText.text = PlayerPrefs.GetInt("CurrXP") + "/" + dataHolder.GetComponent<Preferences>().xpLevels[PlayerPrefs.GetInt("XpLevel") - 1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
