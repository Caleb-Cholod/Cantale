using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMapThorny : MonoBehaviour
{
    public Sprite greenArrow;
    public Sprite defaultArrow;
    private SpriteRenderer spriteRenderer;

    private bool hasPlayed;
    public AudioSource MouseOver;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hasPlayed = false;
    }
    void damagePlayer(){
        PlayerPrefs.SetInt("CurrentPlayerHP", PlayerPrefs.GetInt("CurrentPlayerHP") - 5);
        PlayerPrefs.Save();
        Debug.Log("Hurty");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsMouseOverObject()){
            //Damage Player
            damagePlayer();
            

            //Go To Map
            this.GetComponent<FadeScene>().LoadLevelUpDown(1);
        }
        if(IsMouseOverObject()){
            spriteRenderer.sprite = greenArrow;  
            if(!hasPlayed){
                MouseOver.Play();
                hasPlayed = true;
            }
        }
        else{
            spriteRenderer.sprite = defaultArrow;
            hasPlayed = false;
        }
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
