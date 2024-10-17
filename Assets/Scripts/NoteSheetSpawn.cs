using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteSheetSpawn : MonoBehaviour
{
    private TextAsset textFile;
    public GameObject Note;
    private GameObject note;

    public GameObject holdBar;
    private GameObject HoldBar;
    public Transform parentHolder;
    private float parentCurrentY;

    public GameObject switchBar;
    private GameObject switchbar;


    public GameObject atkBar;
    private GameObject Atkbar;
    public GameObject defBar;
    private GameObject Defbar;

    public int noteConut;

    private bool myatk;

    private GameObject Dataholder;

    public GameObject switchconveyorpanel;

    private int[] tempNoteSlides = new int[4];

    private float[] firstPosSlides = new float[4];
    private string[] pairs = new string[2];
    private float z;

    // Start is called before the first frame update
    void Start()
    {
        Dataholder = GameObject.FindWithTag("DataHolder");
        textFile = GameObject.FindWithTag("SongTextHolder").GetComponent<SongTextDatabase>().Files[Dataholder.GetComponent<EnemyData>().enemyID];


            //After splitting it, we have to check if we have a ~ sliding note
            //If so we have to add that arrrownum to an array and keep checking that num for when we have it again

        myatk = false;
        
        string[] words = textFile.text.Split('-');
        

        for(int i = 0; i<= words.Length-1; i++){
            //SWITCH SIDES ON /

            if(words[i].Trim() == "/"){
                //Create bar for switching sides
                switchbar = Instantiate(switchBar, parentHolder);
                switchbar.SetActive(true);
                switchbar.transform.position += new Vector3(0.0f, note.transform.position.y, 0.0f);

                if(myatk){
                    Atkbar = Instantiate(atkBar, parentHolder);
                    Atkbar.SetActive(true);
                    Atkbar.transform.position += new Vector3(0.0f, note.transform.position.y, 0.0f);
                    myatk = false;
                }
                else{
                    Defbar = Instantiate(defBar, parentHolder);
                    Defbar.SetActive(true);
                    Defbar.transform.position += new Vector3(0.0f, note.transform.position.y, 0.0f);
                    myatk = true;
                }
                

                //Create Conveyor panel blue or red
                switchconveyorpanel.GetComponent<ConveyorPanelHandler>().Create(note.transform.position.y, switchconveyorpanel, parentHolder);

                //Mark note as final
                note.GetComponent<NoteObject>().isFinalinSide = true;
            }
            //RESET SONG, GENERATE NEW NOTES
            else if (words[i].Trim() == "&")
            {
                switchbar.GetComponent<FightSwitchBar>().isFinalBar = true;
            }

            //GENERATE A NEW NODE
            else{

                note = Instantiate(Note, parentHolder);
                note.SetActive(true);
                pairs = words[i].Split(',');
                string t = pairs[0].Trim();

                if(t.Length >= 2){
                    pairs[0] = t.Remove(1, 1);
                    note.GetComponent<NoteObject>().isStartingSlide = true;
                }
                
                    float x = 0-float.Parse(pairs[0].Trim());
                    float y = float.Parse(pairs[1]);
                    note.transform.Translate(new Vector3(1.5f*x, 0-y, 0));
                    z = float.Parse(pairs[0]);
                

                if(note.GetComponent<NoteObject>().isStartingSlide == true){
                        tempNoteSlides[int.Parse(pairs[0])] = 1;
                        firstPosSlides[int.Parse(pairs[0])] = note.transform.position.y;
                }

                //Check for an ending to the held note
                else if(tempNoteSlides[int.Parse(pairs[0])] == 1){
                    note.GetComponent<NoteObject>().isEndingSlide = true;

                    //Create Hold bar
                    Debug.Log("Creating slide");
                    HoldBar = Instantiate(holdBar, parentHolder);

                    HoldBar.GetComponent<holdNoteBar>().create(int.Parse(pairs[0]), firstPosSlides[int.Parse(pairs[0])], note.transform.position.y);

                    //Reset Slides
                    tempNoteSlides[int.Parse(pairs[0])] = 0; 
                }

                    //Assign Arrows with Correct Key and rotation
                        switch(z){
                            case 0:
                                note.transform.Rotate(0, 0, 0);
                                note.GetComponent<NoteObject>().keyToPress = KeyCode.LeftArrow;
                                note.GetComponent<NoteObject>().noteIndex = 0;
                            break;

                            case 1:
                                note.transform.Rotate(0, 0, 90);
                                note.GetComponent<NoteObject>().keyToPress = KeyCode.DownArrow;
                                note.GetComponent<NoteObject>().noteIndex = 1;
                            break;

                            case 2:
                                note.transform.Rotate(0, 0, -90);
                                note.GetComponent<NoteObject>().keyToPress = KeyCode.UpArrow;
                                note.GetComponent<NoteObject>().noteIndex = 2;
                            break;

                            case 3:
                                note.transform.Rotate(0, 0, 180);
                                note.GetComponent<NoteObject>().keyToPress = KeyCode.RightArrow;
                                note.GetComponent<NoteObject>().noteIndex = 3;
                            break;
                        }

                }
        }
            
        }



    public void LoopSong(float Yoffset)
    {
        Dataholder = GameObject.FindWithTag("DataHolder");
        textFile = GameObject.FindWithTag("SongTextHolder").GetComponent<SongTextDatabase>().Files[Dataholder.GetComponent<EnemyData>().enemyID];

        myatk = true;

        parentCurrentY = parentHolder.position.y;
        Debug.Log("GENNING NEW NOTES w/OFFSET" + parentCurrentY);

        
        string[] words = textFile.text.Split('-');


        for (int i = 0; i <= words.Length - 1; i++)
        {
            //SWITCH SIDES ON /

            if (words[i].Trim() == "/")
            {
                //Create bar for switching sides
                switchbar = Instantiate(switchBar, parentHolder);
                switchbar.SetActive(true);
                switchbar.transform.position += new Vector3(0.0f, parentCurrentY + note.transform.position.y, 0.0f);

                if (myatk)
                {
                    Atkbar = Instantiate(atkBar, parentHolder);
                    Atkbar.SetActive(true);
                    Atkbar.transform.position += new Vector3(0.0f, parentCurrentY + note.transform.position.y, 0.0f);
                    myatk = false;
                }
                else
                {
                    Defbar = Instantiate(defBar, parentHolder);
                    Defbar.SetActive(true);
                    Defbar.transform.position += new Vector3(0.0f, parentCurrentY + note.transform.position.y, 0.0f);
                    myatk = true;
                }


                //Create Conveyor panel blue or red
                switchconveyorpanel.GetComponent<ConveyorPanelHandler>().Create(parentCurrentY + note.transform.position.y, switchconveyorpanel, parentHolder);

                //Mark note as final
                note.GetComponent<NoteObject>().isFinalinSide = true;
            }
            //RESET SONG, GENERATE NEW NOTES
            else if (words[i].Trim() == "&")
            {
                //switchbar.GetComponent<FightSwitchBar>().isFinalBar = true;
            }

            //GENERATE A NEW NODE
            else
            {

                note = Instantiate(Note, parentHolder);
                
                pairs = words[i].Split(',');
                string t = pairs[0].Trim();

                if (t.Length >= 2)
                {
                    pairs[0] = t.Remove(1, 1);
                    note.GetComponent<NoteObject>().isStartingSlide = true;
                }

                float x = 0 - float.Parse(pairs[0].Trim());
                float y = float.Parse(pairs[1]);

                note.transform.position = new Vector3(-2.23f, 22, 0.0f);

                note.transform.Translate(new Vector3(1.5f * x, 0 - y, 0));
                z = float.Parse(pairs[0]);

                note.SetActive(true);
                noteConut += 1;
                //Debug.Log("numnotes" + noteConut);


                //Debug.Log("NOTE" + note.transform.position);

                if (note.GetComponent<NoteObject>().isStartingSlide == true)
                {
                    tempNoteSlides[int.Parse(pairs[0])] = 1;

                    firstPosSlides[int.Parse(pairs[0])] = note.transform.position.y;

                }




                //Check for an ending to the held note
                else if (tempNoteSlides[int.Parse(pairs[0])] == 1)
                {
                    note.GetComponent<NoteObject>().isEndingSlide = true;

                    //Create Hold bar
                    Debug.Log("Creating slide");
                    HoldBar = Instantiate(holdBar, parentHolder);

                    HoldBar.GetComponent<holdNoteBar>().create(int.Parse(pairs[0]), firstPosSlides[int.Parse(pairs[0])], note.transform.position.y + Yoffset);

                    //Reset Slides
                    tempNoteSlides[int.Parse(pairs[0])] = 0;
                }

                //Assign Arrows with Correct Key and rotation
                switch (z)
                {
                    case 0:
                        note.transform.Rotate(0, 0, 0);
                        note.GetComponent<NoteObject>().keyToPress = KeyCode.LeftArrow;
                        note.GetComponent<NoteObject>().noteIndex = 0;
                        break;

                    case 1:
                        note.transform.Rotate(0, 0, 90);
                        note.GetComponent<NoteObject>().keyToPress = KeyCode.DownArrow;
                        note.GetComponent<NoteObject>().noteIndex = 1;
                        break;

                    case 2:
                        note.transform.Rotate(0, 0, -90);
                        note.GetComponent<NoteObject>().keyToPress = KeyCode.UpArrow;
                        note.GetComponent<NoteObject>().noteIndex = 2;
                        break;

                    case 3:
                        note.transform.Rotate(0, 0, 180);
                        note.GetComponent<NoteObject>().keyToPress = KeyCode.RightArrow;
                        note.GetComponent<NoteObject>().noteIndex = 3;
                        break;
                }

            }
        }
    }
















    public void ResetSong()
    {
        //Dragon Boss===============


        //Delete noteholder motes
        parentHolder.GetComponent<BeatScroller>().Reset();


        Dataholder = GameObject.FindWithTag("DataHolder");
        textFile = GameObject.FindWithTag("SongTextHolder").GetComponent<SongTextDatabase>().Files[4];


            //After splitting it, we have to check if we have a ~ sliding note
            //If so we have to add that arrrownum to an array and keep checking that num for when we have it again

        myatk = false;
        
        string[] words = textFile.text.Split('-');
        

        for(int i = 0; i<= words.Length-1; i++){

            if(words[i].Trim() == "/"){
                //Create bar for switching sides
                switchbar = Instantiate(switchBar, parentHolder);
                switchbar.SetActive(true);
                switchbar.transform.position += new Vector3(0.0f, note.transform.position.y, 0.0f);

                if(myatk){
                    Atkbar = Instantiate(atkBar, parentHolder);
                    Atkbar.SetActive(true);
                    Atkbar.transform.position += new Vector3(0.0f, note.transform.position.y, 0.0f);
                    myatk = false;
                }
                else{
                    Defbar = Instantiate(defBar, parentHolder);
                    Defbar.SetActive(true);
                    Defbar.transform.position += new Vector3(0.0f, note.transform.position.y, 0.0f);
                    myatk = true;
                }
                

                //Create Conveyor panel blue or red
                switchconveyorpanel.GetComponent<ConveyorPanelHandler>().Create(note.transform.position.y, switchconveyorpanel, parentHolder);

                //Mark note as final
                note.GetComponent<NoteObject>().isFinalinSide = true;
            }
            else{

                note = Instantiate(Note, parentHolder);
                note.SetActive(true);
                pairs = words[i].Split(',');
                string t = pairs[0].Trim();

                if(t.Length >= 2){
                    pairs[0] = t.Remove(1, 1);
                    note.GetComponent<NoteObject>().isStartingSlide = true;
                }
                
                    float x = 0-float.Parse(pairs[0].Trim());
                    float y = float.Parse(pairs[1]);
                    note.transform.Translate(new Vector3(1.5f*x, 0-y, 0));
                    z = float.Parse(pairs[0]);
                

                if(note.GetComponent<NoteObject>().isStartingSlide == true){
                        tempNoteSlides[int.Parse(pairs[0])] = 1;

                        firstPosSlides[int.Parse(pairs[0])] = note.transform.position.y;

                }

                


                //Check for an ending to the held note
                else if(tempNoteSlides[int.Parse(pairs[0])] == 1){
                    note.GetComponent<NoteObject>().isEndingSlide = true;

                    //Create Hold bar
                    Debug.Log("Creating slide");
                    HoldBar = Instantiate(holdBar, parentHolder);

                    HoldBar.GetComponent<holdNoteBar>().create(int.Parse(pairs[0]), firstPosSlides[int.Parse(pairs[0])], note.transform.position.y);

                    //Reset Slides
                    tempNoteSlides[int.Parse(pairs[0])] = 0; 
                }

                    



                        switch(z){
                            case 0:
                                note.transform.Rotate(0, 0, 0);
                                note.GetComponent<NoteObject>().keyToPress = KeyCode.LeftArrow;
                            break;

                            case 1:
                                note.transform.Rotate(0, 0, 90);
                                note.GetComponent<NoteObject>().keyToPress = KeyCode.DownArrow;
                            break;

                            case 2:
                                note.transform.Rotate(0, 0, -90);
                                note.GetComponent<NoteObject>().keyToPress = KeyCode.UpArrow;
                            break;

                            case 3:
                                note.transform.Rotate(0, 0, 180);
                                note.GetComponent<NoteObject>().keyToPress = KeyCode.RightArrow;
                            break;
                        }

                }
        }
            
        }
    }

