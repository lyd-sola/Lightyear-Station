using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/¹Ø¿¨Êý¾Ý")]
public class LevelData : ScriptableObject
{
    [Header("Items")]
    public Spawnable[] obstacles;
    public Spawnable rewardShield;
    public Spawnable ejector;

    [Header("Obstacle Generate")]
    [Range(0f, 180f)] public float obstacleInterval = 20f;
    [Range(0f, 180f)] public float obstacleRandomRange = 10f;
    [Range(0f, 180f)] public float obstacleBeforePlayer = 35f;
    public AnimationCurve obstacleGenerateCurve;    // Indicates expected time of generate

    [Header("Reward Shield Generate")]
    public int maxRewardShieldGenerate = 1;
    public AnimationCurve rewardGenerateCurve;


    [Header("Level Settings")]
    public AudioClip BGM;
    public float genInterval = 0.5f;
    public float exitGenTime = 40f;
    //public float suddenShuffle;

    [Header("UI Settings")]
    public Color timerColor;
    public Sprite ring;
    public Material sphereMaterial;
}
