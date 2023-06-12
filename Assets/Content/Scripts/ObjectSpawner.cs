using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectSpawner : MonoBehaviour, IPersistedData
{
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshRandom navMeshRandom;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private GameObject container;
    [SerializeField] private int count;
    [SerializeField] private Transform itemContainer;

    private List<Enemy> enemies = new List<Enemy>();

    public void Initalize()
    {
        GameData.Instance.persistedDatas.Add(this);
    }

    private Enemy SpawnRandom()
    {
        var enemy = Instantiate(enemyPrefab, container.transform);
        enemies.Add(enemy);
        navMeshRandom.Recalculate();
        enemy.objectSpawner = this;
        enemy.transform.position = NavMeshRandom.GetRandomPointOnNavMesh();
        enemy.SetTarget(player);
        enemy.ItemContainer = itemContainer;
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    private Enemy Spawn(Entity entity)
    {
        var enemy = Instantiate(enemyPrefab, container.transform);
        enemies.Add(enemy);
        enemy.objectSpawner = this;
        enemy.Initialize(entity);
        enemy.SetTarget(player);
        enemy.ItemContainer = itemContainer;
        enemy.gameObject.SetActive(true);
        enemy.RefreshHealtBar();
        return enemy;
    }

    public void SaveEnemyData(Entity entity)
    {
        var enemyEntites = GameData.Instance.enemyEntities;
        for (int i = 0; i < enemyEntites.Count; i++)
        {
            if (enemyEntites[i] == entity)
            {
                enemyEntites[i] = entity;
                return;
            }
        }
        GameData.Instance.enemyEntities.Add(entity);
    }

    public void RemoveEnemyData(Entity entity, Enemy enemy)
    {
        enemies.Remove(enemy);
        GameData.Instance.enemyEntities.Remove(entity);

        if (enemies.Count < 1)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnRandom();
            }
        }
    }

    void IPersistedData.Save()
    {
        foreach (var item in enemies)
        {
            item.SetSpawnerData();
        }
    }

    void IPersistedData.Load()
    {
        if (GameData.Instance.enemyEntities.Count < 1)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnRandom();
            }

            return;
        }

        foreach (var item in GameData.Instance.enemyEntities)
        {
            Spawn(item);
        }
    }
}
