using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class Firefly : MonoBehaviour
{
    private float xVelocity = 0.0f;
    private float yVelocity = 0.0f;
    private float xPosition = 0.0f;
    private float yPosition = 0.0f;
    private float moveLen = 0f;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        xPosition = Random.Range(-8f, 8f);
        yPosition = Random.Range(8f, 4f);
        xVelocity = Random.Range(-0.002f, 0.002f);
        yVelocity = Random.Range(-0.002f, 0.002f);
        moveLen = Random.Range(0.5f, 3f);

        transform.position = new Vector3(xPosition, yPosition, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        if (time < moveLen){
            transform.position += new Vector3(xVelocity, yVelocity, 0.0f);
        }
        else{
            moveLen = Random.Range(0.5f, 3f);
            xVelocity = Random.Range(-0.002f, 0.002f);
            yVelocity = Random.Range(-0.002f, 0.002f);
            time = 0f;
        }
    }
}
