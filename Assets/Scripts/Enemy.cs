using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHP;
    public int CurrentEnemyHP;
    public int goldDropped;
    public int xpDropped;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        CurrentEnemyHP = enemyHP;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    public void Death()
    {
        animator.Play("Die");
    }
}
