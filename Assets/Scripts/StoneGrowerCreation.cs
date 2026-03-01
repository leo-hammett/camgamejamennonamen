using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class StoneGrowerCreation : MonoBehaviour
{
    private float startTime;
    private float lastSpawnTime;
    private GameObject stoneGrower;
    [SerializeField] private float spawnInterval = 10f;
    private GameSettings gameSettings;
    private float minRadius = 10f;
    private float radialRange = 10f; // maxRadius - minRadius
    private GameObject player;
    private MenuUIController menu;

    void Awake()
    {
        stoneGrower = Resources.Load<GameObject>("StoneGrower");
        gameSettings = Resources.Load<GameSettings>("GameSettings");
        player = GameObject.Find("Player");
        menu = FindFirstObjectByType<MenuUIController>();
    }

    // Set start time on start
    void Start()
    {
        menu.StartGame += OnGameStart;
    }

    void OnGameStart()
    {
        startTime = menu.startTime;
        lastSpawnTime = startTime;
    }

    // Every (spawnInterval) seconds, spawn a StoneGrower
    void Update()
    {
        if (!menu.playing)
        {
            return;
        }

        if (Time.time >= lastSpawnTime + spawnInterval)
        {
            float spawnRadius = minRadius + Random.value * radialRange;
            float spawnAngle = Random.value * math.TAU;
            Vector3 relativeSpawn = new Vector3(spawnRadius * math.cos(spawnAngle), spawnRadius * math.sin(spawnAngle), 0);
            Instantiate(stoneGrower, relativeSpawn + player.transform.position, Quaternion.identity);
            lastSpawnTime = Time.time;
        }
    }
}
