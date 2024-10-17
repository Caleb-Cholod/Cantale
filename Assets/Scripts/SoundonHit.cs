using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundonHit : MonoBehaviour
{
    public AudioSource onclick;
    private bool hasPlayed;
    private SpriteRenderer spriteRenderer;
    private Collider2D buttonCollider;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsMouseOverObject() == true){
            if (Input.GetMouseButtonDown(0))
            {
                // Play the click sound when the button is clicked
                onclick.Play();

            }
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
