using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSwitchBar : MonoBehaviour
{
    public bool isFinalBar = false;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinalBar)
        {
            if(transform.position.y <= 10f)
            {
                //Reset Song
                isFinalBar = false;
                gameManager.GetComponent<NoteSheetSpawn>().LoopSong(transform.position.y);

            }
        }
    }
}
