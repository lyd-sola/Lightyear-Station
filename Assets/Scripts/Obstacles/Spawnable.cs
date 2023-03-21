using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for environment obstacles that can spawn
public class Spawnable : MonoBehaviour
{
    [SerializeField] PlanetData planetData;
    [SerializeField] public ObstacleData obstacleData;

    public float angle = 0;

    Vector3 down;
    Vector3 up;
    float animProgress = 0;
    bool isAppear = false, isDisappear = false;
    bool toDeactivate = false;
    float deactivateCount;

    System.Action<Spawnable> deactivateAction;  // Set when instanciated by pool

    public void Spawn(float angle)
    {
        this.angle = angle;

        var rotate = Quaternion.AngleAxis(angle, Vector3.forward);
        down = rotate * new Vector3(planetData.radius + obstacleData.downPoint, 0, 0) + planetData.planetCenter;
        up = rotate * new Vector3(planetData.radius + obstacleData.upPoint, 0, 0) + planetData.planetCenter;

        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.position = down;

        gameObject.SetActive(true);
        isAppear = true;
        isDisappear = false;
        animProgress = 0;
    }
    
    // start hiding animation
    public void Hide()
    {
        isDisappear = true;
        isAppear = false;
        animProgress = 0;
    }

    private void Update()
    {
        // in appear progress
        if (isAppear)
        {
            animProgress += Time.deltaTime / obstacleData.appearTime;
            transform.position = down + (up - down) * animProgress;
            if (animProgress >= 1f)
                isAppear = false;
        }
        // in disappear progress, deactivate after hide animation
        else if (isDisappear)
        {
            animProgress += Time.deltaTime / obstacleData.disappearTime;
            transform.position = up + (down - up) * animProgress;
            if (animProgress >= 1f)
            {
                gameObject.SetActive(false);
                // call pool to release this obj
                deactivateAction?.Invoke(this);
                isDisappear = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // deactivate
        if(toDeactivate)
        {
            deactivateCount -= Time.fixedDeltaTime;
            if (deactivateCount < 0)
            {
                Hide();
                toDeactivate = false;
            }
        }
    }

    public void SetDeactivateAction(System.Action<Spawnable> action)
    {
        deactivateAction += action;
    }

    // player passes obstacle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!obstacleData.isDeactivateOnPass)
            return;

        if (collision.TryGetComponent(out Player player))
        {
            // start deactivate timer
            Deactivate();
        }

    }

    // deactivate, start deactivate timer
    public void DeactivateSudden()
    {
        gameObject.SetActive(false);
        // call pool to release this obj
        deactivateAction?.Invoke(this);
    }

    // start deactivate timer
    public void Deactivate()
    {
        if (!toDeactivate)
            deactivateCount = obstacleData.deactivateDelay;
        toDeactivate = true;
    }
}
