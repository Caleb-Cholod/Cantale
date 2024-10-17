using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;
    private GameObject songTextHolder;
    private GameObject Dataholder;
    

    // Start is called before the first frame update
    void Start()
    {
        Dataholder = GameObject.FindWithTag("DataHolder");

        songTextHolder = GameObject.FindWithTag("SongTextHolder");
        float bpm = (float)songTextHolder.GetComponent<SongTextDatabase>().bpms[Dataholder.GetComponent<EnemyData>().enemyID];
        beatTempo = bpm / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            hasStarted = true;
        }
        else{
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
    public void Reset(){

        //Destroy all children
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        transform.position = new Vector3(2.392f, 7f, -0.086598f);
    }
}
