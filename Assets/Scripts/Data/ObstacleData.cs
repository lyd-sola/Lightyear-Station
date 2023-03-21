using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObject/ÕÏ°­ÎïÊý¾Ý")]
public class ObstacleData : ScriptableObject
{
    [Header("Animation Range")]
    public float downPoint = -5f;
    public float upPoint = 2f;


    [Header("Animation Time Settings")]
    public float appearTime = 0.2f;
    public float disappearTime = 0.2f;
    public float deactivateDelay = 0.5f;


    [Header("Other")]
    public bool isDeactivateOnPass = true;


    [Header("Level Settings")]
    [Range(0f, 10f)] public float generateWeight = 5f;
}
