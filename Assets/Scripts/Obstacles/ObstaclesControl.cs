using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesControl : MonoBehaviour
{
    [SerializeField] Spawnable[] prefabs;

    List<SpawnablePool> pools = new List<SpawnablePool>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Spawnable prefab in prefabs)
        {
            var poolHolder = new GameObject($"Pool: {prefab.name}");

            poolHolder.transform.parent = transform;
            poolHolder.transform.position = transform.position;
            poolHolder.SetActive(false);

            var pool = poolHolder.AddComponent<SpawnablePool>();

            pool.Initialize(prefab);
            pools.Add(pool);

            poolHolder.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            pools[Random.Range(0, pools.Count)].Get(Random.Range(0f, 360f));
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            
        }
    }
}
