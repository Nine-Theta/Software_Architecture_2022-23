using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevEnemySpawner : MonoBehaviour
{
    public EnemyFactory Factory;
    public EnemyScriptable TestSpawn;

    public GameObject Target;

    public int EnemiesToSpawn = 10;

    public float SpawnCooldown;

    [SerializeField]
    private float timer = 1f;

    public bool AllowSpawn { get; set; }


    void Start()
    {
        
    }

    private void Update()
    {
        if (AllowSpawn && Time.time > timer)
        {
            SpawnEnemy();
            timer = Time.time + SpawnCooldown;
        }
    }

    public void SpawnEnemy()
    {
        EnemyObject annie = Factory.CreateInstance(TestSpawn, transform.position + new Vector3(0, 1, 0)).GetComponent<EnemyObject>();
        annie.TargetPos = Target.transform.position;
        annie.Move();

        AllowSpawn = (--EnemiesToSpawn > 0);
    }
}
