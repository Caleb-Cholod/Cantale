using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    //public int[] xpLevels = new int[4];

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ANIMATION STUFF
        animator.SetFloat("Timer", animator.GetFloat("Timer") + Time.deltaTime);
        if(animator.GetFloat("Timer") > 10)
        animator.SetFloat("Timer", 0.0f);
        //END OF ANIMATION STUFF


        
    }
}
