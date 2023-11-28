using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomSpawn", menuName = "Strategy/Spawn/Random")]
public class RandomSpawnStrategy/*<T>*/ : SpawnStrategyBase/*<T>*/ /*where T : ScriptableObject*/
{
    //todo: redo strategies to be event based

    public override event System.Action OnSpawningComplete;

    public override event System.Action/*<T>*/ OnNextSpawn;

    public override void SpawnGroup(List<EnemySpawnSettings/*<T>*/> pSpawnables, MonoBehaviour pMono)
    {
        Spawnables = new List<EnemySpawnSettings/*<T>*/>(pSpawnables);

        Debug.Log("groupcount: " + Spawnables.Count);

        mono = pMono;
        mono.StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        Debug.Log("routine started");
        int index = Random.Range(0, Spawnables.Count);

        //OnNextSpawn?.Invoke(Spawnables[index].SpawnType);

        float delay = Spawnables[index].SpawnDelay;

        Spawnables[index].SpawnCount--;

        yield return new WaitForSeconds(delay);

        Debug.Log("index " + index);
        Debug.Log("list count " + Spawnables.Count);
        //Debug.Log("index name: " + Spawnables[index].SpawnType.name);

        if (Spawnables[index].SpawnCount <= 0)
        {
            Debug.Log("No enemies left of index");
            Spawnables.RemoveAt(index);
        }

        if (Spawnables.Count <= 0)
        {

            Debug.Log("No enemies left of any index");
            OnSpawningComplete?.Invoke();
            yield break;
        }

        mono.StartCoroutine(Spawner());
    }
}
