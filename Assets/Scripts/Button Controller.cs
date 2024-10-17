using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public GameObject particleSys;
    private ParticleSystem pSystem;
    public KeyCode keyToPress;
    public int noteIndex;
    public GameObject gamemanager;

    private bool pressed;
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        ParticleSystem pSystem = particleSys.GetComponent<ParticleSystem>();
        pSystem.Pause();
        pressed = false;

        //buttons
        if (keyToPress == KeyCode.UpArrow)
        {
            noteIndex = 2;
        }
        else if (keyToPress == KeyCode.DownArrow)
        {
            noteIndex = 1;
        }
        else if (keyToPress == KeyCode.LeftArrow)
        {
            noteIndex = 0;
        }
        else
        {
            noteIndex = 3;
        }
        print(noteIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            pressed = true;
            theSR.sprite = pressedImage;

        }
        if(Input.GetKeyUp(keyToPress)){
            pressed = false;
            theSR.sprite = defaultImage;
        }
    }
}
