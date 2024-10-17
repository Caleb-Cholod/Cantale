using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorPanelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private float position1;
    private float position2;

    void Start()
    {
        position1 = 0.0f;
    }

    // Update is called once per frame
    public void Create(float pos, GameObject obj, Transform parent)
    {
        if(position1 == 0.0f){
            position1 = pos;
        }
        else{
            position2 = pos;
            Debug.Log("Conveyor " + position1 + " - " + position2);
            float avg = (position1 + position2) / 2;
            GameObject switchConveyorPanel = Instantiate(obj, parent);
            switchConveyorPanel.SetActive(true);
            switchConveyorPanel.transform.position += new Vector3(0f, avg, 0.0f);
            
            switchConveyorPanel.transform.localScale = new Vector3(5.78f, (position2-position1), 1f);
            
            position1 = 0.0f;
        }
        
    }
}
