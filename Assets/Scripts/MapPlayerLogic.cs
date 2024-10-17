using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;

public class MapPlayerLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mapGen;
    private GameObject[,] nodesmap = new GameObject[10, 6];

    private GameObject currentNode;
    private GameObject dataHolder;
    private GameObject idHolder;

    private bool hasMoved = false;


    void Start()
    {
        Invoke("DelayedStart", 0.1f);
    }
    void DelayedStart(){

        dataHolder = GameObject.FindWithTag("DataHolder");
        idHolder = GameObject.FindWithTag("IDTag");
        mapGen = GameObject.FindWithTag("MapGen");

        Debug.Log(mapGen.GetComponent<GenerateMap>().nodesMap[1, 0].GetComponent<NodeObject>().nodeType);
        

        if(mapGen.GetComponent<GenerateMap>().currentNodex == 0){
            mapGen.GetComponent<GenerateMap>().currentNodex = 0;
            mapGen.GetComponent<GenerateMap>().currentNodey = 0;
            currentNode = mapGen.GetComponent<GenerateMap>().nodesMap[0, 0];
        }
        else{
            currentNode = mapGen.GetComponent<GenerateMap>().nodesMap[mapGen.GetComponent<GenerateMap>().currentNodex,mapGen.GetComponent<GenerateMap>().currentNodey];
        }


        //Move player to current position
        //transform.position = currentNode.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        hasMoved=false;

        if (Input.GetMouseButtonDown(0))
            {
            Debug.Log("Mosus");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            

            //Move player to an available clicked node

            if(currentNode.GetComponent<NodeObject>().node1.GetComponent<Collider>().bounds.Contains(mousePos) && currentNode.GetComponent<NodeObject>().node1 != null)// if either of the next nodes is in collider with mouse
            { 
                currentNode = currentNode.GetComponent<NodeObject>().node1;      
                hasMoved = true;         
            }
            else if (currentNode.GetComponent<NodeObject>().node2.GetComponent<Collider>().bounds.Contains(mousePos) && currentNode.GetComponent<NodeObject>().node2 != null)// if either of the next nodes is in collider with mouse
            {
                currentNode = currentNode.GetComponent<NodeObject>().node2;
                hasMoved = true;                     
            }
            else if (currentNode.GetComponent<NodeObject>().node3.GetComponent<Collider>().bounds.Contains(mousePos)  && currentNode.GetComponent<NodeObject>().node3 != null)// if either of the next nodes is in collider with mouse
            {
                currentNode = currentNode.GetComponent<NodeObject>().node3;
                hasMoved = true;                     
            }

            if(hasMoved){
                transform.position = currentNode.transform.position;
                dataHolder.GetComponent<Preferences>().currentnode = currentNode;
                

                Debug.Log(currentNode.GetComponent<NodeObject>().nodeType);
                Debug.Log(dataHolder.GetComponent<Preferences>().currentnode.GetComponent<NodeObject>().nodeType);
                //idHolder.GetComponent<RetainObject>().enemyID = Random.Range(0, 4); //For now this is random but need to decide if each tile should be randomly given an id in future.

                //Go to correct scene
                if(currentNode.GetComponent<NodeObject>().nodeType == 2){
                    SceneManager.LoadScene(2);
                }
                else if(currentNode.GetComponent<NodeObject>().nodeType == 3){
                    SceneManager.LoadScene(4);
                }
                else if(currentNode.GetComponent<NodeObject>().nodeType == 5){
                    SceneManager.LoadScene(5);
                }
                else{
                    Debug.Log("ehh...not implemented yet");
                }
            }
    }
}
}
