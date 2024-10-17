using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObject : MonoBehaviour
{
    public GameObject node1;
    public GameObject node2;
    public GameObject node3;

    public Sprite[] icons = new Sprite[6];
    private GameObject icon;

    public int phase;
    public int nodeType;

    public int arrayX;
    public int arrayY;

    //NodeTypes
    //==============================
    //1 - Blank
    //2 - Enemy
    //3 - Chest
    //4 - Boss
    //5 - Shop
    //6 - Healing Well
    //7 - Gates
    //8 - Thorns
    //==============================



    public int depth;


    private SpriteRenderer spriteRenderer;
    private SpriteRenderer iconSR;
    public int enemyID;

    //colors
    public Color newColor1 = new Color(1f, 0.2f, 0f);
    public Color newColor2 = new Color(0.9f, 0.4f, 0f);
    public Color newColor3 = Color.yellow;
    public Color newColor4 = Color.green;
    public Color newColor5 = new Color(0.9f, 0.1f, 0.8f);
    public Color newColor6 = new Color(0.5f, 0.0f, 0.1f);
    public Color newColor7 = new Color(1f, 0.2f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        enemyID = -1;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D collider = GetComponent<Collider2D>();

        
        //dataHolder = GameObject.FindWithTag("DataHolder");
        //spriteRenderer.material.color = newColor1;
    }

    // Update is called once per frame
    public void Recolor()
    {
        icon = transform.GetChild(0).gameObject;
        iconSR = icon.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        iconSR.sprite = icons[nodeType - 1];

        switch (nodeType)
        {
            case 1:
                spriteRenderer.material.color = newColor4;
                break;
            case 2:
                spriteRenderer.material.color = newColor2;
                break;
            case 3:
                spriteRenderer.material.color = newColor3;
                break;
            case 4:
                spriteRenderer.material.color = newColor1;
                break;
            case 5:
                spriteRenderer.material.color = newColor5;
                break;
            case 6:
                spriteRenderer.material.color = newColor6;
                break;
            case 7:
                spriteRenderer.material.color = newColor7;
                break;
            default:
                break;
        }
    }
}
