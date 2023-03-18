using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/Events/OneParmEventChannel", fileName = "OneParmEvent")]
public class OneParmEventChannel<T> : ScriptableObject
{
    event System.Action<T> deleg;

    public void Broadcast(T obj)
    {
        deleg?.Invoke(obj);
    }

    public void AddListener(System.Action<T> action)
    {
        deleg += action;
    }

    public void RemoveListener(System.Action<T> action)
    {
        deleg -= action;
    }
}
