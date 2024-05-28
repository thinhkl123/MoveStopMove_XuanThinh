using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private List<Transform> spawnPosList;

    private int amountHasSpawned;

    private void Awake()
    {
        amountHasSpawned = 0;
    }

    public void SpawnEnemy()
    {
        int tempIdx = amountHasSpawned%spawnPosList.Count;

        Enemy enemy = SimplePool.Spawn<Enemy>(enemyPrefab, spawnPosList[tempIdx].position, spawnPosList[tempIdx].rotation);
        enemy.OnInit();
        ColorSO colorSO = SOManager.Ins.GetColorSO();
        enemy.ChangeBody(colorSO.material);
        enemy.ChangeColorTarget(colorSO.color);
        amountHasSpawned++;
    }
}
