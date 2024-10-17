using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    private bool wasReleased;
    public KeyCode keyToPress;
    public GameManager gameManager;

    private SpriteRenderer spriteRenderer;
    public int noteIndex;

    public Sprite red;
    public Sprite blue;
    public Sprite green;
    public Sprite yellow;


    public bool isFinalinSide = false;
    public bool isFinalinSong = false;


    public bool isStartingSlide = false;
    public bool isEndingSlide = false;
    private bool isinSlide = false;


    //public GameObject p, hitEffect, goodEffect, perfectEffect, missEffect;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //p = Instantiate(perfectEffect);
        //p.SetActive(false);
        if(keyToPress == KeyCode.UpArrow){
            spriteRenderer.sprite = red;
        }
        else if(keyToPress == KeyCode.DownArrow){
            spriteRenderer.sprite = green;

        }
        else if(keyToPress == KeyCode.LeftArrow){
            spriteRenderer.sprite = blue;

        }
        else if(keyToPress == KeyCode.RightArrow){
            spriteRenderer.sprite = yellow;
            
        }

        wasReleased = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(isinSlide){
            if(Input.GetKey(keyToPress)){
            }
            else{
                wasReleased = true;   
            }
        }
        //
        if(isEndingSlide && !wasReleased){
            if(canBePressed && Input.GetKeyUp(keyToPress)){
                Debug.Log("NoteReleased!");
                Destroy(gameObject); 
                wasReleased = true;
                
                print("note " + noteIndex);
                //Run perf,good,etc. tests
                if(Mathf.Abs(transform.position.y) < 0.1f){
                    gameManager.GetComponent<GameManager>().characterFrame = noteIndex;
                    GameManager.instance.PerfectHit();
                }
                else if(Mathf.Abs(transform.position.y) < 0.2f){
                    gameManager.GetComponent<GameManager>().characterFrame = noteIndex;
                    GameManager.instance.GoodHit();
                }
                else if(Mathf.Abs(transform.position.y) < 0.3f){
                    gameManager.GetComponent<GameManager>().characterFrame = noteIndex;
                    GameManager.instance.NormalHit();
                }
                else{
                    GameManager.instance.NoteMissed();
                }

                if(isFinalinSide){
                    gameManager.GetComponent<GameManager>().SwitchSide();
                }
            
            }
        }


        else if(Input.GetKeyDown(keyToPress)){

            if(canBePressed){
                if(isStartingSlide){
                    wasReleased = false;
                    isinSlide = true;
                }
                else{
                    Destroy(gameObject);    

                }

                if(Mathf.Abs(transform.position.y) < 0.1f){
                    gameManager.GetComponent<GameManager>().characterFrame = noteIndex;
                    GameManager.instance.PerfectHit();
                }
                else if(Mathf.Abs(transform.position.y) < 0.2f){
                    gameManager.GetComponent<GameManager>().characterFrame = noteIndex;
                    GameManager.instance.GoodHit();
                }
                else if(Mathf.Abs(transform.position.y) < 0.3f){
                    gameManager.GetComponent<GameManager>().characterFrame = noteIndex;
                    GameManager.instance.NormalHit();
                }
                else{
                GameManager.instance.NoteMissed();
                }


                //If Note Hit
                if(isFinalinSide){
                    gameManager.GetComponent<GameManager>().SwitchSide();
                }
            
            }
        }
        //If note has gone past
        if(transform.position.y < -0.5f && !isStartingSlide){
            Destroy(gameObject);
            GameManager.instance.NoteMissed();

            //Also check if that missed note was a gfinal one
                if(isFinalinSide){
                    gameManager.GetComponent<GameManager>().SwitchSide();
                }
        }
    }


    public void createHoldNote(){
        //Instantiate(HNB;)
        //Want to get the key
        //then when canbepressed want to check key for Input.GetButton() every update, this loop ends only when either note is lifted or next endingslide note with keynum is in its canbe pressed state.

    }

    public void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Activator"){
            canBePressed = true;
        }
    }

    public void OnTriggerExit2D(Collider2D coll){
        if(coll.tag == "Activator"){
            canBePressed = false;

        }
    }
}
