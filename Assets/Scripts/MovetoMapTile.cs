using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovetoMapTile : MonoBehaviour
{
    //public gameObject player;
    // Start is called before the first frame update
    public GameObject player;
    private GameObject dataHolder;
    void Start()
    {
        dataHolder = GameObject.FindWithTag("DataHolder");
        //player.transform.position = dataHolder.GetComponent<Preferences>().mapSpot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
