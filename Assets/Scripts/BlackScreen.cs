using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    public static BlackScreen instance;

    [SerializeField] AnimationCurve curve;
    [SerializeField, Range(0.5f, 2f)] float speed = 1f; //���ƽ��뽥�����ٶ�

    SpriteRenderer spriteRenderer;
    Color tmpColor;

    private void Awake()
    {
        instance = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        gameObject.SetActive(false);
    }

    public void StartBlackScreen()
    {
        GameUI.instance.HideUI();
        gameObject.SetActive(true);
        StartCoroutine(Black());
    }

    IEnumerator Black()
    {
        float timer = 0f;
        tmpColor = spriteRenderer.color;
        do
        {
            timer += Time.deltaTime;
            SetColorAlpha(curve.Evaluate(timer * speed));
            yield return null;

        } while (tmpColor.a > 0);
        gameObject.SetActive(false);
    }

    //ͨ������ͼƬ��͸����ʵ�ֽ��뽥��
    void SetColorAlpha(float a)
    {
        tmpColor.a = a;
        spriteRenderer.color = tmpColor;
    }
}
