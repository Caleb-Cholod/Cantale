using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class cursorLight : MonoBehaviour
{
    private Light2D my2DLight;
    // Start is called before the first frame update
    void Start()
    {
        my2DLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        my2DLight.transform.position = new Vector3(mousePos.x, mousePos.y, my2DLight.transform.position.z);
    }
}
