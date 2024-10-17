using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetainObject : MonoBehaviour
{
    //public int enemyID;
    public AudioSource Music;
    //public string Tag;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(gameObject.tag);
        Debug.Log(objs.Length);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        // Mark this GameObject to not be destroyed on scene load
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
