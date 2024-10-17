using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random=UnityEngine.Random;

public class dmgNumberAddForce : MonoBehaviour
{
    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(-50.0f, 50.0f);
        float randomY = Random.Range(-50.0f, 50.0f);
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(randomX, randomY);
    }

    // Update is called once per frame
    public void Changetext(int number)
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = number.ToString();
    }
}
