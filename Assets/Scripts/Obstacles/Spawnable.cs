using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for environment obstacles that can spawn
public class Spawnable : MonoBehaviour
{
    [SerializeField] protected PlanetData planetData;

    public float angle = 0;

    // Spawn animation range
    public float downPoint = -10f;
    public float upPoint = 10f;
    public float appearTime = 1f;
    public float disappearTime = 1f;

    Vector3 down;
    Vector3 up;
    float animProgress = 0;
    bool isAppear = false, isDisappear = false;

    private void Start()
    {
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        var rotate = Quaternion.AngleAxis(angle, Vector3.forward);
        down = rotate * new Vector3(planetData.radius + downPoint, 0, 0) + planetData.planetCenter;
        up = rotate * new Vector3(planetData.radius + upPoint, 0, 0) + planetData.planetCenter;

       
    }

    virtual public void Spawn()
    {
        isAppear = true;
        animProgress = 0;

        Debug.DrawLine(down, up, Color.red);
    }

    private void FixedUpdate()
    {
        // in appear progress
        if (isAppear)
        {
            transform.position = Vector3.Slerp(down, up, animProgress);
            animProgress += Time.fixedDeltaTime / appearTime;
            if (animProgress > 1 - 1e-6)
                isAppear = false;
        }
        // in disappear progress
        else if(isDisappear)
        {
            transform.position = Vector3.Slerp(up, down, animProgress);
            animProgress += Time.fixedDeltaTime / disappearTime;
            if (animProgress > 1 - 1e-6)
                isAppear = false;
        }
    }

    virtual public void Hide()
    {
        isDisappear = true;
        animProgress = 0;
    }

    virtual protected void OnEnable()
    {
        Spawn();
    }

    virtual protected void OnDisable()
    {
        Hide();
    }
}
