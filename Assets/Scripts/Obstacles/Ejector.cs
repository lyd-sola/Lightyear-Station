using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// µØ…‰≤÷
public class Ejector : MonoBehaviour
{
    [SerializeField] VoidEventChannel levelSuccessEvent;

    bool flag = false;

    private void OnEnable()
    {
        flag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag && collision.TryGetComponent(out Player player))
        {
            flag = true;
            Debug.Log("Next Level!");
            levelSuccessEvent.Broadcast();
            GetComponent<Spawnable>().Deactivate();
        }
    }
}
