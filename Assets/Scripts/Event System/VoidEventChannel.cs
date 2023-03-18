using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Events/VoidEventChannel", fileName = "VoidEvent")]
public class VoidEventChannel : ScriptableObject
{
    event System.Action deleg;

    public void Broadcast()
    {
        deleg?.Invoke();
    }

    public void AddListener(System.Action action)
    {
        deleg += action;
    }

    public void RemoveListener(System.Action action)
    {
        deleg -= action;
    }
}
