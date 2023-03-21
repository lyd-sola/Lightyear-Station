using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for environment obstacles that can kill player
public class Killable : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.Damage();
        }
    }
}
