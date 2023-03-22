using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtons : MonoBehaviour
{
    public static MainButtons instance;
    public int index;

    [SerializeField] float rotateTime;

    // rotate
    public bool isRotating = false;
    Quaternion rotateStart;
    Quaternion rotateEnd;
    float progress;

    // text
    [SerializeField] Text text;
    string[] texts = new string[] { "开始游戏", "背包升级", "退出游戏", "访问星图" };

    private void Awake()
    {
        instance = this;
        index = 0;
    }

    private void OnEnable()
    {
        text.text = texts[index];
    }

    void Update()
    {
        if(isRotating)
        {
            progress += Time.deltaTime / rotateTime;
            if(progress > 1f)
            {
                progress = 1f;
                isRotating = false;
                text.text = texts[index];
                MainMenPlayer.instance.stopRun();
            }
            transform.rotation = Quaternion.Slerp(rotateStart, rotateEnd, progress);
        }
    }

    public void RightRoll()
    {
        if(!isRotating)
        {
            progress = 0f;
            isRotating = true;
            index = (index + 1) % 4;
            text.text = "";

            MainMenPlayer.instance.runRight();

            rotateStart = transform.rotation;
            rotateEnd = rotateStart * Quaternion.Euler(new Vector3(0, 0, 90f));
        }
    }

    public void LeftRoll()
    {
        if (!isRotating)
        {
            progress = 0f;
            isRotating = true;
            index = (index + 3) % 4;
            text.text = "";

            MainMenPlayer.instance.runLeft();

            rotateStart = transform.rotation;
            rotateEnd = rotateStart * Quaternion.Euler(new Vector3(0, 0, -90f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainMenPlayer menu))
        {

        }    
    }
}
