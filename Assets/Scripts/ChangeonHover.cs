using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeonHover : MonoBehaviour
{
    private bool hasPlayed;
    private SpriteRenderer spriteRenderer;

    public Sprite basic;
    public Sprite hit;

    private Collider2D buttonCollider;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D collider = GetComponent<Collider2D>();

        spriteRenderer.sprite = basic;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(IsMouseOverObject() == true){
            spriteRenderer.sprite = hit;
        }
        else{
            spriteRenderer.sprite = basic;
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

