using UnityEngine;
using UnityEngine.Pool;

public class SpawnablePool : MonoBehaviour
{
    protected Spawnable prefab;
    [SerializeField] int defaultSize = 20;
    [SerializeField] int maxSize = 100;

    ObjectPool<Spawnable> pool;

    public int ActiveCount => pool.CountActive;
    public int InactiveCount => pool.CountInactive;
    public int TotalCount => pool.CountAll;

    float spawnAngle;   // for parameter passing

    protected virtual Spawnable OnCreatePoolItem()
    {
        Spawnable n = Instantiate(prefab, transform);
        n.SetDeactivateAction(delegate { Release(n); });
        return n;
    }
    protected virtual void OnGetPoolItem(Spawnable obj) => obj.Spawn(spawnAngle);
    protected virtual void OnReleasePoolItem(Spawnable obj) => obj.Hide();
    protected virtual void OnDestroyPoolItem(Spawnable obj) => Destroy(obj.gameObject);

    // Interface for user
    public void Initialize(Spawnable prefab, bool collectionCheck = true)
    {
        pool = new ObjectPool<Spawnable>(OnCreatePoolItem, OnGetPoolItem, OnReleasePoolItem, OnDestroyPoolItem, collectionCheck, defaultSize, maxSize);
        this.prefab = prefab;
    }
    public Spawnable Get(float spawnAngle)
    {
        this.spawnAngle = spawnAngle;

        return pool.Get();
    }
    public void Release(Spawnable obj) => pool.Release(obj);
    public void Clear() => pool.Clear();
}