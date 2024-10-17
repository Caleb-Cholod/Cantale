using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ChestButton : MonoBehaviour
{
    public AudioSource MouseOver;
    public AudioSource onclick;
    private bool hasPlayed;
    private SpriteRenderer spriteRenderer;

    public Sprite basic;
    public Sprite hit;

    private Collider2D buttonCollider;
    private bool hasGiven;
    public GameObject canvas;

    public Image image;
    public TMP_Text desc;

    private GameObject itemholder;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hasPlayed = false;
        hasGiven = false;

        itemholder = GameObject.FindWithTag("ItemHolder");
        
    }

    // Update is called once per frame
    void Update()
    {

        if(IsMouseOverObject() == true && !hasGiven){
            spriteRenderer.sprite = hit;
            if(!hasPlayed){
                MouseOver.Play();
                hasPlayed = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Play the click sound when the button is clicked
                onclick.Play();
                GetComponent<Animator>().SetBool("Open", true);

                int itemid = Random.Range(0, 3);
                GetComponent<giveItem>().Give(itemid);
                hasGiven = true;
                canvas.SetActive(true);

                image.sprite = itemholder.GetComponent<ItemDatabase>().sprites[itemid];
                desc.text = itemholder.GetComponent<ItemDatabase>().ItemDescriptions[itemid];
                


            }
        }
        else{
            spriteRenderer.sprite = basic;
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
