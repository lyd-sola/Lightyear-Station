using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    private Collider2D coll;
    private Rigidbody2D rb;
    public Transform planet_center;

    // Settings
    public float speed;
    public float jumpSpeed;
    public float gravity;

    // Player status
    private bool has_gravity = true;
    Vector3 gravityUp;
    Vector3 facing;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

       
    }

    // Update is called once per frame
    void Update()
    {
        Attract();
        Movement();
    }

    private void Attract()
    {
        gravityUp = (transform.position - planet_center.position);  // planet -> player
        facing = Quaternion.AngleAxis(-90, Vector3.forward) * gravityUp;
        float dist = gravityUp.magnitude;
        gravityUp = gravityUp.normalized;

        //Debug.Log((transform.position - planet_center.position).normalized);
        //Debug.Log(right);

        if (has_gravity)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime); // 进行插值旋转
            transform.rotation = targetRotation;

            Vector2 v_up = Vector2.Dot(rb.velocity, gravityUp) * gravityUp;
            Vector2 v_line = speed * dist / 53f * facing;    // 线速度

            if (Input.GetKeyDown(KeyCode.Space))
                v_up = jumpSpeed * gravityUp;

                Debug.DrawLine(transform.position, transform.position + (Vector3)v_up, Color.blue);
            Debug.DrawLine(transform.position, transform.position + (Vector3)v_line, Color.red);

            rb.velocity = v_up + v_line;

            rb.AddForce(gravityUp * -gravity);
            
        }
        
    }
    private void Movement()
    {
        // jump
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    rb.AddForce(gravityUp * jumpSpeed);
        //}
        
    }
}
