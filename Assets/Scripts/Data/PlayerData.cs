using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/½ÇÉ«Êý¾Ý", order = 0)]
public class PlayerData : ScriptableObject
{
    [Header("Speed Settings")]
    public float speed;
    public float jumpSpeed;
    public float fastFallSpeed;

    [Header("Gravity Settings")]
    public float gravity;
    public int jumpTimes;

    [Header("Collider Settings")]
    public Vector2 rollColliderSize;
    public Vector2 normalColliderSize;
    public Vector2 rollColliderOff;
    public Vector2 normalColliderOff;

    [Header("Color Settings")]
    public Color normalColor;
    public Color normalTrailerColor;
    public Color shieldColor;
    public Color shieldTrailerColor;

    [Header("Sound Effects")]
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip shieldSound;
}
