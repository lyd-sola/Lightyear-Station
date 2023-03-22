using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenPlayer : MonoBehaviour
{
    public static MainMenPlayer instance;

    //[Header("Components")]
    Animator animator;
    Rigidbody2D rb;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stopRun();
    }


    void Update()
    {
        // left
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MainButtons.instance.LeftRoll();
        }
        // right
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MainButtons.instance.RightRoll();
        }
        // jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            animator.Play("Player_jump");
            rb.velocity = new Vector2(0, 30f);
        }
    }

    public void runRight()
    {
        transform.localScale = new Vector3(1, 1, 1);
        animator.Play("Player_run");
    }

    public void runLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        animator.Play("Player_run");
    }

    public void stopRun()
    {
        animator.Play("Player_idle");
    }
}
