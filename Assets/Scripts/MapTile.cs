using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public GameObject playerIcon;
    private GameObject idHolder;
    private int lockInstruction;
    private GameObject dataHolder;

    public int EnemyID;
    //public GameObject idHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        lockInstruction = 0;
        dataHolder = GameObject.FindWithTag("DataHolder");
    }

    // Update is called once per frame
    void Update(){
        if(IsMouseOverObject()){

            if (Input.GetMouseButtonDown(0)){
                Debug.Log(EnemyID);
                //dataHolder.GetComponent<Preferences>().mapSpot = EnemyID;

                if(EnemyID != 2){
                        
                        playerIcon.transform.position = transform.position;
                        //dataHolder.GetComponent<Preferences>().mapSpot = playerIcon.transform.position;

                        idHolder = GameObject.Find("IDHolder");

                        //idHolder.GetComponent<RetainObject>().enemyID = EnemyID;
                        //Debug.Log("Given ID: " + idHolder.GetComponent<RetainObject>().enemyID);

                        if(EnemyID == -1){
                            this.GetComponent<MovetoScene>().GoToScene(3);
                        }
                        else if(EnemyID == -2){
                            this.GetComponent<MovetoScene>().GoToScene(4);
                        }
                        else
                            this.GetComponent<MovetoScene>().GoToScene(2);
                }
                else{                    //Use a lock to grab the first enemy, ncm this is weird
                    if(lockInstruction == 1){
                        //do 2nd enemy THIS IS GROSS FIX IT AAAAA
                        playerIcon.transform.position = transform.position;
                        //dataHolder.GetComponent<Preferences>().mapSpot = playerIcon.transform.position;

                        idHolder = GameObject.Find("IDHolder");

                        //idHolder.GetComponent<RetainObject>().enemyID = EnemyID;
                        //Debug.Log("Given ID: " + idHolder.GetComponent<RetainObject>().enemyID);

                        this.GetComponent<MovetoScene>().GoToScene(2);
                    }
                    else{
                        lockInstruction = 1;
                    }
                }
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
