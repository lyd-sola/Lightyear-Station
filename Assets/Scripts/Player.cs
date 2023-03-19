using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // Components
    Collider2D coll;
    Rigidbody2D rb;
    PlayerInput input;

    // Settings
    public PlayerData playerData;
    public PlanetData planetData;

    // Player status
    private bool has_gravity = true;
    public bool onGround => rb.IsTouchingLayers(planetData.ground);
    public int jumpTimes;

    // Player positions
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
        coll    = GetComponent<Collider2D>();
        rb      = GetComponent<Rigidbody2D>();
        input   = GetComponent<PlayerInput>();

    }

    void Start()
    {
        input.EnableGameplayInput();    // call after InputActions instantiate
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
        // Compute facing and gravityUp dirctions each frame
        gravityUp   = (transform.position - planetData.planetCenter);  // planet -> player
        facing      = Quaternion.AngleAxis(-90, Vector3.forward) * gravityUp;
        dist        = gravityUp.magnitude;
        gravityUp   = gravityUp.normalized;
        angle       = Mathf.Atan2(gravityUp.y, gravityUp.x) * Mathf.Rad2Deg;

        // Rotate Player
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        //Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
        //transform.rotation = targetRotation;
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
    }

    // Movement under planet gravity
    private void Movement()
    {
        if (has_gravity)
        {
            Vector2 v_up = Vector2.Dot(rb.velocity, gravityUp) * gravityUp;

            Debug.DrawLine(transform.position, transform.position + (Vector3)v_up, Color.blue);
            Debug.DrawLine(transform.position, transform.position + (Vector3)LineSpeed, Color.red);

            rb.velocity = v_up + LineSpeed;

            rb.AddForce(gravityUp * - playerData.gravity);
        }

    }

    public void Jump()
    {
        rb.velocity = LineSpeed + (Vector2)gravityUp * playerData.jumpSpeed;
    }

    public void Roll()
    {
        Debug.Log("Roll!");
    }


    public void Kill()
    {
        Debug.Log("Kill!" + Time.time.ToString());
    }
}
