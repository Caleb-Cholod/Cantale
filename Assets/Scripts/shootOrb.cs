using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootOrb : MonoBehaviour
{
    public bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-4.76f, 3f, 0f);
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isShooting){
            transform.position += new Vector3(0.3f, 0f, 0f);
        }
        if(transform.position.x > 7f){
            isShooting = false;
            transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            transform.position = new Vector3(-4.76f, 3f, 0f);
        }
    }
    //Grow orb func
    public void growOrb(){
        transform.localScale += new Vector3(0.1f, 0.1f, 0.0f);
    }
}
