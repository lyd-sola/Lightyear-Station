using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for environment obstacles that can kill player
public class ReverseObstacle : Spawnable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Reverse
        if (collision.TryGetComponent(out Player player))
        {
            player.rotateDir *= -1;
            player.transform.localScale = new Vector3(player.transform.localScale.x * -1, 1f, 1f);
        }
    }
}
