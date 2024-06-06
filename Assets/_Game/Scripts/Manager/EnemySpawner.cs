using System;
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
        
    }

    private void Start()
    {
        LevelManager.Ins.OnLoadLevel += LevelManager_OnLoadLevel;
    }

    private void LevelManager_OnLoadLevel(object sender, System.EventArgs e)
    {
        OnInit();
    }

    private void OnInit()
    {
        amountHasSpawned = 0;
    }

    public void SpawnEnemy()
    {
        int tempIdx = amountHasSpawned%spawnPosList.Count;

        Enemy enemy = SimplePool.Spawn<Enemy>(enemyPrefab, spawnPosList[tempIdx].position, spawnPosList[tempIdx].rotation);
        enemy.OnInit();
        ColorSO colorSO = SOManager.Ins.GetColorSO(amountHasSpawned%6);
        enemy.ChangeBody(colorSO.material);
        enemy.ChangeColorTarget(colorSO.color);
        enemy.ChangeColorBar(colorSO.color);

        int l = Math.Max(Player.Instance.score - 5, 0);
        int r = Player.Instance.score + 5;

        int enemyScore = UnityEngine.Random.Range(l, r);
        enemy.targetArrow.SetScore(enemy.score);
        enemy.UpdateScore(enemyScore);
        //Debug.Log(enemyScore.ToString());
        //enemy.targetArrow.SetScore(enemy.score);
        amountHasSpawned++;
    }
}
