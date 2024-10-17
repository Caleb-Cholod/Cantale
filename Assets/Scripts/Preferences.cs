using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences : MonoBehaviour
{
    public GameObject currentnode;
    //public GameObject[,] currentMap = new GameObject[10, 6];
    public bool mapHasGenerated = false;
    public bool isInDragonBoss = false;

    public int EquipmentID = 1;

    public int[] xpLevels = new int[4];
    public int baseDamage = 1;
    public int additionalDMG = 0;
    public int baseArmor = 0;
    //Poison is going to add .x% chance on note hit to add one to total dmg, in future add items to incraese the poison dmg.
    public int baseAddPoison = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("My dmg" + additionalDMG);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
