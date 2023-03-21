using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // Only one player instace, for other scripts
    public static Player instance;

    // Components
    CapsuleCollider2D coll;
    Rigidbody2D rb;
    PlayerInput input;
    TrailRenderer trailRenderer;

    [Header("Settings")]
    public PlayerData playerData;
    public PlanetData planetData;


    [Header("Player status")]
    bool has_gravity = true;
    bool has_shield = false;
    public bool onGround => rb.IsTouchingLayers(planetData.ground);
    public int jumpTimes;


    [Header("Player positions")]
    public Vector3 gravityUp;
    public Vector3 facing;
    public float dist;
    public float angle;         // angle position
    public float rotateDir = 1;
    public float ySpeed => Vector2.Dot(rb.velocity, gravityUp);
    public Vector2 LineSpeed => playerData.speed * dist / 53f * facing* rotateDir;
    public float ys;


    private void OnEnable()
    {
        instance = this;
        coll    = GetComponent<CapsuleCollider2D>();
        rb      = GetComponent<Rigidbody2D>();
        input   = GetComponent<PlayerInput>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Start()
    {
        input.EnableGameplayInput();    // call after InputActions instantiate
        coll.size = playerData.normalColliderSize;
        coll.offset = playerData.normalColliderOff;
    }

    void Update()
    {
        Attract();
        ys = ySpeed;

        if (Input.GetKeyDown(KeyCode.A))
        {
            rotateDir = -1f;
            transform.localScale = new Vector3(rotateDir, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rotateDir = 1f;
            transform.localScale = new Vector3(rotateDir, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Attract()
    {
        if (has_gravity)
        {
            // Compute facing and gravityUp dirctions each frame
            gravityUp = (transform.position - planetData.planetCenter);  // planet -> player
            facing = Quaternion.AngleAxis(-90, Vector3.forward) * gravityUp;
            dist = gravityUp.magnitude;
            gravityUp = gravityUp.normalized;
            angle = Mathf.Atan2(gravityUp.y, gravityUp.x) * Mathf.Rad2Deg;

            // Rotate Player
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            //Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
            //transform.rotation = targetRotation;
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
        }

    }

    // Movement under planet gravity
    private void Movement()
    {
        if (has_gravity)
        {
            Vector2 v_up = Vector2.Dot(rb.velocity, gravityUp) * gravityUp;

            //Debug.DrawLine(transform.position, transform.position + (Vector3)v_up, Color.blue);
            //Debug.DrawLine(transform.position, transform.position + (Vector3)LineSpeed, Color.red);

            rb.velocity = v_up + LineSpeed;

            rb.AddForce(gravityUp * - playerData.gravity);
        }

    }

    public void Jump()
    {
        rb.velocity = LineSpeed + (Vector2)gravityUp * playerData.jumpSpeed;
    }
    
    public void FallFast()
    {
        rb.velocity += (Vector2)gravityUp * -playerData.fastFallSpeed;
    }

    public void Roll()
    {
        coll.size = playerData.rollColliderSize;
        coll.offset = playerData.rollColliderOff;
        transform.position = gravityUp * (planetData.radius + playerData.rollColliderSize.y / 2) + planetData.planetCenter;
    }

    public void StopRoll()
    {
        coll.size = playerData.normalColliderSize;
        coll.offset = playerData.normalColliderOff;
        transform.position = gravityUp * (planetData.radius + playerData.rollColliderSize.y / 2) + planetData.planetCenter;
    }

    public void Damage()
    {
        if (has_shield)
        {
            Debug.Log("Damage!" + Time.time.ToString());
            BreakShield();
        }
        else
        {
            Debug.Log("Killed!" + Time.time.ToString());
        }
    }

    public void AddShield()
    {
        Debug.Log("½±Àø»¤¶Ü£¡" + Time.time.ToString());
        has_shield = true;
    }

    public void BreakShield()
    {
        has_shield = false;
    }
}
