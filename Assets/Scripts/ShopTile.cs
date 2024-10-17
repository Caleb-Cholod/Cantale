using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTile : MonoBehaviour
{
    
    public AudioSource MouseOver;
    public AudioSource onclick;
    private bool hasPlayed;
    private SpriteRenderer spriteRenderer;

    public Sprite basic;
    public Sprite hit;

    private Collider2D Collider;

    private bool hasGiven;

    public int ItemID;

    private GameObject itemDatabase;

    public GameObject ShopLoader;

    public int tileID;

    public bool isEquipmentTile;


    // Start is called before the first frame update
    void Start()
    {
        itemDatabase = GameObject.FindWithTag("ItemHolder");
        spriteRenderer = GetComponent<SpriteRenderer>();
        hasPlayed = false;
        hasGiven = false;
    }

    // Update is called once per frame
    void Update(){
        if(IsMouseOverObject() == true && !hasGiven){
            spriteRenderer.sprite = hit;
            if(!hasPlayed){
                MouseOver.Play();
                hasPlayed = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if(itemDatabase.GetComponent<ItemDatabase>().costs[ItemID] <= PlayerPrefs.GetInt("Gold")){
                    // Play the click sound when the button is clicked
                    onclick.Play();
                    PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - itemDatabase.GetComponent<ItemDatabase>().costs[ItemID]);
                    Debug.Log(PlayerPrefs.GetInt("Gold"));
                    PlayerPrefs.Save();
                    
                    if(isEquipmentTile){
                        itemDatabase.GetComponent<giveItem>().GiveEquip(ItemID);
                    }
                    else{
                        itemDatabase.GetComponent<giveItem>().Give(ItemID);
                    }
                    hasGiven = true;

                    ShopLoader.GetComponent<ShopLoader>().EmptyShopItem(tileID, ItemID);
                

            }
        }
    }
        else{
            spriteRenderer.sprite = basic;
            hasPlayed = false;
        }
    


    // Function to check if the mouse is over the GameObject
    bool IsMouseOverObject()
    {
        
        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        // Check if the mouse position is within the GameObject's bounds
        Collider2D collider = GetComponent<Collider2D>();
        return collider.bounds.Contains(mousePosition);
    }    
    
}
}
