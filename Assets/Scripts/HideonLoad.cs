using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideonLoad : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(sceneIndex != 1){
            transform.position = new Vector3(999f, 999f, 0f);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
