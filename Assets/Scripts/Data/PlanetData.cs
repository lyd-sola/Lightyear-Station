using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "ScriptableObject/ÐÇÇòÊý¾Ý", order = 0)]
public class PlanetData : ScriptableObject
{
    public Vector3 planetCenter;
    public LayerMask ground;
    public float radius;
}
