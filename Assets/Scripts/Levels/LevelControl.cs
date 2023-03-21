using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public static LevelControl instance;

    [Header("关卡数据")]
    [SerializeField] LevelData[] levelData;
    [SerializeField] int level = 0;

    LevelData nowLevelData;

    List<SpawnablePool> obstaclePools = new List<SpawnablePool>();
    SpawnablePool rewardPool;

    float gameStartTime = 0f;
    public float GameTime => Time.time - gameStartTime;

    float totWeight = 0f;   // tot gen weight of obstacles

    bool exitGenerated = false;

    // Generate Time Interval
    [SerializeField] float genInterval = 0f;
    [SerializeField] float obstacleInterval = 0f;
    [SerializeField] float rewardInterval = 0f;


    // empty place
    [SerializeField] List<int> obstacleSlotRemain;
    int maxObstacleCount;
    float obstacleSlotSize;
    float IntPosToAngle(int pos) => (360 - maxObstacleCount * obstacleSlotSize) / 2f + pos * obstacleSlotSize;
    int AngleToIntSlot(float angle) => Mathf.FloorToInt((angle - (360 - maxObstacleCount * obstacleSlotSize) / 2f) / obstacleSlotSize);

    public void ResetSlot(Spawnable obj) => obstacleSlotRemain.Add(AngleToIntSlot(obj.angle));

    private void OnEnable()
    {
        instance = this;
    }

    void Start()
    {
        InitializeLevel(0);
    }

    public void InitializeLevel(int level)
    {
        this.level = level;
        nowLevelData = levelData[level];
        foreach (Spawnable prefab in nowLevelData.obstacles)
        {
            var poolHolder = new GameObject($"Pool: {prefab.name}");

            poolHolder.transform.parent = transform;
            poolHolder.SetActive(false);

            var pool = poolHolder.AddComponent<SpawnablePool>();

            pool.Initialize(prefab);
            obstaclePools.Add(pool);

            poolHolder.SetActive(true);

            totWeight += prefab.obstacleData.generateWeight;
        }
        // reward pool
        var ph = new GameObject($"Pool: {nowLevelData.rewardShield.name}");
        ph.transform.parent = transform;
        ph.SetActive(false);
        rewardPool = ph.AddComponent<SpawnablePool>();
        rewardPool.Initialize(nowLevelData.rewardShield);
        ph.SetActive(true);

        // reset obstacle array
        obstacleSlotSize = nowLevelData.obstacleInterval + nowLevelData.obstacleRandomRange;
        maxObstacleCount = Mathf.FloorToInt(360f / obstacleSlotSize);
        obstacleSlotRemain = new List<int>(maxObstacleCount);
        for (int i = 0; i < maxObstacleCount; ++i)
            obstacleSlotRemain.Add(i);

        // rest time
        gameStartTime = Time.time;
        exitGenerated = false;
        genInterval = 0f;
        obstacleInterval = 0f;
        rewardInterval = 0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            obstaclePools[Random.Range(0, obstaclePools.Count)].Get(Random.Range(0f, 360f));
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            rewardPool.Get(Random.Range(0f, 360f));
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            Spawnable n = Instantiate(nowLevelData.ejector, transform);
            n.SetDeactivateAction(delegate { Destroy(n.gameObject); });

            n.Spawn(0f);
        }
    }

    private void FixedUpdate()
    {
        // try generate each genInterval
        genInterval += Time.fixedDeltaTime;
        obstacleInterval += Time.fixedDeltaTime;
        rewardInterval += Time.fixedDeltaTime;
        if (genInterval >= nowLevelData.genInterval)
        {
            //GenerateEjector();
            //GenerateRewardShield();
            GenerateObstacle();
            genInterval = 0f;
        }
    }

    float RandomGenAngle()
    {
        // randomly choose which slot to gen
        int genSlot = 0;
        float genAngle = 0f;
        bool success = false;
        for (int i = 0; i < 5; ++i)  // try 5 times
        {
            genSlot = Random.Range(0, obstacleSlotRemain.Count - 1);
            genAngle = IntPosToAngle(obstacleSlotRemain[genSlot]);
            genAngle = Random.Range(genAngle, genAngle + nowLevelData.obstacleRandomRange);
            // Player distance restrict
            if ((genAngle - Player.instance.angle + 360f) % 360f > nowLevelData.obstacleBeforePlayer)
            {
                success = true;
                break;
            }
        }
        if (!success) return -1f;
        obstacleSlotRemain.RemoveAt(genSlot);

        return genAngle;
    }

    void GenerateObstacle()
    {
        float genProb = levelData[0].obstacleGenerateCurve.Evaluate(GameTime);
        genProb = obstacleInterval / (2 * genProb + 1e-5f);     // 概率从0-1递增，超过曲线值两倍必生成
        if (Random.Range(0f, 1f) <= genProb)
        {
            // slot full
            if (obstacleSlotRemain.Count == 0)
                return;

            // reset gen time interval
            obstacleInterval = 0;

            // randomly choose which slot to gen
            float genAngle = RandomGenAngle();
            if (genAngle < 0) return;

            // randomly choose which obstacle to gen
            float genWeight = Random.Range(0f, totWeight);
            var genPool = obstaclePools[0];
            for (int i = 0; i < nowLevelData.obstacles.Length; ++i)
            {
                float weight = nowLevelData.obstacles[i].obstacleData.generateWeight;
                if (genWeight <= weight)    // random weight inside this interval
                {
                    genPool = obstaclePools[i];
                    break;
                }
                genWeight -= weight;    // check next interval
            }

            // generate
            var gen = genPool.Get(genAngle);
        }
    }

    void GenerateRewardShield()
    {
        float genProb = levelData[0].rewardGenerateCurve.Evaluate(GameTime);
        genProb = rewardInterval / (2 * genProb + 1e-5f);     // 概率从0-1递增，超过曲线值两倍必生成
        if (Random.Range(0f, 1f) <= genProb)
        {
            // slot full
            if (obstacleSlotRemain.Count == 0)
                return;

            // reset gen time interval
            rewardInterval = 0;
        }
    }

    void GenerateEjector()
    {
        if (GameTime >= nowLevelData.exitGenTime && !exitGenerated)
        {
            // slot full
            if (obstacleSlotRemain.Count == 0)
                return;

            exitGenerated = true;

            Spawnable n = Instantiate(nowLevelData.ejector, transform);
            n.SetDeactivateAction(delegate { Destroy(n.gameObject); });

            n.Spawn(0f);
        }
    }
}
