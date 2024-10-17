using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeonHover : MonoBehaviour
{
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
            transform.localScale = new Vector3(2.2f, 2.2f, 1f);
        }
        else{
            transform.localScale = new Vector3(2.1f, 2.1f, 1f);
        }
    }

        bool IsMouseOverObject()
    {
        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the mouse position is within the GameObject's bounds
        Collider2D collider = GetComponent<Collider2D>();
        return collider.bounds.Contains(mousePosition);
    }
}
