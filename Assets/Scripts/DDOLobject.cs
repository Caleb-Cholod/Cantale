using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLobject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // Ensure that there is only one instance of this object
        if (FindObjectsOfType<DDOLobject>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // Make this object persist across scenes
            DontDestroyOnLoad(gameObject);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
