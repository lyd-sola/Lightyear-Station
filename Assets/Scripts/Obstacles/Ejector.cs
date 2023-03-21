using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// µØ…‰≤÷
public class Ejector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Debug.Log("Next Level!");
            GetComponent<Spawnable>().Deactivate();
            //player.AddShield();
            //GetComponent<Spawnable>().DeactivateSudden();
        }
    }
}
