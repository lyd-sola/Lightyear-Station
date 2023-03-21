using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for environment obstacles that reverse player
public class RewardShield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.AddShield();
            GetComponent<Spawnable>().DeactivateSudden();
        }
    }
}
