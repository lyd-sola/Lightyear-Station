using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for environment obstacles that can kill player
public class Killable : Spawnable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Kill();
        }
    }
}
