using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public AudioSource MouseOver;
    public AudioSource onclick;
    private bool hasPlayed;
    private SpriteRenderer spriteRenderer;

    public Sprite basic;
    public Sprite hit;

    public Animator transition;

    private Image imageComponent;


    private Collider2D buttonCollider;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        imageComponent = GetComponent<Image>();
        hasPlayed = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(IsMouseOverObject() == true){
            imageComponent.sprite = hit;
            if(!hasPlayed){
                MouseOver.Play();
                hasPlayed = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Play the click sound when the button is clicked
                onclick.Play();
                LoadLevel();

            }
        }
        else{
            imageComponent.sprite = basic;
            hasPlayed = false;
        }
    }

    public void LoadLevel(){
        StartCoroutine(LoadmyLevel(SceneManager.GetActiveScene().buildIndex + 1));
        //GetComponent<MovetoScene>().GoToScene(1);
        
    }

    IEnumerator LoadmyLevel(int levelIndex){
        Debug.Log("CORoutine");
        transition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
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
