using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class holdNoteBar : MonoBehaviour
{
    public Sprite red;
    public Sprite blue;
    public Sprite green;
    public Sprite yellow;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        
    }
    public void create(int arrow, float firstY, float lastY){
        this.gameObject.SetActive(true);
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Set Color of Note
        
            if(arrow == 0){
                spriteRenderer.sprite = blue;
            }
            else if(arrow == 1){
                spriteRenderer.sprite = green;
            }
            else if(arrow == 2){
                spriteRenderer.sprite = red;
            }
            else if(arrow == 3){
                spriteRenderer.sprite = yellow;
            }
        
        
        //Resize and Reposition

        float avg = (firstY + lastY) / 2;

        Debug.Log(avg + " : " + firstY + " : " + lastY);

        //Move position x by arrowid
        transform.position += new Vector3(1.5f * arrow, avg - 12.83f, 0.0f);


        //Resize scale based on distance between start and end values
        float y = lastY - firstY;

        transform.localScale = new Vector3(0.5875f, y, 1.0f);


    }
}
