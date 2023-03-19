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
    public float upPoint = 2f;

    public float appearTime = 0.2f;
    public float disappearTime = 0.2f;
    public float deactivateDelay = 0.5f;

    Vector3 down;
    Vector3 up;
    float animProgress = 0;
    bool isAppear = false, isDisappear = false;
    bool toDeactivate = false;
    float deactivateCount;

    System.Action<Spawnable> deactivateAction;

    virtual public void Spawn(float angle)
    {
        this.angle = angle;

        var rotate = Quaternion.AngleAxis(angle, Vector3.forward);
        down = rotate * new Vector3(planetData.radius + downPoint, 0, 0) + planetData.planetCenter;
        up = rotate * new Vector3(planetData.radius + upPoint, 0, 0) + planetData.planetCenter;

        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.position = down;

        gameObject.SetActive(true);
        isAppear = true;
        isDisappear = false;
        animProgress = 0;
    }
    virtual public void Hide()
    {
        isDisappear = true;
        isAppear = false;
        animProgress = 0;
    }

    private void FixedUpdate()
    {
        // in appear progress
        if (isAppear)
        {
            animProgress += Time.fixedDeltaTime / appearTime;
            transform.position = down + (up - down) * animProgress;
            if (animProgress >= 1f)
                isAppear = false;
        }
        // in disappear progress
        else if(isDisappear)
        {
            animProgress += Time.fixedDeltaTime / disappearTime;
            transform.position = up + (down - up) * animProgress;
            if (animProgress >= 1f)
            {
                isDisappear = false;
                gameObject.SetActive(false);
            }
        }
        // deactivate
        if(toDeactivate)
        {
            deactivateCount -= Time.fixedDeltaTime;
            if (deactivateCount < 0)
            {
                // call pool to release this obj
                deactivateAction.Invoke(this);
                toDeactivate = false;
            }
                
        }
    }

    public void SetDeactivateAction(System.Action<Spawnable> action)
    {
        deactivateAction = action;
    }

    // player passes obstacle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if(!toDeactivate)
                deactivateCount = deactivateDelay;
            toDeactivate = true;
        }

    }
}
