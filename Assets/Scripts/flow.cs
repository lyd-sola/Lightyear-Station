using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flow : MonoBehaviour
{
    [SerializeField] float speed = -0.05f;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        material.mainTextureOffset += new Vector2(speed, 0) * Time.fixedDeltaTime;
    }
}
