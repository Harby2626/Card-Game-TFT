using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public List<Spawnable> spawnables;
    [HideInInspector]
    public List<EnemyCharacter> aliveEnemyCharacters;

    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnables.Count; i++)
        {
            EnemyCharacter newEnemy = GameObject
                .Instantiate(spawnables[i].spawn, spawnables[i].location.position, Quaternion.identity)
                .GetComponent<EnemyCharacter>();
            aliveEnemyCharacters.Add(newEnemy);
        }
    }
}