using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GenerateMap : MonoBehaviour
{
    public GameObject node;
    public GameObject line;

    public Sprite path1;
    public Sprite path2;
    public Sprite path3;

    private GameObject dataHolder;
    private GameObject idHolder;

    public int currentNodex;
    public int currentNodey;

    private bool hasMoved = false;
    private static GameObject currentNode;
    public GameObject playerIcon;

    public bool isGeneratingMap = true;

    private bool hasStarted = false;

    public int[] nodesCount = new int[10];

    public int[,] enemyIds = new int[10, 6];

    public GameObject[,] nodesMap = new GameObject[10, 6];
    public static int[,] nodesSave;// = new int[10, 6]; 
    public static Vector3[,] nodePositions;

    private SpriteRenderer spriteRenderer;

    private GameObject CursorIcon;
    
    private int numNextNodes = 0;
    private int currentNodeNum = 0;

    private float cursorTimer = 0.0f;
    private GameObject SwitchSound;

    private int EnemyID;

    private GameObject HelpPanel;


    private bool sceneOne;
    void Start(){
        
        hasStarted = false;
        sceneOne = true;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("MapGen");
        idHolder = GameObject.FindWithTag("IDTag");
        playerIcon = GameObject.FindWithTag("PlayerIcon");
        CursorIcon = GameObject.FindWithTag("CursorIcon");
        HelpPanel = GameObject.FindWithTag("HelpPanel");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        else{
    // Mark this GameObject to not be destroyed on scene load
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

            dataHolder = GameObject.FindWithTag("DataHolder");
            SwitchSound = GameObject.FindWithTag("SFX1");

            
        if(dataHolder.GetComponent<Preferences>().mapHasGenerated == false){

            isGeneratingMap = false;
            hasStarted = false;
            hasMoved = false;

            nodesSave = new int[10, 6];

            currentNodex = 0;
            currentNodey = 0;
            
            //IF GENERATING MAP FOR FIRST TIME
            dataHolder.GetComponent<Preferences>().mapHasGenerated = true;

            nodesCount[0] = 1;
            nodesCount[1] = Random.Range(2, 3);
            nodesCount[2] = Random.Range(3, 4);
            nodesCount[3] = Random.Range(3, 5);
            nodesCount[4] = Random.Range(4, 6);
            nodesCount[5] = Random.Range(4, 6);
            nodesCount[6] = Random.Range(3, 5);
            nodesCount[7] = Random.Range(2, 5);
            nodesCount[8] = Random.Range(2, 4);
            nodesCount[9] = 1;

        
        //Save map in preferences

        
        GenerateNodes();
        currentNode = nodesMap[0, 0];
        playerIcon.transform.position = currentNode.transform.position;

        generatePaths();
        crudeFill();
        savePositions();

        getnumNodes();

        hasStarted = true; 
        }
        else{

            currentNode = nodesMap[currentNodex, currentNodey];
            
            hasStarted = true;
        }
    }

    void Update(){
        if(sceneOne){
            
            //Help Panel
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                // Toggle the active state of the GameObject
                HelpPanel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Time.timeScale = 0.0f;
                
            }
            else{
                HelpPanel.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                Time.timeScale = 1.0f;
        
            }
        }
    
        if(hasStarted){
            goToNode();

    }
}

void getnumNodes(){
    numNextNodes = 1;
        if(currentNode.GetComponent<NodeObject>().node2 != null){
            numNextNodes = 2;
        }
        if(currentNode.GetComponent<NodeObject>().node3 != null){
            numNextNodes = 3;
        }
        
        currentNodeNum = 1;
}


    void goToNode(){


            cursorTimer -= Time.deltaTime;
            

            if(Input.GetKey(KeyCode.DownArrow) && cursorTimer <= 0){
                cursorTimer = 0.7f;

                currentNode = nodesMap[currentNodex, currentNodey];
                if(numNextNodes == 1){
                    currentNodeNum = 1;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node1.transform.position;
                }
                if(currentNodeNum == 1 && numNextNodes == 2) {
                    currentNodeNum = 2;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node2.transform.position;
                }
                else if(currentNodeNum == 2 && numNextNodes == 2) {
                    currentNodeNum = 1;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node1.transform.position;
                }
                else if(currentNodeNum == 1 && numNextNodes == 3) {
                    currentNodeNum = 2;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node2.transform.position;
                }
                else if(currentNodeNum == 2 && numNextNodes == 3) {
                    currentNodeNum = 3;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node3.transform.position;
                }
                else if(currentNodeNum == 3 && numNextNodes == 3) {
                    currentNodeNum = 1;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node1.transform.position;
                }

                Debug.Log("ID IS " + enemyIds[currentNodex, currentNodey]);

            }

            else if(Input.GetKey(KeyCode.UpArrow) && cursorTimer <= 0){
                cursorTimer = 0.7f;
                SwitchSound.GetComponent<AudioSource>().Play();
                //change cursor pos
                currentNode = nodesMap[currentNodex, currentNodey];


                if(numNextNodes == 1){
                    currentNodeNum = 1;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node1.transform.position;
                }
                
                if(currentNodeNum == 1 && numNextNodes == 2) {
                    currentNodeNum = 2;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node2.transform.position;
                }
                else if(currentNodeNum == 2 && numNextNodes == 2) {
                    currentNodeNum = 1;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node1.transform.position;
                }

                else if(currentNodeNum == 1 && numNextNodes == 3) {
                    currentNodeNum = 3;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node3.transform.position;
                }
                else if(currentNodeNum == 2 && numNextNodes == 3) {
                    currentNodeNum = 1;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node1.transform.position;
                }
                else if(currentNodeNum == 3 && numNextNodes == 3) {
                    currentNodeNum = 2;
                    CursorIcon.transform.position = currentNode.GetComponent<NodeObject>().node2.transform.position;
                }
                Debug.Log("ID IS " + enemyIds[currentNodex, currentNodey]);
            
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {


                if(currentNodeNum == 1)// if either of the next nodes is in collider with mouse
                { 
                    currentNodex = currentNode.GetComponent<NodeObject>().node1.GetComponent<NodeObject>().arrayX;
                    currentNodey = currentNode.GetComponent<NodeObject>().node1.GetComponent<NodeObject>().arrayY;      
                    hasMoved = true;         
                }
                else if (currentNodeNum == 2)// if either of the next nodes is in collider with mouse
                {
                    currentNodex = currentNode.GetComponent<NodeObject>().node2.GetComponent<NodeObject>().arrayX;
                    currentNodey = currentNode.GetComponent<NodeObject>().node2.GetComponent<NodeObject>().arrayY;  
                    hasMoved = true;                     
                }
                else if (currentNodeNum == 3)// if either of the next nodes is in collider with mouse
                {
                    currentNodex = currentNode.GetComponent<NodeObject>().node3.GetComponent<NodeObject>().arrayX;
                    currentNodey = currentNode.GetComponent<NodeObject>().node3.GetComponent<NodeObject>().arrayY;  
                    hasMoved = true;                     
                }
            }

            if(hasMoved){

                currentNode = nodesMap[currentNodex, currentNodey];

                playerIcon.transform.position = currentNode.transform.position;
                dataHolder.GetComponent<Preferences>().currentnode = currentNode;


                
                //hasMoved = false;

                 
                
                //Go to correct scene
                if(currentNode.GetComponent<NodeObject>().nodeType == 1){
                    hasMoved = false;
                    getnumNodes();
                }
                else if(currentNode.GetComponent<NodeObject>().nodeType == 2){
                    hasStarted = false;

                    //For now this is random but need to decide if each tile should be randomly given an id in future.
                    EnemyID = enemyIds[currentNodex, currentNodey];
                    dataHolder.GetComponent<EnemyData>().enemyID = EnemyID;
                    Debug.Log("MY ID IS " + EnemyID);




                    GetComponent<FadeScene>().LoadLevelUpDown(2);
                }
                else if(currentNode.GetComponent<NodeObject>().nodeType == 3){
                    hasStarted = false;
                    GetComponent<FadeScene>().LoadLevelUpDown(4);
                }
                else if(currentNode.GetComponent<NodeObject>().nodeType == 5){
                    hasStarted = false;
                    GetComponent<FadeScene>().LoadLevelUpDown(3);
                }
                else if(currentNode.GetComponent<NodeObject>().nodeType == 6){
                    hasStarted = false;
                    GetComponent<FadeScene>().LoadLevelUpDown(5);
                }
                else if(currentNode.GetComponent<NodeObject>().nodeType == 7){
                    hasStarted = false;
                    GetComponent<FadeScene>().LoadLevelUpDown(6);
                }
                else if(currentNode.GetComponent<NodeObject>().nodeType == 4){{
                    hasStarted = false;
                    EnemyID = enemyIds[currentNodex, currentNodey];
                    dataHolder.GetComponent<EnemyData>().enemyID = 3;
                    GetComponent<FadeScene>().LoadLevelUpDown(2);
                }
            }

            
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){ 
        sceneOne = false;
        hasStarted = false;
        node = GameObject.FindWithTag("Node");
        line = GameObject.FindWithTag("Path");
        CursorIcon = GameObject.FindWithTag("CursorIcon");
        dataHolder = GameObject.FindWithTag("DataHolder");
        SwitchSound = GameObject.FindWithTag("SFX1");
        

        //InfoPanel = GameObject.FindWithTag("InfoPanel");
        //InfoPanel.transform.position = new Vector3(999f, 999f, 0f);
        
        if(scene.buildIndex == 1){
            sceneOne = true;
            HelpPanel = GameObject.FindWithTag("HelpPanel");
            playerIcon.SetActive(true);
            if(isGeneratingMap == false){
            regenMap();
            currentNode = nodesMap[currentNodex, currentNodey];

            placeNodes();

            loadPositions();

            generatePaths();
            repaint();
            hasStarted = true;
            hasMoved = false;
            getnumNodes();

            }
            
        }
        else{
            playerIcon.SetActive(false);
        }
    }

    public void regenMap(){
        for(int i = 0; i <= nodesCount.Length - 1; i++){
            for(int j = 0; j <= nodesCount[i]; j++){
                GameObject tode = Instantiate(node);
                tode.SetActive(true);
                
                tode.GetComponent<NodeObject>().nodeType = nodesSave[i, j];

                tode.GetComponent<NodeObject>().arrayX = i;
                tode.GetComponent<NodeObject>().arrayY = j;
                
                nodesMap[i, j] = tode;
                Debug.Log(i + " - " + nodesMap[i, j].GetComponent<NodeObject>().nodeType);

            }
        }
    }

    public void GenerateNodes(){
        //Fill Out Array with Nodes
            for(int i = 0; i <= nodesCount.Length - 1; i++){

                //Reset Position of NodePlacer
                transform.position = new Vector3((1.7f * i) - 8f, Random.Range(1.6f, 2.0f) * (nodesCount[i]-1)/2, 0.0f);

                
                //Fill Array out
                for(int j = 0; j <= nodesCount[i] - 1; j++){
                    
                    GameObject tode = Instantiate(node);
                    tode.SetActive(true);

                    tode.GetComponent<NodeObject>().arrayX = i;
                    tode.GetComponent<NodeObject>().arrayY = j;

                    tode.GetComponent<NodeObject>().depth = i;

                    nodesMap[i, j] = tode;

                    //Debug.Log("depth is " + tode.GetComponent<NodeObject>().depth);

                    transform.position -= new Vector3(0f, 1.3f, 0f);

                    tode.transform.position = transform.position;


                    //Randomize node position
                    tode.transform.position += new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.2f, 0.2f), 0f);
                
                
                    
                }
            }
    }
    public void savePositions(){
       nodePositions = new Vector3[10, 6];

       for(int i = 0; i <= nodesCount.Length - 1; i++){
                for(int j = 0; j <= nodesCount[i] - 1; j++){
                    nodePositions[i, j] = nodesMap[i, j].transform.position;
            }
       }
    }
    public void loadPositions(){
        for(int i = 0; i <= nodesCount.Length - 1; i++){
                for(int j = 0; j <= nodesCount[i] - 1; j++){
                    nodesMap[i, j].transform.position = nodePositions[i,j];
            }
       }
    }

    public void placeNodes(){
        for(int i = 0; i <= nodesCount.Length - 1; i++){

                //Reset Position of NodePlacer
                transform.position = new Vector3((1.6f * i) - 8f, Random.Range(1.6f, 2.0f) * (nodesCount[i]-1)/2, 0.0f);

                
                //Fill Array out
                for(int j = 0; j <= nodesCount[i] - 1; j++){
                    //Debug.Log("depth is " + tode.GetComponent<NodeObject>().depth);

                    transform.position += new Vector3(0f, -1.3f, 0f);
                    Debug.Log(nodesMap[i, j]);

                    nodesMap[i, j].transform.position = transform.position;


                    //Randomize node position
                    nodesMap[i, j].transform.position += new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.2f, 0.2f), 0f);
                
                
                    
                }
            }
    }


    public void generatePaths(){
        //Fill Out Array Paths
        for(int i = 0; i <= nodesCount.Length - 1; i++){       
            for(int j = 0; j <= nodesCount[i] - 1; j++){
                //If we aren't at final depth d=10
                if(i < 9){

                    //If next level has more nodes than current layer
                    if(nodesCount[i] < nodesCount[i+1]){
                        //If we have to jump up by 2 nodes ex. from 3 to 5.
                        if(nodesCount[i+1] - nodesCount[i] > 1){
                            nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j];
                            nodesMap[i, j].GetComponent<NodeObject>().node2 = nodesMap[i+1, j+1];
                            nodesMap[i, j].GetComponent<NodeObject>().node3 = nodesMap[i+1, j+2];
                            
                            makePath(nodesMap[i, j], nodesMap[i+1, j]);
                            makePath(nodesMap[i, j], nodesMap[i+1, j+1]);
                            makePath(nodesMap[i, j], nodesMap[i+1, j+2]);
                        }
                        else{   
                            nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j];
                            nodesMap[i, j].GetComponent<NodeObject>().node2 = nodesMap[i+1, j+1];
                            makePath(nodesMap[i, j], nodesMap[i+1, j]);
                            makePath(nodesMap[i, j], nodesMap[i+1, j+1]);
                        }
                        
                    }


                    //If next level has equal nodes than current layer
                    if(nodesCount[i] == nodesCount[i+1]){
                        //Map each node to itself on next layer
                        if(j == 0){
                            nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j];
                            nodesMap[i, j].GetComponent<NodeObject>().node2 = nodesMap[i+1, j+1];
                            makePath(nodesMap[i, j], nodesMap[i+1, j]);
                            makePath(nodesMap[i, j], nodesMap[i+1, j+1]);
                        }
                        else if(j == nodesCount[i] - 1){
                            nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-1];
                            nodesMap[i, j].GetComponent<NodeObject>().node2 = nodesMap[i+1, j];
                            makePath(nodesMap[i, j], nodesMap[i+1, j-1]);
                            makePath(nodesMap[i, j], nodesMap[i+1, j]);
                        }
                        else{
                            nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-1];
                            nodesMap[i, j].GetComponent<NodeObject>().node2 = nodesMap[i+1, j];
                            nodesMap[i, j].GetComponent<NodeObject>().node3 = nodesMap[i+1, j+1];
                            
                            makePath(nodesMap[i, j], nodesMap[i+1, j-1]);
                            makePath(nodesMap[i, j], nodesMap[i+1, j]);
                            makePath(nodesMap[i, j], nodesMap[i+1, j+1]);
                        }
                    }



                    //If next level has less nodes than current layer
                    if(nodesCount[i] > nodesCount[i+1]){
                        //Map first and last node of layer to n[0] and n[i] respectively
                        // Then map i to i and map i to i - 1 for each node

                        //If we have to jump down more than 1 node, ex. 5 to 3.
                        if(nodesCount[i] - nodesCount[i+1] > 1){
                            //Map Top node to Top node
                            if(j == 0){
                                nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j];
                                makePath(nodesMap[i, j], nodesMap[i+1, j]);
                            }
                            //Map bottom node to bottom node
                            else if(j == nodesCount[i] - 1){
                                nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-2];
                                makePath(nodesMap[i, j], nodesMap[i+1, j-2]);
                            }
                            else{
                                //3 Nodes to 1 Node
                                if(nodesCount[i] - 2 == 1){
                                        nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-1];
                                        makePath(nodesMap[i, j], nodesMap[i+1, j-1]);
                                }
                                //4 Nodes To 2 Nodes
                                else if(nodesCount[i] - 2 == 2){
                                    if(j == 1){
                                        nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-1];
                                        nodesMap[i, j].GetComponent<NodeObject>().node2 = nodesMap[i+1, j];
                                        makePath(nodesMap[i, j], nodesMap[i+1, j-1]);
                                        makePath(nodesMap[i, j], nodesMap[i+1, j]);
                                    }
                                    else{
                                        nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-2];
                                        nodesMap[i, j].GetComponent<NodeObject>().node2 = nodesMap[i+1, j-1];
                                        makePath(nodesMap[i, j], nodesMap[i+1, j-2]);
                                        makePath(nodesMap[i, j], nodesMap[i+1, j-1]);                                  
                                    }
                                }
                                //5 Nodes to 3 Nodes
                                else{
                                    if(j == 1){
                                    nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-1];
                                    nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j];
                                    makePath(nodesMap[i, j], nodesMap[i+1, j-1]);
                                    makePath(nodesMap[i, j], nodesMap[i+1, j]);
                                    }
                                    else if(j == 2){
                                    nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-2];
                                    nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-1];
                                    nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j];
                                    makePath(nodesMap[i, j], nodesMap[i+1, j-2]);
                                    makePath(nodesMap[i, j], nodesMap[i+1, j-1]);
                                    makePath(nodesMap[i, j], nodesMap[i+1, j]);
                                    }
                                    else if(j == 3){
                                    nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-2];
                                    nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-1];
                                    makePath(nodesMap[i, j], nodesMap[i+1, j-2]);
                                    makePath(nodesMap[i, j], nodesMap[i+1, j-1]);
                                    }

                                }
                            }

                        }
                        //If we have a jump of one node
                        else{

                            if(j == 0){
                                nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j];
                                makePath(nodesMap[i, j], nodesMap[i+1, j]);
                            }
                            else if(j == nodesCount[i] - 1){
                                nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-1];
                                makePath(nodesMap[i, j], nodesMap[i+1, j-1]);
                            }
                            else{
                                nodesMap[i, j].GetComponent<NodeObject>().node1 = nodesMap[i+1, j-1];
                                nodesMap[i, j].GetComponent<NodeObject>().node2 = nodesMap[i+1, j];
                                makePath(nodesMap[i, j], nodesMap[i+1, j-1]);
                                makePath(nodesMap[i, j], nodesMap[i+1, j]);
                            }
                        }
                    }

                    

                }
            }
        }
    }

    public void makePath(GameObject Node1, GameObject Node2){
        //groosss grossss yuckk we have to use trig. it is
        double RadAngle = Math.Atan2((Node2.transform.position.y - Node1.transform.position.y), (Node2.transform.position.x - Node1.transform.position.x));
        float DegAngle = (float)(RadAngle * (180.0 / Math.PI));

        //double hyp1
        //double hyp2

        float hypot = (float)Math.Pow((Math.Pow(Node2.transform.position.y - Node1.transform.position.y, 2)) + (Math.Pow(Node2.transform.position.x - Node1.transform.position.x, 2)), 0.5);

        GameObject path = Instantiate(line);
        path.SetActive(true);
        spriteRenderer = path.GetComponent<SpriteRenderer>();

        path.transform.position = Node1.transform.position;
        path.transform.localScale = new Vector3(hypot, 1f, 1f);
        
        Vector3 rot = path.transform.localRotation.eulerAngles; //get the angles
        rot.Set(0f, 0f, DegAngle); //set the angles
        path.transform.localRotation = Quaternion.Euler(rot); //update the transform

        path.transform.Translate(new Vector3(hypot/2f, 0.0f, 0.0f));


        //Give Path a random Path type Sprite
        int Rand = Random.Range(0, 3);
        if(Rand == 0){
            spriteRenderer.sprite = path1;
        }
        else if(Rand == 1){
            spriteRenderer.sprite = path2;
        }
        else if(Rand == 2){
            spriteRenderer.sprite = path3;
        }

    }

    // Update is called once per frame
    public void Destroymap()
    {
        Debug.Log("Reseting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

        // Function to get a specific row from a 2D array
    int[] GetRow(int[,] array, int rowIndex)
    {
        int numCols = array.GetLength(1);
        int[] row = new int[numCols];

        for (int i = 0; i < numCols; i++)
        {
            row[i] = array[rowIndex, i];
        }

        return row;
    }

    //Node Assignment----------------------

    //int currentPhase = 0;

    

    //NodeTypes
    //==============================
    //1 - Blank
    //2 - Enemy
    //3 - Chest
    //4 - Boss
    //5 - Shop
    //6 - Healing Well
    //7 - Gates
    //8 - Thorns
    //==============================


    //------------------------------------------------------
    //Go over every node in nodesmap and do phase for each one
    public void crudeFill(){


        int shopDelayWait = 0; 
        int healthpoolWait = 3;

        for(int i = 0; i <= nodesCount.Length - 1; i++){       
                for(int j = 0; j <= nodesCount[i] - 1; j++){
                    //Phase 1
                    //Empty Fill
                    nodesMap[i, j].GetComponent<NodeObject>().nodeType = 1;
                    nodesSave[i,j] = 1;
                    nodesMap[i, j].GetComponent<NodeObject>().Recolor();

                    //Phase 2
                    //Enemy Fill
                    if(Random.Range(0, 4) >= 1){
                        nodesMap[i, j].GetComponent<NodeObject>().nodeType = 2;
                        nodesSave[i,j] = 2;
                        nodesMap[i, j].GetComponent<NodeObject>().Recolor();
                        
                        //We are going to start filling out enemies here and then "intelligently"
                        //Adding enemies to enemytiles in a rotation type algorithm
                        int nodeID = Random.Range(0, 3);
                        nodesMap[i, j].GetComponent<NodeObject>().enemyID = nodeID;
                        enemyIds[i, j] = nodeID;

                    }

                    //Phase 3
                    //Shop Fill
                    if(Random.Range(0, 5) == 0 && shopDelayWait <= 0){  //In the future maybe increase the chance to spawn shop with increased time since last shop spawn
                        shopDelayWait = 4;
                        nodesMap[i, j].GetComponent<NodeObject>().nodeType = 5;
                        nodesSave[i,j] = 5;
                        nodesMap[i, j].GetComponent<NodeObject>().Recolor();
                    }
                    else{
                        shopDelayWait -= 1;
                    }

                    //Phase 4
                    //Health Pool Fill
                    if(Random.Range(0, 10) == 0 && healthpoolWait <= 0){
                        healthpoolWait = 8;
                        nodesMap[i, j].GetComponent<NodeObject>().nodeType = 6;
                        nodesSave[i,j] = 6;
                        nodesMap[i, j].GetComponent<NodeObject>().Recolor();
                    }
                    else{
                        healthpoolWait -= 1;
                    }
                    //Phase 5
                    //Thorn Fill
                    if(Random.Range(0,12) == 0){
                        nodesMap[i, j].GetComponent<NodeObject>().nodeType = 7;
                        nodesSave[i,j] = 7;
                        nodesMap[i, j].GetComponent<NodeObject>().Recolor();
                    }
                    //Phase 6
                    //Chest Fill
                    if(i == 5){
                        nodesMap[i, j].GetComponent<NodeObject>().nodeType = 3;
                        nodesSave[i,j] = 3;
                        nodesMap[i, j].GetComponent<NodeObject>().Recolor();
                    }

                    //Phase 7
                    //Boss Fill
                    if(i == 9){
                        nodesMap[i, j].GetComponent<NodeObject>().nodeType = 4;
                        //Chooose boss, later there will be more than 1 and will be in its own array.
                        nodesSave[i,j] = 4;
                        nodesMap[i, j].GetComponent<NodeObject>().Recolor();

                        int nodeID = 3;
                        nodesMap[i, j].GetComponent<NodeObject>().enemyID = nodeID;
                        enemyIds[i, j] = nodeID;
                    }

                    //Place Empty at Start
                    if(i == 0){
                        nodesMap[i, j].GetComponent<NodeObject>().nodeType = 1;
                        nodesSave[i,j] = 1;
                        nodesMap[i, j].GetComponent<NodeObject>().Recolor();
                    }

                    //Make EnemyIDs only on enemies
                    if(nodesMap[i, j].GetComponent<NodeObject>().nodeType != 2){
                    }

            }

        }


    }

    public void repaint(){
        for(int i = 0; i <= nodesCount.Length - 1; i++){       
            for(int j = 0; j <= nodesCount[i] - 1; j++){
                nodesMap[i, j].GetComponent<NodeObject>().Recolor();
            }
        }
    }
}
