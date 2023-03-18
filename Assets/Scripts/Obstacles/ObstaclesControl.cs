using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesControl : MonoBehaviour
{
    Transform ch;
    // Start is called before the first frame update
    void Start()
    {
        ch = transform.Find("ตุดฬ");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            ch.gameObject.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            ch.gameObject.SetActive(false);
        }
    }
}
