using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random=UnityEngine.Random;

public class ShopLoader : MonoBehaviour
{
    private GameObject itemDatabase;

    public GameObject Sprite1;
    public GameObject Sprite2;
    public GameObject Sprite3;
    public GameObject SpriteEquip;

    public Sprite none;

    public GameObject ShopItemButton1;
    public GameObject ShopItemButton2;
    public GameObject ShopItemButton3;
    public GameObject ShopItemButtonEquip;

    public TMP_Text item1Cost;
    public TMP_Text item2Cost;
    public TMP_Text item3Cost;
    public TMP_Text itemEquipCost;

    private int item1ID;
    private int item2ID;
    private int item3ID;
    private int itemEquipID;

    public TMP_Text goldtext;

    // Start is called before the first frame update
    void Start()
    {
        itemDatabase = GameObject.FindWithTag("ItemHolder");

        item1ID = Random.Range(0, itemDatabase.GetComponent<ItemDatabase>().sprites.Length);
        item2ID = Random.Range(0, itemDatabase.GetComponent<ItemDatabase>().sprites.Length);
        item3ID = Random.Range(0, itemDatabase.GetComponent<ItemDatabase>().sprites.Length);

        itemEquipID = Random.Range(0, 1);

        //LOAD SPRITES FROM DATABASE
        Sprite1.GetComponent<SpriteRenderer>().sprite = itemDatabase.GetComponent<ItemDatabase>().sprites[item1ID];
        Sprite2.GetComponent<SpriteRenderer>().sprite = itemDatabase.GetComponent<ItemDatabase>().sprites[item2ID];
        Sprite3.GetComponent<SpriteRenderer>().sprite = itemDatabase.GetComponent<ItemDatabase>().sprites[item3ID];

        SpriteEquip.GetComponent<SpriteRenderer>().sprite = itemDatabase.GetComponent<ItemDatabase>().Equipmentsprites[itemEquipID];

        //SpriteEquip.GetComponent<SpriteRenderer>().Sprite = itemDatabase.GetComponent<ItemDatabase>().sprites[item1ID];

        //LOAD COST TEXT
        item1Cost.text = itemDatabase.GetComponent<ItemDatabase>().costs[item1ID].ToString();
        item2Cost.text = itemDatabase.GetComponent<ItemDatabase>().costs[item2ID].ToString();
        item3Cost.text = itemDatabase.GetComponent<ItemDatabase>().costs[item3ID].ToString();

        itemEquipCost.text = itemDatabase.GetComponent<ItemDatabase>().costs[itemEquipID].ToString();

        //Set up shop buttons
        ShopItemButton1.GetComponent<ShopTile>().ItemID = item1ID;
        ShopItemButton2.GetComponent<ShopTile>().ItemID = item2ID;
        ShopItemButton3.GetComponent<ShopTile>().ItemID = item3ID;
        ShopItemButtonEquip.GetComponent<ShopTile>().ItemID = itemEquipID;


    }

    // Update is called once per frame
        public void EmptyShopItem(int TileID, int Itemid)
        {
            
            goldtext.text = PlayerPrefs.GetInt("Gold").ToString();

            switch(TileID){
                case 1:
                    Sprite1.GetComponent<SpriteRenderer>().sprite = none;
                    PlayerPrefs.Save();
                    break;
                case 2:
                    Sprite2.GetComponent<SpriteRenderer>().sprite = none;
                    break;
                case 3:
                    Sprite3.GetComponent<SpriteRenderer>().sprite = none;
                    //PlayerPrefs.SetInt("Gold", PlayersPrefs.GetInt("Gold") - itemDatabase.GetComponent<ItemDatabase>().costs[item3ID].ToString());
                    break;
                case 4:
                    SpriteEquip.GetComponent<SpriteRenderer>().sprite = none;
                    //PlayerPrefs.SetInt("Gold", PlayersPrefs.GetInt("Gold") - itemDatabase.GetComponent<ItemDatabase>().costs[itemEquipID].ToString());
                    break;
        }
    }
}
