using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/½ÇÉ«Êý¾Ý", order = 0)]
public class PlayerData : ScriptableObject
{
    // Settings
    public float speed;
    public float jumpSpeed;
    public float gravity;
    public int jumpTimes;
}
