using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for environment obstacles that reverse player
public class ReverseObstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.rotateDir *= -1;
            player.transform.localScale = new Vector3(player.transform.localScale.x * -1, 1f, 1f);
        }
    }
}
